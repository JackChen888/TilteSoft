using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;

namespace TitleSoftT.ViewModels
{
    public class AddTitlePageViewModel: ViewModelBase
    {

        public AddTitlePageViewModel()
        {
            No = MainWindowViewModel.Incccc.No;
            Task.Run(new Action(Init));
        }


        private static AddTitlePageViewModel inccc;
        public static AddTitlePageViewModel Incccc
        {
            get
            {
                if (inccc == null)
                    inccc = new AddTitlePageViewModel();
                return inccc;
            }

        }

        

        private ObservableCollection<MainWindowModel> _items;
        public ObservableCollection<MainWindowModel> Items
        {
            get { return _items; }
            set { Set("Items", ref _items, value); }
        }


        private MainWindowModel _selectItems;
        public MainWindowModel SelectItems
        {
            get { return _selectItems; }
            set { Set("SelectItems", ref _selectItems, value); }
        }


        private string _no;

        public string No
        {
            get { return _no; }
            set { Set("No", ref _no, value); }
        }

       


        #region 加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        public async void Init()
        {
            await Task.Run(() =>
            {
                try
                {
                    Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                    TitleInfoRequest request = new TitleInfoRequest();
                    request.PageIndex = 0;
                    request.PageSize = 500;
                    request.IsOccupy = 0;
                    request.UserNo = No;
                    request.IsStep = 2;
                    TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
                    if (str.RowCount > 0)
                    {
                        //PageMax = str.PageMax;
                        var tList = str.mainWindowModels.ToList();
                        ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                        tList.ForEach(x => oc.Add(x));
                        Items = oc;
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
            });
        }
        #endregion





    }
}
