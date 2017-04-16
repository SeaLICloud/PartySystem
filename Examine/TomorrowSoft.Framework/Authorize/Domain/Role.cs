using System.Collections.Generic;
using System.Linq;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Role : Entity<Role>
    {
        protected Role()
        {
            roleauthorities = new List<RoleAuthority>();
            IsVisible = true;
        }

        public Role(RoleIdentifier id):this()
        {
            Id = id;
        }

        public virtual RoleIdentifier Id { get; protected set; }

        /// <summary>
        /// 角色中文名
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public virtual bool IsVisible { get; set; }
        
        private IList<RoleAuthority> roleauthorities;
        public virtual IEnumerable<RoleAuthority> RoleAuthorities
        {
            get { return roleauthorities; }
        }

        public virtual IEnumerable<IAuthority> GetAuthorities()
        {
            return RoleAuthorities
                .OrderBy(x => x.Function.Group)
                .ThenBy(x => x.Function.Id.DBID);
        }

        public virtual void AddAuthority(RoleAuthority roleAuthority)
        {
            roleauthorities.Add(roleAuthority);
        }

        public virtual void MergeAuthoritiesWith(Role other)
        {
            var thisAuthorities = GetAuthorities().ToList();
            var otherAuthorities = other.GetAuthorities().ToList();
            for (var i = 0; i < thisAuthorities.Count; i++)
            {
                thisAuthorities[i].IsAuthorized = thisAuthorities[i].IsAuthorized || otherAuthorities[i].IsAuthorized;
            }
        }
    }
}