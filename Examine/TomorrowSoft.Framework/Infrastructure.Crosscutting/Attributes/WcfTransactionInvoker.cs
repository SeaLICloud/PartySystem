using System;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;
using NHibernate;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Attributes
{
    public class WcfTransactionInvoker : IOperationInvoker
    {
        private readonly IOperationInvoker _inner;

        public WcfTransactionInvoker(IOperationInvoker inner)
        {
            this._inner = inner;
        }

        public object[] AllocateInputs()
        {
            return _inner.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            var unitOfWork = IoC.Get<IUnitOfWork>();
            var result = _inner.Invoke(instance, inputs, out outputs);
            unitOfWork.Commit();
            return result;
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronous { get { return true; } }
    }
}