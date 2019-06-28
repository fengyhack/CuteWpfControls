using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CuteWpfControls
{
    public class ImageButton : Button
    {
        #region DependencyProperty

        #region ImagePath

        [Bindable(true), Description("图片路径")]
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(ImageButton), new PropertyMetadata(""));

        #endregion ImagePath

        #endregion DependencyProperty

        #region Constructors

        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        #endregion Constructors
    }
}
