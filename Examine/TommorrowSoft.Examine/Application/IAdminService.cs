using TommorrowSoft.Examine.Domian;

namespace TommorrowSoft.Examine.Application
{
    public interface IAdminService
    {
        IPartyMoneyCommand CreatePartyMoney(string s);
        void DeletePartyMoney(PartyMoneyIdentifier id);
    }
}