using System;
using System.IO;

namespace CyclicTracker
{
    public class Tasker
    {
        private string currentTask;
        private DateTime currentTaskStart;

        public string CurrentTask { get => currentTask; set => currentTask = value; }
        public DateTime CurrentTaskStart { get => currentTaskStart; set => currentTaskStart = value; }

        public Tasker() : this(string.Empty) {}

        public Tasker(string task)
        {
            CurrentTask = task;
            CurrentTaskStart = DateTime.Now;
            WriteToFile("[" + DateTime.Now.ToString("dd MMMM yyyy, dddd") + "]");
        }

        public void Save()
        {
           WriteToFile(string.Format(
                    "[{0} - {1}] {2}", 
                    CurrentTaskStart.ToString("HH:mm"),
                    DateTime.Now.ToString("HH:mm"),
                    CurrentTask)
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
    }
}
