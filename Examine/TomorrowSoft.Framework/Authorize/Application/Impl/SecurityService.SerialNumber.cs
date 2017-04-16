using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public partial class SecurityService
    {
        public RunningNumber GetRunningNumber(string key)
        {
            return repository.FindOne(new RunningNumber.By(RunningNumberIdentifier.of(key)));
        }

        public void CreateRunningNumber(string key, RunningNumberMask mask)
        {
            var sn = new RunningNumber(key, mask);
            repository.Save(sn);
        }
    }
}