using LegacyApp.Services.Abstract;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace LegacyApp
{
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IUserCreditServiceChannel : IUserCreditService, IClientChannel
    {
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class UserCreditServiceClient : ClientBase<IUserCreditService>, IUserCreditService
    {
        private IUserCreditServiceChannel _userCreditServiceChannelImplementation;
        public UserCreditServiceClient() { }

        public UserCreditServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        { }

        public UserCreditServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        { }

        public UserCreditServiceClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        { }

        public decimal GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            return base.Channel.GetCreditLimit(firstname, surname, dateOfBirth);
        }
    }
}