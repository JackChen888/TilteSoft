using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;

namespace TitleSoftT.ViewModels
{
    public class TitleActionViewModel : ViewModelBase
    {


        public TitleActionViewModel()
        {
            Items = new ObservableCollection<MainWindowModel>();
            Task.Run(new Action(Init));
        }


        private ObservableCollection<MainWindowModel> _items;
        public ObservableCollection<MainWindowModel> Items
        {
            get { return _items; }
            set { Set("Items", ref _items, value); }
        }

        private MainWindowModel _selectItem;
        public MainWindowModel SelectItem
        {
            get { return _selectItem; }
            set { Set("SelectItem", ref _selectItem, value); }
        }

        private string _account;

        public string Account
        {
            get { return _account; }
            set { Set("Account", ref _account, value); }
        }

        private string _no;

        public string No
        {
            get { return _no; }
            set { Set("No", ref _no, value); }
        }

        private int _pageNo;
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageNo
        {
            get { return _pageNo; }
            set { Set("PageNo", ref _pageNo, value); }
        }

        private int _pageMax;
        /// <summary>
        /// 页面最大值
        /// </summary>
        public int PageMax
        {
            get { return _pageMax; }
            set { Set("PageMax", ref _pageMax, value); }
        }

        private int _pageMin;
        /// <summary>
        /// 页面最小值
        /// </summary>
        public int PageMin
        {
            get { return _pageMin; }
            set { Set("PageMin", ref _pageMin, value); }
        }

        private string _titleName;

        public string TitleName
        {
            get { return _titleName; }
            set { Set("TitleName", ref _titleName, value); }
        }


        #region 加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        public async void Init()
        {
            Items.Clear();
            await Task.Run(() =>
            {
                try
                {
                    Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                    TitleInfoRequest request = new TitleInfoRequest();
                    request.PageIndex = 0;
                    request.PageSize = 20;
                    //request.IsOccupy = 0;
                    request.UserNo = No;
                    var str = titleInfo.SelectAllTitleInfoList(request);
                    if (str.RowCount > 0)
                    {
                        
                        PageMax = str.PageMax;
                        var tList = str.mainWindowModels.ToList();
                        ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                        tList.ForEach(x => oc.Add(x));
                        Items = oc;
                        
                        //foreach (var item in tList)
                        //{
                        //    Items.Add(item);
                        //}

                        //foreach (var item in Items)
                        //{
                        //    item.IsHandle = 0;
                        //}
                    }
                    else
                    {
                        return;
                    }
                    //var result = new ActionResult();
                    //result.Result = true;
                }
                catch (Exception e)
                {
                   var str= e.Message;
                }
                finally
                {

                }
            });
        }
        #endregion



        


        #region 
        private ICommand _selectTitle;

        public ICommand SelectTitle
        {
            get
            {
                if (_selectTitle == null)
                {
                    _selectTitle = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {
                            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                            TitleInfoRequest request = new TitleInfoRequest();
                            request.TitleName = TitleName;
                            request.PageIndex = PageMax;
                            request.PageSize = 20;
                            //request.IsOccupy = 0;
                            //PageNo = request.PageIndex;
                            request.UserNo = No;
                            TitleInfoListResponse str = titleInfo.SelectAllTitleInfoList(request);
                            
                            if (str.RowCount > 0)
                            {
                                PageMax = str.PageMax;
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
                        catch (Exception exception)
                        {

                        }
                        finally
                        {

                        }

                    });
                }
                return _selectTitle;
            }
        }
        #endregion


        #region 末页
        private ICommand _btnLast;

        public ICommand BtnLast
        {
            get
            {
                if (_btnLast == null)
                {
                    _btnLast = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {
                            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                            TitleInfoRequest request = new TitleInfoRequest();
                            request.PageIndex = PageMax;
                            request.PageSize = 20;
                            //request.IsOccupy = 0;
                            PageNo = request.PageIndex;
                            request.UserNo = No;
                            TitleInfoListResponse str = titleInfo.SelectAllTitleInfoList(request);
                            if (str.RowCount > 0)
                            {
                                PageMax = str.PageMax;
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
                        catch (Exception exception)
                        {

                        }
                        finally
                        {

                        }

                    });
                }
                return _btnLast;
            }
        }
        #endregion

