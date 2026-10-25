using System;
using System.Globalization;

namespace JBOpale
{
    /// <summary>
    /// Méthodes d'extension pour les nombres et valeurs comparables
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Indique si la valeur est comprise entre deux bornes (incluses).
        /// </summary>
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        /// <summary>
        /// Limite la valeur à l'intervalle [min, max].
        /// </summary>
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (min.CompareTo(max) > 0) throw new ArgumentException("min doit être inférieur ou égal à max");
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }

        /// <summary>
        /// Formate une taille en octets de façon lisible : "1,5 Ko", "3,2 Mo"...
        /// </summary>
        public static string ToFileSize(this long bytes, CultureInfo culture = null)
        {
            string[] units = { "o", "Ko", "Mo", "Go", "To", "Po" };
            double size = Math.Abs((double)bytes);
            int unit = 0;
            while (size >= 1024 && unit < units.Length - 1)
            {
                size /= 1024;
                unit++;
            }
            if (bytes < 0) size = -size;
            string format = unit == 0 ? "0" : "0.##";
            return size.ToString(format, culture ?? CultureInfo.CurrentCulture) + " " + units[unit];
        }

        /// <summary>
        /// Arrondit à l'entier le plus proche, les .5 allant vers le haut (comportement "commercial").
        /// </summary>
        public static decimal RoundHalfUp(this decimal value, int decimals = 0)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Pourcentage de la valeur par rapport au total (0 si le total est nul).
        /// </summary>
        public static double PercentOf(this double value, double total)
        {
            return total == 0 ? 0 : value / total * 100;
        }
    }
}
