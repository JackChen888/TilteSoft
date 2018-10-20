using CefSharp;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;
using TitleSoftT.View;
using TitleSoftT.ViewModel;
using TitleSoftT.ViewModels;

namespace TitleSoftT
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = MainWindowViewModel.Incccc; //ViewModelLocator.Get<MainWindowViewModel>();  //new MainWindowViewModel();
            #region
            this.WindowState = System.Windows.WindowState.Maximized;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            #endregion
        }

        public MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;



        private void BaiDuSearch_Click(object sender, RoutedEventArgs e)
        {
            GetHtml();
        }

        string nextPage = "";

        public void GetHtml()
        {
            this.Dispatcher.InvokeAsync(() =>
            {
                Task<string> str = this.cefWebBrowser.GetSourceAsync();
                var data = str.Result;
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(data);
                var page = doc.DocumentNode.SelectNodes("//div[@id='page']/a[@class='n']");
                foreach (var list in page)
                {
                    nextPage = list.Attributes[0].Value.Replace("&amp;", "&");
                }
                int i = 1;
                var nodes = doc.DocumentNode.SelectNodes("//li[@class='imgitem']/a/img");
                ObservableCollection<PictureControlModel> newPictureControlViewModels = new ObservableCollection<PictureControlModel>();
                for (int z = 0; z < nodes.Count(); z++)
                {
                    string url = nodes[z].Attributes["src"].Value.Replace("&amp;", "&");
                    Console.WriteLine(url);

                    PictureControlModel model = new PictureControlModel();
                    model.Url = url;
                    newPictureControlViewModels.Add(model);
                }
                ViewModel.PictureControlViewModels = newPictureControlViewModels;
            });
        }

        private void TitleAndPicSelect_Click(object sender, RoutedEventArgs e)
        {
            if (NewTitle.Text.Trim() == "")
            {
                MessageBox.Show("请输入查询条件");
                return;
            }
            string searchUrl = string.Format("https://image.baidu.com/search/flip?tn=baiduimage&ie=utf-8&word={0}", this.NewTitle.Text);
            this.cefWebBrowser.Address = searchUrl;
            this.cefWebBrowser.FrameLoadEnd += CefWebBrowser_FrameLoadEnd;
        }


        private void CefWebBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            try
            {
                GetHtml();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void SaveCheckPi_Click(object sender, RoutedEventArgs e)
        {
            AddTitlePage addTitlePage = new AddTitlePage();
            addTitlePage.ShowDialog();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NewTitle.Text.Trim() == "")
            {
                MessageBox.Show("请输入查询条件");
                return;
            }
            string searchUrl = string.Format("https://image.baidu.com/search/flip?tn=baiduimage&ie=utf-8&word={0}", this.NewTitle.Text);
            this.cefWebBrowser.Address = searchUrl;
            this.cefWebBrowser.FrameLoadEnd += CefWebBrowser_FrameLoadEnd;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (ViewModel.PicSelectItem.IsCheck)
            {
                ViewModel.PicSelectItem.IsCheck = false;
            }
            else
            {
                ViewModel.PicSelectItem.IsCheck = true;
            }
        }


        /// <summary>
        /// 将SelectedItem滚动为第一行
        /// </summary>
        /// <param name="dataGrid">目标DagaGrid</param>
        /// <param name="selectedItem">选中项</param>
        public static void SetSelectedItemFirstRow(object dataGrid, object selectedItem)
        {
            //若目标datagrid为空，抛出异常
            if (dataGrid == null)
            {
                throw new ArgumentNullException("目标无" + dataGrid + "无法转换为DataGrid");
            }
            //获取目标DataGrid，为空则抛出异常
            System.Windows.Controls.DataGrid dg = dataGrid as System.Windows.Controls.DataGrid;
            if (dg == null)
            {
                throw new ArgumentNullException("目标无" + dataGrid + "无法转换为DataGrid");
            }
            //数据源为空则返回
            if (dg.Items == null || dg.Items.Count < 1)
            {
                return;
            }

            //首先滚动为末行
            dg.SelectedItem = dg.Items[dg.Items.Count - 1];
            dg.CurrentColumn = dg.Columns[0];
            dg.ScrollIntoView(dg.SelectedItem, dg.CurrentColumn);

            //获取焦点，滚动为目标行
            dg.Focus();
            dg.SelectedItem = selectedItem;
            dg.CurrentColumn = dg.Columns[0];
            dg.ScrollIntoView(dg.SelectedItem, dg.CurrentColumn);
        }

        private void OpenTitleAction_Click(object sender, RoutedEventArgs e)
        {
            TitleAction titleAction = new TitleAction();
            titleAction.ShowDialog();

        }

        private void Handle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                TitleHandleRequest request = new TitleHandleRequest();
                //request.PageIndex = 0;
                //request.PageSize = 20;
                ////request.IsOccupy = 0;
                //PageNo = request.PageIndex;
                //request.UserNo = No;
                request.ID = ViewModel.SelectItem.ID;
                var str = titleInfo.TitleHandle(request);
                if (str == 1)
                {
                    //Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                    TitleInfoRequest requestTitleInfo = new TitleInfoRequest();
                    requestTitleInfo.PageIndex = ViewModel.PageNo;
                    requestTitleInfo.PageSize = 20;
                    //request.IsOccupy = 0;
                    requestTitleInfo.UserNo = ViewModel.No;
                    var strTitleInfo = titleInfo.SelectAllTitleInfoList(requestTitleInfo);
                    if (strTitleInfo.RowCount > 0)
                    {
                        ViewModel.PageMax = strTitleInfo.PageMax;
                        var tList = strTitleInfo.mainWindowModels.ToList();
                        ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                        tList.ForEach(x => oc.Add(x));
                        ViewModel.Items = oc;
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        private void ClickNewTitle_Click(object sender, RoutedEventArgs e)
        {
            NewTitlePage newTitlePage = new NewTitlePage();
            newTitlePage.ShowDialog();
        }
    }
}
