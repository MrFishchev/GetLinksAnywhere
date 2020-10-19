using System.Text.RegularExpressions;
using GetLinksAnywhere.Common.Interfaces;
using GetLinksAnywhere.Model;

namespace GetLinksAnywhere.Common.Classes
{
    public class RegexUrlBuilder : IBuilder<RegexData>
    {
        #region Fields

        private readonly RegexData _regexData;

        #endregion

        #region MyRegion

        public RegexUrlBuilder()
        {
            _regexData = new RegexData();
        }

        #endregion

        #region Public Methods

        public RegexData Build()
        {
            _regexData.Pattern = BuildPattern();
            _regexData.Regex = new Regex(_regexData.Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return _regexData;
        }

        #endregion

        #region Private Methods

        private string BuildPattern()
        {
            var schemes = string.Join('|', IANASettings.Schemes);
            var domains = string.Join('|', IANASettings.Domains);

            var patternTemplate =
                @"\b((%SCHEMES%):\/\/)?(((?<=\s|\/|^)(www\.)?([a-z0-9\-]\.)?([a-z0-9\-]+|[\ud000-\udfff]+)+\.((%DOMAINS%)($|(?=\/|\s|[,!?<>]))){1})|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(?=\s|!|,|$|\?|\*|\/))(\/[^,!<>\s|$]+)?";
            return patternTemplate.Replace("%SCHEMES%", schemes)
                .Replace("%DOMAINS%", domains);
        }

        #endregion
    }
}
