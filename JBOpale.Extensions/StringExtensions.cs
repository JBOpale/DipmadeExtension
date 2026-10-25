using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace JBOpale
{
    /// <summary>
    /// Méthodes d'extension pour les chaînes de caractères
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indique si la chaîne est null ou vide.
        /// </summary>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indique si la chaîne est null, vide ou ne contient que des espaces.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Retourne null si la chaîne est vide ou ne contient que des espaces, sinon la chaîne elle-même.
        /// </summary>
        public static string NullIfEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        /// <summary>
        /// Retourne la chaîne, ou la valeur par défaut si elle est null ou vide.
        /// </summary>
        public static string IfEmpty(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        /// <summary>
        /// Tronque la chaîne à une longueur maximale et ajoute un suffixe si elle a été coupée.
        /// La longueur du résultat, suffixe compris, ne dépasse pas <paramref name="maxLength"/>.
        /// </summary>
        /// <param name="value">Chaîne à tronquer</param>
        /// <param name="maxLength">Longueur maximale</param>
        /// <param name="suffix">Suffixe ajouté si la chaîne est coupée</param>
        public static string Truncate(this string value, int maxLength, string suffix = "...")
        {
            if (maxLength < 0) throw new ArgumentOutOfRangeException("maxLength");
            if (value == null || value.Length <= maxLength) return value;
            suffix = suffix ?? "";
            if (suffix.Length >= maxLength) return value.Substring(0, maxLength);
            return value.Substring(0, maxLength - suffix.Length) + suffix;
        }

        /// <summary>
        /// Retourne les <paramref name="length"/> premiers caractères.
        /// </summary>
        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return "";
            return value.Length <= length ? value : value.Substring(0, length);
        }

        /// <summary>
        /// Retourne les <paramref name="length"/> derniers caractères.
        /// </summary>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return "";
            return value.Length <= length ? value : value.Substring(value.Length - length);
        }

        /// <summary>
        /// Supprime les accents et signes diacritiques (é devient e, ç devient c...).
        /// </summary>
        public static string RemoveAccents(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            string normalized = value.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder(normalized.Length);
            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Transforme le texte en slug utilisable dans une URL ou un nom de fichier :
        /// minuscules, sans accents, mots séparés par des tirets.
        /// </summary>
        public static string ToSlug(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";
            string s = value.RemoveAccents().ToLowerInvariant();
            s = Regex.Replace(s, @"[^a-z0-9]+", "-");
            return s.Trim('-');
        }

        /// <summary>
        /// Met la première lettre de chaque mot en majuscule (culture courante).
        /// </summary>
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Comparaison avec choix de la casse, sans écrire de StringComparison.
        /// </summary>
        public static bool ContainsIgnoreCase(this string value, string search)
        {
            if (value == null || search == null) return false;
            return value.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Égalité insensible à la casse.
        /// </summary>
        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return string.Equals(value, other, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Convertit en entier, ou retourne null si la conversion échoue.
        /// </summary>
        public static int? ToIntOrNull(this string value)
        {
            int result;
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result) ? (int?)result : null;
        }

        /// <summary>
        /// Convertit en entier, ou retourne la valeur par défaut si la conversion échoue.
        /// </summary>
        public static int ToInt(this string value, int defaultValue = 0)
        {
            return value.ToIntOrNull() ?? defaultValue;
        }

        /// <summary>
        /// Convertit en décimal en acceptant le point ou la virgule, ou retourne null si la conversion échoue.
        /// </summary>
        public static decimal? ToDecimalOrNull(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            decimal result;
            string s = value.Trim().Replace(" ", "").Replace(",", ".");
            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out result) ? (decimal?)result : null;
        }

        /// <summary>
        /// Convertit "true", "1", "oui", "yes", "vrai", "o", "y" en vrai (insensible à la casse).
        /// </summary>
        public static bool ToBool(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            switch (value.Trim().ToLowerInvariant())
            {
                case "true":
                case "1":
                case "oui":
                case "o":
                case "yes":
                case "y":
                case "vrai":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Indique si la chaîne a la forme d'une adresse e-mail (contrôle de syntaxe simple).
        /// </summary>
        public static bool IsValidEmail(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.CultureInvariant);
        }

        /// <summary>
        /// Indique si la chaîne ne contient que des chiffres.
        /// </summary>
        public static bool IsDigitsOnly(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            foreach (char c in value)
            {
                if (c < '0' || c > '9') return false;
            }
            return true;
        }

        /// <summary>
        /// Encode la chaîne (UTF-8) en Base64.
        /// </summary>
        public static string ToBase64(this string value)
        {
            if (value == null) return null;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Décode une chaîne Base64 en texte UTF-8.
        /// </summary>
        public static string FromBase64(this string value)
        {
            if (value == null) return null;
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        /// Calcule l'empreinte SHA-256 (UTF-8) en hexadécimal minuscule.
        /// Ne convient pas au stockage de mots de passe.
        /// </summary>
        public static string ToSha256(this string value)
        {
            if (value == null) return null;
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        /// <summary>
        /// Remplace les espaces multiples (et retours à la ligne) par un seul espace et retire les espaces en bordure.
        /// </summary>
        public static string NormalizeSpaces(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return Regex.Replace(value, @"\s+", " ").Trim();
        }

        /// <summary>
        /// Remplace les caractères interdits dans un nom de fichier par <paramref name="replacement"/>
        /// (jeu Windows, appliqué quel que soit le système pour obtenir un résultat identique partout).
        /// </summary>
        public static string ToSafeFileName(this string value, char replacement = '_')
        {
            if (string.IsNullOrEmpty(value)) return value;
            char[] invalid = "<>:\"/\\|?*".ToCharArray();
            StringBuilder sb = new StringBuilder(value.Length);
            foreach (char c in value)
                sb.Append(c < 32 || Array.IndexOf(invalid, c) >= 0 ? replacement : c);
            return sb.ToString();
        }
    }
}
