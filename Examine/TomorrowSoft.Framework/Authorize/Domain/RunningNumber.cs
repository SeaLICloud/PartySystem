using System;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    /// <summary>
    /// 流水号
    /// </summary>
    public partial class RunningNumber : Entity<RunningNumber>
    {
        protected RunningNumber()
        {
        }

        public RunningNumber(string key, RunningNumberMask mask) : this()
        {
            Id = RunningNumberIdentifier.of(key);
            Mask = mask;
            LastNumber = "";
        }

        public virtual RunningNumberIdentifier Id { get; protected set; }

        /// <summary>
        /// 当前流水号
        /// </summary>
        public virtual string LastNumber { get; set; }

        /// <summary>
        /// 流水号掩码
        /// </summary>
        public virtual RunningNumberMask Mask { get; protected set; }

        /// <summary>
        /// 根据时间生成新流水号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public virtual RunningNumber Next(DateTime dt)
        {
            LastNumber = Mask.Next(LastNumber, dt);
            return this;
        }
        
    }
}