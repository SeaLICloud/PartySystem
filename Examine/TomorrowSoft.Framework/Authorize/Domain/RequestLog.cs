using System;
using System.Linq.Expressions;
using LinqSpecs;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public class RequestLog : Entity<RequestLog>
    {
        public virtual RequestLogIdentifier Identifier { get; protected set; }

        protected RequestLog()
        {
            Time = DateTime.Now;
        }

        public RequestLog(RequestLogIdentifier identifier) : this()
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Url
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime Time { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        public virtual string HttpMethod { get; set; }

        /// <summary>
        /// 用户Ip
        /// </summary>
        public virtual string UserIp { get; set; }

        /// <summary>
        /// 参数数据
        /// </summary>
        public virtual string ActionParameters { get; set; }

        public class BySessionId : Specification<RequestLog>
        {
            private readonly string sessionId;

            public BySessionId(string sessionId)
            {
                this.sessionId = sessionId;
            }

            public override Expression<Func<RequestLog, bool>> IsSatisfiedBy()
            {
                return x => x.Identifier.SessionId == sessionId;
            }
        }
    }
}