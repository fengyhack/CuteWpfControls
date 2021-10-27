using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CuteWpfControls
{
    [TemplatePart(Name = "PART_ClearText", Type = typeof(Path))]
    [TemplatePart(Name = "PART_ContentHost", Type = typeof(ScrollViewer))]
    public class TitleTextBox : TextBox
    {
        private Path PART_ClearText;
        private ScrollViewer PART_ScrollViewer;

        static TitleTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleTextBox), new FrameworkPropertyMetadata(typeof(TitleTextBox)));
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TitleTextBox));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty ShowTitleProperty = DependencyProperty.Register("ShowTitle", typeof(bool), typeof(TitleTextBox), new PropertyMetadata(true));

        public bool ShowTitle
        {
            get => (bool)GetValue(ShowTitleProperty);
            set => SetValue(ShowTitleProperty, value);
        }

        public static readonly DependencyProperty CanClearTextProperty = DependencyProperty.Register("CanClearText", typeof(bool), typeof(TitleTextBox));

        public bool CanClearText
        {
            get => (bool)GetValue(CanClearTextProperty);
            set => SetValue(CanClearTextProperty, value);
        }

        public static readonly DependencyProperty TitleOrientationProperty = DependencyProperty.Register("TitleOrientation", typeof(TitleOrientationEnum), typeof(TitleTextBox));

        public TitleOrientationEnum TitleOrientation
        {
            get => (TitleOrientationEnum)GetValue(TitleOrientationProperty);
            set => SetValue(TitleOrientationProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_ClearText = VisualHelper.FindVisualElement<Path>(this, "PART_ClearText");
            if (PART_ClearText != null)
            {
                PART_ClearText.MouseLeftButtonDown += PART_ClearText_MouseLeftButtonDown;
            }

            PART_ScrollViewer = VisualHelper.FindVisualElement<ScrollViewer>(this, "PART_ContentHost");

            PreviewMouseWheel += TitleTextBox_PreviewMouseWheel;
        }

        private void TitleTextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (TitleOrientation == TitleOrientationEnum.Vertical && PART_ScrollViewer != null)
            {
                PART_ScrollViewer.ScrollToVerticalOffset(PART_ScrollViewer.VerticalOffset - e.Delta);
            }
        }

        private void PART_ClearText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Text = string.Empty;
        }
    }
}

