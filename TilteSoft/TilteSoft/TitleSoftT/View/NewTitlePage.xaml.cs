using CefSharp;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TitleSoftT.Dto;
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;
using TitleSoftT.ViewModels;

namespace TitleSoftT.View
{
    /// <summary>
    /// NewTitlePage.xaml 的交互逻辑
    /// </summary>
    public partial class NewTitlePage : Window
    {
        public NewTitlePage()
        {
            InitializeComponent();

            DataContext = new NewTitlePageViewModel();
            LoadData();
            ImageItemViewModel.ShowSelectImgCount += ImageItemViewModel_ShowSelectImgCount;
            //DataContext = NewTitlePageViewModel.Incccc;
            //DataContext = new AddTitleAndPicViewModel(); //AddTitlePageViewModel.Incccc; 
            #region
            this.WindowState = System.Windows.WindowState.Maximized;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            #endregion
            currentPage = 1;
        }
        Dictionary<int, string> dictPage = new Dictionary<int, string>();
        public int currentPage;
        public NewTitlePageViewModel ViewModel => DataContext as NewTitlePageViewModel;


        private void ImageItemViewModel_ShowSelectImgCount(object sender, EventArgs e)
        {
            var count = sender as List<ImageItemView>;
            this.labCounts.Content = string.Format("已选素材{0}张", count.Count());
        }


        public void LoadData()
        {
            warpPanelKeys.Children.Clear();
            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
            TitleInfoRequest request = new TitleInfoRequest();
            request.PageIndex = 0;
            request.PageSize = 10;
            request.IsOccupy = 0;
            request.UserNo = ViewModel.No;
            TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
            var tList = new List<MainWindowModel>();
            if (str.RowCount > 0)
            {
                //PageMax = str.PageMax;
                tList = str.mainWindowModels.ToList();
                //ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                //tList.ForEach(x => oc.Add(x));
                //Items = oc;
            }
            else
            {
                return;
            }
            foreach (var l in tList)
            {
                Button btn = new Button() { BorderThickness = new Thickness(2), FontSize = Convert.ToDouble(15.0) };
                //FontSize = "24"
                btn.FontSize = 20;
                btn.Margin = new Thickness(10);
                btn.Click += Btn_Click;
                btn.Content = l.NewTitle;
                this.warpPanelKeys.Children.Add(btn);
            }
        }




