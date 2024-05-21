using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Classes
{
    public static class Extensions
    {
        public static string AsQueryString(this List<KeyValuePair<string, object>> keyValuePairs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                KeyValuePair<string, object> kvp = keyValuePairs[i];
                sb.Append(kvp.Key);
                sb.Append("=");
                sb.Append(kvp.Value.ToString());
                if (i < keyValuePairs.Count - 1)
                {
                    sb.Append("&");
                }
            }
            return sb.ToString();
        }
    }
}
