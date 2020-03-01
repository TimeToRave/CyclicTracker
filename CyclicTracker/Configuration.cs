using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CyclicTracker
{
    public class Configuration
    {
        private string configFilePath;
        private string outputFilePath;
        private int timePeriod;
        private string taskStringMask;
        private string startDayStringMask;
        private string durationStringMask;

        public string OutputFilePath { get => outputFilePath; set => outputFilePath = value; }
        public int TimePeriod { get => timePeriod; set => timePeriod = value; }
        public string ConfigFilePath { get => configFilePath; set => configFilePath = value; }
        public string TaskStringMask { get => taskStringMask; set => taskStringMask = value; }
        public string StartDayStringMask { get => startDayStringMask; set => startDayStringMask = value; }
        public string DurationStringMask { get => durationStringMask; set => durationStringMask = value; }
       

        public Configuration() : this(@"CyclicTracker.config") {}

        public Configuration(string configFilePath) {

            Dictionary<string, string> configurationFromFile = ReadFromFile(configFilePath);

            OutputFilePath = configurationFromFile["path"];
            TimePeriod = int.Parse(configurationFromFile["period"]);
            TaskStringMask = configurationFromFile["taskMask"];
            StartDayStringMask = configurationFromFile["startDayMask"];
            DurationStringMask = configurationFromFile["durationMask"];
        }

        private Dictionary<string, string> ReadFromFile(string fileName)
        {
            Dictionary<string, string> configurationFromFile = new Dictionary<string, string>();
            if (!File.Exists(fileName))
            {
                CreateDefaultConfigurationFile(fileName);   
            }

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

        private void CreateDefaultConfigurationFile(string fileName)
        {
            Dictionary<string, string> defaultConfiguration = new Dictionary<string, string>();
            defaultConfiguration.Add("path", @"Tasks.txt");
            defaultConfiguration.Add("period", "15");
            defaultConfiguration.Add("taskMask", "[{0} - {1}] {2} - {3}");
            defaultConfiguration.Add("startDayMask", "[{0}]");
            defaultConfiguration.Add("durationMask", "{0:hh\\:mm\\:ss}");

            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                foreach (var parameter in defaultConfiguration)
                    outputFile.WriteLine(string.Format("{0} = {1}", parameter.Key, parameter.Value));    
            }
        }


    }
}