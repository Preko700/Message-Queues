namespace MQClient
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// Clase que representa un cliente de MQBroker.
    /// </summary>
    public class MQClient
    {
        private string ip;
        private int port;
        private Guid appId;

        /// <summary>
        /// Constructor que crea un nuevo MQClient.
        /// </summary>
        /// <param name="ip">IP donde está escuchando el MQBroker.</param>
        /// <param name="port">Puerto donde está escuchando el MQBroker.</param>
        /// <param name="appId">AppID generado por la aplicación que utiliza la biblioteca MQClient.</param>
        public MQClient(string ip, int port, Guid appId)
        {
            this.ip = ip;
            this.port = port;
            this.appId = appId;
        }

        /// <summary>
        /// Envía la petición Subscribe al MQBroker mediante un socket de tipo cliente.
        /// </summary>
        /// <param name="topic">Objeto Topic que encapsula el tema al que se quiere suscribir.</param>
        /// <returns>True si la suscripción se realizó, false en caso contrario.</returns>
        public bool Subscribe(Topic topic)
        {
            string message = $"SUBSCRIBE|{appId}|{topic.Name}";
            return SendMessage(message);
        }

        /// <summary>
        /// Envía la petición Unsubscribe al MQBroker mediante un socket de tipo cliente.
        /// </summary>
        /// <param name="topic">Objeto Topic que encapsula el tema del que se quiere desuscribir.</param>
        /// <returns>True si la desuscripción se realizó, false en caso contrario.</returns>
        public bool Unsubscribe(Topic topic)
        {
            string message = $"UNSUBSCRIBE|{appId}|{topic.Name}";
            return SendMessage(message);
        }

        /// <summary>
        /// Envía la petición Publish al MQBroker mediante un socket de tipo cliente.
        /// </summary>
        /// <param name="message">Objeto Message que encapsula el contenido de la publicación.</param>
        /// <param name="topic">Objeto Topic que encapsula el tema al que se desea publicar.</param>
        /// <returns>True si el mensaje se pudo colocar, false en caso contrario.</returns>
        public bool Publish(Message message, Topic topic)
        {
            string msg = $"PUBLISH|{appId}|{topic.Name}|{message.Content}";
            return SendMessage(msg);
        }

        /// <summary>
        /// Envía la petición Receive al MQBroker mediante un socket de tipo cliente.
        /// </summary>
        /// <param name="topic">Objeto Topic que encapsula el tema del que se desea obtener un mensaje.</param>
        /// <returns>Objeto Message con el contenido del mensaje recibido.</returns>
        /// <exception cref="Exception">Lanza una excepción si no se puede enviar el mensaje o si hay un error en la respuesta del servidor.</exception>
        public Message Receive(Topic topic)
        {
            string message = $"RECEIVE|{appId}|{topic.Name}";
            if (SendMessage(message, out string response))
            {
                var parts = response.Split('|');
                if (parts[0] == "OK")
                {
                    return new Message(parts[1]);
                }
                throw new Exception("Respuesta de error del servidor: " + response);
            }
            throw new Exception("No se pudo enviar el mensaje");
        }

        /// <summary>
        /// Envía un mensaje al MQBroker.
        /// </summary>
        /// <param name="message">El mensaje a enviar.</param>
        /// <returns>True si el mensaje se envió correctamente, false en caso contrario.</returns>
        private bool SendMessage(string message)
        {
            return SendMessage(message, out _);
        }

        /// <summary>
        /// Envía un mensaje al MQBroker y obtiene la respuesta.
        /// </summary>
        /// <param name="message">El mensaje a enviar.</param>
        /// <param name="response">La respuesta del servidor.</param>
        /// <returns>True si el mensaje se envió correctamente, false en caso contrario.</returns>
        private bool SendMessage(string message, out string response)
        {
            response = null;
            try
            {
                using (var client = new TcpClient(ip, port))
                using (var stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();

                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("IOException al enviar el mensaje: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción al enviar el mensaje: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cierra la conexión del cliente.
        /// </summary>
        public void Close()
        {
            // No es necesario cerrar nada aquí ya que estamos usando nuevas conexiones para cada mensaje
        }
    }

    /// <summary>
    /// Clase que representa un tema en el MQBroker.
    /// </summary>
    public class Topic
    {
        public string Name { get; }

        /// <summary>
        /// Constructor que crea un nuevo Topic.
        /// </summary>
        /// <param name="name">El nombre del tema.</param>
        public Topic(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Clase que representa un mensaje en el MQBroker.
    /// </summary>
    public class Message
    {
        public string Content { get; }

        /// <summary>
        /// Constructor que crea un nuevo Message.
        /// </summary>
        /// <param name="content">El contenido del mensaje.</param>
        public Message(string content)
        {
            Content = content;
        }
    }
}
