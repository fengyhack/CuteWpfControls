using System.Windows;
using System.Windows.Controls;

namespace CuteWpfControls
{
    public class InputBoxBase : TextBox
    {
        #region Watermark

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(InputBoxBase));

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(InputBoxBase), new PropertyMetadata(CornerRadiusChanged));

        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InputBoxBase textbox = d as InputBoxBase;
            if (textbox != null && e.NewValue != null)
            {
                textbox.OnCornerRadiusChanged((CornerRadius)e.NewValue);
            }
        }

        #endregion

        public virtual void OnCornerRadiusChanged(CornerRadius newValue)
        {
            // override this in derived class
        }
    }
}
