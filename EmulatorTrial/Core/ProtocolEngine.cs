// File: ProtocolEngine.cs
using System;
using System.Text.Json;
using System.Collections.Generic;

namespace EmulatorTrial.Core
{
    internal class ProtocolEngine
    {
        private JsonDocument _protocolDoc;
        private Dictionary<string, JsonElement> _commandMap = new();

        public void LoadProtocol(string json)
        {
            _protocolDoc = JsonDocument.Parse(json);
            _commandMap.Clear();

            if (_protocolDoc.RootElement.TryGetProperty("commands", out var commands))
            {
                foreach (var cmd in commands.EnumerateArray())
                {
                    if (cmd.TryGetProperty("name", out var name))
                    {
                        _commandMap[name.GetString()] = cmd;
                    }
                }
            }
        }

        public string GetResponse(string command)
        {
            foreach (var entry in _commandMap)
            {
                if (command.Contains(entry.Key))
                {
                    if (entry.Value.TryGetProperty("response_success", out var success))
                        return success.GetString();
                }
            }
            return "[ESC]ER:Unknown command[CR][LF]";
        }

        public JsonDocument GetProtocolDocument()
        {
            return _protocolDoc;
        }
    }
}