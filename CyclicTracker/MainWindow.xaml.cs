using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace CyclicTracker
{
    public partial class MainWindow : Window
    {
        private Tasker currentTasker;
        private Configuration configuration;

        public static MainWindow main;


        public void OnTop ()
        {
            this.Show();
            this.Activate();
        }

        public MainWindow()
        {
            InitializeComponent();
            

            configuration = new Configuration();

            currentTasker = new Tasker(string.Empty, configuration.OutputFilePath);


            int minutes = configuration.TimePeriod;
            main = this;

            async Task RunPeriodicSave()
            {
                while (true)
                {
                    await Task.Delay(minutes * 60 * 1000);
                    OnTop();
                }
            }

            RunPeriodicSave();
        }

        private static void Count(Object obj)
        {
            main.OnTop();
        }


        private void NextTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if(TaskTextBox.Text == currentTasker.CurrentTask)
            {
                return;
            }

            if (currentTasker.CurrentTask != string.Empty)
            {
                currentTasker.Save();
            }

            string newTask = TaskTextBox.Text;
            currentTasker = new Tasker(newTask, configuration.OutputFilePath);
            this.Hide();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (!currentTasker.CurrentTask.Equals(string.Empty))
            {
                this.Hide();
            }
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTasker.CurrentTask != string.Empty)
            {
                currentTasker.Save();
                TaskTextBox.Text = string.Empty;
            }
        }

        private void OpenTasksFile_Click(object sender, RoutedEventArgs e)
        {
            string file = configuration.OutputFilePath;
            ProcessStartInfo pi = new ProcessStartInfo(file);
            pi.Arguments = Path.GetFileName(file);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = Path.GetDirectoryName(file);
            pi.FileName = file;
            pi.Verb = "OPEN";
            Process.Start(pi);
        }
    }
}
