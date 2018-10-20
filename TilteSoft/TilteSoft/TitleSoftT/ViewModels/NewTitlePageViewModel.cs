using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TitleSoftT.ServiceProxy;

namespace TitleSoftT.ViewModels
{

    public class NewTitlePageViewModel: ViewModelBase
    {

        public NewTitlePageViewModel()
        {
            No = MainWindowViewModel.Incccc.No;
            AddTitlePageViewModel = AddTitlePageViewModel.Incccc;
        }


        private static NewTitlePageViewModel inccc;
        public static NewTitlePageViewModel Incccc
        {
            get
            {
                if (inccc == null)
                    inccc = new NewTitlePageViewModel();
                return inccc;
            }
        }

        private AddTitlePageViewModel _addTitlePageViewModel;
        public AddTitlePageViewModel AddTitlePageViewModel
        {
            get { return _addTitlePageViewModel; }
            set { Set("AddTitlePageViewModel", ref _addTitlePageViewModel, value); }
        }


        private string _msg;
        public string Msg
        {
            get { return _msg; }
            set { Set("Msg", ref _msg, value); }
        }


        private string _no;
        public string No
        {
            get { return _no; }
            set { Set("No", ref _no, value); }
        }

        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set { Set("TitleName", ref _titleName, value); }
        }



    }
}
