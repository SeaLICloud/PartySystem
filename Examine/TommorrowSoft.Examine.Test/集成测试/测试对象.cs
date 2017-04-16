using Machine.Specifications;
using Microsoft.Practices.Unity;
using Rhino.Mocks;
using TommorrowSoft.Examine.Application;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Files;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Specs;

namespace TommorrowSoft.Examine.Test.集成测试
{
    public class 测试对象 : BaseActionSpec
    {
        protected Establish WorkstageManageContext =() =>
            {
                AdminService = A<IAdminService>();
                var fileHelper = MockRepository.GenerateMock<IFileHelper>();
                IoC.Current.RegisterInstance(typeof(IFileHelper), fileHelper);
            };
       
        protected static IAdminService AdminService;
    }
}