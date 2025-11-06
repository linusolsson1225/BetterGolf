using BetterGolfASP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace BetterGolfASP.Infrastructure.DB
{
    public class UoW
    {
        private bool _isDisposed = false;
        private readonly bool _disposeContext = false;
        protected Context Context { get; }
       
        public CustomerRepository CustomerRepository { get; private set; }
        public OrderRepository OrderRepository { get; private set; }
        public OrderRowRepository OrderRowRepository { get; private set; }
        public ProductRepository ProductRepository { get; private set; }
 
        

        public UoW(Context context)
        {
            Context = context;
           
            CustomerRepository = new CustomerRepository(context);
            OrderRepository = new OrderRepository(context);
            OrderRowRepository = new OrderRowRepository(context);
            ProductRepository = new ProductRepository(context);
            
        }

        public UoW()
            : this(new Context())
        {
            _disposeContext = true;
        }

        public void Update<T>(T entity) where T : class
        {
            try
            {
                Context.Set<T>().Update(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }



        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (_disposeContext)
                {
                    Context.Dispose();
                }
            }
            _isDisposed = true;
        }

        ~UoW()
        {
            Dispose(false);
        }
    }
}
