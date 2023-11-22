namespace BLL.Infrastructure.Models.Book;
public class BookCopyListItemModel
{
    public Guid BookCopyId { get; set; }

    public string RFID { get; set; }

    public string Condition { get; set; }

    public bool IsAvailable { get; set; }
}
