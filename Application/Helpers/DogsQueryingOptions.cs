using Application.Interfaces;

namespace Application.Helpers;

public record DogsQueryingOptions(
    SortingOption SortingOption,
    DogSortingProperty? SortingProperty,
    int PageNumber = 1,
    int PageSize = 10) 
    : QueryingOptions (SortingOption, PageNumber, PageSize);


