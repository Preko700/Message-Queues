# MQBroker y MQClient

## Descripción

Este proyecto implementa un sistema de mensajería para comunicar aplicaciones distribuidas de manera asincrónica utilizando un middleware orientado a colas de mensajes. El sistema incluye dos componentes principales:

1. **MQBroker**: Un programa consola en .NET que gestiona las colas y transmite los mensajes entre las colas utilizando una topología publicador/subscriptor.
2. **MQClient**: Una biblioteca (class library) en .NET que permite a los clientes interactuar con el broker.

## Objetivos

### General
- Implementar un sistema de mensajería para comunicar asincrónicamente aplicaciones distribuidas.

### Específicos
- Implementar las diferentes estructuras de datos lineales (listas, pilas y colas).
- Desarrollar algoritmos para dar solución a un problema.
- Fomentar la creatividad mediante el análisis y diseño de algoritmos.
- Utilizar diagramas de clases UML para modelar una solución a un problema.
- Aplicar patrones de diseño en la elaboración de una solución a un problema.

## Requerimientos

### MQBroker
- Al iniciar, se activa un socket para escuchar peticiones entrantes de los clientes.
- Escucha una petición llamada `Subscribe` para suscribirse a un tema.
- Escucha una petición llamada `Unsubscribe` para desuscribirse de un tema.
- Escucha una petición llamada `Publish` para publicar un mensaje en un tema.
- Escucha una petición llamada `Receive` para recibir un mensaje de un tema.

### MQClient
- `MQClient(string ip, int port, Guid AppID)`: Constructor que crea un nuevo MQClient.
- `bool Subscribe(Topic topic)`: Envía la petición `Subscribe` al MQBroker.
- `bool Unsubscribe(Topic topic)`: Envía la petición `Unsubscribe` al MQBroker.
- `bool Publish(Message message, Topic topic)`: Envía la petición `Publish` al MQBroker.
- `Message Receive(Topic topic)`: Envía la petición `Receive` al MQBroker.

## Estructura del Proyecto

- **MQBroker**: Contiene el programa consola que gestiona las colas de mensajes.
- **MQClient**: Contiene la biblioteca que permite a los clientes interactuar con el broker.
- **GUI**: Contiene una aplicación de Windows Forms para probar las funcionalidades del sistema MQBroker.


   
   
