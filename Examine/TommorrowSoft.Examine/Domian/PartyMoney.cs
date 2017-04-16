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
            Id = PartyMoneyIdentifier.Of(code);
        }


        public virtual PartyMoneyIdentifier Id { get; set; }
        

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
    }
}