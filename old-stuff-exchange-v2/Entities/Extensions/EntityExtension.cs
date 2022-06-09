using Microsoft.EntityFrameworkCore;

namespace old_stuff_exchange_v2.Entities.Extensions
{
    public static class EntityExtension
    {
        public static void ClearAll<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
