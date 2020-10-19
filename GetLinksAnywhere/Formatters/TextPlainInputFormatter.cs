using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GetLinksAnywhere.Formatters
{
    public class TextPlainInputFormatter : InputFormatter
    {
        #region Fields
        
        private const string ContentType = "text/plain";

        #endregion

        #region Constructor

        public TextPlainInputFormatter()
        {
            SupportedMediaTypes.Add(ContentType);
        }

        #endregion

        #region Public Methods

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using var reader = new StreamReader(request.Body);

            var content = await reader.ReadToEndAsync();
            return await InputFormatterResult.SuccessAsync(content);
        }

        public override bool CanRead(InputFormatterContext context)
        {
            var contentType = context.HttpContext.Request.ContentType;
            return contentType.StartsWith(ContentType);
        }

        #endregion
    }
}
