using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BusinessLogic.Utilities;
internal static class PasswordHasher
{
    internal static string HashPassword(string password)
    {
        using var hmac = new HMACSHA256();
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }
}
