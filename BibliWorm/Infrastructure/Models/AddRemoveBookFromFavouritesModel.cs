namespace BibliWorm.Infrastructure.Models;

public class AddRemoveBookFromFavouritesModel
{
    public int UserId { get; set; }

    public Guid BookId { get; set; }
}
