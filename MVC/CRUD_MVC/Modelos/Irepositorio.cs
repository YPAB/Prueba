using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    //Interfaces del tipo generico
    interface IRepositorio<TEntity>
    {
        TEntity Create(TEntity toCreate);
        TEntity Retrieve(Expression<Func<TEntity, bool>> criterio);
        bool Update(TEntity toUpdate);
        bool Delete(TEntity toDelete);
        List<TEntity> Filter(Expression<Func<TEntity, bool>> criterio);

    }
}
