using System;
using System.Windows;

namespace ProgrammerPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Top = Top + 30;
                window.Left = Left + 30;
            }
        }
    }
}