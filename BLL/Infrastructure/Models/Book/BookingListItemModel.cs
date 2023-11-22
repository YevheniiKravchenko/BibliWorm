namespace BLL.Infrastructure.Models.Book;
public class BookingListItemModel
{
    public Guid BookingId { get; set; }

    public string BookedOn { get; set; }

    public string MustReturnOn { get; set; }

    public string BookTitle { get; set; }

    public bool IsReturned { get; set; }
}
