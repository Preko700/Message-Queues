using System;
using System.Windows.Forms;
using MQClient;
using Message = MQClient.Message; // Resolver ambigüedad

namespace GUI
{
    public partial class Form1 : Form
    {
        private MQClient client;
        private Guid appId;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Form1"/>.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // Generar un nuevo GUID automáticamente
            appId = Guid.NewGuid();

            // Mostrar el GUID en el campo txtAppID
            txtAppID.Text = appId.ToString();

            // Opcional: Deshabilitar el campo para que no se pueda editar
            txtAppID.ReadOnly = true;
        }

        /// <summary>
        /// Maneja el evento Click del control btnConnect.
        /// Conecta al MQBroker.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnConnect_Click(Object sender, EventArgs e)
        {
            string input = /* retrieve your input string */;
            if (Guid.TryParse(input, out Guid guid))
            {
                // Proceed with the valid GUID
            }
            else
            {
                MessageBox.Show("Invalid GUID format. Please enter a valid GUID.");
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnSubscribe.
        /// Se suscribe al tema especificado.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                Topic topic = new Topic(txtTopic.Text);
                if (client.Subscribe(topic))
                {
                    MessageBox.Show("Suscrito al tema: " + topic.Name);
                }
                else
                {
                    MessageBox.Show("Error al suscribirse al tema: " + topic.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al suscribirse: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnUnsubscribe.
        /// Se desuscribe del tema especificado.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                Topic topic = new Topic(txtTopic.Text);
                if (client.Unsubscribe(topic))
                {
                    MessageBox.Show("Desuscrito del tema: " + topic.Name);
                }
                else
                {
                    MessageBox.Show("Error al desuscribirse del tema: " + topic.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desuscribirse: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnPublish.
        /// Publica un mensaje en el tema especificado.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnPublish_Click(object sender, EventArgs e)
        {
            try
            {
                Topic topic = new Topic(txtTopic.Text);
                Message message = new Message(txtMessage.Text, appId);
                if (client.Publish(message, topic))
                {
                    MessageBox.Show("Mensaje publicado en el tema: " + topic.Name);
                }
                else
                {
                    MessageBox.Show("Error al publicar el mensaje en el tema: " + topic.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al publicar el mensaje: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnReceive.
        /// Recibe un mensaje del tema especificado.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                Topic topic = new Topic(txtTopic.Text);
                Message receivedMessage = client.Receive(topic);

                // Mostrar el mensaje incluyendo el AppID del remitente
                if (receivedMessage.SenderAppId != Guid.Empty)
                {
                    lstMessages.Items.Add($"Mensaje de {receivedMessage.SenderAppId}: {receivedMessage.Content}");
                }
                else
                {
                    lstMessages.Items.Add($"Mensaje recibido: {receivedMessage.Content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recibir el mensaje: " + ex.Message);
            }
        }

        private void txtAppID_TextChanged(object sender, EventArgs e)
        {
            // No implementation needed
        }
    }
}
