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
using System.ServiceModel;

namespace Calculator
{
    enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        None
    }


    [ServiceContract]
    public interface ICalc
    {
        [OperationContract]
        decimal Add(decimal a, decimal b);

        [OperationContract]
        decimal Subtract(decimal a, decimal b);

        [OperationContract]
        decimal Multiply(decimal a, decimal b);

        [OperationContract]
        decimal Divide(decimal a, decimal b);
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal memory=0;
        private decimal operand;
        Operation operation = Operation.None;
        bool newOper = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumButton(int number)
        {
            Error.Text = String.Empty;
            if (newOper == true)
            {
                Screen.Text = "0";
                newOper = false;
            }
            if (Screen.Text.Length < 16)
            {
                if (Screen.Text == "0")
                    Screen.Text = String.Empty;
                if (Screen.Text == "-0")
                    Screen.Text = "-";

                Screen.Text = Screen.Text + number.ToString();
            }
        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            if (Screen.Text!="0")
                NumButton(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NumButton(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NumButton(2);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NumButton(3);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NumButton(4);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            NumButton(5);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            NumButton(6);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            NumButton(7);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            NumButton(8);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            NumButton(9);
        }

        private void Button_Click_Sign(object sender, RoutedEventArgs e)
        {
            Error.Text = String.Empty;
            if (newOper)
                newOper = false;
            if (Screen.Text[0] == '-')
                Screen.Text = Screen.Text.Substring(1);
            else
                Screen.Text = "-" + Screen.Text;
        }

        private void Button_Click_Dot(object sender, RoutedEventArgs e)
        {
            Error.Text = String.Empty;
            if (newOper==true)
            {
                Screen.Text = "0.";
                newOper = false;
            }
            if (!Screen.Text.Contains("."))
                Screen.Text = Screen.Text + ",";
        }

        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            if (newOper)
                newOper = false;
            if (Screen.Text.Length > 1 && Screen.Text!="-0")
                Screen.Text = Screen.Text.Remove(Screen.Text.Length - 1);
            else
                Screen.Text = "0";
            Error.Text = String.Empty;
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            Error.Text = String.Empty;
            operation = Operation.None;
            newOper = false;
            Screen.Text = "0";
            operand = 0;
        }

        private void Button_Click_Equals(object sender, RoutedEventArgs e)
        {
            using (ChannelFactory<ICalc> factory = new ChannelFactory<ICalc>("CalcConfig"))
            {
                ICalc channel = factory.CreateChannel();
                if (operation != Operation.None)
                {
                    decimal operand2 = Decimal.Parse(Screen.Text);
                    decimal result = 0;
                    switch (operation)
                    {
                        case Operation.Add:
                            result = channel.Add(operand,operand2);
                            break;
                        case Operation.Subtract:
                            result = channel.Subtract(operand, operand2);
                            break;
                        case Operation.Multiply:
                            result = channel.Multiply(operand, operand2);
                            break;
                        case Operation.Divide:
                            if (operand2 == 0)
                            {
                                Error.Text = "E";
                                MessageBox.Show("Нельзя делить на ноль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                Screen.Text = operand.ToString();
                                return;
                            }
                            result = channel.Divide(operand, operand2);
                            break;
                    }


                    int resultExp = Decimal.Round(result).ToString().Length;
                    if (resultExp > 25)
                    {
                        Error.Text = "E";
                        MessageBox.Show("Переполнение результата", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        Screen.Text = operand.ToString();
                    }
                    else
                    {
                        Error.Text = String.Empty;
                        newOper = true;
                        if (result != Decimal.Round(result))
                        {
                            int roundingFactor = 25 - resultExp;
                            result = Decimal.Round(result, roundingFactor);
                        }
                        Screen.Text = result.ToString();
                        operand = result;
                    }
                } 
            }
        }

        private void Operate (Operation operation)
        {
            Error.Text = String.Empty;
            operand = Decimal.Parse(Screen.Text);
            this.operation = operation;
            newOper = true;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Operate(Operation.Add);
        }

        private void Button_Click_Minus(object sender, RoutedEventArgs e)
        {
            Operate(Operation.Subtract);
        }

        private void Button_Click_Multiply(object sender, RoutedEventArgs e)
        {
            Operate(Operation.Multiply);
        }

        private void Button_Click_Divide(object sender, RoutedEventArgs e)
        {
            Operate(Operation.Divide);
        }

        private void Button_Click_MemAdd(object sender, RoutedEventArgs e)
        {
            memory = memory + Decimal.Parse(Screen.Text);
            Memory.Text = "M";
        }

        private void Button_Click_Mem(object sender, RoutedEventArgs e)
        {
            Screen.Text = memory.ToString();
        }

        private void Button_Click_MemClear(object sender, RoutedEventArgs e)
        {
            memory = 0;
            Memory.Text = String.Empty;
        }
    }
}
