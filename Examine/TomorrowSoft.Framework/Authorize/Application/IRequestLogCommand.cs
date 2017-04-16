using System;

namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface IRequestLogCommand
    {
        IRequestLogCommand Url(string url);
        IRequestLogCommand Time(DateTime time);
        IRequestLogCommand HttpMethod(string httpMethod);
        IRequestLogCommand UserIp(string userIp);
        IRequestLogCommand UserName(string userName);
        IRequestLogCommand ActionParameters(string parameters);
    }
}