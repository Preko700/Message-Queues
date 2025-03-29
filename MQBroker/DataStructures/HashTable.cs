using System;

namespace MQBrokerNamespace.DataStructures
{
    /// <summary>
    /// Implementación básica de una tabla hash para simular un diccionario.
    /// </summary>
    /// <typeparam name="TKey">Tipo de las claves.</typeparam>
    /// <typeparam name="TValue">Tipo de los valores.</typeparam>
    public class HashTable<TKey, TValue>
    {
        private class KeyValuePair
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public KeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private LinkedList<KeyValuePair>[] buckets;
        private int count;
        private const int DefaultCapacity = 16;

        /// <summary>
        /// Obtiene la cantidad de elementos en la tabla.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Constructor que crea una nueva tabla hash con capacidad por defecto.
        /// </summary>
        public HashTable() : this(DefaultCapacity)
        {
        }

        /// <summary>
        /// Constructor que crea una nueva tabla hash con capacidad especificada.
        /// </summary>
        /// <param name="capacity">Capacidad inicial de la tabla.</param>
        public HashTable(int capacity)
        {
            buckets = new LinkedList<KeyValuePair>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = new LinkedList<KeyValuePair>();
            }
            count = 0;
        }

        /// <summary>
        /// Agrega o actualiza un elemento en la tabla.
        /// </summary>
        /// <param name="key">La clave del elemento.</param>
        /// <param name="value">El valor del elemento.</param>
        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            LinkedList<KeyValuePair> bucket = buckets[bucketIndex];

            foreach (KeyValuePair pair in bucket.ToArray())
            {
                if (key.Equals(pair.Key))
                {
                    bucket.Remove(pair);
                    bucket.Add(new KeyValuePair(key, value));
                    return;
                }
            }

            bucket.Add(new KeyValuePair(key, value));
            count++;
        }

        /// <summary>
        /// Elimina un elemento con la clave especificada.
        /// </summary>
        /// <param name="key">La clave del elemento a eliminar.</param>
        /// <returns>True si se eliminó el elemento, false si no se encontró.</returns>
        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            LinkedList<KeyValuePair> bucket = buckets[bucketIndex];

            foreach (KeyValuePair pair in bucket.ToArray())
            {
                if (key.Equals(pair.Key))
                {
                    bool result = bucket.Remove(pair);
                    if (result)
                        count--;
                    return result;
                }
            }

            return false;
        }

        /// <summary>
        /// Intenta obtener el valor asociado a una clave.
        /// </summary>
        /// <param name="key">La clave a buscar.</param>
        /// <param name="value">El valor asociado a la clave, si se encuentra.</param>
        /// <returns>True si se encontró la clave, false en caso contrario.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            LinkedList<KeyValuePair> bucket = buckets[bucketIndex];

            foreach (KeyValuePair pair in bucket.ToArray())
            {
                if (key.Equals(pair.Key))
                {
                    value = pair.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Verifica si la tabla contiene una clave específica.
        /// </summary>
        /// <param name="key">La clave a buscar.</param>
        /// <returns>True si la clave existe en la tabla, false en caso contrario.</returns>
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            LinkedList<KeyValuePair> bucket = buckets[bucketIndex];

            foreach (KeyValuePair pair in bucket.ToArray())
            {
                if (key.Equals(pair.Key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Elimina todos los elementos de la tabla.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i].Clear();
            }
            count = 0;
        }

        /// <summary>
        /// Obtiene un array con todas las claves en la tabla.
        /// </summary>
        /// <returns>Array con las claves.</returns>
        public TKey[] GetKeys()
        {
            TKey[] keys = new TKey[count];
            int index = 0;

            for (int i = 0; i < buckets.Length; i++)
            {
                foreach (KeyValuePair pair in buckets[i].ToArray())
                {
                    keys[index++] = pair.Key;
                }
            }

            return keys;
        }

        /// <summary>
        /// Calcula el índice del bucket basado en el hash code de la clave.
        /// </summary>
        /// <param name="key">La clave para la que se calcula el índice.</param>
        /// <returns>El índice del bucket.</returns>
        private int GetBucketIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }
    }
}