using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace CuteWpfControls
{
    // This 'ZoomBorder' is the cotent holder
    // Supporting: 'Pan(DragMove)' and 'Zoom(Scaling)'
    public class ZoomBorder : Border
    {
        private UIElement _Child = null;
        private Point _TransformOrigin;
        private Point _StartPosition;

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform).Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform).Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get
            {
                return base.Child;
            }
            set
            {
                if (value != null && value != Child)
                {
                    Initialize(value);
                }

                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            _Child = element;
            if (_Child != null)
            {
                var group = new TransformGroup();
                group.Children.Add(new ScaleTransform());
                group.Children.Add(new TranslateTransform());
                _Child.RenderTransform = group;
                _Child.RenderTransformOrigin = new Point(0.0, 0.0);

                MouseWheel += Child_MouseWheel;
                MouseLeftButtonDown += Child_MouseLeftButtonDown;
                MouseLeftButtonUp += Child_MouseLeftButtonUp;
                MouseMove += Child_MouseMove;
                PreviewMouseRightButtonUp += new MouseButtonEventHandler(Child_PreviewMouseRightButtonUp);
            }
        }

        /// <summary>
        /// Reset Zoom and Pan
        /// </summary>
        public void Reset()
        {
            if (_Child != null)
            {
                // reset zoom
                var st = GetScaleTransform(_Child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(_Child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        #region MouseActions

        private void Child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_Child != null)
            {
                var st = GetScaleTransform(_Child);

                double scale = st.ScaleX;

                if (e.Delta < 0) // scale down
                {
                    if (scale < 0.2)
                    {
                        return;
                    }
                    else if (scale <= 1.0)
                    {
                        scale -= 0.1;
                    }
                    else
                    {
                        scale -= 0.5;
                    }
                }
                else if (e.Delta > 0) // scale up
                {
                    if (scale >= 20.0)
                    {
                        return;
                    }
                    else if (scale >= 1.0)
                    {
                        scale += 0.5;
                    }
                    else
                    {
                        scale += 0.2;
                    }
                }

                Point relative = e.GetPosition(_Child);

                var tt = GetTranslateTransform(_Child);

                double abosuluteX = relative.X * st.ScaleX + tt.X;
                double abosuluteY = relative.Y * st.ScaleY + tt.Y;

                st.ScaleX = st.ScaleY = scale;

                tt.X = abosuluteX - relative.X * st.ScaleX;
                tt.Y = abosuluteY - relative.Y * st.ScaleY;
            }
        }

        private void Child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_Child != null)
            {
                var tt = GetTranslateTransform(_Child);
                _StartPosition = e.GetPosition(this);
                _TransformOrigin = new Point(tt.X, tt.Y);
                Cursor = Cursors.Hand;
                _Child.CaptureMouse();
            }
        }

        private void Child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_Child != null)
            {
                _Child.ReleaseMouseCapture();
                Cursor = Cursors.Arrow;
            }
        }

        void Child_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Reset();
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Child != null)
            {
                if (_Child.IsMouseCaptured)
                {
                    var tt = GetTranslateTransform(_Child);
                    Vector v = _StartPosition - e.GetPosition(this);
                    tt.X = _TransformOrigin.X - v.X;
                    tt.Y = _TransformOrigin.Y - v.Y;
                }
            }
        }

        #endregion MouseActions
    }
}
