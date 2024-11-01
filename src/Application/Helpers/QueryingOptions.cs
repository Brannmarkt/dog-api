namespace Application.Helpers;

public enum SortingOption
{
    Ascending,
    Descending
}

public record QueryingOptions
(
    SortingOption SortingOption, 
    int PageNumber = 1, 
    int PageSize = 10
);
