using Sample2.View;
using Sample2.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sample2
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //  로그인 
            LoginView loginView = new LoginView();
            LoginViewModel loginViewModel = new LoginViewModel();
            loginView.DataContext = loginViewModel;


            // 로그인 창을 모달 다이얼로그로 열기
            bool? loginResult = loginView.ShowDialog();

            // 로그인이 성공하면 MainWindow를 열고, 로그인 창은 닫기
            if (loginResult.HasValue)
            {
                //MainWindow mainWindow = new MainWindow();
                //mainWindow.Show();
            }
            else
            {
                // 로그인이 실패하거나 창이 닫힌 경우 앱 종료
                Shutdown();
            }
        }
    }
}
