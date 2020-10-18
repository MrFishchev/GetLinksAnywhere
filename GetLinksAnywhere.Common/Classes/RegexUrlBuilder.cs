using System.Text.RegularExpressions;
using GetLinksAnywhere.Common.Interfaces;
using GetLinksAnywhere.Model;

namespace GetLinksAnywhere.Common.Classes
{
    public class RegexUrlBuilder : IBuilder<RegexData>
    {
        private RegexData _regexData;

        public RegexUrlBuilder()
        {
            _regexData = new RegexData();
        }

        public RegexData Build()
        {
            _regexData.Pattern = BuildPattern();
            _regexData.Regex = new Regex(_regexData.Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return _regexData;
        }

        private string BuildPattern()
        {
            var schemes = string.Join('|', IANASettings.Schemes);
            var domains = string.Join('|', IANASettings.Domains);

            var patternTemplate =
                @"\b((%SCHEMES%):\/\/)?(((?<=\s|\/|^)(www\.)?([a-z0-9\-]\.)?([a-z0-9\-]+|[\ud000-\udfff]+)+\.((%DOMAINS%)((?=\/|\s|[,!?<>]))){1})|(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))(\/[^,!<>\s]+)?";
            return patternTemplate.Replace("%SCHEMES%", schemes)
                .Replace("%DOMAINS%", domains);
        }
    }
}
