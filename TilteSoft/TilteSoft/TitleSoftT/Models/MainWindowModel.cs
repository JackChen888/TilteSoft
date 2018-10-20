using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleSoftT.Models
{
    public class MainWindowModel : ViewModelBase
    {
        private int _id;

        public int ID
        {
            get { return _id; }

            set { Set("ID", ref _id, value); }
        }

        private int _oldID;

        public int OldID
        {
            get { return _oldID; }
            set { Set("OldID", ref _oldID, value); }
        }

        private string _oldTitle;

        public string OldTitle
        {
            get { return _oldTitle; }
            set { Set("OldTitle", ref _oldTitle, value); }
        }

        private string _newTitle;

        public string NewTitle
        {
            get { return _newTitle; }
            set { Set("NewTitle", ref _newTitle, value); }
        }


        private string _upTime;

        public string UpTime
        {
            get { return _upTime; }
            set { Set("UpTime", ref _upTime, value); }
        }

        private int? _isHandle;

        public int? IsHandle
        {
            get { return _isHandle; }
            set {
                Set("IsHandle", ref _isHandle, value);
            }
        }

        private int? _isAdd;

        public int? IsAdd
        {
            get { return _isAdd; }
            set { Set("IsAdd", ref _isAdd, value); }
        }

        private string _modificationTime;

        public string ModificationTime
        {
            get { return _modificationTime; }
            set { Set("ModificationTime", ref _modificationTime, value); }
        }


    }
}
