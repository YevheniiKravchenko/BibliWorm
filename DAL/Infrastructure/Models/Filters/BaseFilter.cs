using Microsoft.EntityFrameworkCore;

namespace DAL.Infrastructure.Models.Filters;
public class BaseFilter<TEntity>
    where TEntity : class
{
    public PagingModel PagingModel { get; set; }

    public virtual IQueryable<TEntity> Filter(DbSet<TEntity> entities)
    {
        return entities.AsQueryable();
    }
}
