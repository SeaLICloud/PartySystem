using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TommorrowSoft.Examine.Infrastructure.Repositories.Map
{
    internal class PartyMoneyMap : BaseClassMap<PartyMoney>
    {
        public PartyMoneyMap()
        {
            Component(x => x.Id, m => m.Map(y => y.Code).Unique());
            Map(x => x.Name);
            Map(x => x.PostWage);
            Map(x => x.SalaryRankWage);
            Map(x => x.Allowance);
            Map(x => x.PerformanceWage);
            Map(x => x.UnionExpenses);
            Map(x => x.MedicalInsurance);
            Map(x => x.UnemploymentInsurance);
            Map(x => x.OldAgeInsurance);
            Map(x => x.JobAnnuity);
            Map(x => x.IndividualIncomeTax);
        }
    }
}