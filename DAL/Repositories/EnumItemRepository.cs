using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class EnumItemRepository : IEnumItemRepository
{
    private readonly DbContextBase _dbContext;
    private readonly Lazy<IMapper> _mapper;

    private readonly DbSet<EnumItem> _enumItems;
    private readonly DbSet<Domain.Models.Enum> _enums;

    public EnumItemRepository(
        DbContextBase dbContext,
        Lazy<IMapper> mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _enumItems = dbContext.EnumItems;
        _enums = dbContext.Enums;
    }
    public void Create(EnumItem newEnumItem)
    {
        var enumEl = _enums.FirstOrDefault(e => e.EnumID == newEnumItem.EnumId)
            ?? throw new ArgumentException("INVALID_ENUM_ID");

        _enumItems.Add(newEnumItem);
        _dbContext.Commit();
    }

    public void Delete(int enumItemId)
    {
        var enumItemToDelete = _enumItems.FirstOrDefault(ei => ei.EnumItemId == enumItemId)
            ?? throw new ArgumentException("INVALID_ENUM_ITEM_ID");

        _enumItems.Remove(enumItemToDelete);
        _dbContext.Commit();
    }

    public IQueryable<EnumItem> GetAll()
    {
        return _enumItems.AsQueryable();
    }

    public EnumItem GetById(int enumItemId)
    {
        var enumItem = _enumItems.FirstOrDefault(ei => ei.EnumItemId == enumItemId)
            ?? throw new ArgumentException("ENUM_ITEM_NOT_FOUND");

        return enumItem;
    }

    public void Update(EnumItem updatedEnumItem)
    {
        var enumItem = _enumItems.FirstOrDefault(ei => ei.EnumItemId == updatedEnumItem.EnumItemId)
            ?? throw new ArgumentException("INVALID_ENUM_ITEM_ID");

        enumItem = _mapper.Value.Map(updatedEnumItem, enumItem);

        _dbContext.Commit();
    }
}
