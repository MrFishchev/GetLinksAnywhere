using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GetLinksAnywhere.Common.Helpers
{
    public static class StreamHelper
    {
        public static async  Task<IEnumerable<string>> ReadAllLines(Func<Stream> streamProvider,
            Encoding encoding)
        {
            var result = new List<string>();

            await using var stream = streamProvider();
            using var reader = new StreamReader(stream, encoding);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if(line.StartsWith("#")) continue;
                result.Add(line);
            }

            return result;
        }
    }
}
