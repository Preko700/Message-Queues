using System;

namespace MQBrokerNamespace.DataStructures
{
    /// <summary>
    /// Implementación de una cola (FIFO) utilizando una lista enlazada.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos almacenados en la cola.</typeparam>
    public class Queue<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int count;

        /// <summary>
        /// Obtiene la cantidad de elementos en la cola.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Constructor que crea una nueva cola vacía.
        /// </summary>
        public Queue()
        {
            head = null;
            tail = null;
            count = 0;
        }

        /// <summary>
        /// Agrega un elemento al final de la cola.
        /// </summary>
        /// <param name="value">Valor a encolar.</param>
        public void Enqueue(T value)
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
        /// Elimina y devuelve el elemento al inicio de la cola.
        /// </summary>
        /// <returns>El valor del elemento al inicio de la cola.</returns>
        /// <exception cref="InvalidOperationException">Se lanza cuando la cola está vacía.</exception>
        public T Dequeue()
        {
            if (head == null)
                throw new InvalidOperationException("La cola está vacía");

            T value = head.Value;
            head = head.Next;
            count--;

            if (head == null)
                tail = null;

            return value;
        }

        /// <summary>
        /// Devuelve el elemento al inicio de la cola sin eliminarlo.
        /// </summary>
        /// <returns>El valor del elemento al inicio de la cola.</returns>
        /// <exception cref="InvalidOperationException">Se lanza cuando la cola está vacía.</exception>
        public T Peek()
        {
            if (head == null)
                throw new InvalidOperationException("La cola está vacía");

            return head.Value;
        }

        /// <summary>
        /// Verifica si la cola está vacía.
        /// </summary>
        /// <returns>True si la cola está vacía, false en caso contrario.</returns>
        public bool IsEmpty()
        {
            return count == 0;
        }

        /// <summary>
        /// Elimina todos los elementos de la cola.
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
    }
}