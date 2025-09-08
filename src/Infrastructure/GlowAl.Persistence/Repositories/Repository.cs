using System.Linq.Expressions;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GlowAl.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    private GlowAlDbContext _context { get;}
    private DbSet<T> Table {  get;  }
    public Repository(GlowAlDbContext context)
    {
        _context = context;
        Table =_context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }
    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public async void Delete(T entity)
    {
        Table.Remove(entity);
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Table.FindAsync(id);
    }
    public IQueryable<T> GetByFiltered(Expression<Func<T, bool>>? predicate = null,
     Expression<Func<T, object>>[]? include = null,
     bool inTracking = false)
    {
        IQueryable<T> query = Table;
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }
        if (include is not null)
        {
            foreach (var includeExpression in include)
            {
                query = query.Include(includeExpression);
            }
        }
        if (!inTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public IQueryable<T> GetAll(bool isTracking = false)
    {
        if (!isTracking)
        {
            return Table.AsNoTracking();
        }
        return Table;
    }

    public IQueryable<T> GetAllFiltered(Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? include = null,
        Expression<Func<T, object>>? orderBy = null,
        bool isOrderByAsc = true,
        bool inTracking = false)
    {
        IQueryable<T> query = Table;
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }
        if (include is not null)
        {
            foreach (var includeExpression in include)
            {
                query = query.Include(includeExpression);
            }
        }
        if (orderBy is not null)
        {
            if (isOrderByAsc)
            {
                query = query.OrderBy(orderBy);
            }
            else
            {
                query = query.OrderByDescending(orderBy);
            }
        }
        if (!inTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }





    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }


}
