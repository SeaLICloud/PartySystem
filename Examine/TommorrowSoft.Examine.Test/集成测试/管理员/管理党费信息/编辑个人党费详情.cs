using System.Globalization;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using Machine.Specifications;
using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Examine.Web.Controllers;
using TomorrowSoft.Examine.Web.Helps;


namespace TommorrowSoft.Examine.Test.集成测试.管理员.管理党费信息
{
    [Subject(typeof (PartyMoneyController), "Edit")]
    public class 编辑个人党费详情 : 数据准备工作
    {

        private static ActionResult _result;


        public static decimal UnionExpenses = 50.00m;
        public static decimal IndividualIncomeTax =50.00m;
        public static decimal JobAnnuity = 50.00m;
        public static decimal OldAgeInsurance = 50.00m;
        public static decimal UnemploymentInsurance = 50.00m;
        public static decimal MedicalInsurance = 50.00m;
        public static decimal PerformanceWage = 200.00m;
        public static decimal Allowance = 200.00m;
        public static decimal SalaryRankWage = 200.00m;
        public static decimal PostWage = 200.00m;
        public static string Name = "张七";
        public static string Code = "1";

        private Establish _context = () =>
        {
            创建报表(1);
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
                {Keys.UnionExpenses, UnionExpenses.ToString(CultureInfo.InvariantCulture)},
            };
            subject = Action<PartyMoneyController>(x => x.Edit(partyMoneyCollection, "1"));
        };

        private Because _of = () => _result = subject.Invoke();

        private It _应该被成功编辑 = () =>
        {
            var partyMoneyCollection = repository.FindOne(new PartyMoney.By(报表(1)));
            partyMoneyCollection.ShouldNotBeNull();
            partyMoneyCollection.Code.ShouldEqual(Code);
            partyMoneyCollection.Name.ShouldEqual(Name);
            partyMoneyCollection.PostWage.ShouldEqual(PostWage);
            partyMoneyCollection.SalaryRankWage.ShouldEqual(SalaryRankWage);
            partyMoneyCollection.Allowance.ShouldEqual(Allowance);
            partyMoneyCollection.PerformanceWage.ShouldEqual(PerformanceWage);
            partyMoneyCollection.MedicalInsurance.ShouldEqual(MedicalInsurance);
            partyMoneyCollection.UnemploymentInsurance.ShouldEqual(UnemploymentInsurance);
            partyMoneyCollection.OldAgeInsurance.ShouldEqual(OldAgeInsurance);
            partyMoneyCollection.JobAnnuity.ShouldEqual(JobAnnuity);
            partyMoneyCollection.IndividualIncomeTax.ShouldEqual(IndividualIncomeTax);
            partyMoneyCollection.UnionExpenses.ShouldEqual(UnionExpenses);
        };
    }
}
