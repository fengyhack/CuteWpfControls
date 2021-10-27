using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Reflection;

namespace CuteWpfControls
{
    public class FlatSilder : Slider
    {
        private Thumb PART_Thumb;
        private Track PART_Track;
        private ToolTip _AutoToolTip;
        private bool _IsPressed;

        public Brush DecreaseColor
        {
            get { return (Brush)GetValue(DecreaseColorProperty); }
            set { SetValue(DecreaseColorProperty, value); }
        }

        public static readonly DependencyProperty DecreaseColorProperty =
            DependencyProperty.Register("DecreaseColor", typeof(Brush), typeof(FlatSilder));

        public Brush IncreaseColor
        {
            get { return (Brush)GetValue(IncreaseColorProperty); }
            set { SetValue(IncreaseColorProperty, value); }
        }

        public static readonly DependencyProperty IncreaseColorProperty =
            DependencyProperty.Register("IncreaseColor", typeof(Brush), typeof(FlatSilder));

        public static readonly RoutedEvent DropValueChangedEvent = EventManager.RegisterRoutedEvent("DropValueChanged",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(FlatSilder));

        public event RoutedPropertyChangedEventHandler<double> DropValueChanged
        {
            add
            {
                AddHandler(DropValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(DropValueChangedEvent, value);
            }
        }

        public virtual void OnDropValueChanged(double oldValue, double newValue)
        {
            RoutedPropertyChangedEventArgs<double> args = new RoutedPropertyChangedEventArgs<double>(oldValue, newValue, DropValueChangedEvent);
            RaiseEvent(args);
        }

        static FlatSilder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatSilder), new FrameworkPropertyMetadata(typeof(FlatSilder)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Thumb = GetTemplateChild("PART_Thumb") as Thumb;
            PART_Track = GetTemplateChild("PART_Track") as Track;
            if (PART_Thumb != null)
            {
                PART_Thumb.PreviewMouseLeftButtonDown += PART_Thumb_PreviewMouseLeftButtonDown;
                PART_Thumb.PreviewMouseLeftButtonUp += PART_Thumb_PreviewMouseLeftButtonUp;
            }
            if (PART_Track != null)
            {
                PART_Track.MouseLeftButtonDown += PART_Track_MouseLeftButtonDown;
                PART_Track.MouseLeftButtonUp += PART_Track_MouseLeftButtonUp;
            }

            ValueChanged += FlatSilder_ValueChanged;
        }

        protected override void OnThumbDragStarted(DragStartedEventArgs e)
        {
            base.OnThumbDragStarted(e);
            if (_AutoToolTip == null)
            {
                FieldInfo field = typeof(Slider).GetField("_autoToolTip", BindingFlags.NonPublic | BindingFlags.Instance);
                _AutoToolTip = field.GetValue(this) as ToolTip;
                if (_AutoToolTip != null)
                {
                    _AutoToolTip.Background = Brushes.Transparent;
                    _AutoToolTip.BorderThickness = new Thickness(0);
                }
            }
        }

        private void PART_Track_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _IsPressed = true;
        }

        private void PART_Thumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _IsPressed = true;
        }

        private void PART_Thumb_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnDropValueChanged(Value, Value);
        }

        private void PART_Track_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnDropValueChanged(Value, Value);
        }

        private void FlatSilder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_IsPressed)
            {
                OnDropValueChanged(Value, Value);
            }
        }
    }
}
