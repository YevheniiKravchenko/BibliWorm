namespace BLL.Infrastructure.Models.Book;
public class CreateUpdateBookCopyModel
{
    public Guid BookCopyId { get; set; }

    public string RFID { get; set; }

    public string Condition { get; set; }

    public bool IsAvailable { get; set; }

    public Guid BookId { get; set; }
}
