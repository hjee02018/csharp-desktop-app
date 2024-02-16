using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sample1
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // MainWindow.xaml을 로드하도록 StartupUri 속성 설정
            StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}
