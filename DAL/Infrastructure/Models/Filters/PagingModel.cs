namespace DAL.Infrastructure.Models.Filters;
public class PagingModel
{
    public int PageSize { get; set; } = int.MaxValue;

    public int PageNumber { get; set; } = 1;
}