        #region 下一页

        private ICommand _btnNext;

        public ICommand BtnNext
        {
            get
            {
                if (_btnNext == null)
                {
                    _btnNext = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {
                            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                            TitleInfoRequest request = new TitleInfoRequest();

                            if (PageMax > PageNo)
                            {
                                request.PageIndex = PageNo + 1;
                            }
                            else
                            {
                                request.PageIndex = PageNo;
                            }
                            PageNo = request.PageIndex;
                            request.PageSize = 20;
                            //request.IsOccupy = 0;
                            //request.UserNo = No;
                            TitleInfoListResponse str = titleInfo.SelectAllTitleInfoList(request);
                            if (str.RowCount > 0)
                            {
                                PageMax = str.PageMax;
                                List<MainWindowModel> tList = str.mainWindowModels.ToList();
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
                return _btnNext;
            }

        }


        #endregion

        #region 上一页
        private ICommand _btnPrev;

        public ICommand BtnPrev
        {
            get
            {
                if (_btnPrev == null)
                {
                    _btnPrev = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {
                            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                            TitleInfoRequest request = new TitleInfoRequest();
                            if (PageNo > 1)
                            {
                                request.PageIndex = PageNo - 1;
                            }
                            request.PageSize = 20;
                            //request.IsOccupy = 0;
                            //request.UserNo = No;
                            PageNo = request.PageIndex;
                            TitleInfoListResponse str = titleInfo.SelectAllTitleInfoList(request);
                            if (str.RowCount > 0)
                            {
                                PageMax = str.PageMax;
                                List<MainWindowModel> tList = str.mainWindowModels.ToList();
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
                return _btnPrev;
            }

        }
        #endregion

        #region 首页
        private ICommand _btnFirst;
        /// <summary>
        /// 首页
        /// </summary>
        public ICommand BtnFirst
        {
            get
            {
                if (_btnFirst == null)
                {
                    _btnFirst = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {
                            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                            TitleInfoRequest request = new TitleInfoRequest();
                            request.PageIndex = 0;
                            request.PageSize = 20;
                            //request.IsOccupy = 0;
                            PageNo = request.PageIndex;
                            request.UserNo = No;
                            TitleInfoListResponse str = titleInfo.SelectAllTitleInfoList(request);
                            if (str.RowCount > 0)
                            {
                                PageMax = str.PageMax;
                                List<MainWindowModel> tList = str.mainWindowModels.ToList();
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
                return _btnFirst;
            }
        }
        #endregion




        #region 隐藏标签
        private ICommand _titleHandle;
        /// <summary>
        /// 首页
        /// </summary>
        public ICommand TitleHandle
        {
            get
            {
                if (_titleHandle == null)
                {
                    _titleHandle = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
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
                            request.ID =SelectItem.ID;
                            var str = titleInfo.TitleHandle(request);
                            if (str==  0)
                            {
                                //Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                                TitleInfoRequest requestTitleInfo = new TitleInfoRequest();
                                requestTitleInfo.PageIndex = PageNo;
                                requestTitleInfo.PageSize = 20;
                                //request.IsOccupy = 0;
                                requestTitleInfo.UserNo = No;
                                var strTitleInfo = titleInfo.SelectAllTitleInfoList(requestTitleInfo);
                                if (strTitleInfo.RowCount > 0)
                                {

                                    PageMax = strTitleInfo.PageMax;
                                    var tList = strTitleInfo.mainWindowModels.ToList();
                                    ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                                    tList.ForEach(x => oc.Add(x));
                                    Items = oc;
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

                    });

                }
                return _titleHandle;
            }
        }
        #endregion


        


    }
}
