using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuteWpfControls
{
    public class IPAddressBox : Control
    {
        #region Private Members

        private TextBox PART_BOX1;
        private TextBox PART_BOX2;
        private TextBox PART_BOX3;
        private TextBox PART_BOX4;

        #endregion

        #region DependencyProperty

        #region IPAddrBoxType

        public IPAddrBoxTypeEnum IPAddrBoxType
        {
            get { return (IPAddrBoxTypeEnum)GetValue(IPAddrBoxTypeProperty); }
            set { SetValue(IPAddrBoxTypeProperty, value); }
        }

        public static readonly DependencyProperty IPAddrBoxTypeProperty =
            DependencyProperty.Register("IPAddrBoxType", typeof(IPAddrBoxTypeEnum), typeof(IPAddressBox), new PropertyMetadata(IPAddrBoxTypeEnum.IPAddress));

        #endregion

        #region HasError

        /// <summary>
        /// 获取或者设置是否输入了不合法的数字
        /// </summary>
        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            private set { SetValue(HasErrorProperty, value); }
        }

        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.Register("HasError", typeof(bool), typeof(IPAddressBox), new PropertyMetadata(false));

        #endregion

        #region ErrorContent

        /// <summary>
        /// 获取或者设置非法输入时的提示内容
        /// </summary>
        public string ErrorContent
        {
            get { return (string)GetValue(ErrorContentProperty); }
            private set { SetValue(ErrorContentProperty, value); }
        }

        public static readonly DependencyProperty ErrorContentProperty =
            DependencyProperty.Register("ErrorContent", typeof(string), typeof(IPAddressBox), new PropertyMetadata(string.Empty));

        #endregion

        #region IsKeyboardFocused

        public new bool IsKeyboardFocused
        {
            get { return (bool)GetValue(IsKeyboardFocusedProperty); }
            private set { SetValue(IsKeyboardFocusedProperty, value); }
        }

        public new static readonly DependencyProperty IsKeyboardFocusedProperty =
            DependencyProperty.Register("IsKeyboardFocused", typeof(bool), typeof(IPAddressBox), new PropertyMetadata(false));

        #endregion

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IPAddressBox), new PropertyMetadata(string.Empty));

        #endregion

        #endregion

        #region Constructors

        static IPAddressBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IPAddressBox), new FrameworkPropertyMetadata(typeof(IPAddressBox)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_BOX1 = GetTemplateChild("PART_BOX1") as TextBox;
            PART_BOX2 = GetTemplateChild("PART_BOX2") as TextBox;
            PART_BOX3 = GetTemplateChild("PART_BOX3") as TextBox;
            PART_BOX4 = GetTemplateChild("PART_BOX4") as TextBox;

            if (PART_BOX1 != null)
            {
                PART_BOX1.PreviewTextInput += PART_BOX1_PreviewTextInput;
                PART_BOX1.TextChanged += PART_BOX1_TextChanged;
                PART_BOX1.GotFocus += (o, e) => { IsKeyboardFocused = true; };
                PART_BOX1.LostFocus += (o, e) => { IsKeyboardFocused = false; };
                PART_BOX1.PreviewKeyDown += PART_BOX1_PreviewKeyDown;
            }

            if (PART_BOX2 != null)
            {
                PART_BOX2.PreviewTextInput += PART_BOX2_PreviewTextInput;
                PART_BOX2.TextChanged += PART_BOX2_TextChanged;
                PART_BOX2.GotFocus += (o, e) => { IsKeyboardFocused = true; };
                PART_BOX2.LostFocus += (o, e) => { IsKeyboardFocused = false; };
                PART_BOX2.PreviewKeyDown += PART_BOX1_PreviewKeyDown;
            }

            if (PART_BOX3 != null)
            {
                PART_BOX3.PreviewTextInput += PART_BOX3_PreviewTextInput;
                PART_BOX3.TextChanged += PART_BOX3_TextChanged;
                PART_BOX3.GotFocus += (o, e) => { IsKeyboardFocused = true; };
                PART_BOX3.LostFocus += (o, e) => { IsKeyboardFocused = false; };
                PART_BOX3.PreviewKeyDown += PART_BOX1_PreviewKeyDown;
            }

            if (PART_BOX4 != null)
            {
                PART_BOX4.PreviewTextInput += PART_BOX4_PreviewTextInput;
                PART_BOX4.GotFocus += (o, e) => { IsKeyboardFocused = true; };
                PART_BOX4.LostFocus += (o, e) => { IsKeyboardFocused = false; };
                PART_BOX4.PreviewKeyDown += PART_BOX1_PreviewKeyDown;
            }
        }

        void PART_BOX1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.V)
            {
                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(DataFormats.Text))
                {
                    string text = (string)data.GetData(DataFormats.UnicodeText);
                    Regex regex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
                    if (regex.IsMatch(text))
                    {
                        string[] strs = text.Split(new char[] { '.' });
                        //因为已经判断过是正确的IP地址，因此不用判断索引是否越界
                        PART_BOX1.Text = strs[0];
                        PART_BOX2.Text = strs[1];
                        PART_BOX3.Text = strs[2];
                        PART_BOX4.Text = strs[3];

                        PART_BOX1.Focus();
                        PART_BOX1.SelectionStart = 0;
                    }
                    else
                    {
                        HasError = true;
                        ErrorContent = "您正在尝试将格式错误的 IP 地址粘贴到该字段";
                        PART_BOX1.Text = string.Empty;
                        PART_BOX2.Text = string.Empty;
                        PART_BOX3.Text = string.Empty;
                        PART_BOX4.Text = string.Empty;
                    }
                }
                e.Handled = true;
            }
        }

        #endregion

        #region Private Functions

        private bool IsNumber(string input)
        {
            Regex regex = new Regex("^[0-9]*$");
            return regex.IsMatch(input);
        }

        private bool IsNumberInRange(int number, int start, int end)
        {
            return number >= start && number <= end;
        }

        /// <summary>
        /// 检查输入的文本是否符合IP地址格式
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        private bool CheckNumberIsLegal(string text1, string text2)
        {
            if (!IsNumber(text2)) return true;

            int text = Convert.ToInt32(text1);

            return !IsNumberInRange(text, 0, 255);
        }
        #endregion

        #region EventHandlers

        void PART_BOX1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = PART_BOX1.Text + e.Text;
            if (!string.IsNullOrWhiteSpace(input) && ".".Equals(e.Text))
            {
                e.Handled = true;
                PART_BOX2.Focus();
            }
            else
            {
                //输入的不是数字，则直接返回
                if (!IsNumber(e.Text))
                {
                    e.Handled = true;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(PART_BOX1.SelectedText))
                {
                    input = PART_BOX1.Text.Remove(PART_BOX1.SelectionStart, PART_BOX1.SelectionLength) + e.Text;
                }

                int text = Convert.ToInt32(input);
                switch (IPAddrBoxType)
                {
                    case IPAddrBoxTypeEnum.IPAddress:
                        if (!IsNumberInRange(text, 0, 223))
                        {
                            e.Handled = true;
                            HasError = true;
                            ErrorContent = string.Format("{0} 不是有效项。请指定一个介于 1 和 223 间的值", input);
                            PART_BOX1.Text = "223";
                        }
                        else
                        {
                            HasError = false;
                        }
                        break;
                    case IPAddrBoxTypeEnum.SubnetMask:
                        if (!IsNumberInRange(text, 0, 255))
                        {
                            e.Handled = true;
                            HasError = true;
                            ErrorContent = string.Format("{0} 不是有效项。请指定一个介于 0 和 255 间的值", input);
                            PART_BOX1.Text = "255";
                        }
                        else
                        {
                            HasError = false;
                        }
                        break;
                }
            }
        }

        void PART_BOX2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = PART_BOX2.Text + e.Text;

            if (!string.IsNullOrWhiteSpace(PART_BOX2.SelectedText))
            {
                input = PART_BOX2.Text.Remove(PART_BOX2.SelectionStart, PART_BOX2.SelectionLength) + e.Text;
            }

            //如果输入了.且该文本框不为空，则自动跳到下一个输入框中
            if (!string.IsNullOrWhiteSpace(input) && ".".Equals(e.Text))
            {
                e.Handled = true;
                PART_BOX3.Focus();
            }
            else
            {
                e.Handled = CheckNumberIsLegal(input, e.Text);
                //如果输入不合法，则默认为255
                if (e.Handled)
                {
                    PART_BOX2.Text = "255";
                    PART_BOX2.SelectionStart = PART_BOX2.Text.Length + 1;
                    HasError = true;
                    ErrorContent = string.Format("{0} 不是有效项。请指定一个介于 0 和 255 间的值", input);
                }
                else
                {
                    HasError = false;
                }
            }
        }

        void PART_BOX3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = PART_BOX3.Text + e.Text;

            if (!string.IsNullOrWhiteSpace(PART_BOX3.SelectedText))
            {
                input = PART_BOX3.Text.Remove(PART_BOX3.SelectionStart, PART_BOX3.SelectionLength) + e.Text;
            }

            if (!string.IsNullOrWhiteSpace(input) && ".".Equals(e.Text))
            {
                e.Handled = true;
                PART_BOX4.Focus();
            }
            else
            {
                e.Handled = CheckNumberIsLegal(input, e.Text);
                if (e.Handled)
                {
                    PART_BOX3.Text = "255";
                    PART_BOX3.SelectionStart = PART_BOX3.Text.Length + 1;
                    HasError = true;
                    ErrorContent = string.Format("{0} 不是有效项。请指定一个介于 0 和 255 间的值", input);
                }
                else
                {
                    HasError = false;
                }
            }
        }

        void PART_BOX4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = PART_BOX4.Text + e.Text;

            if (!string.IsNullOrWhiteSpace(PART_BOX4.SelectedText))
            {
                input = PART_BOX4.Text.Remove(PART_BOX4.SelectionStart, PART_BOX4.SelectionLength) + e.Text;
            }

            e.Handled = CheckNumberIsLegal(input, e.Text);
            if (e.Handled)
            {
                PART_BOX4.Text = "255";
                PART_BOX4.SelectionStart = PART_BOX4.Text.Length + 1;
                HasError = true;
                ErrorContent = string.Format("{0} 不是有效项。请指定一个介于 0 和 255 间的值", input);
            }
            else
            {
                HasError = false;
            }
        }

        void PART_BOX1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PART_BOX1.Text.Length == 3)
            {
                int number = 1;
                if (Int32.TryParse(PART_BOX1.Text, out number))
                {
                    switch (IPAddrBoxType)
                    {
                        case IPAddrBoxTypeEnum.IPAddress:
                            if (number < 1)
                            {
                                PART_BOX1.Text = "1";
                            }
                            break;
                        case IPAddrBoxTypeEnum.SubnetMask:
                            break;
                    }
                }
                PART_BOX2.Focus();
            }
        }

        void PART_BOX2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PART_BOX2.Text.Length == 3)
            {
                PART_BOX3.Focus();
            }
        }

        void PART_BOX3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PART_BOX3.Text.Length == 3)
            {
                PART_BOX4.Focus();
            }
        }
        
        #endregion
    }

}
