namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class SearchResult
    {
        public SearchResult(string title, int count, string controller_name, string action_name)
        {
            Title = title;
            Count = count;
            ControllerName = controller_name;
            ActionName = action_name;
        }

        public string Title { get; private set; }
        public int Count { get; private set; }
        public string ControllerName { get; private set; }
        public string ActionName { get; private set; }
    }
}