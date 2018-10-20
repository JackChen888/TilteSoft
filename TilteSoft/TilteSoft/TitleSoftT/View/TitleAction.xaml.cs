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
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;
using TitleSoftT.ViewModels;

namespace TitleSoftT.View
{
    /// <summary>
    /// TitleAction.xaml 的交互逻辑
    /// </summary>
    public partial class TitleAction : Window
    {
        public TitleAction()
        {
            InitializeComponent();
            DataContext = new TitleActionViewModel();
            #region
            this.WindowState = System.Windows.WindowState.Maximized;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            #endregion
        }

        public TitleActionViewModel ViewModel => DataContext as TitleActionViewModel;



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
                //var result = new ActionResult();
                //result.Result = true;
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }
    }
}
