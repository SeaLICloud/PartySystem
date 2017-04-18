using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;
using TomorrowSoft.Framework.Domain.Repositories;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TommorrowSoft.Examine.Application.Imp
{
    [RegisterToContainer]
    public class AdminService : IAdminService
    {

        private readonly IRepository _repository;
        private readonly ISecurityService _securityService;


        //AdminServise
        public AdminService(IRepository repository, ISecurityService securityService)
        {
            _repository = repository;
            _securityService = securityService;
        }

        public ISecurityService SecurityService
        {
            get { return _securityService; }
        }

        //CreatePartyMoney
        public IPartyMoneyCommand CreatePartyMoney(string s)
        {

            var partyMoneyCollection = new PartyMoney(s);
            _repository.Save(partyMoneyCollection);
            return new PartyMoneyCommand(_repository, partyMoneyCollection);
        }

        //DeletePartyMoney
        public void DeletePartyMoney(PartyMoneyIdentifier id)
        {
            var partyMoneyCollection = GetPartyMoney(id);
            _repository.Remove(partyMoneyCollection);
        }

        //GetPartyMoney
        public PartyMoney GetPartyMoney(PartyMoneyIdentifier id)
        {
            if (!_repository.IsExisted(new PartyMoney.By(id))) 
                throw new DomainErrorException("该报表不存在");
            return _repository.FindOne(new PartyMoney.By(id));
        }

        //EditPartyMoney
        public IPartyMoneyCommand EditPartyMoney(PartyMoneyIdentifier id)
        {
            var partyMoneyCollection = GetPartyMoney(id);
            return new PartyMoneyCommand(_repository,partyMoneyCollection);
        }
    }
}