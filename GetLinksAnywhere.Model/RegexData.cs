using System.Text.RegularExpressions;

namespace GetLinksAnywhere.Model
{
    public class RegexData
    {
        public string Pattern { get; set; }

        public Regex Regex { get; set; }
    }
}
