using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorTrial
{
    public static class ProtocolParser
    {
        public static string Process(string message)
        {
            if (!message.StartsWith(((char)ProtocolConstants.ESC).ToString()))
                return Error("Missing ESC");

            message = message.Trim((char)ProtocolConstants.ESC, (char)ProtocolConstants.CR, (char)ProtocolConstants.LF);
            string[] parts = message.Split(' ');

            string cmd = parts[0];
            switch (cmd)
            {
                case "MGN":
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int id) && id >= 0 && id <= 9)
                        return BuildOK("MGN", parts[1]);
                    return Error("Invalid ID");

                case "MGS":
                    return BuildOK("MGS");

                default:
                    return Error("Unknown protocol");
            }
        }

        private static string BuildOK(string cmd, string param = "1")
        {
            return $"{(char)ProtocolConstants.ESC}{cmd} OK : {param}{(char)ProtocolConstants.CR}{(char)ProtocolConstants.LF}";
        }

        private static string Error(string msg)
        {
            return $"{(char)ProtocolConstants.ESC}AAA ER : {msg}{(char)ProtocolConstants.CR}{(char)ProtocolConstants.LF}";
        }
    }

}
