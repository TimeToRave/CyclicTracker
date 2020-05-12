using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyclicTracker
{
    public partial class MainWindow : Window
    {
        private Tasker currentTasker;
        private Configuration configuration;

        public static MainWindow main;


        public void OnTop ()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            TaskTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            this.Activate();
            this.TaskTextBox.Focus();
        }

        public MainWindow()
        {
            InitializeComponent();
           
            configuration = new Configuration();

            currentTasker = new Tasker(string.Empty, configuration);

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

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Normal;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.MainGrid.RowDefinitions[0].MaxHeight = 20;
            this.MainGrid.RowDefinitions[1].MaxHeight = 5;
            this.MainGrid.RowDefinitions[2].MaxHeight = 0;

            this.MaxHeight = 25;
            
            this.MainGrid.Margin = new Thickness(5, 0, 0, 0);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;

            if (currentTasker.Task == string.Empty)
            {
                OnTop();
            }

            Background = new SolidColorBrush(Color.FromArgb(30, 0, 0, 0));
            TaskTextBox.Background = new SolidColorBrush(Color.FromArgb(30, 0, 0, 0));
        }

    }
}
