using Microsoft.EntityFrameworkCore;
using Phone.Data;
using System.Threading.Tasks;


namespace Phone.Repositories
{
    public abstract class MainRepository
    {

        protected ApplicationDbContext dbContext;

        public MainRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Method update-create profile or throw exception
        /// <summary>
        /// <returns>void</returns>
        public virtual async Task SaveAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbuException)
            {
                throw new DbUpdateException(dbuException.Message, dbuException);
            }
        }

    }
}
