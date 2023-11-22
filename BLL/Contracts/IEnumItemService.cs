using BLL.Infrastructure.Models.EnumItem;

namespace BLL.Contracts;
public interface IEnumItemService
{
    void AddEnumItem(EnumItemModel enumItemModel);

    void UpdateEnumItem(EnumItemModel enumItemModel);

    void DeleteEnumItem(int enumItemId);

    IEnumerable<EnumItemModel> GetAllEnumItems();

    EnumItemModel GetEnumItemById(int enumItemId);
}
