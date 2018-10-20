using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TitleSoftT.ViewModels;

namespace TitleSoftT
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [DllImport("doModelUserLogin.dll", EntryPoint = "doModelLoginExecuteForAccessA")]
        static extern bool doModelLoginExecuteForAccessA(IntPtr wnd, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder no, [MarshalAs(UnmanagedType.LPTStr)]StringBuilder name, int usertype, ref bool isAdmin);
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    ThemeManager.ChangeTheme(ThemeStyle.Blue);
        //    base.OnStartup(e);
        //}

        protected override void OnStartup(StartupEventArgs e)
        {
            if (App.Current.StartupUri.ToString() == "MainWindow.xaml")
            {
                StringBuilder no = new StringBuilder(50);
                StringBuilder name = new StringBuilder(50);
                bool isAdmin = true;
                var boolReturn = doModelLoginExecuteForAccessA(IntPtr.Zero, no, name, 4, ref isAdmin);
                if (boolReturn)
                {
                    var DataContext = MainWindowViewModel.Incccc;
                    DataContext.Account = name.ToString();
                    DataContext.No = no.ToString();

                    //var DataContextAddTitlePage = AddTitlePageViewModel.Incccc;
                    //DataContextAddTitlePage.No = no.ToString();

                    //PubUserData.auditUser = no.ToString();
                }
                //PubUserData.auditUser = no.ToString();
                else
                    Application.Current.Shutdown();
            }
        }




    }
}
