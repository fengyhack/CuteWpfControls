using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CuteWpfControls
{
    public class Gauge : UserControl
    {
        #region PrivateMembers

        private Canvas Canvas { get; set; }
        private PieSlice PieBg { get; set; }
        private PieSlice PieFg { get; set; }
        private TextBlock MeasureTextBlock { get; set; }
        private TextBlock LeftLabel { get; set; }
        private TextBlock RightLabel { get; set; }
        private bool IsNew { get; set; }
        private bool IsChartInitialized { get; set; }

        #endregion PrivateMembers

        #region DependencyProperty

        /// <summary>
        /// The gauge active fill property
        /// </summary>
        public static readonly DependencyProperty GaugeActiveFillProperty = 
            DependencyProperty.Register("GaugeActiveFill", typeof(Brush), typeof(Gauge), new PropertyMetadata(default(Brush)));
        
        /// <summary>
        /// Gets or sets the gauge active fill, if this property is set, From/to color properties interpolation will be ignored
        /// </summary>
        /// <value>
        /// The gauge active fill.
        /// </value>
        public Brush GaugeActiveFill
        {
            get { return (Brush)GetValue(GaugeActiveFillProperty); }
            set { SetValue(GaugeActiveFillProperty, value); }
        }

        /// <summary>
        /// The labels visibility property
        /// </summary>
        public static readonly DependencyProperty LabelsVisibilityProperty = 
            DependencyProperty.Register("LabelsVisibility", typeof(Visibility), typeof(Gauge), new PropertyMetadata(default(Visibility)));
        
        /// <summary>
        /// Gets or sets the labels visibility.
        /// </summary>
        /// <value>
        /// The labels visibility.
        /// </value>
        public Visibility LabelsVisibility
        {
            get { return (Visibility)GetValue(LabelsVisibilityProperty); }
            set { SetValue(LabelsVisibilityProperty, value); }
        }

        /// <summary>
        /// The gauge render transform property
        /// </summary>
        public static readonly DependencyProperty GaugeRenderTransformProperty = 
            DependencyProperty.Register("GaugeRenderTransform", typeof(Transform), typeof(Gauge), new PropertyMetadata(default(Transform)));
        
        /// <summary>
        /// Gets or sets the gauge render transform.
        /// </summary>
        /// <value>
        /// The gauge render transform.
        /// </value>
        public Transform GaugeRenderTransform
        {
            get { return (Transform)GetValue(GaugeRenderTransformProperty); }
            set { SetValue(GaugeRenderTransformProperty, value); }
        }

        /// <summary>
        /// The uses360 mode property
        /// </summary>
        public static readonly DependencyProperty Uses360ModeProperty = 
            DependencyProperty.Register("Uses360Mode", typeof(bool), typeof(Gauge), new PropertyMetadata(default(bool), UpdateCallback));
        
        /// <summary>
        /// Gets or sets whether the gauge uses 360 mode, 360 mode will plot a full circle instead of a semi circle
        /// </summary>
        public bool Uses360Mode
        {
            get { return (bool)GetValue(Uses360ModeProperty); }
            set { SetValue(Uses360ModeProperty, value); }
        }

        /// <summary>
        /// From property
        /// </summary>
        public static readonly DependencyProperty FromProperty = 
            DependencyProperty.Register("From", typeof(double), typeof(Gauge), new PropertyMetadata(0d, UpdateCallback));
        
        /// <summary>
        /// Gets or sets the value where the gauge starts
        /// </summary>
        public double From
        {
            get { return (double)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        /// <summary>
        /// To property
        /// </summary>
        public static readonly DependencyProperty ToProperty = 
            DependencyProperty.Register("To", typeof(double), typeof(Gauge), new PropertyMetadata(1d, UpdateCallback));
        
        /// <summary>
        /// Gets or sets the value where the gauge ends
        /// </summary>
        public double To
        {
            get { return (double)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        /// <summary>
        /// The value property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register("Value", typeof(double), typeof(Gauge), new PropertyMetadata(default(double), UpdateCallback));
       
        /// <summary>
        /// Gets or sets the current value of the gauge
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// The inner radius property
        /// </summary>
        public static readonly DependencyProperty InnerRadiusProperty = 
            DependencyProperty.Register("InnerRadius", typeof(double?), typeof(Gauge), new PropertyMetadata(null, UpdateCallback));
        
        /// <summary>
        /// Gets o sets inner radius
        /// </summary>
        public double? InnerRadius
        {
            get { return (double?)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        /// <summary>
        /// The stroke property
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = 
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(Gauge), new PropertyMetadata(default(Brush)));
        
        /// <summary>
        /// Gets or sets stroke, the stroke is the brush used to draw the gauge border.
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// The stroke thickness property
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = 
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Gauge), new PropertyMetadata(default(double)));
        
        /// <summary>
        /// Gets or sets stroke brush thickness
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        /// <summary>
        /// To color property
        /// </summary>
        public static readonly DependencyProperty ToColorProperty = 
            DependencyProperty.Register("ToColor", typeof(Color), typeof(Gauge), new PropertyMetadata(default(Color), UpdateCallback));
       
        /// <summary>
        /// Gets or sets the color when the current value equals to min value, any value between min and max will use an interpolated color.
        /// </summary>
        public Color ToColor
        {
            get { return (Color)GetValue(ToColorProperty); }
            set { SetValue(ToColorProperty, value); }
        }

        /// <summary>
        /// From color property
        /// </summary>
        public static readonly DependencyProperty FromColorProperty = 
            DependencyProperty.Register("FromColor", typeof(Color), typeof(Gauge), new PropertyMetadata(default(Color), UpdateCallback));
        
        /// <summary>
        /// Gets or sets the color when the current value equals to max value, any value between min and max will use an interpolated color.
        /// </summary>
        public Color FromColor
        {
            get { return (Color)GetValue(FromColorProperty); }
            set { SetValue(FromColorProperty, value); }
        }

        /// <summary>
        /// The gauge background property
        /// </summary>
        public static readonly DependencyProperty GaugeBackgroundProperty = 
            DependencyProperty.Register("GaugeBackground", typeof(Brush), typeof(Gauge), new PropertyMetadata(default(Brush)));
       
        /// <summary>
        /// Gets or sets the gauge background
        /// </summary>
        public Brush GaugeBackground
        {
            get { return (Brush)GetValue(GaugeBackgroundProperty); }
            set { SetValue(GaugeBackgroundProperty, value); }
        }

        /// <summary>
        /// The animations speed property
        /// </summary>
        public static readonly DependencyProperty AnimationsSpeedProperty = 
            DependencyProperty.Register("AnimationsSpeed", typeof(TimeSpan), typeof(Gauge), new PropertyMetadata(default(TimeSpan)));
        
        /// <summary>
        /// G3ts or sets the gauge animations speed
        /// </summary>
        public TimeSpan AnimationsSpeed
        {
            get { return (TimeSpan)GetValue(AnimationsSpeedProperty); }
            set { SetValue(AnimationsSpeedProperty, value); }
        }

        /// <summary>
        /// The disablea animations property
        /// </summary>
        public static readonly DependencyProperty DisableAnimationsProperty = 
            DependencyProperty.Register("DisableAnimations", typeof(bool), typeof(Gauge), new PropertyMetadata(default(bool)));
        
        /// <summary>
        /// Gets or sets whether the chart is animated
        /// </summary>
        public bool DisableAnimations
        {
            get { return (bool)GetValue(DisableAnimationsProperty); }
            set { SetValue(DisableAnimationsProperty, value); }
        }

        /// <summary>
        /// The label formatter property
        /// </summary>
        public static readonly DependencyProperty LabelFormatterProperty = 
            DependencyProperty.Register("LabelFormatter", typeof(Func<double, string>), typeof(Gauge), new PropertyMetadata(default(Func<double, string>)));
        
        /// <summary>
        /// Gets or sets the label formatter, a label formatter takes a double value, and return a string, e.g. val => val.ToString("C");
        /// </summary>
        public Func<double, string> LabelFormatter
        {
            get { return (Func<double, string>)GetValue(LabelFormatterProperty); }
            set { SetValue(LabelFormatterProperty, value); }
        }

        /// <summary>
        /// The high font size property
        /// </summary>
        public static readonly DependencyProperty HighFontSizeProperty = 
            DependencyProperty.Register("HighFontSize", typeof(double?), typeof(Gauge), new PropertyMetadata(null));
        
        /// <summary>
        /// Gets o sets the label size, if this value is null then it will be automatically calculated, default is null.
        /// </summary>
        public double? HighFontSize
        {
            get { return (double?)GetValue(HighFontSizeProperty); }
            set { SetValue(HighFontSizeProperty, value); }
        }

        #endregion DependencyProperty

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Gauge"/> class.
        /// </summary>
        public Gauge()
        {
            Canvas = new Canvas();
            Content = Canvas;

            PieBg = new PieSlice();
            PieFg = new PieSlice();
            MeasureTextBlock = new TextBlock();
            LeftLabel = new TextBlock();
            RightLabel = new TextBlock();

            Canvas.Children.Add(PieBg);
            Canvas.Children.Add(PieFg);
            Canvas.Children.Add(MeasureTextBlock);
            Canvas.Children.Add(RightLabel);
            Canvas.Children.Add(LeftLabel);

            Panel.SetZIndex(MeasureTextBlock, 1);
            Panel.SetZIndex(RightLabel, 1);
            Panel.SetZIndex(LeftLabel, 1);

            Panel.SetZIndex(PieBg, 0);
            Panel.SetZIndex(PieFg, 1);

            Canvas.SetBinding(WidthProperty,new Binding { Path = new PropertyPath(WidthProperty), Source = this });
            Canvas.SetBinding(HeightProperty,new Binding { Path = new PropertyPath(HeightProperty), Source = this });

            PieBg.SetBinding(Shape.FillProperty, new Binding { Path = new PropertyPath(GaugeBackgroundProperty), Source = this });
            PieBg.SetBinding(Shape.StrokeThicknessProperty, new Binding { Path = new PropertyPath(StrokeThicknessProperty), Source = this });
            PieBg.SetBinding(Shape.StrokeProperty, new Binding { Path = new PropertyPath(StrokeProperty), Source = this });
            PieBg.SetBinding(RenderTransformProperty, new Binding { Path = new PropertyPath(GaugeRenderTransformProperty), Source = this });

            PieFg.SetBinding(Shape.StrokeThicknessProperty, new Binding { Path = new PropertyPath(StrokeThicknessProperty), Source = this });
            PieFg.SetBinding(RenderTransformProperty, new Binding { Path = new PropertyPath(GaugeRenderTransformProperty), Source = this });
            PieFg.Stroke = Brushes.Transparent;

            SetCurrentValue(GaugeBackgroundProperty, new SolidColorBrush(Color.FromRgb(21, 101, 191)) { Opacity = .1 });
            SetCurrentValue(StrokeThicknessProperty, 0d);
            SetCurrentValue(StrokeProperty, new SolidColorBrush(Color.FromRgb(222, 222, 222)));

            SetCurrentValue(FromColorProperty, Color.FromRgb(100, 180, 245));
            SetCurrentValue(ToColorProperty, Color.FromRgb(21, 101, 191));

            SetCurrentValue(MinHeightProperty, 20d);
            SetCurrentValue(MinWidthProperty, 20d);

            SetCurrentValue(AnimationsSpeedProperty, TimeSpan.FromMilliseconds(800));

            MeasureTextBlock.FontWeight = FontWeights.Bold;

            IsNew = true;

            SizeChanged += (o, e) =>
            {
                IsChartInitialized = true;
                Update();
            };
        }

        #endregion Constructors

        #region PrivateFunctions

        private static void UpdateCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var gauge = (Gauge)dependencyObject;

            gauge.Update();
        }

        private void Update()
        {
            if (!IsChartInitialized) return;

            Func<double, string> defFormatter = x => x.ToString(CultureInfo.InvariantCulture);

            var completed = (Value - From) / (To - From);

            var t = 0d;

            if (double.IsNaN(completed) || double.IsInfinity(completed))
            {
                completed = 0;
            }

            completed = completed > 1 ? 1 : (completed < 0 ? 0 : completed);
            var angle = Uses360Mode ? 360 : 180;

            if (!Uses360Mode)
            {
                LeftLabel.Text = (LabelFormatter ?? defFormatter)(From);
                RightLabel.Text = (LabelFormatter ?? defFormatter)(To);

                LeftLabel.UpdateLayout();
                RightLabel.UpdateLayout();

                LeftLabel.Visibility = LabelsVisibility;
                RightLabel.Visibility = LabelsVisibility;

                t = LeftLabel.ActualHeight;
            }
            else
            {
                LeftLabel.Visibility = Visibility.Hidden;
                RightLabel.Visibility = Visibility.Hidden;
            }

            double r, top;

            if (Uses360Mode)
            {
                r = ActualWidth > ActualHeight ? ActualHeight : ActualWidth;
                r = r / 2 - 2 * t;
                top = ActualHeight / 2;
            }
            else
            {
                r = ActualWidth;

                if (ActualWidth > ActualHeight * 2)
                {
                    r = ActualHeight * 2;
                }
                else
                {
                    t = 0;
                }

                r = r / 2 - 2 * t;

                top = ActualHeight / 2 + r / 2;
            }

            if (r < 0) r = 1;

            PieBg.Radius = r;
            PieBg.InnerRadius = InnerRadius ?? r * .6;
            PieBg.RotationAngle = 270;
            PieBg.WedgeAngle = angle;

            PieFg.Radius = PieBg.Radius;
            PieFg.InnerRadius = PieBg.InnerRadius;
            PieFg.RotationAngle = PieBg.RotationAngle;

            Canvas.SetLeft(PieBg, ActualWidth / 2);
            Canvas.SetTop(PieBg, top);
            Canvas.SetLeft(PieFg, ActualWidth / 2);
            Canvas.SetTop(PieFg, top);

            Canvas.SetTop(LeftLabel, top);
            Canvas.SetTop(RightLabel, top);
            Canvas.SetRight(LeftLabel, ActualWidth / 2 + (r + PieBg.InnerRadius) / 2 - LeftLabel.ActualWidth / 2);
            Canvas.SetRight(RightLabel, ActualWidth / 2 - (r + PieBg.InnerRadius) / 2 - RightLabel.ActualWidth / 2);

            MeasureTextBlock.FontSize = HighFontSize ?? PieFg.InnerRadius * .4;
            MeasureTextBlock.Text = (LabelFormatter ?? defFormatter)(Value);
            MeasureTextBlock.UpdateLayout();
            Canvas.SetTop(MeasureTextBlock, top - MeasureTextBlock.ActualHeight * (Uses360Mode ? .5 : 1));
            Canvas.SetLeft(MeasureTextBlock, ActualWidth / 2 - MeasureTextBlock.ActualWidth / 2);

            var interpolatedColor = new Color
            {
                R = LinearInterpolation(FromColor.R, ToColor.R),
                G = LinearInterpolation(FromColor.G, ToColor.G),
                B = LinearInterpolation(FromColor.B, ToColor.B),
                A = LinearInterpolation(FromColor.A, ToColor.A)
            };

            if (IsNew)
            {
                PieFg.Fill = new SolidColorBrush(FromColor);
                PieFg.WedgeAngle = 0;
            }

            if (DisableAnimations)
            {
                PieFg.WedgeAngle = completed * angle;
            }
            else
            {
                PieFg.BeginAnimation(PieSlice.WedgeAngleProperty, new DoubleAnimation(completed * angle, AnimationsSpeed));
            }

            if (GaugeActiveFill == null)
            {
                ((SolidColorBrush)PieFg.Fill).BeginAnimation(SolidColorBrush.ColorProperty,
                    new ColorAnimation(interpolatedColor, AnimationsSpeed));
            }
            else
            {
                PieFg.Fill = GaugeActiveFill;
            }

            IsNew = false;
        }

        private byte LinearInterpolation(double from, double to)
        {
            var p1 = new Point(From, from);
            var p2 = new Point(To, to);

            var deltaX = p2.X - p1.X;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            var m = (p2.Y - p1.Y) / (deltaX == 0 ? double.MinValue : deltaX);

            var v = Value > To ? To : (Value < From ? From : Value);

            return (byte)(m * (v - p1.X) + p1.Y);
        }

        #endregion PrivateFunctions
    }
}
