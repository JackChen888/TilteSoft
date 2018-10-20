using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TitleSoftT.Models
{
    public class PictureControlModel : ViewModelBase
    {

        //public string TitleInfoID { get; set; }

        private int _titleInfoID;

        public int TitleInfoID
        {
            get { return _titleInfoID; }

            set { Set("TitleInfoID", ref _titleInfoID, value); }
        }


        private int _userInfoID;

        public int UserInfoID
        {
            get { return _userInfoID; }

            set { Set("UserInfoID", ref _userInfoID, value); }
        }


        private string _url;

        public string Url
        {
            get { return _url; }

            set { Set("Url", ref _url, value); }
        }

        private bool _isCheck;

        public bool IsCheck
        {
            get { return _isCheck; }

            set { Set("IsCheck", ref _isCheck, value); }
        }

        private string _titleName;

        public string TitleName
        {
            get { return _titleName; }

            set { Set("TitleName", ref _titleName, value); }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get { return _imageSource; }

            set { Set("ImageSource", ref _imageSource, value); }

        }

    }
}
