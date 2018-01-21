using System;
using System.ServiceModel;

namespace Service
{
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

    public class Calc : ICalc
    {
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

        public decimal Divide(decimal a, decimal b)
        {
            return a/b;
        }

        public decimal Multiply(decimal a, decimal b)
        {
            return a * b;
        }

        public decimal Subtract(decimal a, decimal b)
        {
            return a / b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh = new ServiceHost(typeof(Calc));
            sh.Open();
            Console.WriteLine("Нажмите <ENTER> для завершения...");
            Console.ReadLine();
            sh.Close();
        }
    }
}
