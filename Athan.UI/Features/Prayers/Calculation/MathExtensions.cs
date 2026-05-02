using System;

namespace Athan.UI.Features.Prayers.Calculation;

internal static class MathExtensions
{
    extension(Math)
    {
        public static double DegreesToRadian(double value)
        {
            return value * Math.PI / 180D;
        }

        public static double RadianToDegrees(double value)
        {
            return value * 180D / Math.PI;
        }

        public static double Modulo(double value, double modulus)
        {
            return (value % modulus + modulus) % modulus;
        }
    }
}