using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TitleSoftT.View;

namespace TitleSoftT.ViewModels
{
    public class ImageItemViewModel : PropertyNotifyObject
    {
        public static event EventHandler ShowSelectImgCount;
        public ImageItemViewModel()
        {

        }
        #region 命令
        /// <summary>
        /// 图片点击事件
        /// </summary>
        private ICommand _StateImageMouseLeftButtonDown;
        public ICommand StateImageMouseLeftButtonDown
        {
            get
            {
                if (this._StateImageMouseLeftButtonDown == null)
                {
                    this._StateImageMouseLeftButtonDown = new DefaultCommand(o =>
                    {
                        var img = o as System.Windows.Controls.Image;
                        if (img.Source == null)
                        {
                            imgCheckUrl = "pack://application:,,,/TitleSoftT;Component/Images/check.png";
                            isImgCheck = Visibility.Visible;
                        }
                        else
                        {
                            imgCheckUrl = null;
                            isImgCheck = Visibility.Collapsed;

                        }
                        List<ImageItemView> imageSelect = imageCount.imgItem.Where(m => m.imgCheck.Source != null).ToList();
                        ShowSelectImgCount(imageSelect, null);
                    });
                }
                return this._StateImageMouseLeftButtonDown;
            }
        }

        #endregion
        #region 属性
        /// <summary>
        /// 选中是否显示
        /// </summary>
        private Visibility _isImgCheck = Visibility.Hidden;
        public Visibility isImgCheck
        {
            get { return this._isImgCheck; }
            set
            {
                this._isImgCheck = value;
                RaisePropertyChanged(() => isImgCheck);
            }
        }
        private string _imgCheckUrl = "";
        public string imgCheckUrl
        {
            get { return this._imgCheckUrl; }
            set
            {
                this._imgCheckUrl = value;
                RaisePropertyChanged(() => imgCheckUrl);
            }
        }
        #endregion
    }
}


