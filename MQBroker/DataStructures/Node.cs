using System;

namespace MQBrokerNamespace.DataStructures
{
    /// <summary>
    /// Clase que representa un nodo en una estructura de datos enlazada.
    /// </summary>
    /// <typeparam name="T">Tipo de dato almacenado en el nodo.</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Obtiene o establece el valor almacenado en el nodo.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Obtiene o establece el siguiente nodo en la estructura.
        /// </summary>
        public Node<T> Next { get; set; }

        /// <summary>
        /// Constructor que crea un nuevo nodo con un valor especificado.
        /// </summary>
        /// <param name="value">El valor a almacenar en el nodo.</param>
        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}