using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MetodosDeExtension;

namespace Modelos
{
    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs e);

   
    public class ExceptionEventArgs
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public Exception InnerException { get; set; }
        public MethodBase TargetSite { get; set; }

    }

    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class

    {
        public event ExceptionEventHandler Exception;
        CRUD_MVCEntities Context;

        public Repositorio()
        {
            Context = new CRUD_MVCEntities();
        }

        private DbSet<TEntity> EntitySet { get { return Context.Set<TEntity>(); } }

        public TEntity Create(TEntity toCreate)
        {
            TEntity Result = null;
            try
            {
                Result = EntitySet.Add(toCreate);
                Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() {InnerException = ex.InnerException, Message= ex.Message, Source =ex.Source, StackTrace = ex.StackTrace, TargetSite= ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            return Result;
        }

        public bool Delete(TEntity toDelete)
        {
            bool Result = false;
            try
            {
                EntitySet.Attach(toDelete);
                EntitySet.Remove(toDelete);
                Result = Context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            return Result;
        }

        public List<TEntity> Filter(Expression<Func<TEntity, bool>> criterio)
        {
            List<TEntity> Resultado = new List<TEntity>();

            try
            {
                Resultado = EntitySet.Where(criterio).ToList();

            }
            catch (DbEntityValidationException ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            return Resultado;
        }

        public TEntity Retrieve(Expression<Func<TEntity, bool>> criterio)
        {
            TEntity Resultado = null;
            try
            {
                Resultado = EntitySet.FirstOrDefault(criterio);
            }
            catch (DbEntityValidationException ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            return Resultado;
        }

        public bool Update(TEntity toUpdate)
        {
            bool Resultado = false;
            try
            {
                EntitySet.Attach(toUpdate);
                Context.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                Resultado = Context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            return Resultado;
        }

        public bool Update(Expression<Func<TEntity, bool>> criterio, string propertyName, object valor)
        {
            bool Resultado = false;
            try
            {
                
                Context.Entry<TEntity>(EntitySet.FirstOrDefault(criterio)).Property(propertyName).CurrentValue = valor;
                Resultado = Context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs() { InnerException = ex.InnerException, Message = ex.Message, Source = ex.Source, StackTrace = ex.StackTrace, TargetSite = ex.TargetSite });
                this.EscribirEnArchivoLog(ex);

            }
            return Resultado;
        }
    }

}
   

