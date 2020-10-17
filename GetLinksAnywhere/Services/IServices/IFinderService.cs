using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetLinksAnywhere.Services.IServices
{
    public interface IFinderService
    {
        Task<IEnumerable<string>> FindAllLinks(string data);
    }
}
