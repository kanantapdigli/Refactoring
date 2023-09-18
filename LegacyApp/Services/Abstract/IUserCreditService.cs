using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Services.Abstract
{
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName = "LegacyApp.IUserCreditService")]
    public interface IUserCreditService
    {
        [OperationContract(Action = "http://totally-real-service.com/IUserCreditService/GetCreditLimit")]
        decimal GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
    }
}
