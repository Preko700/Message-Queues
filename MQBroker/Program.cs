using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Agregar un retraso para dar tiempo a que MQBroker se inicie
        Thread.Sleep(2000); // 2 segundos

        Guid appId = Guid.NewGuid();
        MQClient client = new MQClient("127.0.0.1", 5000, appId);

        Topic topic = new Topic("TestTopic");
        Message message = new Message("Hello, World!");

        // Suscribirse al tema
        if (client.Subscribe(topic))
        {
            Console.WriteLine("Suscrito al tema: " + topic.Name);
        }
        else
        {
            Console.WriteLine("Error al suscribirse al tema: " + topic.Name);
        }

        // Publicar un mensaje en el tema
        if (client.Publish(message, topic))
        {
            Console.WriteLine("Mensaje publicado en el tema: " + topic.Name);
        }
        else
        {
            Console.WriteLine("Error al publicar el mensaje en el tema: " + topic.Name);
        }

        // Recibir un mensaje del tema
        try
        {
            Message receivedMessage = client.Receive(topic);
            Console.WriteLine("Mensaje recibido: " + receivedMessage.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al recibir el mensaje: " + ex.Message);
        }

        // Desuscribirse del tema
        if (client.Unsubscribe(topic))
        {
            Console.WriteLine("Desuscrito del tema: " + topic.Name);
        }
        else
        {
            Console.WriteLine("Error al desuscribirse del tema: " + topic.Name);
        }

        client.Close();

        // Esperar la entrada del usuario antes de cerrar
        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
