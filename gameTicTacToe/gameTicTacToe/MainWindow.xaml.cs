using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace gameTicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        DispatcherTimer tic = null;
        int count_sec = 0;
        int count_res = 0;
        public Image image;
        BitmapImage bit;
        DateTime startTime;
        
        public MainWindow()
        {
            InitializeComponent();
            rbtnFirstPlayer.IsChecked = true;
            tic = new DispatcherTimer();
            tic.Interval = TimeSpan.FromMilliseconds(1000);
            tic.Tick += Timer_Tick;
            count_res = (Application.Current as App).Settings.BestTime;
            Result.Text = $"{((double)count_res) / 10.0}";
        }

        private void NewGame()
        {
            foreach (FrameworkElement elem in GameGrid.Children)
            {
                elem.Tag = "free";
                (elem as TextBlock).Text = "";
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            count_sec++;
            Time.Text = $"{count_sec} sec";            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            tic.Start();
            NewGame();
        } 
               

        private void rbtnFirstPlayer_Checked(object sender, RoutedEventArgs e)
        {
            if (rbtnFirstPlayer.IsChecked == true)
            {
                rbtnFirstPlayer.FontSize = 20.0;
                rbtnSecondPlayer.FontSize = 15.0;

            }
            else
            {
                rbtnFirstPlayer.FontSize = 15.0;
                rbtnSecondPlayer.FontSize = 20.0;

            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBlock)
            {
                image = new Image();
                bit = new BitmapImage();
                bit.BeginInit();

                if (rbtnFirstPlayer.IsChecked == true)
                {
                    bit.UriSource = new Uri("X.bmp", UriKind.RelativeOrAbsolute);
                    (e.OriginalSource as TextBlock).Tag = "krest";
                    rbtnSecondPlayer.IsChecked = true;

                }

                else
                {
                    bit.UriSource = new Uri("O.bmp", UriKind.RelativeOrAbsolute);
                    (e.OriginalSource as TextBlock).Tag = "zero";
                    rbtnFirstPlayer.IsChecked = true;
                }

                bit.EndInit();
                image.Source = bit;
                image.Stretch = Stretch.Fill;
                (e.OriginalSource as TextBlock).Inlines.Add(new InlineUIContainer(image));

                FullLine();
            }
        }

        private void FullLine()
        {
            for (int i = 0; i < GameGrid.ColumnDefinitions.Count; i++)
            {
                //по горизонтале
                if ((GameGrid.Children[i * 3] as TextBlock).Tag == (GameGrid.Children[i * 3 + 1] as TextBlock).Tag &&
                   (GameGrid.Children[i * 3] as TextBlock).Tag == (GameGrid.Children[i * 3 + 2] as TextBlock).Tag &&
                    (GameGrid.Children[i * 3] as TextBlock).Tag.ToString() == "krest")
                {
                    Finish("x");
                    break;
                }
                else if ((GameGrid.Children[i * 3] as TextBlock).Tag == (GameGrid.Children[i * 3 + 1] as TextBlock).Tag &&
                    (GameGrid.Children[i * 3] as TextBlock).Tag == (GameGrid.Children[i * 3 + 2] as TextBlock).Tag &&
                     (GameGrid.Children[i * 3] as TextBlock).Tag.ToString() == "zero")
                {
                    Finish("0");
                    break;
                }

                //по вертикали
                else if ((GameGrid.Children[i] as TextBlock).Tag == (GameGrid.Children[i + 3] as TextBlock).Tag &&
                    (GameGrid.Children[i] as TextBlock).Tag == (GameGrid.Children[i + 6] as TextBlock).Tag &&
                     (GameGrid.Children[i] as TextBlock).Tag.ToString() == "krest")
                {
                    Finish("x");
                    break;
                }

                else if ((GameGrid.Children[i] as TextBlock).Tag == (GameGrid.Children[i + 3] as TextBlock).Tag &&
                   (GameGrid.Children[i] as TextBlock).Tag == (GameGrid.Children[i + 6] as TextBlock).Tag &&
                    (GameGrid.Children[i] as TextBlock).Tag.ToString() == "zero")
                {
                    Finish("0");
                    break;
                }

                //по диагонале
                else if ((GameGrid.Children[0] as TextBlock).Tag == (GameGrid.Children[4] as TextBlock).Tag &&
                   (GameGrid.Children[0] as TextBlock).Tag == (GameGrid.Children[8] as TextBlock).Tag &&
                    (GameGrid.Children[0] as TextBlock).Tag.ToString() == "krest")
                {
                    Finish("x");
                    break;
                }

                else if ((GameGrid.Children[0] as TextBlock).Tag == (GameGrid.Children[4] as TextBlock).Tag &&
             (GameGrid.Children[0] as TextBlock).Tag == (GameGrid.Children[8] as TextBlock).Tag &&
              (GameGrid.Children[0] as TextBlock).Tag.ToString() == "zero")
                {
                    Finish("0");
                    break;
                }

                else if ((GameGrid.Children[2] as TextBlock).Tag == (GameGrid.Children[4] as TextBlock).Tag &&
              (GameGrid.Children[2] as TextBlock).Tag == (GameGrid.Children[6] as TextBlock).Tag &&
               (GameGrid.Children[2] as TextBlock).Tag.ToString() == "krest")
                {
                    Finish("x");
                    break;
                }

                else if ((GameGrid.Children[2] as TextBlock).Tag == (GameGrid.Children[4] as TextBlock).Tag &&
              (GameGrid.Children[2] as TextBlock).Tag == (GameGrid.Children[6] as TextBlock).Tag &&
               (GameGrid.Children[2] as TextBlock).Tag.ToString() == "zero")
                {
                    Finish("0");
                    break;
                }
               


            }
            int num = 0;
            for(int i = 0; i < GameGrid.Children.Count; i++)
            {
                if((GameGrid.Children[i] as FrameworkElement).Tag.ToString() == "krest"|| (GameGrid.Children[i] as FrameworkElement).Tag.ToString() == "zero")
                {
                    num++;
                }
            }
            if (num >= 8)
            {
                Finish("=");
                   }
        }

        private void Finish(string condition)
        {
            switch (condition)
            {
                case "x":
                    MessageBox.Show("Игрок X победил!");
                    break;

                case "0":
                    MessageBox.Show("Игрок O победил!");
                    break;

                case "=":
                    MessageBox.Show("Ни чья!");
                    break;
                default:
                    break;
            }

            TimeSpan ts = DateTime.Now - startTime;
            if (count_res > ts.Milliseconds)
            {
                count_res = ts.Milliseconds;
                (Application.Current as App).Settings.BestTime = count_res;
                Result.Text = $"{((double)count_res)/10.0}";
            }          
            tic.Stop();
            NewGame();
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tic.Start();
            startTime = DateTime.Now;
            GameGrid.IsEnabled = true;          
        }
    }
}
