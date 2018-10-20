using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;

namespace TitleSoftT.ViewModels
{
    public class AddTitleAndPicViewModel : ViewModelBase
    {
        public AddTitleAndPicViewModel()
        {
            Task.Run(new Action(Init));
            TitleID = AddTitlePageViewModel.Incccc.SelectItems.ID;
        }




        private int _titleID;

        public int TitleID
        {
            get { return _titleID; }
            set { Set("TitleID", ref _titleID, value); }
        }


        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { Set("UserName", ref _userName, value); }
        }


        private string _userNo;

        public string UserNo
        {
            get { return _userNo; }
            set { Set("UserNo", ref _userNo, value); }
        }


        private string _newTitle;

        public string NewTitle
        {
            get { return _newTitle; }
            set { Set("NewTitle", ref _newTitle, value); }
        }



        private string _oldTitle;

        public string OldTitle
        {
            get { return _oldTitle; }
            set { Set("OldTitle", ref _oldTitle, value); }
        }

        private string _upTime;

        public string UpTime
        {
            get { return _upTime; }
            set { Set("UpTime", ref _upTime, value); }
        }


        private string _savaPicMsg;

        public string SavaPicMsg
        {
            get { return _savaPicMsg; }
            set { Set("SavaPicMsg", ref _savaPicMsg, value); }
        }

        private ObservableCollection<PictureControlModel> _picItmes;

        public ObservableCollection<PictureControlModel> PicItmes
        {
            get { return _picItmes; }
            set { Set("PicItmes", ref _picItmes, value); }
        }


        //private ObservableCollection<MainWindowModel> _items;
        //public ObservableCollection<MainWindowModel> Items
        //{
        //    get { return _items; }
        //    set { Set("Items", ref _items, value); }
        //}




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
                    BaiDuInfoRequest request = new BaiDuInfoRequest();
                    request.TitleID = TitleID;
                    var str = titleInfo.SelectBaiDuInfoList(request);
                    UserName = str.UserName;
                    UserNo = str.UserNo;
                    NewTitle = str.NewTitle;
                    OldTitle = str.OldTitle;
                    UpTime = str.UpTime;
                    var tList = str.PicModelList;
                    ObservableCollection<PictureControlModel> oc = new ObservableCollection<PictureControlModel>();
                    tList.ForEach(x => oc.Add(x));
                    PicItmes = oc;

                    //PicItmes = str.PicModelList;
                    //if (str.RowCount > 0)
                    //{
                    //    PageMax = str.PageMax;
                    //    var tList = str.mainWindowModels.ToList();
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
        #endregion


        private ICommand _submitCmd;


        public ICommand SubmitCmd
        {
            get
            {
                if (_submitCmd == null)
                {
                    _submitCmd = new GalaSoft.MvvmLight.Command.RelayCommand<object>((obj) =>
                    {
                        try
                        {
                           
                            //if (!(obj is Window win))
                            //    return;
                            Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                            //TitleHandleRequest request = new TitleHandleRequest();
                            //request.ID = SelectItem.ID;
                            var ret = titleInfo.RelieveBinds(TitleID);
                            if (ret == 1)
                            {
                                //win.Close();
                                SavaPicMsg = "成功还原";
                                AddTitlePageViewModel.Incccc.Init();
                                //PageNo = 0;
                            }
                            else if (ret == 3)
                            {
                                SavaPicMsg = "失败还原";
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
                return _submitCmd;
            }
        }


        #region 命令

        private RelayCommand sendCommand;
        /// <summary>
        /// 发送命令
        /// </summary>
        public RelayCommand SendCommand
        {
            get
            {
                if (sendCommand == null)
                    sendCommand = new RelayCommand(() => ExcuteSendCommand());
                return sendCommand;

            }
            set { sendCommand = value; }
        }

        private void ExcuteSendCommand()
        {


            try
            {
                //if (!(obj is Window win))
                //    return;
                Entity.EFOperation.TitleInfo titleInfo = new Entity.EFOperation.TitleInfo();
                //TitleHandleRequest request = new TitleHandleRequest();
                //request.ID = SelectItem.ID;
                var ret = titleInfo.RelieveBinds(TitleID);
                if (ret == 1)
                {
                    //win.Close();
                    SavaPicMsg = "成功还原";
                    AddTitlePageViewModel.Incccc.Init();
                    //PageNo = 0;
                }
                else if (ret == 3)
                {
                    SavaPicMsg = "失败还原";
                }
                else if (ret == 101)
                {
                    SavaPicMsg = "未知错误";
                }
                Messenger.Default.Send<String>(ret.ToString(), "ViewAlert"); //注意：token参数一致
            }
            catch (Exception)
            {

            }
            finally
            {

            }

           
        }

        #endregion


    }
}
