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



            RightButton.IsEnabled = true;
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

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            GetTitles();
        }

        private LyricWindow lyricWindow;

        private async void LoadStart()
        {
            while (true)
            {
                GetTitles();
                await Task.Delay(5000);
            }
        }
        private async void GetTitles()
        {
            RightButton.Content = "Loading....";
            string apiUrl = "https://script.google.com/macros/s/AKfycbxqUf7vVaonNgO5jFTDh5f0Rt-VQ0YlhaCyEA-ZCHT6KgLyBfzwrCQUP6oPw0BhqVvj/exec"; // Google Apps ScriptのWebアプリケーションのURLを指定

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // GETリクエストを送信してAPIからデータを取得
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // レスポンスボディからデータを取得
                    // JSONをパースして必要なデータを抽出する場合は、適宜処理を追加してください
                    Console.WriteLine("APIからのレスポンス:");
                    Console.WriteLine(responseBody);

                    // 必要な処理に応じてデータを変数に格納
                    // 以下はJSONのパース例です
                    JObject data = JObject.Parse(responseBody);
                    string organization = (string)data["organization"];
                    string title = (string)data["title"];
                    // Console.WriteLine("発表団体: " + organization);
                    // Console.WriteLine("発表タイトル: " + title);
                    lyricWindow.SetLyric(title, "Title");
                    lyricWindow.SetLyric(organization, "Grade");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("エラーが発生しました: " + ex.Message);
                }
            }
            {
                RightButton.Content = "表示内容更新";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lyricWindow.Close();
        }
    }
}
