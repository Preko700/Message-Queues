# Message-Queues

## Descripción General

Message-Queues es una biblioteca en C# que proporciona una implementación robusta de patrones de colas de mensajes para construir sistemas distribuidos y asíncronos. Esta biblioteca simplifica el desarrollo de aplicaciones que necesitan procesamiento confiable de mensajes, funcionalidad de publicación/suscripción y arquitecturas dirigidas por eventos.

## Características

- **API Simple**: Interfaces fáciles de usar para publicar y consumir mensajes
- **Múltiples Tipos de Colas**: Soporte para diferentes patrones de colas (FIFO, prioridad, basadas en temas, etc.)
- **Opciones de Persistencia**: Almacenamiento de mensajes en memoria o persistente
- **Garantías de Entrega**: Opciones de entrega al menos una vez o exactamente una vez
- **Manejo de Errores**: Políticas de reintento incorporadas y colas de mensajes fallidos
- **Monitoreo**: Métricas de rendimiento y monitoreo de salud de las colas
- **Escalabilidad**: Capacidades de escalado horizontal para escenarios de alto rendimiento

## Instalación

### A través del Gestor de Paquetes NuGet

```
Install-Package Message-Queues
```

### A través de .NET CLI

```
dotnet add package Message-Queues
```

## Inicio Rápido

Aquí hay un ejemplo simple para empezar:

```csharp name=InicioRapido.cs
using Message.Queues;
using System;
using System.Threading.Tasks;

namespace MessageQueueDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Inicializar una cola
            var cola = new MessageQueue<string>("demo-cola");
            
            // Publicador
            await cola.PublishAsync("¡Hola Mundo!");
            
            // Consumidor
            cola.Subscribe(mensaje => {
                Console.WriteLine($"Recibido: {mensaje}");
                return Task.CompletedTask;
            });
            
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
```

## Documentación

### Crear una Cola

```csharp name=CrearCola.cs
// Crear una cola básica
var colaSimple = new MessageQueue<string>("mi-cola");

// Crear una cola con configuraciones personalizadas
var colaPersonalizada = new MessageQueue<Order>(new QueueOptions
{
    Name = "cola-pedidos",
    MaxQueueSize = 10000,
    PersistenceMode = PersistenceMode.Disk,
    DeliveryGuarantee = DeliveryGuarantee.AtLeastOnce
});
```

### Publicar Mensajes

```csharp name=PublicarMensajes.cs
// Publicar un mensaje
await cola.PublishAsync("Mensaje simple");

// Publicar con metadatos
await cola.PublishAsync(new Message<string>
{
    Body = "Mensaje con metadatos",
    Headers = new Dictionary<string, string>
    {
        { "Prioridad", "Alta" },
        { "IdCorrelacion", Guid.NewGuid().ToString() }
    }
});

// Publicar por lotes
await cola.PublishBatchAsync(new[] 
{
    "Mensaje 1",
    "Mensaje 2",
    "Mensaje 3"
});
```

### Consumir Mensajes

```csharp name=ConsumirMensajes.cs
// Suscripción básica
cola.Subscribe(mensaje => {
    Console.WriteLine($"Procesando: {mensaje}");
    return Task.CompletedTask;
});

// Con manejo de errores
cola.Subscribe(
    messageHandler: async mensaje => {
        // Procesar mensaje
        await ProcesarMensajeAsync(mensaje);
    },
    errorHandler: async (mensaje, excepcion) => {
        // Manejar error
        await LogErrorAsync(mensaje, excepcion);
        return ErrorAction.Requeue; // Reintentar el mensaje
    }
);

// Crear un grupo de consumidores (para procesamiento compartido)
var grupoConsumidores = cola.CreateConsumerGroup("procesadores-pedidos");
grupoConsumidores.Subscribe(mensaje => {
    // Solo un consumidor en el grupo recibirá cada mensaje
    return Task.CompletedTask;
});
```

## Configuración Avanzada

### Opciones de Cola

```csharp name=OpcionesCola.cs
var opciones = new QueueOptions
{
    // Configuraciones básicas
    Name = "mi-cola",
    MaxQueueSize = 100000,
    
    // Configuraciones de rendimiento
    BatchSize = 100,
    PrefetchCount = 50,
    
    // Configuraciones de confiabilidad
    PersistenceMode = PersistenceMode.Disk,
    DeliveryGuarantee = DeliveryGuarantee.ExactlyOnce,
    
    // Manejo de errores
    MaxRetryCount = 3,
    RetryDelay = TimeSpan.FromSeconds(5),
    EnableDeadLetterQueue = true
};
```

### Monitoreo

```csharp name=Monitoreo.cs
// Obtener estadísticas de la cola
QueueStats estadisticas = await cola.GetStatisticsAsync();
Console.WriteLine($"Longitud de Cola: {estadisticas.MessageCount}");
Console.WriteLine($"Cantidad de Consumidores: {estadisticas.ConsumerCount}");
Console.WriteLine($"Tiempo Promedio de Procesamiento: {estadisticas.AverageProcessingTime}ms");

// Registrarse para eventos
cola.OnMessagePublished += (sender, args) => {
    Console.WriteLine($"Mensaje publicado: {args.MessageId}");
};

cola.OnMessageProcessed += (sender, args) => {
    Console.WriteLine($"Mensaje procesado en {args.ProcessingTime}ms");
};
```

## Requisitos

- .NET 6.0 o superior
- Windows, Linux o macOS

## Contribuir

¡Las contribuciones son bienvenidas! No dudes en enviar un Pull Request.

1. Haz un fork del repositorio
2. Crea tu rama de características (`git checkout -b feature/caracteristica-asombrosa`)
3. Haz commit de tus cambios (`git commit -m 'Agregar alguna característica asombrosa'`)
4. Sube a la rama (`git push origin feature/caracteristica-asombrosa`)
5. Abre un Pull Request

## Licencia

Este proyecto está licenciado bajo la Licencia Apache2-0 - consulta el archivo LICENSE para más detalles.

## Agradecimientos

- Inspirado en sistemas populares de colas de mensajes como RabbitMQ, Apache Kafka y Azure Service Bus
- Construido con prácticas y patrones modernos de C#

---

Creado por [Preko700](https://github.com/Preko700)
