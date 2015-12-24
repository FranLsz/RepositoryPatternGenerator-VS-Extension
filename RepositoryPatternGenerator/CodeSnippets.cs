using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternGenerator
{
    public class CodeSnippets
    {
        public static string IRepository = @"
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Repository.ViewModel;

namespace Repository.Repository
{
    public interface IRepository<TModel, TViewModel> where TModel : class where TViewModel : IViewModel<TModel>
    {
        ICollection<TViewModel> Get();
        TViewModel Get(params object[] keys);
        ICollection<TViewModel> Get(Expression<Func<TModel, bool>> expression);
        TViewModel Add(TViewModel model);
        int Update(TViewModel model);
        int Delete(TViewModel model);
        int Delete(Expression<Func<TModel, bool>> expression);
    }
}";

        public static string IViewModel = @"
namespace Repository.ViewModel
{
    public interface IViewModel<TModel> where TModel : class
        {
            TModel ToDataBase();
            void FromDataBase(TModel model);
            void UpdateDataBase(TModel model);
            object[] GetKeys();
        }
}";
    }
}
