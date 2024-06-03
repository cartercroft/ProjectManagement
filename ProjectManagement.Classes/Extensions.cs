using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

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
        public static string GetDisplayNameString(this Enum enumMember)
        {
            DisplayAttribute attribute = GetAttribute<DisplayAttribute>(enumMember);
            if (attribute != null)
            {
                return attribute.Name;
            }
            return enumMember.ToString();
        }
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
}
