using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CyclicTracker
{
    class Configuration
    {
        private string configFilePath;
        private string outputFilePath;
        private int timePeriod;

        public string OutputFilePath { get => outputFilePath; set => outputFilePath = value; }
        public int TimePeriod { get => timePeriod; set => timePeriod = value; }
        public string ConfigFilePath { get => configFilePath; set => configFilePath = value; }

        public Configuration() : this(@"CyclicTracker.config") {}

        public Configuration(string configFilePath) {

            Dictionary<string, string> configurationFromFile = ReadFromFile(configFilePath);
            OutputFilePath = configurationFromFile["path"];
            TimePeriod = int.Parse(configurationFromFile["period"]);

            var t = 0;
        }

        private Dictionary<string, string> ReadFromFile(string fileName)
        {
            Dictionary<string, string> configurationFromFile = new Dictionary<string, string>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyvalue = line.Replace(" ", string.Empty).Split('=');
                    if (keyvalue.Length == 2)
                    {
                        configurationFromFile.Add(keyvalue[0], keyvalue[1]);
                    }
                }
            }

            return configurationFromFile;
        }
    }
}