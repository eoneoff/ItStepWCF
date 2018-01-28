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
        [OperationContract]
        void Create(int accountNo);

        [OperationContract]
        void Change(int accountNo, decimal sum);

        [OperationContract]
        void Delete(int accountNo);

        [OperationContract]
        decimal Balance(int accountNo);

        [OperationContract]
        IEnumerable<TransactionData> Movement(int accountNo);
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
