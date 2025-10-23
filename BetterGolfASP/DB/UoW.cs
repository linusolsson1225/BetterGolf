using BetterGolfASP.Models;
using BetterGolfASP.Repositories;
using DB.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGolfASP.DB
{
    public class UoW
    {
        private bool isDisposed = false;
        private readonly bool disposeContext = false;
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
            disposeContext = true;
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
            if (isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (disposeContext)
                {
                    Context.Dispose();
                }
            }
            isDisposed = true;
        }

        ~UoW()
        {
            Dispose(false);
        }
    }
}
