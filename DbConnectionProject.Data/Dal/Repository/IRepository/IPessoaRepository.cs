using DbConnectionProject.Models.Models;
using System.Linq.Expressions;

namespace DbConnectionProject.Controllers
{
    public interface IRepository <T> where T : class
    {
        void Create(T entity);
        T Read(int id);
        void Update(T entity);
        void Delete(T entity);
        List<T> ReadAll();
    }
}