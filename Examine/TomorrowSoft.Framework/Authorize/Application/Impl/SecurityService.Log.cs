using System.Linq;
using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public partial class SecurityService
    {
        public IRequestLogCommand CreateRequestLog(string sessionId)
        {
            var nextIndex = 1;
            var logs = repository.FindAll(new RequestLog.BySessionId(sessionId));
            if (logs.Any())
                nextIndex = logs.Max(x => x.Identifier.Index) + 1;
            var identifier = RequestLogIdentifier.of(sessionId, nextIndex);
            var log = new RequestLog(identifier);
            repository.Save(log);
            return new RequestLogCommand(log);
        }
    }
}