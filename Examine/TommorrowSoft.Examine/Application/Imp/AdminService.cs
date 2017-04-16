using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Domain.Repositories;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TommorrowSoft.Examine.Application.Imp
{
    [RegisterToContainer]
    public class AdminService : IAdminService
    {
        private readonly IRepository _repository;
        private readonly ISecurityService _securityService;

        public AdminService(IRepository repository, ISecurityService securityService)
        {
            _repository = repository;
            _securityService = securityService;
        }

        public ISecurityService SecurityService
        {
            get { return _securityService; }
        }

        public IPartyMoneyCommand CreatePartyMoney(string s)
        {

            var partyMoneyCollection = new PartyMoney(s);
            _repository.Save(partyMoneyCollection);
            return new PartyMoneyCommand(_repository,partyMoneyCollection);
        }
    }
}