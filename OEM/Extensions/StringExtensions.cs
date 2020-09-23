using System;
namespace OEM.Extensions
{
    public static class StringExtensions
    {
        public static readonly int MAX_VIN = 17;

        public static bool IsValidVin(this string vin)
        {
            return vin.Length == MAX_VIN;
        }
    }
}
