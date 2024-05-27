using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;

namespace TitleShower
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



            RefleshButton.IsEnabled = true;
            if (lyricWindow == null)
            {
                lyricWindow = new LyricWindow();
                lyricWindow.Closed += (s, args) => lyricWindow = null;
            }
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.FirstOrDefault(s => !s.Primary);
            if (secondaryScreen != null)
            {
                lyricWindow.Left = secondaryScreen.Bounds.Left;
                lyricWindow.Top = secondaryScreen.Bounds.Top;
                lyricWindow.Width = secondaryScreen.Bounds.Width;
                lyricWindow.Height = secondaryScreen.Bounds.Height;
                lyricWindow.WindowState = WindowState.Normal;
            }
            lyricWindow.Show();



            LoadStart();
       }

        private LyricWindow lyricWindow;

        private async void LoadStart()
        {
           
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lyricWindow.Close();
        }

        private void RefleshButton_Click(object sender, RoutedEventArgs e)
        {
            string title, organization;
            title = TitleBox.Text;
            organization = OranizationBox.Text;
            lyricWindow.SetLyric(title, "Title");
            lyricWindow.SetLyric(organization, "Grade");
        }
    }
}
