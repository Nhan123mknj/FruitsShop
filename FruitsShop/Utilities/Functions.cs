using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Policy;
using System.Security.Cryptography;
using System.Text;

namespace FruitsShop.Utilities
{
	public class Functions
	{
		public static int _AccountId = 0;
		public static string _Username = string.Empty;
		public static string _Email = string.Empty;
		public static string _Message = string.Empty;
		public static string _MessageEmail = string.Empty;
		public static string TitleSlugGenerationAlias(string title)
		{
			return SlugGenerator.SlugGenerator.GenerateSlug(title);
		}
		public static string TitleSlugGeneration(string type, string title, long id)
		{
			string url = string.Empty;
			if (type == string.Empty)
				url = "/" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString() + ".html";
			else
                 url = type + "/" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString() + ".html";
            return url;
		}
		public static string ToVnd(double donGia)
		{
			return donGia.ToString("#,##0") + " đ";
		}

		public static string ToTitleCase(string str)
		{
			string result = str;
			if (!string.IsNullOrEmpty(str))
			{
				var words = str.Split(' ');
				for (int index = 0; index < words.Length; index++)
				{
					var s = words[index];
					if (s.Length > 0)
					{
						words[index] = s[0].ToString().ToUpper() + s.Substring(1);
					}
				}
				result = string.Join(" ", words);
			}
			return result;
		}
		public static string getCurrentDate()
		{
			return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}
        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Functions._Username) || string.IsNullOrEmpty(Functions._Email) || (Functions._AccountId <= 0))
                return false;
            return true;
        }
    }
}