using System;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    [RegisterToContainer]
    public class RequestLogCommand : IRequestLogCommand
    {
        private readonly RequestLog requestLog;

        public RequestLogCommand(RequestLog requestLog)
        {
            this.requestLog = requestLog;
        }

        public IRequestLogCommand Url(string url)
        {
            requestLog.Url = url;
            return this;
        }

        public IRequestLogCommand Time(DateTime time)
        {
            requestLog.Time = time;
            return this;
        }

        public IRequestLogCommand HttpMethod(string httpMethod)
        {
            requestLog.HttpMethod = httpMethod;
            return this;
        }

        public IRequestLogCommand UserIp(string userIp)
        {
            requestLog.UserIp = userIp;
            return this;
        }

        public IRequestLogCommand UserName(string userName)
        {
            requestLog.UserName = userName;
            return this;
        }

        public IRequestLogCommand ActionParameters(string parameters)
        {
            requestLog.ActionParameters = parameters;
            return this;
        }
    }
}