namespace UltraGroup.Core.Application.Filters
{
    public record HotelFilter(string startDate, string EndDate, int Quantity, int LocationId)
    {
        HotelFilter() : this(string.Empty, string.Empty, 0, 0) { }
    }
}
