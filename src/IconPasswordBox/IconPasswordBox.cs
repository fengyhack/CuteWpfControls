using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CuteWpfControls
{
    public class IconPasswordBox : IconInputBoxBase
    {
        #region PrivateMembers

        private ToggleButton PART_RevealPassword;

        /// <summary>
        /// 该属性是为了防止明文转化为密文后，设置Text时，再次触发Text_Changed事件
        /// </summary>
        private bool isTextChangedHandled = true;

        private StringBuilder passwdStr;

        #endregion

        #region DependencyProperty

        #region CanRevealPassword

        [Bindable(true), Description("IsReveal")]
        public bool CanRevealPassword
        {
            get { return (bool)GetValue(CanRevealPasswordProperty); }
            set { SetValue(CanRevealPasswordProperty, value); }
        }

        public static readonly DependencyProperty CanRevealPasswordProperty =
            DependencyProperty.Register("CanRevealPassword", typeof(bool), typeof(IconPasswordBox), new PropertyMetadata(true, CanRevealPasswordChangedCallback));

        private static void CanRevealPasswordChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IconPasswordBox passowrdBox = d as IconPasswordBox;
            if (passowrdBox != null && passowrdBox.PART_RevealPassword != null)
            {
                passowrdBox.PART_RevealPassword.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        #region Password

        [Bindable(true), Description("CurrentPassword")]
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(IconPasswordBox), new PropertyMetadata(string.Empty));

        #endregion

        #region PasswordChar

        [Bindable(true), Description("MaskCharacter")]
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(IconPasswordBox), new PropertyMetadata('●'));

        #endregion

        #region IsPasswordVisible

        public bool IsPasswordVisible
        {
            get { return (bool)GetValue(IsPasswordVisibleProperty); }
            private set { SetValue(IsPasswordVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsPasswordVisibleProperty =
            DependencyProperty.Register("IsPasswordVisible", typeof(bool), typeof(IconPasswordBox), new PropertyMetadata(false, IsPasswordVisibleChanged));

        private static void IsPasswordVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IconPasswordBox passwordBox = d as IconPasswordBox;
            if (passwordBox != null)
            {
                passwordBox.SelectionStart = passwordBox.Text.Length + 1;
            }
        }

        #endregion

        #endregion

        #region Constructors

        static IconPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconPasswordBox), new FrameworkPropertyMetadata(typeof(IconPasswordBox)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_RevealPassword = GetTemplateChild("PART_RevealPassword") as ToggleButton;
            if (PART_RevealPassword != null)
            {
                PART_RevealPassword.Visibility = CanRevealPassword ? Visibility.Visible : Visibility.Collapsed;
            }
            SetEvent();

            //display
            SetText(ConvertToPasswordChar(Password.Length));

            //disable copy
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, CommandBinding_Executed, CommandBinding_CanExecute));
        }

        public override void OnCornerRadiusChanged(CornerRadius newValue)
        {
            IconCornerRadius = new CornerRadius(newValue.TopLeft, 0, 0, newValue.BottomLeft);
        }
        
        #endregion

        #region Private Functions

        private void SetEvent()
        {
            TextChanged += PasswordBoxEx_TextChanged;

            if (PART_RevealPassword != null)
            {
                PART_RevealPassword.Checked += (o, e) =>
                {
                    SetText(Password);
                    IsPasswordVisible = true;
                };

                PART_RevealPassword.Unchecked += (o, e) =>
                {
                    SetText(ConvertToPasswordChar(Password.Length));
                    IsPasswordVisible = false;
                };
            }
        }

        private void SetText(string str)
        {
            isTextChangedHandled = false;
            Text = str;
            isTextChangedHandled = true;
        }

        private string ConvertToPasswordChar(int length)
        {
            if (passwdStr != null)
            {
                passwdStr.Clear();
            }
            else
            {
                passwdStr = new StringBuilder();
            }

            for (var i = 0; i < length; ++i)
            {
                passwdStr.Append(PasswordChar);
            }

            return passwdStr.ToString();
        }

        #endregion

        #region EventHandlers

        private void PasswordBoxEx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isTextChangedHandled)
            {
                return;
            }

            foreach (TextChange c in e.Changes)
            {
                Password = Password.Remove(c.Offset, c.RemovedLength);
                Password = Password.Insert(c.Offset, Text.Substring(c.Offset, c.AddedLength));
            }

            if (!IsPasswordVisible)
            {
                SetText(ConvertToPasswordChar(Text.Length));
            }

            SelectionStart = Text.Length + 1;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #endregion
    }
}
