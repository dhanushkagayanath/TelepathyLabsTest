using Models;
using System;
using System.Collections.Generic;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string input = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "0";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "1";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "2";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "3";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "4";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "5";
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "6";
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "7";
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "8";
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "9";
        }

        private void Button_Click_CAL(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "";

            Processor p = new Processor();
            try
            {
                CalResult result = p.ProcessString(ResultLabel.Text.ToString());

                if (result == null) {
                    ResultText.Text = "Error ocurred while processing";
                    return;
                }

                if (result.Errors != null)
                {
                    ResultText.Text = "Invalid String" + Environment.NewLine + "----------------" + Environment.NewLine + result.Errors.ToString();
                    return;
                }

                ResultText.Text = result.Result.ToString()+ Environment.NewLine + "----------------" + Environment.NewLine + result.BTreeString;

            }
            catch
            {
                ResultText.Text = "Error ocurred while processing";
            }
           
        }

        private void Button_Click_ADD(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "+";
        }

        private void Button_Click_P2(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += ")";
        }

        private void Button_Click_P1(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "(";
        }

        private void Button_Click_MIN(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "-";
        }

        private void Button_Click_MUL(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "x";
        }

        private void Button_Click_D(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text += "÷";
        }

        private void Button_Click_RESET(object sender, RoutedEventArgs e)
        {
            ResultLabel.Text = "";
            ResultText.Text = "";
        }

        private void Button_Click_BACK(object sender, RoutedEventArgs e)
        {
            string Text = ResultLabel.Text.ToString();
            if (Text != null && Text.Length > 0)
            {
                ResultLabel.Text = Text.Remove(Text.Length - 1);
            }

        }
    }
}
