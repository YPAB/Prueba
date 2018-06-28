using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Modelos
{
    interface IRepositorio1<TEntity>
    {
        TEntity Create(TEntity toCreate);
        bool Delete(TEntity toDelete);
        List<TEntity> Filter(Expression<Func<TEntity, bool>> criterio);
        TEntity Retrieve(Expression<Func<TEntity, bool>> criterio);
        bool Update(TEntity toUpdate);
    }
}