using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.Models;

namespace TitleSoftT.ViewModels
{
    public class PictureControlViewModel : ViewModelBase
    {

        public PictureControlViewModel()
        {

        }



        private PictureControlModel _selectItem;
        public PictureControlModel SelectItem
        {
            get { return _selectItem; }
            set { Set("SelectItem", ref _selectItem, value); }
        }

        


    }
}
