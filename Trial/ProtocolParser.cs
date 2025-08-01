using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Trial.MainWindow;
using System.IO;

namespace Trial
{
    public static class ProtocolParser
    {
        private static Dictionary<string, ProtocolItem> protocolMap;

        static ProtocolParser()
        {
            // 프로그램 시작 시 JSON 로딩
            //LoadProtocolDefinitions("Protocols/inkjet_protocol.json");
        }

        public static void LoadProtocolDefinitions(string filePath)
        {
            string json = File.ReadAllText(filePath);
            protocolMap = JsonSerializer.Deserialize<Dictionary<string, ProtocolItem>>(json);
        }

        public static string Process(string message)
        {
            // 현재 실행 디렉터리 (보통 bin\Debug\net8.0-windows\ 등)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // 상위의 상위의 상위 디렉터리로 이동
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));

            // 대상 파일 경로 조합
            string filePath = Path.Combine(projectRoot, "Protocols", "inkjet_protocol.json");

            LoadProtocolDefinitions(filePath);

            if (!message.StartsWith(((char)ProtocolConstants.ESC).ToString()))
                return Error("Missing ESC");

            message = message.Trim((char)ProtocolConstants.ESC, (char)ProtocolConstants.CR, (char)ProtocolConstants.LF);

            string cmd = message.Substring(0, 3); // e.g., "TXC"
            string paramPart = message.Substring(3); // e.g., "100,50,13,0,weee"
            string[] parameters = paramPart.Split(',');

            if (!protocolMap.ContainsKey(cmd))
                return Error("Unknown protocol");

            ProtocolItem def = protocolMap[cmd];
            if (def.parameters.Count != parameters.Length)
                return Error("Parameter count mismatch");

            int idx = 0;
            foreach (var kv in def.parameters)
            {
                string expectedType = kv.Value;
                string actualValue = parameters[idx];

                if (!ValidateParameter(actualValue, expectedType))
                    return Error($"Invalid parameter at {kv.Key}: {actualValue}");

                idx++;
            }

            return BuildOK(cmd);
        }

        private static bool ValidateParameter(string value, string type)
        {
            return type switch
            {
                "int" => int.TryParse(value, out _),
                "string" => !string.IsNullOrEmpty(value),
                _ => false
            };
        }

        private static string BuildOK(string cmd, string param = "1")
        {
            return $"{(char)ProtocolConstants.ESC}{cmd} OK : {param}{(char)ProtocolConstants.CR}{(char)ProtocolConstants.LF}";
        }

        private static string Error(string reason)
        {
            return $"{(char)ProtocolConstants.ESC}ERR : {reason}{(char)ProtocolConstants.CR}{(char)ProtocolConstants.LF}";
        }
    }


}
