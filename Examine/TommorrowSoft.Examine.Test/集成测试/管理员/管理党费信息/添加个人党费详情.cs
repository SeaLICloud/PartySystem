using System.Globalization;
using System.Web.Mvc;
using Machine.Specifications;
using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Examine.Web.Controllers;
using TomorrowSoft.Examine.Web.Helps;

namespace TommorrowSoft.Examine.Test.集成测试.管理员.管理党费信息
{
    [Subject(typeof (PartyMoneyController), "Create")]
    internal class 添加个人党费详情 : 数据准备工作
    {
        private static ActionResult _result;

        public static string Code = "1";
        public static string Name = "王五";
        public static decimal PostWage = 200.00m;
        public static decimal SalaryRankWage = 200.00m;
        public static decimal Allowance = 200.00m;
        public static decimal PerformanceWage = 200.00m;
        public static decimal UnionExpenses = 50.00m;
        public static decimal MedicalInsurance = 50.00m;
        public static decimal UnemploymentInsurance = 50.00m;
        public static decimal OldAgeInsurance = 50.00m;
        public static decimal JobAnnuity = 50.00m;
        public static decimal IndividualIncomeTax = 50.00m;

        private Establish _context = () =>
        {
            var partyMoneyCollection = new FormCollection
            {
                
                {Keys.PartyCode, Code},
                {Keys.Name, Name},
                {Keys.PostWage, PostWage.ToString(CultureInfo.InvariantCulture)},
                {Keys.SalaryRankWage, SalaryRankWage.ToString(CultureInfo.InvariantCulture)},
                {Keys.Allowance, Allowance.ToString(CultureInfo.InvariantCulture)},
                {Keys.PerformanceWage, PerformanceWage.ToString(CultureInfo.InvariantCulture)},
                {Keys.MedicalInsurance, MedicalInsurance.ToString(CultureInfo.InvariantCulture)},
                {Keys.UnemploymentInsurance, UnemploymentInsurance.ToString(CultureInfo.InvariantCulture)},
                {Keys.OldAgeInsurance, OldAgeInsurance.ToString(CultureInfo.InvariantCulture)},
                {Keys.JobAnnuity, JobAnnuity.ToString(CultureInfo.InvariantCulture)},
                {Keys.IndividualIncomeTax, IndividualIncomeTax.ToString(CultureInfo.InvariantCulture)},
                {Keys.UnionExpenses, UnionExpenses.ToString(CultureInfo.InvariantCulture)}
            };
            subject = Action<PartyMoneyController>(x => x.Create(partyMoneyCollection));
        };

        private Because _of = () => _result = subject.Invoke();

        private It _应该成功被创建 = () =>
        {
            var  partyMoneyCollection = repository.FindOne(new PartyMoney.By(PartyMoneyIdentifier.Of(Code)));
            partyMoneyCollection.ShouldNotBeNull();
            partyMoneyCollection.Name.ShouldEqual(Name);
            partyMoneyCollection.PostWage.ShouldEqual(PostWage);
            partyMoneyCollection.SalaryRankWage.ShouldEqual(SalaryRankWage);
            partyMoneyCollection.Allowance.ShouldEqual(Allowance);
            partyMoneyCollection.PerformanceWage.ShouldEqual(PerformanceWage);
            partyMoneyCollection.UnionExpenses.ShouldEqual(UnionExpenses);
            partyMoneyCollection.MedicalInsurance.ShouldEqual(MedicalInsurance);
            partyMoneyCollection.UnemploymentInsurance.ShouldEqual(UnemploymentInsurance);
            partyMoneyCollection.OldAgeInsurance.ShouldEqual(OldAgeInsurance);
            partyMoneyCollection.JobAnnuity.ShouldEqual(JobAnnuity);
            partyMoneyCollection.IndividualIncomeTax.ShouldEqual(IndividualIncomeTax);
        };
    }
}