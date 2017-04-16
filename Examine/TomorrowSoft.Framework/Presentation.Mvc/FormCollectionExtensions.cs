using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    public static class FormCollectionExtensions
    {
         public static IDictionary<string, string> ToDictionary(this FormCollection collection)
         {
             return collection.AllKeys.ToDictionary(key => key, key => collection[key]);
         }
    }
}