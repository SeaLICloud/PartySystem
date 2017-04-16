using System;
using LinqSpecs;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Version : Entity<Version>
    {
        /// <summary>
        /// 软件模块的类型，如Web、Database、Android App
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 软件模块的版本
        /// </summary>
        public virtual string VersionNumber { get; set; }

        /// <summary>
        /// 软件名称
        /// </summary>
        public virtual string SoftwareTitle { get; set; }
    }
}