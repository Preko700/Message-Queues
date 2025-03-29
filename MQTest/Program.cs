using System;
using System.Threading;
using MQClient;

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

        // Subscribe to the topic
        if (client.Subscribe(topic))
        {
            Console.WriteLine("Subscribed to topic: " + topic.Name);
        }
        else
        {
            Console.WriteLine("Failed to subscribe to topic: " + topic.Name);
        }

        // Publish a message to the topic
        if (client.Publish(message, topic))
        {
            Console.WriteLine("Published message to topic: " + topic.Name);
        }
        else
        {
            Console.WriteLine("Failed to publish message to topic: " + topic.Name);
        }

        // Receive a message from the topic
        try
        {
            Message receivedMessage = client.Receive(topic);
            Console.WriteLine("Received message: " + receivedMessage.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to receive message: " + ex.Message);
        }

        // Unsubscribe from the topic
        if (client.Unsubscribe(topic))
        {
            Console.WriteLine("Unsubscribed from topic: " + topic.Name);
        }
        else
        {
            Console.WriteLine("Failed to unsubscribe from topic: " + topic.Name);
        }

        client.Close();

        // Wait for user input before closing
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

