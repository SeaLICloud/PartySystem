using System.Collections.Generic;
using System.Text;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    public class Rule
    {
        public string TagName { get; private set; }
        public IDictionary<string, string> Rules { get; private set; }
        public IDictionary<string, string> Messages { get; private set; }

        public Rule(string tagName)
        {
            TagName = tagName;
            Rules = new Dictionary<string, string>();
            Messages = new Dictionary<string, string>();
        }

        public static Rule Tag(string name)
        {
            return new Rule(name);
        }
        
        public string ToRules()
        {
            var sb = new StringBuilder();
            sb.Append(TagName).Append(":{");
            foreach (var dict in Rules)
            {
                sb.Append(dict.Key);
                sb.Append(":");
                sb.Append(dict.Value);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        public string ToMessages()
        {
            var sb = new StringBuilder();
            sb.Append(TagName).Append(":{");
            foreach (var dict in Messages)
            {
                if (!string.IsNullOrEmpty(dict.Value))
                {
                    sb.Append(dict.Key);
                    sb.Append(":");
                    sb.Append("\"").Append(dict.Value).Append("\"");
                    sb.Append(",");
                }
            }
            if(sb[sb.Length-1]==',')
                sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        public Rule required()
        {
            Rules.Add("required", "true");
            return this;
        }
        
        public Rule required(string value, string message)
        {
            Rules.Add("required", value);
            Messages.Add("required", message);
            return this;
        }

        public Rule email()
        {
            Rules.Add("email", "true");
            return this;
        }

        public Rule email(string value, string message)
        {
            Rules.Add("email", value);
            Messages.Add("email", message);
            return this;
        }

        public Rule isIdCardNo()
        {
            Rules.Add("isIdCardNo", "true");
            return this;
        }

        public Rule isIdCardNo(string value, string message)
        {
            Rules.Add("isIdCardNo", value);
            Messages.Add("isIdCardNo", message);
            return this;
        }

        public Rule equalTo(string tagId, string message)
        {
            Rules.Add("equalTo", string.Format("\"#{0}\"", tagId));
            Messages.Add("equalTo", message);
            return this;
        }

        public Rule Add(string rule)
        {
            Rules.Add(rule, "true");
            return this;
        }
    }
}