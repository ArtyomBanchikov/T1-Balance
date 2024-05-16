using System.Collections.Generic;
using System.Threading.Tasks;

namespace T1Balance.Services.WebServices.Interfaces
{
    public interface IModelWebService<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);
    }
}
