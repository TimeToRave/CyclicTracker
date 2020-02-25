using System;
using System.IO;

namespace CyclicTracker
{
    public class Tasker
    {
        private string currentTask;
        private DateTime currentTaskStart;
        private string outputFilepath;

        public string CurrentTask { get => currentTask; set => currentTask = value; }
        public DateTime CurrentTaskStart { get => currentTaskStart; set => currentTaskStart = value; }
        public string OutputFilepath { get => outputFilepath; set => outputFilepath = value; }

        public Tasker() : this(string.Empty, @"D:\tasks.txt") {}

        public Tasker(string task, string path)
        {
            CurrentTask = task;
            CurrentTaskStart = DateTime.Now;
            WriteToFile("\n[" + DateTime.Now.ToString("dd MMMM yyyy, dddd") + "]");
        }

        public void Save()
        {
            string duration = string.Format("{0:hh\\:mm\\:ss}", GetDuration());

           WriteToFile(string.Format(
                    "[{0} - {1}] {2} - {3}", 
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
            using (StreamWriter writetext = new StreamWriter(@"D:\tasks.txt", true, System.Text.Encoding.UTF8))
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
