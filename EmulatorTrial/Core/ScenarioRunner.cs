
// File: ScenarioRunner.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmulatorTrial.Core
{
    internal class ScenarioRunner
    {
        private readonly ProtocolEngine _protocolEngine;

        public ScenarioRunner(ProtocolEngine engine)
        {
            _protocolEngine = engine;
        }

        public async Task<List<(string, string)>> RunScenarioAsync(List<string> commands, int delayMs = 500)
        {
            var results = new List<(string, string)>();

            foreach (var cmd in commands)
            {
                string response = _protocolEngine.GetResponse(cmd);
                results.Add((cmd, response));
                await Task.Delay(delayMs);
            }

            return results;
        }
    }
}
