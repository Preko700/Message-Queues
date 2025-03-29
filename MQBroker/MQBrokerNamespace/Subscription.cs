using System;

namespace MQBrokerNamespace
{
    /// <summary>
    /// Clase que representa una suscripción a un tema.
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// Obtiene el identificador de la aplicación suscrita.
        /// </summary>
        public Guid AppId { get; private set; }

        /// <summary>
        /// Obtiene el nombre del tema al que está suscrito.
        /// </summary>
        public string TopicName { get; private set; }

        /// <summary>
        /// Constructor que crea una nueva suscripción.
        /// </summary>
        /// <param name="appId">Identificador de la aplicación.</param>
        /// <param name="topicName">Nombre del tema.</param>
        public Subscription(Guid appId, string topicName)
        {
            AppId = appId;
            TopicName = topicName;
        }

        /// <summary>
        /// Sobrescribe el método Equals para comparar suscripciones.
        /// </summary>
        /// <param name="obj">El objeto a comparar.</param>
        /// <returns>True si las suscripciones son iguales, false en caso contrario.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Subscription other)
            {
                return AppId.Equals(other.AppId) && TopicName.Equals(other.TopicName);
            }
            return false;
        }

        /// <summary>
        /// Sobrescribe el método GetHashCode para generar un hash code para la suscripción.
        /// </summary>
        /// <returns>El hash code de la suscripción.</returns>
        public override int GetHashCode()
        {
            return AppId.GetHashCode() ^ TopicName.GetHashCode();
        }
    }
}