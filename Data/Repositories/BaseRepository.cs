using Data.Context;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();



    //Create

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {

        if (entity == null)
            return null!;

        try
        {

            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
        catch (Exception exception)
        {
            Debug.WriteLine($"An error creating {nameof(TEntity)} :: {exception.Message}");
            return null!;
        }

    }
    //Read

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _db.ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return null!;
        return await _db.FirstOrDefaultAsync(expression) ?? null!;
    }


    //Update

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity? updatedEntity)
    {
        if (updatedEntity == null)
            return null!;

        try
        {
            var existingEntity = await _db.FirstOrDefaultAsync(expression) ?? null!;
            if (existingEntity == null)
                return null!;

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating {nameof(TEntity)} entity: {ex.Message}");
            return null!;
        }
    }

    //Delete

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
        {
            return false;
        }


        try
        {

            var existingEntity = await _db.FirstOrDefaultAsync(expression) ?? null!;

            if (existingEntity == null)
                return false;

            _db.RemoveRange(existingEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)}: {ex.Message}");

            return false;
        }
    }
}
