using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TitleSoftT.Common.Helper;
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;

namespace TitleSoftT.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Task.Run(new Action(Init));
            PageNo = 0;
        }
        private static MainWindowViewModel inccc;
        public static MainWindowViewModel Incccc
        {
            get
            {
                if (inccc == null)
                    inccc = new MainWindowViewModel();
                return inccc;
            }

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

        private ObservableCollection<PictureControlModel> _pictureControlViewModels;

        public ObservableCollection<PictureControlModel> PictureControlViewModels
        {
            get { return _pictureControlViewModels; }
            set { Set("PictureControlViewModels", ref _pictureControlViewModels, value); }
        }

        private PictureControlModel _picSelectItem;
        public PictureControlModel PicSelectItem
        {
            get { return _picSelectItem; }
            set { Set("PicSelectItem", ref _picSelectItem, value); }
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

        private string _savaPicMsg;

        public string SavaPicMsg
        {
            get { return _savaPicMsg; }
            set { Set("SavaPicMsg", ref _savaPicMsg, value); }
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
                             request.PageSize = 20;
                             request.IsOccupy = 0;
                             request.UserNo = No;
                             TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
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
                             var result = new ActionResult();
                             result.Result = true;
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



        public void DataList()
        {
            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
            TitleInfoRequest request = new TitleInfoRequest();
            request.PageIndex = 1;
            request.PageSize = 100;
            TitleInfoListResponse str = titleInfo.SelectTitleInfoList(request);
            if (str.RowCount > 0)
            {
                List<MainWindowModel> tList = str.mainWindowModels.ToList();
                ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                tList.ForEach(x => oc.Add(x));
                Items = oc;
            }
            else
            {
                return;
            }
            var result = new ActionResult();
            try
            {
                result.Result = true;
            }
            catch (Exception exception)
            {
                result.ErrorMessage = exception.Message;
            }
            finally
            {

            }
        }

        public void GetDateList(Action<ActionResult> action = null)
        {
            Task.Factory.StartNew(() =>
            {
                Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                TitleInfoRequest request = new TitleInfoRequest();
                request.PageIndex = 1;
                request.PageSize = 100;
                TitleInfoListResponse str = titleInfo.SelectTitleInfoList(request);
                if (str.RowCount > 0)
                {
                    List<MainWindowModel> tList = str.mainWindowModels.ToList();
                    ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                    tList.ForEach(x => oc.Add(x));
                    Items = oc;
                }
                else
                {
                    return;
                }
                var result = new ActionResult();
                try
                {
                    result.Result = true;
                }
                catch (Exception exception)
                {
                    result.ErrorMessage = exception.Message;
                }
                finally
                {
                    action?.Invoke(result);
                }
            });
        }

        #region 增加30条数据该登陆用户
        private ICommand _addThirtyData;
        /// <summary>
        /// 
        /// </summary>
        public ICommand AddThirtyData
        {
            get
            {
                if (_addThirtyData == null)
                {
                    _addThirtyData = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                        TitleInfoRequest request = new TitleInfoRequest();
                        request.PageIndex = 1;
                        request.PageSize = 30;
                        request.IsOccupy = 0;
                        request.UserNo = No;
                        request.UserName = Account;
                        request.IsHandle = 0;
                        var str = titleInfo.TitleAddUser(request);
                        if (str == 1)
                        {
                            Init();
                        }
                        else
                        {
                            return;
                        }
                    });
                }
                return _addThirtyData;
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
                            request.IsOccupy = 0;
                            PageNo = request.PageIndex;
                            request.UserNo = No;
                            TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
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
                            var result = new ActionResult();
                            result.Result = true;
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
                            request.IsOccupy = 0;
                            request.UserNo = No;
                            TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
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
                            var result = new ActionResult();
                            result.Result = true;
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
                            request.IsOccupy = 0;
                            request.UserNo = No;
                            PageNo = request.PageIndex;
                            TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
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
                            var result = new ActionResult();
                            result.Result = true;
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
                            request.IsOccupy = 0;
                            PageNo = request.PageIndex;
                            request.UserNo = No;
                            TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
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
                            var result = new ActionResult();
                            result.Result = true;
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

        private ICommand _titleHandle;


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
                            request.ID = SelectItem.ID;
                            var ret = titleInfo.TitleHandle(request);
                            if (ret == 1)
                            {
                                SavaPicMsg = "隐藏成功";
                                Task.Run(new Action(Init));
                                PageNo = 0;
                            }
                            else if (ret == 3)
                            {
                                SavaPicMsg = "隐藏失败";
                            }
                            else if (ret == 101)
                            {
                                SavaPicMsg = "未知错误";
                            }

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



        private ICommand _baiDuSearch;


        public ICommand BaiDuSearch
        {
            get
            {
                if (_baiDuSearch == null)
                {
                    _baiDuSearch = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {


                        }
                        catch (Exception)
                        {

                        }
                        finally
                        {

                        }

                    });

                }
                return _baiDuSearch;
            }
        }


        #region 保存通过标签查询到的图片
        private ICommand _savaPic;

        public ICommand SavaPic
        {
            get
            {
                if (_savaPic == null)
                {
                    _savaPic = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {
                            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                            SavaPicRequest request = new SavaPicRequest();
                            //request.UserInfoID = SelectItem.ID;
                            request.IsEnable = 1;
                            request.TitleInfoID = SelectItem.ID;
                            //for (int i = 0; i < PictureControlViewModels.Count(); i++)
                            //{
                            //    if (PictureControlViewModels[i].IsCheck)
                            //    {

                            //    }
                            //    else
                            //    {

                            //    }
                            //}
                            var urlList = PictureControlViewModels.Where(c => c.IsCheck == true).Select(a => new { a.Url }).ToList();//把选中的url获取出来
                            var newList = new List<string>();
                            for (int i = 0; i < urlList.Count; i++)
                            {
                                newList.Add(urlList[i].Url);
                            }
                            request.BaiDuUrlList = newList;
                            var ret = titleInfo.UserBindBaiDuUrl(request);
                            if (ret == 1)
                            {
                                SavaPicMsg = "保存成功";
                                Task.Run(new Action(Init));
                                PageNo = 0;
                                PictureControlViewModels.Clear();
                            }
                            else if (ret == 3)
                            {
                                SavaPicMsg = "保存失败";
                            }
                            else if (ret == 101)
                            {
                                SavaPicMsg = "未知错误";
                            }
                            //request.BaiDuUrlList=
                            //request.PageIndex = 0;
                            //request.PageSize = 20;
                            //request.IsOccupy = 0;
                            //PageNo = request.PageIndex;
                            //request.UserNo = No;
                            //TitleInfoListResponse str = titleInfo.SelectUserTitleInfoList(request);
                            //if (str.RowCount > 0)
                            //{
                            //    PageMax = str.PageMax;
                            //    List<MainWindowModel> tList = str.mainWindowModels.ToList();
                            //    ObservableCollection<MainWindowModel> oc = new ObservableCollection<MainWindowModel>();
                            //    tList.ForEach(x => oc.Add(x));
                            //    Items = oc;
                            //}
                            //else
                            //{
                            //    return;
                            //}
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
                return _savaPic;
            }
        }
        #endregion



    }
}
