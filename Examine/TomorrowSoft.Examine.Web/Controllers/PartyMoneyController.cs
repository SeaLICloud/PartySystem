using System.Web.Mvc;
using TommorrowSoft.Examine.Application;
using TomorrowSoft.Examine.Web.Helps;
using TomorrowSoft.Examine.Web.Helps.BaseControllers;

namespace TomorrowSoft.Examine.Web.Controllers
{
    public class PartyMoneyController : AdminAreaController
    {

        public PartyMoneyController(IAdminService service) : base(service)
        {
        }


        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection partyMoneyCollection)
        {
            Service.CreatePartyMoney(partyMoneyCollection[Keys.PartyCode])

                .Name(partyMoneyCollection[Keys.Name])
                .PostWage(partyMoneyCollection[Keys.PostWage])
                .SalaryRankWage(partyMoneyCollection[Keys.SalaryRankWage])
                .Allowance(partyMoneyCollection[Keys.Allowance])
                .PerformanceWage(partyMoneyCollection[Keys.PerformanceWage])
                .UnionExpenses(partyMoneyCollection[Keys.UnionExpenses])
                .MedicalInsurance(partyMoneyCollection[Keys.MedicalInsurance])
                .UnemploymentInsurance(partyMoneyCollection[Keys.UnemploymentInsurance])
                .OldAgeInsurance(partyMoneyCollection[Keys.OldAgeInsurance])
                .JobAnnuity(partyMoneyCollection[Keys.JobAnnuity])
                .IndividualIncomeTax(partyMoneyCollection[Keys.IndividualIncomeTax]);
            return View();
        }

    }
}
