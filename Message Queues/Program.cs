using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class MQBroker
{
    private TcpListener server;
    private ConcurrentDictionary<string, ConcurrentQueue<string>> queues;
    private List<TcpClient> clients;

    public MQBroker(string ip, int port)
    {
        server = new TcpListener(IPAddress.Parse(ip), port);
        queues = new ConcurrentDictionary<string, ConcurrentQueue<string>>();
        clients = new List<TcpClient>();
    }

    public void Start()
    {
        server.Start();
        Console.WriteLine("MQBroker iniciado...");
        AcceptClientsAsync();
    }

    private async void AcceptClientsAsync()
    {
        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            clients.Add(client);
            Console.WriteLine("Cliente conectado.");
            HandleClientAsync(client);
        }
    }

    private async void HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0) break;

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Mensaje recibido: {message}");

            // Procesar mensaje aquí (Subscribe, Publish, Receive, etc.)
        }
    }

    public static void Main()
    {
        MQBroker broker = new MQBroker("127.0.0.1", 5000);
        broker.Start();
    }
}

