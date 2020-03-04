using System;
using System.IO;

namespace CyclicTracker
{
    public class Tasker
    {
        private string currentTask;
        private DateTime currentTaskStart;
        private DateTime currentTaskEnd;
        private Configuration configuration;
        private DataBaseConnector dataBase;

        public string Task { get => currentTask; set => currentTask = value; }
        public DateTime Start { get => currentTaskStart; set => currentTaskStart = value; }
        public DateTime End { get => currentTaskEnd; set => currentTaskEnd = value; }
        private Configuration Configuration { get => configuration; set => configuration = value; }
        private DataBaseConnector DataBase { get => dataBase; set => dataBase = value; }

        public Tasker() : this(string.Empty, new Configuration()) {}

        public Tasker(string task, Configuration configuration)
        {
            DataBase = new DataBaseConnector();

            Task = task;
            Start = DateTime.Now;
            Configuration = configuration;

            WriteToFile(string.Format(configuration.StartDayStringMask, DateTime.Now.ToString("dd MMMM yyyy, dddd")));
        }

        public Tasker(string task, string start, string end) 
        {
            Task = task;
            Start = DateTime.Parse(start);
            End = DateTime.Parse(end);
        }

        public void Save()
        {
            string duration = string.Format(configuration.DurationStringMask, GetDuration());

            WriteToFile(string.Format(
                    configuration.TaskStringMask,
                    Start.ToString("HH:mm"),
                    DateTime.Now.ToString("HH:mm"),
                    Task,
                    duration)
            );

            DataBase.InsertTask(this);
            Clear();
        }

        public void Clear()
        {
            Task = string.Empty;
            Start = DateTime.MinValue;
        }

        private void WriteToFile(string stringToWrite)
        {
            using (StreamWriter writetext = new StreamWriter(Configuration.OutputFilePath, true, System.Text.Encoding.UTF8))
            {
                writetext.WriteLine(stringToWrite);
            }
        }

        private TimeSpan GetDuration()
        {
            DateTime endDate = DateTime.Now;
            TimeSpan duration = endDate.Subtract(Start);
            return duration;
        }
    }
}
