using System;
using System.Threading.Tasks;
using System.Windows;

namespace CyclicTracker
{
    public partial class MainWindow : Window
    {
        private Tasker currentTasker;
        public static MainWindow main;


        public void OnTop ()
        {
            this.Show();
            this.Activate();
        }

        public MainWindow()
        {
            InitializeComponent();
            currentTasker = new Tasker();

            main = this;

            async Task RunPeriodicSave()
            {
                while (true)
                {
                    await Task.Delay(900000);
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
            currentTasker = new Tasker(newTask);
            this.Hide();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTasker.CurrentTask != string.Empty)
            {
                currentTasker.Save();
            }
        }
    }
}
