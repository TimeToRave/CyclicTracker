﻿using System;
using System.IO;

namespace CyclicTracker
{
    public class Tasker
    {
        private string currentTask;
        private DateTime currentTaskStart;

        public string CurrentTask { get => currentTask; set => currentTask = value; }
        public DateTime CurrentTaskStart { get => currentTaskStart; set => currentTaskStart = value; }

        public Tasker()
        {
            CurrentTask = string.Empty;
        }

        public Tasker(string task)
        {
            CurrentTask = task;
            CurrentTaskStart = DateTime.Now;
        }

        public void Save()
        {
            using (StreamWriter writetext = new StreamWriter(@"D:\tasks.txt", true, System.Text.Encoding.UTF8))
            {
                writetext.WriteLine(string.Format(
                    "[{0} - {1}] {2}", 
                    CurrentTaskStart.ToString("HH:mm"),
                    DateTime.Now.ToString("HH:mm"),
                    CurrentTask
                ));
            }
            Clear();
        }

        public void Clear()
        {
            CurrentTask = string.Empty;
            CurrentTaskStart = DateTime.MinValue;
        }
    }
}
