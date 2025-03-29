using System;
using MQBrokerNamespace.DataStructures;

namespace MQBrokerNamespace
{
    /// <summary>
    /// Gestor de suscripciones (Patrón Singleton).
    /// </summary>
    public class SubscriptionManager
    {
        private static SubscriptionManager instance;
        private LinkedList<Subscription> subscriptions;

        // Lock para operaciones thread-safe
        private static readonly object lockObject = new object();

        /// <summary>
        /// Constructor privado para garantizar el patrón Singleton.
        /// </summary>
        private SubscriptionManager()
        {
            subscriptions = new LinkedList<Subscription>();
        }

        /// <summary>
        /// Obtiene la instancia única del gestor de suscripciones.
        /// </summary>
        /// <returns>La instancia del gestor de suscripciones.</returns>
        public static SubscriptionManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SubscriptionManager();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Agrega una nueva suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción a agregar.</param>
        public void AddSubscription(Subscription subscription)
        {
            lock (lockObject)
            {
                if (!IsSubscribed(subscription))
                {
                    subscriptions.Add(subscription);
                }
            }
        }

        /// <summary>
        /// Elimina una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción a eliminar.</param>
        /// <returns>True si se eliminó la suscripción, false si no existía.</returns>
        public bool RemoveSubscription(Subscription subscription)
        {
            lock (lockObject)
            {
                return subscriptions.Remove(subscription);
            }
        }

        /// <summary>
        /// Verifica si existe una suscripción.
        /// </summary>
        /// <param name="subscription">La suscripción a verificar.</param>
        /// <returns>True si la suscripción existe, false en caso contrario.</returns>
        public bool IsSubscribed(Subscription subscription)
        {
            lock (lockObject)
            {
                return subscriptions.Contains(subscription);
            }
        }

        /// <summary>
        /// Obtiene todas las suscripciones para un tema específico.
        /// </summary>
        /// <param name="topicName">El nombre del tema.</param>
        /// <returns>Lista de suscripciones para el tema.</returns>
        public LinkedList<Subscription> GetSubscriptionsByTopic(string topicName)
        {
            LinkedList<Subscription> result = new LinkedList<Subscription>();

            lock (lockObject)
            {
                foreach (Subscription subscription in subscriptions.ToArray())
                {
                    if (subscription.TopicName.Equals(topicName))
                    {
                        result.Add(subscription);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Obtiene todas las suscripciones para una aplicación específica.
        /// </summary>
        /// <param name="appId">El ID de la aplicación.</param>
        /// <returns>Lista de suscripciones para la aplicación.</returns>
        public LinkedList<Subscription> GetSubscriptionsByAppId(Guid appId)
        {
            LinkedList<Subscription> result = new LinkedList<Subscription>();

            lock (lockObject)
            {
                foreach (Subscription subscription in subscriptions.ToArray())
                {
                    if (subscription.AppId.Equals(appId))
                    {
                        result.Add(subscription);
                    }
                }
            }

            return result;
        }
    }
}