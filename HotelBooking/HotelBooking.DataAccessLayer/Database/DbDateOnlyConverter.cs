using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelBooking.DataAccessLayer.Database;
internal class DbDateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DbDateOnlyConverter() : base(
       d => d.ToDateTime(TimeOnly.MinValue),
       d => DateOnly.FromDateTime(d))
    { }
}
