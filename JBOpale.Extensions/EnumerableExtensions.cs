using System;
using System.Collections.Generic;
using System.Linq;

namespace JBOpale
{
    /// <summary>
    /// Méthodes d'extension pour les collections et séquences
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Indique si la séquence est null ou ne contient aucun élément.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Retourne la séquence, ou une séquence vide si elle est null.
        /// </summary>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Retire les éléments null.
        /// </summary>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source) where T : class
        {
            return source.OrEmpty().Where(x => x != null);
        }

        /// <summary>
        /// Exécute une action sur chaque élément.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            foreach (T item in source.OrEmpty()) action(item);
        }

        /// <summary>
        /// Découpe la séquence en lots de taille fixe (le dernier lot peut être plus petit).
        /// Utile pour limiter une requête SQL "IN" ou un envoi par paquets.
        /// </summary>
        public static IEnumerable<List<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException("size");
            List<T> batch = new List<T>(size);
            foreach (T item in source.OrEmpty())
            {
                batch.Add(item);
                if (batch.Count == size)
                {
                    yield return batch;
                    batch = new List<T>(size);
                }
            }
            if (batch.Count > 0) yield return batch;
        }

#if !NET6_0_OR_GREATER
        /// <summary>
        /// Retourne les éléments distincts selon une clé.
        /// </summary>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            HashSet<TKey> seen = new HashSet<TKey>();
            foreach (T item in source.OrEmpty())
            {
                if (seen.Add(keySelector(item))) yield return item;
            }
        }

#endif

        /// <summary>
        /// Concatène les éléments avec un séparateur.
        /// </summary>
        public static string JoinToString<T>(this IEnumerable<T> source, string separator = ", ")
        {
            return string.Join(separator, source.OrEmpty().Select(x => x == null ? "" : x.ToString()));
        }

        /// <summary>
        /// Ajoute plusieurs éléments à une collection.
        /// </summary>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            foreach (T item in items.OrEmpty()) collection.Add(item);
        }

        /// <summary>
        /// Indique si la valeur fait partie de la liste (équivalent du IN de SQL).
        /// </summary>
        public static bool In<T>(this T value, params T[] values)
        {
            return values != null && values.Contains(value);
        }

#if NETFRAMEWORK
        /// <summary>
        /// Retourne la valeur associée à la clé, ou la valeur par défaut si la clé est absente.
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dictionary == null) return defaultValue;
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }
#endif

        /// <summary>
        /// Ajoute la valeur, ou remplace celle de la clé si elle existe déjà.
        /// </summary>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            dictionary[key] = value;
        }
    }
}
