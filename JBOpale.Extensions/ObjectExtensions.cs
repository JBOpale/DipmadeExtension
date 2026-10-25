using System;
using System.ComponentModel;
using System.Text;

namespace JBOpale
{
    /// <summary>
    /// Méthodes d'extension pour les énumérations et les exceptions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Retourne le texte de l'attribut [Description] d'une valeur d'énumération, ou son nom à défaut.
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            if (value == null) return null;
            string name = value.ToString();
            System.Reflection.FieldInfo field = value.GetType().GetField(name);
            if (field == null) return name;
            DescriptionAttribute attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr != null ? attr.Description : name;
        }

        /// <summary>
        /// Convertit un texte en valeur d'énumération (insensible à la casse), ou retourne la valeur par défaut.
        /// </summary>
        public static T ToEnum<T>(this string value, T defaultValue = default(T)) where T : struct
        {
            T result;
            return Enum.TryParse(value, true, out result) && Enum.IsDefined(typeof(T), result) ? result : defaultValue;
        }

        /// <summary>
        /// Retourne le message de l'exception et de toutes ses exceptions internes.
        /// </summary>
        public static string GetFullMessage(this Exception ex, string separator = " > ")
        {
            if (ex == null) return "";
            StringBuilder sb = new StringBuilder();
            for (Exception current = ex; current != null; current = current.InnerException)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(current.Message);
            }
            return sb.ToString();
        }
    }
}
