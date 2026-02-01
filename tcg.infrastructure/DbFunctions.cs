//References:
//[1]: Microsoft (2026) Microsoft.EntityFrameworkCore (Version 10.0.2). [Source Code] Available from: https://www.nuget.org/packages/microsoft.entityframeworkcore [Accessed 30/01/2026].

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

// *****Microsoft (2026)[1] - START

using Microsoft.EntityFrameworkCore;

// *****Microsoft (2026)[1] - END

//REF MYSELF FOR THESE FUNCTIONS
//and implement "get record by field name"

namespace TCG.Infrastructure
{
    public class DbFunctions
    {
        //private readonly AppDbContext _context;

        //public DbFunctions(AppDbContext context)
        //{
        //    _context = context;
        //}

        //public List<T> GetAll<T>() where T : class
        //{
        //    //using AppDbContext appDbContext = new();

        //    return _context.Set<T>()
        //        .AsNoTracking()
        //        .ToList();
        //}

        //public async Task<List<T>> GetAllAsync<T>() where T : class
        //{
        //    return await _context.Set<T>()
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        //public async Task<T?> GetByIdAsync<T>(object id) where T : class
        //{
        //    return await _context.Set<T>().FindAsync(id);
        //}

        //public async Task InsertRecord<T>(Dictionary<string, string> record) where T : class, new()
        //{
        //    T entity = new T();

        //    PropertyInfo[] properties = typeof(T).GetProperties();

        //    foreach (var i in record)
        //    {
        //        var property = properties.FirstOrDefault(p => p.Name == i.Key);
        //        if (property == null) continue;

        //        if (string.IsNullOrWhiteSpace(i.Value))
        //        {
        //            property.SetValue(entity, null);
        //            continue;
        //        }

        //        object? value;
        //        var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        //        if (targetType == typeof(string))
        //        {
        //            value = i.Value;
        //        }
        //        else if (targetType == typeof(DateOnly))
        //        {
        //            value = DateOnly.Parse(i.Value);
        //        }
        //        else
        //        {
        //            value = Convert.ChangeType(i.Value, targetType);
        //        }

        //        property.SetValue(entity, value);
        //    }

        //    _context.Set<T>().Add(entity);
        //    _context.SaveChanges();
        //}

        //public async Task UpdateAsync<T>(T entity) where T : class
        //{
        //    _context.Set<T>().Update(entity);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteByIdAsync<T>(object dbItem) where T : class
        //{
        //    var entity = await _context.Set<T>().FindAsync(dbItem);
        //    if (entity == null) return false;

        //    _context.Set<T>().Remove(entity);
        //    await _context.SaveChangesAsync();

        //    return true;
        //}
    }
}
