using TomorrowSoft.Framework.Domain.Bases;

namespace TommorrowSoft.Examine.Domian
{
    public struct PartyMoneyIdentifier : IBusinessIdentifier
    {

        public string Code { get; set; }


        public PartyMoneyIdentifier(string code) : this()
        {
            Code = code;
        }

        public static PartyMoneyIdentifier Of(string code)
        {
            return new PartyMoneyIdentifier(code);
        }


        public override string ToString()
        {
            return string.Format("PartyMoney/{0}", Code);
        }


        public static implicit operator string(PartyMoneyIdentifier id)
        {
            return id.ToString();
        }


        public static implicit operator PartyMoneyIdentifier(string id)
        {
            string[] sub = id.Split(new[] {'/'}, 2);
            return Of(sub[1]);
        }
    }
}