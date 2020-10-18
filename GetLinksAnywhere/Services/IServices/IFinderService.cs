using System.Collections.Generic;
using System.Threading.Tasks;
using GetLinksAnywhere.Common;

namespace GetLinksAnywhere.Services.IServices
{
    public interface IFinderService
    {
        Task<IEnumerable<string>> FindAllLinks(string data, 
            int maxLengthOfChunk = Constants.MaxLengthOfChunk);
    }
}
