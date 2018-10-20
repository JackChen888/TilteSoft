using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.Models;

namespace TitleSoftT.ViewModels
{
     public class PagingUserControlViewModel : ViewModelBase
    {

        public PagingUserControlViewModel()
        {

        }



        private ObservableCollection<MainWindowModel> _items;
        public ObservableCollection<MainWindowModel> Items
        {
            get { return _items; }
            set { Set("Items", ref _items, value); }
        }





    }
}
