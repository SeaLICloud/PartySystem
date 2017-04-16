using System.Web.Mvc.Html;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    public class JavascriptHelper
    {
        public static string Alert(string strongMessage, string message, AlertCategory alertCategory)
        {
            return string.Format("$('#{0}').prepend('{1}')", 
                                FrameworkKeys.MainContent, 
                                GeboExtensions.Alert(null, strongMessage, message, alertCategory));
        }
    }
}