using System;
using System.IO;

namespace CyclicTracker
{
    public class Tasker
    {
        private string currentTask;
        private DateTime currentTaskStart;
        private Configuration configuration;

        public string CurrentTask { get => currentTask; set => currentTask = value; }
        public DateTime CurrentTaskStart { get => currentTaskStart; set => currentTaskStart = value; }
        private Configuration Configuration { get => configuration; set => configuration = value; }

        public Tasker() : this(string.Empty, new Configuration()) {}

        public Tasker(string task, Configuration configuration)
        {
            CurrentTask = task;
            CurrentTaskStart = DateTime.Now;
            Configuration = configuration;

            WriteToFile(string.Format(configuration.StartDayStringMask, DateTime.Now.ToString("dd MMMM yyyy, dddd")));
        }

        public void Save()
        {
            string duration = string.Format(configuration.DurationStringMask, GetDuration());

           WriteToFile(string.Format(
                    configuration.TaskStringMask,
                    CurrentTaskStart.ToString("HH:mm"),
                    DateTime.Now.ToString("HH:mm"),
                    CurrentTask,
                    duration)
            );
            
            Clear();
        }

        public void Clear()
        {
            CurrentTask = string.Empty;
            CurrentTaskStart = DateTime.MinValue;
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
            TimeSpan duration = endDate.Subtract(CurrentTaskStart);
            return duration;
        }
    }
}
