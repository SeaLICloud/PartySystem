using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;

namespace System.Web.Mvc.Html
{
    public enum AlertCategory
    {
        [EnumText("")]
        Default,
        [EnumText("error")]
        Error,
        [EnumText("success")]
        Success,
        [EnumText("info")]
        Info
    }
}