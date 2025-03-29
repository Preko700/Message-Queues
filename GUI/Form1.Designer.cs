namespace GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Contenedor de componentes requerido.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtAppID;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Button btnUnsubscribe;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.ListBox lstMessages;

        /// <summary>
        ///  Limpiar los recursos que se están utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados deben ser eliminados; de lo contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        ///  Método requerido para el soporte del Diseñador: no modificar
        ///  el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            txtIP = new TextBox();
            txtPort = new TextBox();
            txtAppID = new TextBox();
            txtTopic = new TextBox();
            txtMessage = new TextBox();
            btnConnect = new Button();
            btnSubscribe = new Button();
            btnUnsubscribe = new Button();
            btnPublish = new Button();
            btnReceive = new Button();
            lstMessages = new ListBox();
            SuspendLayout();
            // 
            // txtIP
            // 
            txtIP.Location = new Point(12, 12);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(100, 23);
            txtIP.TabIndex = 0;
            txtIP.Text = "127.0.0.1";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(118, 12);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(100, 23);
            txtPort.TabIndex = 1;
            txtPort.Text = "5000";
            // 
            // txtAppID
            // 
            txtAppID.Location = new Point(224, 12);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(100, 23);
            txtAppID.TabIndex = 2;
            txtAppID.Text = "00000000-0000-0000-0000-000000000000";
            txtAppID.TextChanged += txtAppID_TextChanged;
            // 
            // txtTopic
            // 
            txtTopic.Location = new Point(12, 41);
            txtTopic.Name = "txtTopic";
            txtTopic.Size = new Size(312, 23);
            txtTopic.TabIndex = 3;
            txtTopic.Text = "TestTopic";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 70);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(312, 23);
            txtMessage.TabIndex = 4;
            txtMessage.Text = "Hello, World!";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(330, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 5;
            btnConnect.Text = "Conectar";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSubscribe
            // 
            btnSubscribe.Location = new Point(330, 41);
            btnSubscribe.Name = "btnSubscribe";
            btnSubscribe.Size = new Size(75, 23);
            btnSubscribe.TabIndex = 6;
            btnSubscribe.Text = "Suscribir";
            btnSubscribe.UseVisualStyleBackColor = true;
            btnSubscribe.Click += btnSubscribe_Click;
            // 
            // btnUnsubscribe
            // 
            btnUnsubscribe.Location = new Point(330, 70);
            btnUnsubscribe.Name = "btnUnsubscribe";
            btnUnsubscribe.Size = new Size(75, 23);
            btnUnsubscribe.TabIndex = 7;
            btnUnsubscribe.Text = "Desuscribir";
            btnUnsubscribe.UseVisualStyleBackColor = true;
            btnUnsubscribe.Click += btnUnsubscribe_Click;
            // 
            // btnPublish
            // 
            btnPublish.Location = new Point(330, 99);
            btnPublish.Name = "btnPublish";
            btnPublish.Size = new Size(75, 23);
            btnPublish.TabIndex = 8;
            btnPublish.Text = "Publicar";
            btnPublish.UseVisualStyleBackColor = true;
            btnPublish.Click += btnPublish_Click;
            // 
            // btnReceive
            // 
            btnReceive.Location = new Point(330, 128);
            btnReceive.Name = "btnReceive";
            btnReceive.Size = new Size(75, 23);
            btnReceive.TabIndex = 9;
            btnReceive.Text = "Recibir";
            btnReceive.UseVisualStyleBackColor = true;
            btnReceive.Click += btnReceive_Click;
            // 
            // lstMessages
            // 
            lstMessages.FormattingEnabled = true;
            lstMessages.ItemHeight = 15;
            lstMessages.Location = new Point(12, 164);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(393, 274);
            lstMessages.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 444);
            Controls.Add(lstMessages);
            Controls.Add(btnReceive);
            Controls.Add(btnPublish);
            Controls.Add(btnUnsubscribe);
            Controls.Add(btnSubscribe);
            Controls.Add(btnConnect);
            Controls.Add(txtMessage);
            Controls.Add(txtTopic);
            Controls.Add(txtAppID);
            Controls.Add(txtPort);
            Controls.Add(txtIP);
            Name = "Form1";
            Text = "MQClient GUI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
