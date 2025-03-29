using System;

namespace MQBrokerNamespace.DataStructures
{
    /// <summary>
    /// Implementación de una lista enlazada simple.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos almacenados en la lista.</typeparam>
    public class LinkedList<T>
    {
        /// <summary>
        /// Nodo inicial de la lista.
        /// </summary>
        private Node<T> head;

        /// <summary>
        /// Nodo final de la lista.
        /// </summary>
        private Node<T> tail;

        /// <summary>
        /// Cantidad de elementos en la lista.
        /// </summary>
        private int count;

        /// <summary>
        /// Obtiene la cantidad de elementos en la lista.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Constructor que crea una nueva lista enlazada vacía.
        /// </summary>
        public LinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        /// <summary>
        /// Agrega un elemento al final de la lista.
        /// </summary>
        /// <param name="value">Valor a agregar.</param>
        public void Add(T value)
        {
            Node<T> node = new Node<T>(value);

            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                tail.Next = node;
                tail = node;
            }

            count++;
        }

        /// <summary>
        /// Elimina la primera ocurrencia de un valor específico.
        /// </summary>
        /// <param name="value">Valor a eliminar.</param>
        /// <returns>True si se eliminó el elemento, false si no se encontró.</returns>
        public bool Remove(T value)
        {
            if (head == null)
                return false;

            if (head.Value.Equals(value))
            {
                head = head.Next;
                count--;

                if (head == null)
                    tail = null;

                return true;
            }

            Node<T> current = head;
            while (current.Next != null)
            {
                if (current.Next.Value.Equals(value))
                {
                    if (current.Next == tail)
                        tail = current;

                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Busca un elemento en la lista.
        /// </summary>
        /// <param name="value">El valor a buscar.</param>
        /// <returns>True si el elemento existe en la lista, false en caso contrario.</returns>
        public bool Contains(T value)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                    return true;

                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Devuelve un array con todos los elementos de la lista.
        /// </summary>
        /// <returns>Array con los elementos de la lista.</returns>
        public T[] ToArray()
        {
            T[] array = new T[count];
            Node<T> current = head;
            int index = 0;

            while (current != null)
            {
                array[index++] = current.Value;
                current = current.Next;
            }

            return array;
        }

        /// <summary>
        /// Elimina todos los elementos de la lista.
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
    }
}