        public int page = 1;
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtKeyWorlds.Text.Trim() == "")
            {
                return;
            }
            this.labCounts.Content = "已选素材 0 张";
            imageCount.imgItem.Clear();
            dictPage.Clear();
            string searchUrl = string.Format("https://image.baidu.com/search/flip?tn=baiduimage&ie=utf-8&word={0}", this.txtKeyWorlds.Text.Trim());
            dictPage.Add(1, searchUrl);
            this.cefWebBrowser.Address = searchUrl;
            this.cefWebBrowser.FrameLoadEnd += CefWebBrowser_FrameLoadEnd;
            this.AddStackPanel.Children.Clear();
            this.AddScrollViewer.ScrollToHome();
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrePage_Click(object sender, RoutedEventArgs e)
        {
            this.cefWebBrowser.Address = dictPage[dictPage.Count() - 1];
        }
        string current = "";
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (nextPage != "")
            {
                this.cefWebBrowser.Address = "https://image.baidu.com/" + nextPage;
            }
        }

        private void CefWebBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //this.cefWebBrowser.FrameLoadEnd -= CefWebBrowser_FrameLoadEnd;
            getHtml();
        }
        int currents = 1;
        string nextPage = "";
        public void getHtml()
        {
            this.Dispatcher.InvokeAsync(() =>
            {
                //this.AddStackPanel.Children.Clear();
                if (currents == 1)
                {
                    currents++;
                }
                if (!dictPage.ContainsKey(currents))
                {
                    dictPage.Add(currents, this.cefWebBrowser.Address);
                    currents++;
                }

                Task<string> str = this.cefWebBrowser.GetSourceAsync();
                var data = str.Result;
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(data);
                //获取页码
                var page = doc.DocumentNode.SelectNodes("//div[@id='page']/a[@class='n']");

                foreach (var list in page)
                {
                    nextPage = list.Attributes[0].Value.Replace("&amp;", "&");
                }
                //获取图片url
                var nodes = doc.DocumentNode.SelectNodes("//li[@class='imgitem']/a/img");
                foreach (var item in nodes)
                {
                    string url = item.Attributes["src"].Value.Replace("&amp;", "&");
                    Console.WriteLine(url);
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();

                    //image.Width = (double)288;
                    //image.Height = (double)272;
                    //image.Margin = new Thickness(5);
                    //image.ToolTip = "测试";
                    //image.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                    ImageItemView iiv = new ImageItemView();
                    iiv.imgShow.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                    imageCount.imgItem.Add(iiv);
                    this.AddStackPanel.Children.Add(iiv);
                    //this.AddStackPanel.Children.Add(image);
                }
                //this.AddScrollViewer.ScrollToHome();
            });

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.cefWebBrowser.Dispose();
        }
        /// <summary>
        /// 提交数据入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("一个关键字只能提交一次，确认提交数据码？", "温馨提示", System.Windows.Forms.MessageBoxButtons.YesNo);
            //if (dr.ToString() == "Yes")
            //{

            List<ImageCollectsDto> dtos = new List<ImageCollectsDto>();
            var imgList = imageCount.imgItem.Where(m => m.imgCheck.Visibility == Visibility.Visible).ToList();
            var urlList = new List<string>();

            foreach (var imgL in imgList)
            {
                ImageCollectsDto imageCollects = new ImageCollectsDto();
                imageCollects.ImageUrl = imgL.imgShow.Source.ToString();
                imageCollects.ImageFrom = "百度";
                //imageCollects.UserName = this.txtUserName.Text.Trim();
                imageCollects.ImageClassName = this.txtKeyWorlds.Text.Trim();
                dtos.Add(imageCollects);
                urlList.Add(imgL.imgShow.Source.ToString());
            }
            //bool result = HttpHelper.BatchInsert(HttpHelper.ToDataTable(dtos), "ImageCollects");


            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
            SavaPicRequest request = new SavaPicRequest();
            //request.UserInfoID = SelectItem.ID;
            //request.IsEnable = 1;
            //request.TitleInfoID = SelectItem.ID;
            //var urlList = PictureControlViewModels.Where(c => c.IsCheck == true).Select(a => new { a.Url }).ToList();//把选中的url获取出来
            //var newList = new List<string>();
            //for (int i = 0; i < urlList.Count; i++)
            //{
            //    newList.Add(urlList[i].Url);
            //}
            //request.BaiDuUrlList = urlList;
            var ret = titleInfo.UserBindBaiDuUrlT(this.txtKeyWorlds.Text, urlList);
            //if (ret == 1)
            //{
            //    SavaPicMsg = "保存成功";
            //    Task.Run(new Action(Init));
            //    PageNo = 0;
            //    PictureControlViewModels.Clear();
            //}
            //else if (ret == 3)
            //{
            //    SavaPicMsg = "保存失败";
            //}
            //else if (ret == 101)
            //{
            //    SavaPicMsg = "未知错误";
            //}
            if (ret == 1)
            {
                ViewModel.Msg = "保存成功";
                //System.Windows.Forms.MessageBox.Show("数据入库成功!");
                //更改关键字 刷新界面 刷新关键字
                //var res = SqlHelper.cmdExecuteNonQuery("update keywords set isuse=1 where key_name='" + this.txtKeyWorlds.Text.Trim() + "'");
                this.warpPanelKeys.Children.Clear();
                this.AddStackPanel.Children.Clear();
                this.labCounts.Content = "已选素材 0 张";
                this.txtKeyWorlds.Text = "";
                LoadData();
                //var dt = SqlHelper.GetLocalDataTable("select * from keywords where isown='" + this.txtUserName.Text.Trim() + "' and isuse=0 order by search_total");
                //var list = ModelConvertHelper<keyWordDto>.ConvertToModel(dt);
                //foreach (var l in list)
                //{
                //    Button btn = new Button() { BorderThickness = new Thickness(2), FontSize = Convert.ToDouble(15.0) };
                //    btn.Click += Btn_Click;
                //    btn.Content = l.key_name;
                //    this.warpPanelKeys.Children.Add(btn);
                //}

            }
            else
            {
                ViewModel.Msg = "保存失败";
                System.Windows.Forms.MessageBox.Show("数据入库失败!");
            }
            //  }
        }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //string logStr = "select count(*) from UserInfo where UserName='" + this.txtUserName.Text.Trim() + "' and PassWord='" + this.txtPassWold.Password.Trim() + "'";
            //int result = SqlHelper.cmdExecuteScalar(logStr);
            //if (result == 0)
            //{
            //    MessageBox.Show("登陆失败!");
            //    return;
            //}
            //this.Title = "欢迎【" + this.txtUserName.Text.Trim() + "】登陆!";
            //var dt = SqlHelper.GetLocalDataTable("select * from keywords where isown='" + this.txtUserName.Text.Trim() + "' and isuse=0 order by search_total");
            //var list = ModelConvertHelper<keyWordDto>.ConvertToModel(dt);

            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
            TitleInfoRequest request = new TitleInfoRequest();
            request.PageIndex = 0;
            request.PageSize = 10;
            request.IsOccupy = 0;
            //request.UserNo = No;
            TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
            var tList = new List<MainWindowModel>();
            if (str.RowCount > 0)
            {
                //PageMax = str.PageMax;
                tList = str.mainWindowModels.ToList();
                //ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                //tList.ForEach(x => oc.Add(x));
                //Items = oc;
            }
            else
            {
                return;
            }
            foreach (var l in tList)
            {
                Button btn = new Button() { BorderThickness = new Thickness(2), FontSize = Convert.ToDouble(15.0) };
                btn.Click += Btn_Click;
                btn.Content = l.NewTitle;
                this.warpPanelKeys.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            this.txtKeyWorlds.Text = (sender as Button).Content.ToString();
            btnSearch_Click(sender, e);
        }


        private void AddTitle_Click(object sender, RoutedEventArgs e)
        {
            AddTitleAndPic addTitleAndPic = new AddTitleAndPic();
            addTitleAndPic.ShowDialog();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AndPic_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == 1)
            {

            }
            else
            {
                hello1.Visibility = Visibility.Visible;
                hello2.Visibility = Visibility.Collapsed;
                PicStackPanel.Visibility = Visibility.Collapsed;
                DisplayPage1.Visibility = Visibility.Visible;
                LoadData();
                currentPage = 1;
            }
        }

        /// <summary>
        /// 查询已绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectPicList_Click(object sender, RoutedEventArgs e)
        {
            //PicStackPanel
            if (currentPage == 2)
            {

            }
            else
            {
                hello1.Visibility = Visibility.Collapsed;
                hello2.Visibility = Visibility.Visible;
                PicStackPanel.Visibility = Visibility.Visible;
                DisplayPage1.Visibility = Visibility.Collapsed;
                //PicStackPanel.Visibility = Visibility.Collapsed;
                currentPage = 2;
                ViewModel.AddTitlePageViewModel.Init();
            }
        }


        private void TitleHandle_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("隐藏数据不可撤销，确认隐藏数据吗？", "温馨提示", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dr.ToString() == "Yes")
            {
                Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                TitleHandleRequest request = new TitleHandleRequest();
                var str = titleInfo.TitleHandle(txtKeyWorlds.Text);
                if (str == 1)
                {
                  
                    this.warpPanelKeys.Children.Clear();
                    this.AddStackPanel.Children.Clear();
                    MessageBox.Show("成功隐藏该数据");
                    LoadData();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
