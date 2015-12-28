﻿
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPatternGenerator
{
    public class CodeSnippets
    {
        public static string GenerateClassViewModel(string className, Dictionary<string, string> properties, List<string> keys)
        {
            var propsString = "";
            var toDataBaseString = "";
            var fromDataBaseString = "";
            var updateDataBaseString = "";
            var keysString = "";

            foreach (var p in properties.Select((Entry, Index) => new { Entry, Index }))
            {
                propsString += "        public " + p.Entry.Value + " " + p.Entry.Key + " { get; set; }";
                fromDataBaseString += "            " + p.Entry.Key + " = model." + p.Entry.Key + ";";
                updateDataBaseString += "            model." + p.Entry.Key + " = " + p.Entry.Key + ";";
                toDataBaseString += "                " + p.Entry.Key + " = " + p.Entry.Key;
                if (p.Index + 1 < properties.Count)
                {
                    propsString += "\n";
                    toDataBaseString += ",\n";
                    fromDataBaseString += "\n";
                    updateDataBaseString += "\n";
                }
            }

            foreach (var k in keys.Select((Entry, Index) => new { Entry, Index }))
            {
                keysString += k.Entry;
                if (k.Index + 1 < keys.Count)
                    keysString += ",";
            }

            var classViewModel =
@"using Repository.Models;
using System;

namespace Repository.ViewModel
{
    public class " + className + @"ViewModel : IViewModel<" + className + @">
    {

" + propsString + @"

        public " + className + @" ToDataBase()
        {
            return new " + className + @"()
            {
" + toDataBaseString + @"
            };
        }

        public void FromDataBase(" + className + @" model)
        {
" + fromDataBaseString + @"
        }

        public void UpdateDataBase(" + className + @" model)
        {
" + updateDataBaseString + @"
        }

        public object[] GetKeys()
        {
            return new object[] { " + keysString + @" };
        }
    }
}";

            return classViewModel;
        }
        public static string IRepository =
@"using System;
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

        public static string IViewModel =
@"namespace Repository.ViewModel
{
    public interface IViewModel<TModel> where TModel : class
        {
            TModel ToDataBase();
            void FromDataBase(TModel model);
            void UpdateDataBase(TModel model);
            object[] GetKeys();
        }
}";

        public static string EntityRepository =
@"using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.ViewModel;

namespace Repository.Repository
{
    public class EntityRepository<TModel, TViewModel> : IRepository<TModel, TViewModel> where TModel : class where TViewModel : IViewModel<TModel>, new()
    {
        private readonly DbContext _context;

        protected DbSet<TModel> DbSet => _context.Set<TModel>();

        public EntityRepository(DbContext context)
        {
            this._context = context;
        }

        public virtual TViewModel Add(TViewModel model)
        {
            var m = model.ToDataBase();
            var addedModel = DbSet.Add(m);
            try
            {
                _context.SaveChanges();
                model.FromDataBase(addedModel);
                return model;
            }
            catch (Exception)
            {
                return default(TViewModel);
            }
        }

        public virtual int Delete(Expression<Func<TModel, bool>> expression)
        {
            var data = DbSet.Where(expression);
            DbSet.RemoveRange(data);
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public virtual int Delete(TViewModel model)
        {
            var data = DbSet.Find(model.GetKeys());
            DbSet.Remove(data);
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public virtual ICollection<TViewModel> Get()
        {
            var list = new List<TViewModel>();
            foreach (var model in DbSet)
            {
                var vm = new TViewModel();
                vm.FromDataBase(model);
                list.Add(vm);
            }

            return list;
        }

        public virtual TViewModel Get(params object[] keys)
        {
            var data = DbSet.Find(keys);
            if (data == null)
                return default(TViewModel);

            var vm = new TViewModel();
            vm.FromDataBase(data);

            return vm;
        }

        public virtual ICollection<TViewModel> Get(Expression<Func<TModel, bool>> expression)
        {
            var data = new List<TViewModel>();
            foreach (var model in DbSet.Where(expression))
            {
                var obj = new TViewModel();
                obj.FromDataBase(model);
                data.Add(obj);
            }

            return data;
        }

        public virtual int Update(TViewModel model)
        {
            var obj = DbSet.Find(model.GetKeys());
            model.UpdateDataBase(obj);
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}";

    }
}
