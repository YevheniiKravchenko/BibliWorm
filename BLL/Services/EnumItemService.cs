using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models.EnumItem;
using DAL.Contracts;
using Domain.Models;

namespace BLL.Services;
public class EnumItemService : IEnumItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Lazy<IMapper> _mapper;

    public EnumItemService(IUnitOfWork unitOfWork, Lazy<IMapper> mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IEnumerable<EnumItemModel> GetAllEnumItems()
    {
        var enumItems = _unitOfWork.EnumItems.Value.GetAll();
        var enumItemsModels = _mapper.Value.Map<List<EnumItemModel>>(enumItems);

        return enumItemsModels;
    }

    public void AddEnumItem(EnumItemModel enumItemModel)
    {
        var enumItem = _mapper.Value.Map<EnumItem>(enumItemModel);

        _unitOfWork.EnumItems.Value.Create(enumItem);
    }

    public void DeleteEnumItem(int enumItemId)
    {
        _unitOfWork.EnumItems.Value.Delete(enumItemId);
    }

    public EnumItemModel GetEnumItemById(int enumItemId)
    {
        var enumItem = _unitOfWork.EnumItems.Value.GetById(enumItemId);
        var enumItemModel = _mapper.Value.Map<EnumItemModel>(enumItem);

        return enumItemModel;
    }

    public void UpdateEnumItem(EnumItemModel enumItemModel)
    {
        var enumItem = _mapper.Value.Map<EnumItem>(enumItemModel);

        _unitOfWork.EnumItems.Value.Update(enumItem);
    }
}
