using GalaSoft.MvvmLight.Messaging;
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
using TitleSoftT.ViewModels;

namespace TitleSoftT.View
{
    /// <summary>
    /// AddTitleAndPic.xaml 的交互逻辑
    /// </summary>
    public partial class AddTitleAndPic : Window
    {
        public AddTitleAndPic()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Messenger.Default.Register<String>(this, "ViewAlert", ClosePage);
            DataContext = new AddTitleAndPicViewModel(); //AddTitlePageViewModel.Incccc;         
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        private void ClosePage(string msg)
        {
            if (msg == "1")
            {
                this.Close();
            }
            else
            {

            }
        }

    }
}
