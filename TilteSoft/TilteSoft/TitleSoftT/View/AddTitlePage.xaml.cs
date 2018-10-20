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
    /// AddTitlePage.xaml 的交互逻辑
    /// </summary>
    public partial class AddTitlePage : Window
    {
        public AddTitlePage()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = AddTitlePageViewModel.Incccc; // MainWindowViewModel.Incccc;
        }

        private void AddTitle_Click(object sender, RoutedEventArgs e)
        {
            AddTitleAndPic addTitleAndPic = new AddTitleAndPic();
            addTitleAndPic.ShowDialog();
        }
    }
}
