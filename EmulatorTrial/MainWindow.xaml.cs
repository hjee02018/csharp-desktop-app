using EmulatorTrial.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace EmulatorTrial
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLoadScenario_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine("Tests", "s1.json");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("시나리오 파일을 찾을 수 없습니다: " + filePath);
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var scenario = JsonSerializer.Deserialize<Scenario>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                lbScenarioSteps.Items.Clear();

                txtLog.AppendText($"[INFO] 시나리오 로드됨: {scenario.ScenarioName}\n");
                txtLog.AppendText($"[DESC] {scenario.Description}\n");

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

        private void BtnRunScenario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnShowProtocol_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
