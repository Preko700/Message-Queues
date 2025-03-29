using System;

namespace MQBrokerNamespace
{
    /// <summary>
    /// Clase que representa el contenido de un mensaje.
    /// </summary>
    public class MessageContent
    {
        /// <summary>
        /// Obtiene el contenido del mensaje.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Obtiene la fecha y hora de creación del mensaje.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        public Guid SenderAppId { get; private set; } // Nueva propiedad

        /// <summary>
        /// Constructor que crea un nuevo mensaje con contenido específico.
        /// </summary>
        /// <param name="content">El contenido del mensaje.</param>
        public MessageContent(string content, Guid senderAppId)
        {
            Content = content;
            Timestamp = DateTime.Now;
            SenderAppId = senderAppId;
        }
    }
}