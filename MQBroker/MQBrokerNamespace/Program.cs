using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MQBrokerNamespace.DataStructures;

namespace MQBrokerNamespace
{
    /// <summary>
    /// Clase principal del programa MQBroker.
    /// </summary>
    class Program
    {
        // Singleton del gestor de suscripciones
        private static SubscriptionManager subscriptionManager;
        // Singleton del gestor de temas
        private static TopicManager topicManager;
        // Singleton del gestor de colas
        private static QueueManager queueManager;

        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando MQBroker...");

            try
            {
                // Inicializar los gestores
                subscriptionManager = SubscriptionManager.GetInstance();
                topicManager = TopicManager.GetInstance();
                queueManager = QueueManager.GetInstance();

                // Dirección de escucha
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 5000;

                // Crear el servidor TCP
                TcpListener server = new TcpListener(ipAddress, port);
                server.Start();

                Console.WriteLine($"Servidor iniciado en {ipAddress}:{port}");
                Console.WriteLine("Esperando conexiones...");

                // Bucle infinito para atender conexiones
                while (true)
                {
                    // Aceptar conexión entrante
                    TcpClient client = server.AcceptTcpClient();

                    // Crear un hilo para procesar la conexión
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar el servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Maneja una conexión de cliente.
        /// </summary>
        /// <param name="obj">El cliente TCP.</param>
        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;

            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    // Leer el mensaje del cliente
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine($"Mensaje recibido: {message}");

                    // Procesar el mensaje y obtener una respuesta
                    string response = ProcessMessage(message);

                    // Enviar la respuesta al cliente
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);
                    stream.Flush();

                    Console.WriteLine($"Respuesta enviada: {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al manejar cliente: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                client.Close();
            }
        }

        /// <summary>
        /// Procesa un mensaje recibido y genera una respuesta.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>La respuesta al mensaje.</returns>
        private static string ProcessMessage(string message)
        {
            string[] parts = message.Split('|');

            if (parts.Length < 3)
                return "ERROR|Formato de mensaje inválido";

            string command = parts[0];
            Guid appId;

            try
            {
                appId = Guid.Parse(parts[1]);
            }
            catch
            {
                return "ERROR|AppID inválido";
            }

            string topicName = parts[2];

            switch (command)
            {
                case "SUBSCRIBE":
                    return HandleSubscribe(appId, topicName);

                case "UNSUBSCRIBE":
                    return HandleUnsubscribe(appId, topicName);

                case "PUBLISH":
                    if (parts.Length < 4)
                        return "ERROR|Formato de mensaje inválido para PUBLISH";

                    string content = parts[3];
                    return HandlePublish(appId, topicName, content);

                case "RECEIVE":
                    return HandleReceive(appId, topicName);

                default:
                    return "ERROR|Comando desconocido";
            }
        }

        /// <summary>
        /// Maneja la petición de suscripción.
        /// </summary>
        /// <param name="appId">ID de la aplicación.</param>
        /// <param name="topicName">Nombre del tema.</param>
        /// <returns>Respuesta al cliente.</returns>
        private static string HandleSubscribe(Guid appId, string topicName)
        {
            Subscription subscription = new Subscription(appId, topicName);

            if (subscriptionManager.IsSubscribed(subscription))
                return "OK|Ya estaba suscrito a este tema";

            // Crear el tema si no existe
            if (!topicManager.TopicExists(topicName))
                topicManager.CreateTopic(topicName);

            // Crear la suscripción
            subscriptionManager.AddSubscription(subscription);

            // Crear una cola para la suscripción
            queueManager.CreateQueue(subscription);

            return "OK|Suscripción realizada con éxito";
        }

        /// <summary>
        /// Maneja la petición de desuscripción.
        /// </summary>
        /// <param name="appId">ID de la aplicación.</param>
        /// <param name="topicName">Nombre del tema.</param>
        /// <returns>Respuesta al cliente.</returns>
        private static string HandleUnsubscribe(Guid appId, string topicName)
        {
            Subscription subscription = new Subscription(appId, topicName);

            if (!subscriptionManager.IsSubscribed(subscription))
                return "ERROR|No está suscrito a este tema";

            // Eliminar la suscripción
            subscriptionManager.RemoveSubscription(subscription);

            // Eliminar la cola de la suscripción
            queueManager.RemoveQueue(subscription);

            return "OK|Desuscripción realizada con éxito";
        }

        /// <summary>
        /// Maneja la petición de publicación.
        /// </summary>
        /// <param name="appId">ID de la aplicación.</param>
        /// <param name="topicName">Nombre del tema.</param>
        /// <param name="content">Contenido del mensaje.</param>
        /// <returns>Respuesta al cliente.</returns>
        private static string HandlePublish(Guid appId, string topicName, string content)
        {
            // Verificar si el tema existe
            if (!topicManager.TopicExists(topicName))
                return "ERROR|El tema no existe";

            // Obtener las suscripciones para este tema
            LinkedList<Subscription> subscriptions = subscriptionManager.GetSubscriptionsByTopic(topicName);

            if (subscriptions.Count == 0)
                return "OK|No hay suscriptores para este tema";

            Guid senderAppId = appId;
            if (parts.Length >= 5)
            {
                try
                {
                    senderAppId = Guid.Parse(parts[4]);
                }
                catch
                {
                    // Si hay error al parsear, usamos el appId original
                }
            }

            // Crear el mensaje
            MessageContent messageContent = new MessageContent(content, senderAppId);

            // Colocar el mensaje en la cola de cada suscriptor
            foreach (Subscription subscription in subscriptions.ToArray())
            {
                queueManager.EnqueueMessage(subscription, messageContent);
            }

            return "OK|Mensaje publicado con éxito";
        }

        /// <summary>
        /// Maneja la petición de recepción de mensajes.
        /// </summary>
        /// <param name="appId">ID de la aplicación.</param>
        /// <param name="topicName">Nombre del tema.</param>
        /// <returns>Respuesta al cliente con el mensaje o un error.</returns>
        private static string HandleReceive(Guid appId, string topicName)
        {
            Subscription subscription = new Subscription(appId, topicName);

            // Verificar si está suscrito
            if (!subscriptionManager.IsSubscribed(subscription))
                return "ERROR|No está suscrito a este tema";

            // Verificar si hay mensajes en la cola
            if (!queueManager.HasMessages(subscription))
                return "ERROR|No hay mensajes disponibles";

            // Obtener el mensaje
            MessageContent message = queueManager.DequeueMessage(subscription);

            return $"OK|{message.Content}|{message.SenderAppId}";
        }
    }
}