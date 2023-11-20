using Domain.Models;

namespace DAL.Contracts;
public interface IEnumItemRepository
{
    void Create(EnumItem newEnumItem);

    void Delete(int enumItemId);

    IQueryable<EnumItem> GetAll();

    EnumItem GetById(int enumItemId);

    void Update(EnumItem updatedEnumItem);
}
