using System;

namespace JBOpale
{
    /// <summary>
    /// Méthodes d'extension pour les dates
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Premier jour (00:00) de la semaine contenant la date.
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="firstDay">Premier jour de la semaine (lundi par défaut)</param>
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek firstDay = DayOfWeek.Monday)
        {
            int diff = (7 + (date.DayOfWeek - firstDay)) % 7;
            return date.Date.AddDays(-diff);
        }

        /// <summary>
        /// Premier jour du mois à 00:00.
        /// </summary>
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Dernier jour du mois à 00:00.
        /// </summary>
        public static DateTime EndOfMonth(this DateTime date)
        {
            return date.StartOfMonth().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Dernière milliseconde du jour (23:59:59.999).
        /// </summary>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Indique si la date tombe un samedi ou un dimanche.
        /// </summary>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Indique si la date est comprise entre deux bornes (incluses).
        /// </summary>
        public static bool IsBetween(this DateTime date, DateTime from, DateTime to)
        {
            return date >= from && date <= to;
        }

        /// <summary>
        /// Calcule l'âge en années complètes à une date donnée (aujourd'hui par défaut).
        /// </summary>
        public static int Age(this DateTime birthDate, DateTime? at = null)
        {
            DateTime reference = (at ?? DateTime.Today).Date;
            int age = reference.Year - birthDate.Year;
            if (birthDate.Date > reference.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Convertit en secondes écoulées depuis le 01/01/1970 UTC.
        /// </summary>
        public static long ToUnixTimeSeconds(this DateTime date)
        {
            DateTime utc = date.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(date, DateTimeKind.Utc) : date.ToUniversalTime();
            return (long)(utc - UnixEpoch).TotalSeconds;
        }

        /// <summary>
        /// Convertit un horodatage Unix (secondes) en date UTC.
        /// </summary>
        public static DateTime FromUnixTimeSeconds(this long seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }

        /// <summary>
        /// Texte relatif à l'instant présent : "il y a 5 minutes", "dans 2 jours"...
        /// </summary>
        public static string ToRelativeTime(this DateTime date, DateTime? now = null)
        {
            DateTime reference = now ?? DateTime.Now;
            TimeSpan diff = reference - date;
            bool past = diff.TotalSeconds >= 0;
            double seconds = Math.Abs(diff.TotalSeconds);
            string text;
            if (seconds < 60) text = Plural((int)seconds, "seconde");
            else if (seconds < 3600) text = Plural((int)(seconds / 60), "minute");
            else if (seconds < 86400) text = Plural((int)(seconds / 3600), "heure");
            else if (seconds < 86400 * 30) text = Plural((int)(seconds / 86400), "jour");
            else if (seconds < 86400 * 365) text = (int)(seconds / (86400 * 30)) + " mois";
            else text = Plural((int)(seconds / (86400 * 365)), "an");
            return (past ? "il y a " : "dans ") + text;
        }

        private static string Plural(int n, string unit)
        {
            return n + " " + unit + (n > 1 ? "s" : "");
        }
    }
}
