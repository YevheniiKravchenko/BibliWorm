namespace Common.Constants;
public static class ValidationConstant
{
    #region User

    public const int NameMinLength = 4;
    public const int NameMaxLength = 50;

    public const int LoginMinLength = 4;
    public const int LoginMaxLength = 32;

    public const int AddressMinLength = 5;
    public const int AddressMaxLength = 100;

    public const int PasswordMinLength = 4;
    public const int PasswordMaxLength = 32;

    public const int EmailMinLength = 5;

    #endregion

    #region Book

    public const int BookTitleMinLength = 1;
    public const int BookTitleMaxLength = 200;

    public const int AuthorMaxLength = 252;

    public const int PublisherMaxLength = 252;

    public const int DescriptionMaxLength = 512;

    public const int PagesAmountMinValue = 1;
    public const int PagesAmountMaxValue = int.MaxValue;

    public const int KeyWordsMaxLength = 512;

    public const int BookCopyConditionMaxLength = 252;

    #endregion

    #region Booking

    public const int MustReturnInDaysMinValue = 1;
    public const int MustReturnInDaysMaxValue = int.MaxValue;

    #endregion

    #region BookReview

    public const double MarkMinValue = 1;
    public const double MarkMaxValue = 5;

    public const int CommentMinLength = 5;
    public const int CommentMaxLength = 1024;

    #endregion

    #region Department

    public const int DepartmentNameMaxLength = 32;

    public const int NumberOfPeopleAttendedMinValue = 0;
    public const int NumberOfPeopleAttendedMaxValue = int.MaxValue;

    #endregion

    #region Enum

    public const int EnumItemValueMaxLength = 64;

    #endregion
}