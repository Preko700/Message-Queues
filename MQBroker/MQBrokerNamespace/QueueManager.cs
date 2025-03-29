using System;
using MQBrokerNamespace.DataStructures;

namespace MQBrokerNamespace
{
    /// <summary>
    /// Gestor de colas de mensajes (Patrón Singleton).
    /// </summary>
    public class QueueManager
    {
        private static QueueManager instance;
        private HashTable<Subscription, Queue<MessageContent>> messageQueues;

        // Lock para operaciones thread-safe
        private static readonly object lockObject = new object();

        /// <summary>
        /// Constructor privado para garantizar el patrón Singleton.
        /// </summary>
        private QueueManager()
        {
            messageQueues = new HashTable<Subscription, Queue<MessageContent>>();
        }

        /// <summary>
        /// Obtiene la instancia única del gestor de colas.
        /// </summary>
        /// <returns>La instancia del gestor de colas.</returns>
        public static QueueManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new QueueManager();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Crea una cola para una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción para la que se crea la cola.</param>
        public void CreateQueue(Subscription subscription)
        {
            lock (lockObject)
            {
                if (!messageQueues.ContainsKey(subscription))
                {
                    messageQueues.Add(subscription, new Queue<MessageContent>());
                }
            }
        }

        /// <summary>
        /// Elimina la cola de una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción cuya cola se eliminará.</param>
        /// <returns>True si se eliminó la cola, false si no existía.</returns>
        public bool RemoveQueue(Subscription subscription)
        {
            lock (lockObject)
            {
                return messageQueues.Remove(subscription);
            }
        }

        /// <summary>
        /// Agrega un mensaje a la cola de una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción destinataria.</param>
        /// <param name="message">El mensaje a encolar.</param>
        /// <returns>True si se encoló el mensaje, false si la cola no existe.</returns>
        public bool EnqueueMessage(Subscription subscription, MessageContent message)
        {
            lock (lockObject)
            {
                if (messageQueues.TryGetValue(subscription, out Queue<MessageContent> queue))
                {
                    queue.Enqueue(message);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Extrae un mensaje de la cola de una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción de la que se extrae el mensaje.</param>
        /// <returns>El mensaje extraído.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si la cola no existe o está vacía.</exception>
        public MessageContent DequeueMessage(Subscription subscription)
        {
            lock (lockObject)
            {
                if (messageQueues.TryGetValue(subscription, out Queue<MessageContent> queue))
                {
                    if (!queue.IsEmpty())
                    {
                        return queue.Dequeue();
                    }
                    throw new InvalidOperationException("La cola está vacía");
                }
                throw new InvalidOperationException("La cola no existe");
            }
        }

        /// <summary>
        /// Verifica si hay mensajes en la cola de una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción a verificar.</param>
        /// <returns>True si hay mensajes, false en caso contrario.</returns>
        public bool HasMessages(Subscription subscription)
        {
            lock (lockObject)
            {
                if (messageQueues.TryGetValue(subscription, out Queue<MessageContent> queue))
                {
                    return !queue.IsEmpty();
                }
                return false;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de mensajes en la cola de una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción a consultar.</param>
        /// <returns>La cantidad de mensajes o 0 si la cola no existe.</returns>
        public int GetMessageCount(Subscription subscription)
        {
            lock (lockObject)
            {
                if (messageQueues.TryGetValue(subscription, out Queue<MessageContent> queue))
                {
                    return queue.Count;
                }
                return 0;
            }
        }
    }
}