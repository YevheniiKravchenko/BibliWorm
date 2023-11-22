namespace BLL.Infrastructure.Models;
public class UserStatisticsModel
{
    public int UserId { get; set; }

    public int TotalBooksRead { get; set; }

    public string FavouriteGenre { get; set; }

    public string BiggestBookRead { get; set; }

    public int TotalPagesRead { get; set; }

    public string BookReadFastest { get; set; }

    public int CurrentlyReadingBooksAmount { get; set; }

    public int WrittenReviewsAmount { get; set; }
}
