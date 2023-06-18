using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TitleShower
{
    /// <summary>
    /// LyricWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LyricWindow : Window
    {
        public LyricWindow()
        {
            InitializeComponent();
            LyricBoxA_F.Text = "";
            LyricBoxA_D.Text = "";
            WindowStyle = WindowStyle.None; // ウィンドウのスタイルを非表示に設定
            WindowState = WindowState.Maximized; // ウィンドウを最大化して全画面表示
            Topmost = true; // ウィンドウを最前面に表示
        }

        public void SetLyric(string lyric, string destination)
        {
            if (destination == "Title")
            {
                if(lyric.Length > 13)
                {
                    lyric.Insert(13, "\n");
                }
                LyricBoxA_F.Text = lyric;
            }
            else if (destination == "Grade")
            {
                LyricBoxA_D.Text = lyric;

            }

        }
    }
}
