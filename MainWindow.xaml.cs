using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace wpf_simpleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        OperationType selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
            signButton.Click += SignButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
            periodButton.Click += PeriodButton_Click;
        }

        private void PeriodButton_Click(object sender, RoutedEventArgs e)
        {
            if(!resultLabel.Content.ToString().Contains(".") )
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber = 0;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
               try
                {
                    switch (this.selectedOperator)
                    {
                        case OperationType.Addition:
                            result = SimpleMath.Add(this.lastNumber, newNumber);
                            break;
                        case OperationType.Substraction:
                            result = SimpleMath.Substract(this.lastNumber, newNumber);
                            break;
                        case OperationType.Multiplication:
                            result = SimpleMath.Multiply(this.lastNumber, newNumber);
                            break;
                        case OperationType.Division:
                            result = SimpleMath.Divide(this.lastNumber, newNumber);
                            break;

                    }
                    resultLabel.Content = result.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
              
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber / 100;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultLabel.Content = "0";
        }

        
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }
            if (sender == multiplyButton)
            {
                this.selectedOperator = OperationType.Multiplication;
            }
            if (sender == divideButton)
            {
                this.selectedOperator = OperationType.Division;
            }
            if (sender == addButton)
            {
                this.selectedOperator = OperationType.Addition;
            }
            if (sender == substractButton)
            {
                this.selectedOperator = OperationType.Substraction;
            }
        }


        private void Number_Click(object sender, RoutedEventArgs e)
        {
            var  content = (sender as Button).Content.ToString(); // Because the content defines the number
            if(resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = content;
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{content}";
            }
        }
    }

    public enum OperationType
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add (double n1, double n2)
        {
            return n1 + n2;
        }
        public static double Substract(double n1, double n2)
        {
            return n1 - n2;
        }
        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }
        public static double Divide(double n1, double n2)
        {
            if( n2 == 0)
            {
                throw new Exception("Division by Zero is not valid unless you are in other universe :D");
            }
            return n1 / n2;
        }

    }
}
