using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace gameTicTacToe
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        ApplicationSettings settings = new ApplicationSettings();
        public App()
        {
            this.Exit += App_Exit;
        }
        public ApplicationSettings Settings
        {
            get
            {
                return settings;
            }
        }
        private void App_Exit(object sender, ExitEventArgs e)
        {
            Settings.Save();
        }
    }
}
