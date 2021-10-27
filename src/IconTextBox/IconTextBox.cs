using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class IconTextBox: IconInputBoxBase
    {
        static IconTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconTextBox), new FrameworkPropertyMetadata(typeof(IconTextBox)));
        }

        #region Events

        public static readonly RoutedEvent EnterKeyClickEvent = EventManager.RegisterRoutedEvent("EnterKeyClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(IconTextBox));

        public event RoutedPropertyChangedEventHandler<object> EnterKeyClick
        {
            add
            {
                AddHandler(EnterKeyClickEvent, value);
            }
            remove
            {
                RemoveHandler(EnterKeyClickEvent, value);
            }
        }

        protected virtual void OnEnterKeyClick(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg =
                new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, EnterKeyClickEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region DependencyProperty

        #region Path

        public static readonly DependencyProperty IconPlacementProperty = 
            DependencyProperty.Register("IconPlacement", typeof(IconPlacementEnum), typeof(IconTextBox));

        public IconPlacementEnum IconPlacement
        {
            get { return (IconPlacementEnum)GetValue(IconPlacementProperty); }
            set { SetValue(IconPlacementProperty, value); }
        }

        public static readonly DependencyProperty IconColorProperty = 
            DependencyProperty.Register("IconColor", typeof(Brush), typeof(IconTextBox));

        public Brush IconColor
        {
            get { return (Brush)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        #endregion

        #endregion

        public IconTextBox() : base()
        {
            KeyUp += IconTextBox_KeyUp;
        }

        private void IconTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnEnterKeyClick(null, null);
            }
        }
    }
}
