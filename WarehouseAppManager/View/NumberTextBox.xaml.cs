using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WarehouseAppManager.View
{
    public partial class NumberTextBox : UserControl
    {
        public NumberTextBox()
        {
            InitializeComponent();
        }

        #region Zdarznie własne

        public static readonly RoutedEvent NumberChangedEvent =
        EventManager.RegisterRoutedEvent("TabItemSelected",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(NumberTextBox));

        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(NumberChangedEvent, value); }
            remove { RemoveHandler(NumberChangedEvent, value); }
        }

        void RaiseNunberChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(NumberTextBox.NumberChangedEvent);
            RaiseEvent(newEventArgs);
        }
        #endregion

        #region Własność zależna
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(NumberTextBox),
                new FrameworkPropertyMetadata(null)
            );
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region Metody obsługujące wewnętrzne zdarzenia kontrolki

        

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseNunberChanged();
        }
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string znak = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            string t = ((TextBox)sender).Text;

            if (t == "0" || t == "-0")
            {
                ((TextBox)sender).Text = e.Text;
                ((TextBox)sender).CaretIndex = ((TextBox)sender).Text.Length;
                e.Handled = true;
                return;
            }

            if (e.Text == znak)
            {
                e.Handled = true;
                return;
            }

            if (e.Text == "-")
            {
                e.Handled = true;
                return;
            }

            if (!(int.TryParse((e.Text == znak) ? t + e.Text + "0" : t + e.Text, out _)))
            {
                e.Handled = true;
                return;
            }
        }
        #endregion

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}