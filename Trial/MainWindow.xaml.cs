//using EmulatorTrial.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Trial
{

    public partial class MainWindow : Window
    {
        private EmulatorServer mServer;

        public MainWindow()
        {
            InitializeComponent();
            StartInkjetServer(); // 잉크젯 애뮬레이터 실행 (PORT :9100)
        }

        private void StartInkjetServer()
        {
            mServer = new EmulatorServer(9100, LogMessage);
            mServer.Start();
        }

        private void LogMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                txtLog.AppendText($"{DateTime.Now:HH:mm:ss} {message}\n");
                txtLog.ScrollToEnd();
            });
        }
        private void BtnLoadScenario_Click(object sender, RoutedEventArgs e)
        {
            // 현재 실행 디렉터리 (보통 bin\Debug\net8.0-windows\ 등)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // 상위의 상위의 상위 디렉터리로 이동
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));

            // 대상 파일 경로 조합
            string filePath = Path.Combine(projectRoot, "Tests", "s1.json");

            // 파일 존재 여부 확인
            if (!File.Exists(filePath))
            {
                MessageBox.Show("시나리오 파일을 찾을 수 없습니다: " + filePath);
                return;
            }

            //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Tests", "s1.json");
            
            try
            {
                string json = File.ReadAllText(filePath);
                var scenario = JsonSerializer.Deserialize<Scenario>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                lbScenarioSteps.Items.Clear();
                txtLog.AppendText($"{DateTime.Now:HH:mm:ss} [INFO] 시나리오 로드됨: {scenario.ScenarioName}\n");
                txtLog.AppendText($"{DateTime.Now:HH:mm:ss} [DESC] {scenario.Description}\n");

                foreach (var step in scenario.Steps)
                {
                    string paramText = string.Join(", ",
                        step.Parameters?.Select(p => $"{p.Key}={GetValueString(p.Value)}") ?? Enumerable.Empty<string>());

                    lbScenarioSteps.Items.Add(
                        $"[{step.StepId}] {step.Command}({paramText}) → {step.ExpectedResponse}" +
                        (step.DelayAfterMs.HasValue ? $" [delay: {step.DelayAfterMs}ms]" : "")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시나리오 로딩 실패: " + ex.Message);
            }
        }

        // JsonElement 값을 문자열로 변환하는 유틸
        private string GetValueString(JsonElement value)
        {
            return value.ValueKind switch
            {
                JsonValueKind.String => value.GetString(),
                JsonValueKind.Number => value.ToString(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                _ => value.ToString()
            };
        }


        // 시나리오용 클래스 정의
        public class Scenario
        {
            [JsonPropertyName("scenario_name")]
            public string ScenarioName { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("steps")]
            public List<ScenarioStep> Steps { get; set; }
        }

        public class ScenarioStep
        {
            [JsonPropertyName("step_id")]
            public int StepId { get; set; }

            [JsonPropertyName("command")]
            public string Command { get; set; }         // Protocol Name

            [JsonPropertyName("parameters")]
            public Dictionary<string, JsonElement> Parameters { get; set; }

            [JsonPropertyName("expected_response")]
            public string ExpectedResponse { get; set; }

            [JsonPropertyName("delay_after_ms")]
            public int? DelayAfterMs { get; set; }
        }
        //public class ProtocolItem
        //{
        //    public string Command { get; set; }
        //    public string Description { get; set; }
        //    public List<ProtocolParameter> Parameters { get; set; }
        //}

        public class ProtocolItem
        {
            public string Command { get; set; }
            public string description { get; set; }
            public Dictionary<string, string> parameters { get; set; }
        }

        public class ProtocolParameter
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }

        private void BtnExportCsv_Click(object sender, RoutedEventArgs e)
        {
            string path = "log_output.csv";
            var lines = txtLog.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            File.WriteAllLines(path, lines.Select(line => $"\"{line.Replace("\"", "\"\"")}\""));
            MessageBox.Show($"CSV 로그 저장 완료\n→ {Path.GetFullPath(path)}");
        }

        private void BtnExportTxt_Click(object sender, RoutedEventArgs e)
        {
            string path = "log_output.txt";
            File.WriteAllText(path, txtLog.Text);
            MessageBox.Show($"TXT 로그 저장 완료\n→ {Path.GetFullPath(path)}");
        }

        private void BtnRunScenario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnShowProtocol_Click(object sender, RoutedEventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));
            string filePath = Path.Combine(projectRoot, "Protocols", "inkjet_protocol.json");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("프로토콜 파일이 존재하지 않습니다.");
                return;
            }
            // 파일에서 로드하거나 문자열로 불러온 경우:
            string jsonText = File.ReadAllText(filePath);

            // Dictionary 형태로 역직렬화
            Dictionary<string, ProtocolItem> protocolMap = JsonSerializer.Deserialize<Dictionary<string, ProtocolItem>>(jsonText);

            List<string> displayList = protocolMap.Select(p =>
            {
                string paramList = string.Join(", ", p.Value.parameters.Select(kv => $"{kv.Key}:{kv.Value}"));
                return $"{p.Key} - {p.Value.description} [{paramList}]";
            }).ToList();

            lbProtocolList.ItemsSource = displayList;
            lbProtocolList.Visibility = Visibility.Visible;
        }

        private void UpdateCommStatus(bool isConnected, string ip = "-", int port = 0)
        {
            txtCommStatus.Text = isConnected ? "통신 상태: 연결됨" : "통신 상태: 연결 안 됨";
            txtCommStatus.Foreground = isConnected ? Brushes.LightGreen : Brushes.Red;
            txtCommInfo.Text = $"IP: {ip} / PORT: {port}";
        }


    }
}
