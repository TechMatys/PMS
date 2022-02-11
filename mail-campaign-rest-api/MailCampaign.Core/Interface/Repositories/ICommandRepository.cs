using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Interface.Repositories
{
    public interface ICommandRepository<T> where T : class
    {
        Task<T> GetById(long id);
        Task<bool> Create(T entity);
        Task<bool> Update(long id, JObject fields);
        Task<bool> Delete(long id);
        Task<IEnumerable<T>> GetListResult(object queryParams);
    }
}
