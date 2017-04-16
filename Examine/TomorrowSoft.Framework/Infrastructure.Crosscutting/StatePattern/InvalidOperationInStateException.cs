using System;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.StatePattern
{
    public class InvalidOperationInStateException : Exception
    {
        private readonly IState state;
        private readonly string operationName;

        public InvalidOperationInStateException(IState state, string operationName)
        {
            this.state = state;
            this.operationName = operationName;
        }

        public override string Message
        {
            get { return string.Format("在【{0}】状态下，不允许执行【{1}】操作！", state.Name, operationName); }
        }
    }
}