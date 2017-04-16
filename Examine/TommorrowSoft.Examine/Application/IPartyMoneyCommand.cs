namespace TommorrowSoft.Examine.Application
{
    public interface IPartyMoneyCommand
    {
        IPartyMoneyCommand Name(string s);
        IPartyMoneyCommand PostWage(string postWage);
        IPartyMoneyCommand SalaryRankWage(string salaryRankWage);
        IPartyMoneyCommand Allowance(string allowance);
        IPartyMoneyCommand PerformanceWage(string performanceWage);
        IPartyMoneyCommand UnionExpenses(string unionExpenses);
        IPartyMoneyCommand MedicalInsurance(string medicalInsurance);
        IPartyMoneyCommand UnemploymentInsurance(string unemploymentInsurance);
        IPartyMoneyCommand OldAgeInsurance(string oldAgeInsurance);
        IPartyMoneyCommand JobAnnuity(string jobAnnuity);
        IPartyMoneyCommand IndividualIncomeTax(string individualIncomeTax);
        
    }
}