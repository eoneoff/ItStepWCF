using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace BankService
{
    [ServiceContract]
    interface IBankService
    {
        [OperationContract(IsOneWay = true)]
        void Create(int accountNo);

        [OperationContract (IsOneWay = true)]
        void Change(int accountNo, decimal sum);

        [OperationContract(IsOneWay = true)]
        void Delete(int accountNo);

        [OperationContract]
        decimal Balance(int accountNo);

        [OperationContract]
        IEnumerable<TransactionData> Movement(int accountNo);

        [OperationContract]
        bool AccountExists(int accountNo);
    }

    [DataContract]
    public struct TransactionData
    {
        [DataMember]
        public DateTime transDate;

        [DataMember]
        public decimal transAmount;
    }
}
