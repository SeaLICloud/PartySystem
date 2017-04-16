using TomorrowSoft.Framework.Domain.Bases;

namespace TommorrowSoft.Examine.Domian
{
    public partial class PartyMoney : Entity<PartyMoney>
    {
        public PartyMoney()
        {
        }

        public PartyMoney(string code)
        {
            Code = code;
             
        }

        public virtual PartyMoneyIdentifier Id { get { return PartyMoneyIdentifier.Of(Code);}}      

        public virtual string Name { get; set; }
        public virtual decimal PostWage { get; set; }
        public virtual decimal SalaryRankWage { get; set; }
        public virtual decimal Allowance { get; set; }
        public virtual decimal PerformanceWage { get; set; }
        public virtual decimal UnionExpenses { get; set; }
        public virtual decimal MedicalInsurance { get; set; }
        public virtual decimal UnemploymentInsurance { get; set; }
        public virtual decimal OldAgeInsurance { get; set; }
        public virtual decimal JobAnnuity { get; set; }
        public virtual decimal IndividualIncomeTax { get; set; }
        public virtual string  Code { get; set; }
    }
}