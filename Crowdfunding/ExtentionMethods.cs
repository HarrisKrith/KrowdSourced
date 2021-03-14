using System.Security.Claims;

namespace Crowdfunding
{
    public static class ExtentionMethods // static!
    {
        // https://www.dotnetperls.com/remove-html-tags
        // https://stackoverflow.com/questions/13597485/how-to-convert-html-text-to-raw-text-in-code-behind-in-other-words-strip-text-fr
        /// Remove HTML tags from string using char array. USED FOR USER INPUT
        public static string StripTagsCharArray(string source)
        {
            if (source != null)
            {
                char[] array = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }
                }
                return new string(array, 0, arrayIndex);
            }
            else
            {
                return "";
            }
        }

        // Discord
        public static string GetUser(this ClaimsPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return claim.Value;
        }
    }
}
