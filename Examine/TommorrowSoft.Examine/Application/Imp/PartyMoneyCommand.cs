using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Framework.Domain.Repositories;

namespace TommorrowSoft.Examine.Application.Imp
{
    public class PartyMoneyCommand : IPartyMoneyCommand
    {
        public readonly IRepository Repostory;
        public readonly PartyMoney Partymoney;

        public PartyMoneyCommand( IRepository repository, PartyMoney partyMoney)
        {
            Repostory = repository;
            Partymoney = partyMoney;
        }



        public IPartyMoneyCommand Name(string s)
        {         
            Partymoney.Name =s;
            return this;
        }

        public IPartyMoneyCommand PostWage(string s)
        {
            var d = new decimal((double) Convert.ToDecimal(s));
            Partymoney.PostWage = d;
            return this;
        }

        public IPartyMoneyCommand SalaryRankWage(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.SalaryRankWage = d;
            return this;
        }

        public IPartyMoneyCommand Allowance(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.Allowance = d;
            return this;
        }

        public IPartyMoneyCommand PerformanceWage(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.PerformanceWage = d;
            return this;
        }

        public IPartyMoneyCommand UnionExpenses(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.UnionExpenses = d;
            return this;
        }

        public IPartyMoneyCommand MedicalInsurance(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.MedicalInsurance = d;
            return this;
        }

        public IPartyMoneyCommand UnemploymentInsurance(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.UnemploymentInsurance = d;
            return this;
        }

        public IPartyMoneyCommand OldAgeInsurance(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.OldAgeInsurance = d;
            return this;
        }

        public IPartyMoneyCommand JobAnnuity(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.JobAnnuity = d;
            return this;
        }

        public IPartyMoneyCommand IndividualIncomeTax(string s)
        {
            var d = new decimal((double)Convert.ToDecimal(s));
            Partymoney.IndividualIncomeTax = d;
            return this;
        }
    }
}
