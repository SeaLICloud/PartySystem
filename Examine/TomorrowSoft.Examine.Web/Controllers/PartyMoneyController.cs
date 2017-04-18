using System.Web.Mvc;
using TommorrowSoft.Examine.Application;
using TommorrowSoft.Examine.Domian;
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



        public ActionResult Delete(string id) 
        {
            Service.DeletePartyMoney(PartyMoneyIdentifier.Of(id));
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View(Service.GetPartyMoney(PartyMoneyIdentifier.Of(id)));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection partyMoneyCollection, string id)
        {
            Service.EditPartyMoney(PartyMoneyIdentifier.Of(id))
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
            return RedirectToAction("Index");
        }
    }
}
