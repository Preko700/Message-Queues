using System;
using MQBrokerNamespace.DataStructures;

namespace MQBrokerNamespace
{
    /// <summary>
    /// Gestor de temas (Patrón Singleton).
    /// </summary>
    public class TopicManager
    {
        private static TopicManager instance;
        private LinkedList<string> topics;

        // Lock para operaciones thread-safe
        private static readonly object lockObject = new object();

        /// <summary>
        /// Constructor privado para garantizar el patrón Singleton.
        /// </summary>
        private TopicManager()
        {
            topics = new LinkedList<string>();
        }

        /// <summary>
        /// Obtiene la instancia única del gestor de temas.
        /// </summary>
        /// <returns>La instancia del gestor de temas.</returns>
        public static TopicManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new TopicManager();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Crea un nuevo tema.
        /// </summary>
        /// <param name="topicName">El nombre del tema a crear.</param>
        /// <returns>True si se creó el tema, false si ya existía.</returns>
        public bool CreateTopic(string topicName)
        {
            lock (lockObject)
            {
                if (!TopicExists(topicName))
                {
                    topics.Add(topicName);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Elimina un tema.
        /// </summary>
        /// <param name="topicName">El nombre del tema a eliminar.</param>
        /// <returns>True si se eliminó el tema, false si no existía.</returns>
        public bool RemoveTopic(string topicName)
        {
            lock (lockObject)
            {
                return topics.Remove(topicName);
            }
        }

        /// <summary>
        /// Verifica si existe un tema.
        /// </summary>
        /// <param name="topicName">El nombre del tema a verificar.</param>
        /// <returns>True si el tema existe, false en caso contrario.</returns>
        public bool TopicExists(string topicName)
        {
            lock (lockObject)
            {
                return topics.Contains(topicName);
            }
        }

        /// <summary>
        /// Obtiene todos los temas existentes.
        /// </summary>
        /// <returns>Array con los nombres de los temas.</returns>
        public string[] GetAllTopics()
        {
            lock (lockObject)
            {
                return topics.ToArray();
            }
        }
    }
}