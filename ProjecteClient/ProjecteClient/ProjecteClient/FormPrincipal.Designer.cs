
namespace ProjecteClient
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Connect_btn = new System.Windows.Forms.Button();
            this.LogInBtn = new System.Windows.Forms.Button();
            this.GroupConsultasBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EnviarBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.UsuariBtn = new System.Windows.Forms.RadioButton();
            this.PartMaxBtn = new System.Windows.Forms.RadioButton();
            this.PuntMaxBtn = new System.Windows.Forms.RadioButton();
            this.SigInBtn = new System.Windows.Forms.Button();
            this.DisconectBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ConexionActual = new System.Windows.Forms.Panel();
            this.ListaConectados = new System.Windows.Forms.DataGridView();
            this.ConectadosBtn = new System.Windows.Forms.Button();
            this.GroupLogSignInBox = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LogSignInBtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GroupConsultasBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).BeginInit();
            this.GroupLogSignInBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(12, 12);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(95, 75);
            this.Connect_btn.TabIndex = 0;
            this.Connect_btn.Text = "Conect";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // LogInBtn
            // 
            this.LogInBtn.Location = new System.Drawing.Point(113, 94);
            this.LogInBtn.Name = "LogInBtn";
            this.LogInBtn.Size = new System.Drawing.Size(92, 75);
            this.LogInBtn.TabIndex = 5;
            this.LogInBtn.Text = "Log In";
            this.LogInBtn.UseVisualStyleBackColor = true;
            this.LogInBtn.Click += new System.EventHandler(this.LogInBtn_Click);
            // 
            // GroupConsultasBox
            // 
            this.GroupConsultasBox.Controls.Add(this.label3);
            this.GroupConsultasBox.Controls.Add(this.EnviarBtn);
            this.GroupConsultasBox.Controls.Add(this.label1);
            this.GroupConsultasBox.Controls.Add(this.textBox1);
            this.GroupConsultasBox.Controls.Add(this.UsuariBtn);
            this.GroupConsultasBox.Controls.Add(this.PartMaxBtn);
            this.GroupConsultasBox.Controls.Add(this.PuntMaxBtn);
            this.GroupConsultasBox.Location = new System.Drawing.Point(217, 178);
            this.GroupConsultasBox.Name = "GroupConsultasBox";
            this.GroupConsultasBox.Size = new System.Drawing.Size(317, 156);
            this.GroupConsultasBox.TabIndex = 6;
            this.GroupConsultasBox.TabStop = false;
            this.GroupConsultasBox.Text = "Consultas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Jugador";
            // 
            // EnviarBtn
            // 
            this.EnviarBtn.Location = new System.Drawing.Point(214, 84);
            this.EnviarBtn.Name = "EnviarBtn";
            this.EnviarBtn.Size = new System.Drawing.Size(82, 53);
            this.EnviarBtn.TabIndex = 5;
            this.EnviarBtn.Text = "Enviar";
            this.EnviarBtn.UseVisualStyleBackColor = true;
            this.EnviarBtn.Click += new System.EventHandler(this.EnviarBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre del";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // UsuariBtn
            // 
            this.UsuariBtn.AutoSize = true;
            this.UsuariBtn.Location = new System.Drawing.Point(9, 75);
            this.UsuariBtn.Name = "UsuariBtn";
            this.UsuariBtn.Size = new System.Drawing.Size(127, 21);
            this.UsuariBtn.TabIndex = 2;
            this.UsuariBtn.TabStop = true;
            this.UsuariBtn.Text = "verficar usuario";
            this.UsuariBtn.UseVisualStyleBackColor = true;
            // 
            // PartMaxBtn
            // 
            this.PartMaxBtn.AutoSize = true;
            this.PartMaxBtn.Location = new System.Drawing.Point(9, 48);
            this.PartMaxBtn.Name = "PartMaxBtn";
            this.PartMaxBtn.Size = new System.Drawing.Size(310, 21);
            this.PartMaxBtn.TabIndex = 1;
            this.PartMaxBtn.TabStop = true;
            this.PartMaxBtn.Text = "Duracion de la partida más larga del jugador";
            this.PartMaxBtn.UseVisualStyleBackColor = true;
            // 
            // PuntMaxBtn
            // 
            this.PuntMaxBtn.AutoSize = true;
            this.PuntMaxBtn.Location = new System.Drawing.Point(9, 21);
            this.PuntMaxBtn.Name = "PuntMaxBtn";
            this.PuntMaxBtn.Size = new System.Drawing.Size(229, 21);
            this.PuntMaxBtn.TabIndex = 0;
            this.PuntMaxBtn.TabStop = true;
            this.PuntMaxBtn.Text = "jugador con maxima puntuacion";
            this.PuntMaxBtn.UseVisualStyleBackColor = true;
            // 
            // SigInBtn
            // 
            this.SigInBtn.Location = new System.Drawing.Point(12, 94);
            this.SigInBtn.Name = "SigInBtn";
            this.SigInBtn.Size = new System.Drawing.Size(95, 75);
            this.SigInBtn.TabIndex = 7;
            this.SigInBtn.Text = "Sign In";
            this.SigInBtn.UseVisualStyleBackColor = true;
            this.SigInBtn.Click += new System.EventHandler(this.SigInBtn_Click);
            // 
            // DisconectBtn
            // 
            this.DisconectBtn.Location = new System.Drawing.Point(113, 12);
            this.DisconectBtn.Name = "DisconectBtn";
            this.DisconectBtn.Size = new System.Drawing.Size(92, 74);
            this.DisconectBtn.TabIndex = 8;
            this.DisconectBtn.Text = "Disconect";
            this.DisconectBtn.UseVisualStyleBackColor = true;
            this.DisconectBtn.Click += new System.EventHandler(this.DisconectBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Conexión";
            // 
            // ConexionActual
            // 
            this.ConexionActual.Location = new System.Drawing.Point(84, 178);
            this.ConexionActual.Name = "ConexionActual";
            this.ConexionActual.Size = new System.Drawing.Size(23, 24);
            this.ConexionActual.TabIndex = 10;
            // 
            // ListaConectados
            // 
            this.ListaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaConectados.Location = new System.Drawing.Point(15, 220);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.RowHeadersWidth = 51;
            this.ListaConectados.RowTemplate.Height = 24;
            this.ListaConectados.Size = new System.Drawing.Size(92, 150);
            this.ListaConectados.TabIndex = 14;
            // 
            // ConectadosBtn
            // 
            this.ConectadosBtn.Location = new System.Drawing.Point(113, 220);
            this.ConectadosBtn.Name = "ConectadosBtn";
            this.ConectadosBtn.Size = new System.Drawing.Size(92, 55);
            this.ConectadosBtn.TabIndex = 13;
            this.ConectadosBtn.Text = "Ver Conectados";
            this.ConectadosBtn.UseVisualStyleBackColor = true;
            this.ConectadosBtn.Click += new System.EventHandler(this.ConectadosBtn_Click);
            // 
            // GroupLogSignInBox
            // 
            this.GroupLogSignInBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GroupLogSignInBox.Controls.Add(this.textBox3);
            this.GroupLogSignInBox.Controls.Add(this.label4);
            this.GroupLogSignInBox.Controls.Add(this.LogSignInBtn);
            this.GroupLogSignInBox.Controls.Add(this.textBox2);
            this.GroupLogSignInBox.Controls.Add(this.textBox4);
            this.GroupLogSignInBox.Controls.Add(this.label5);
            this.GroupLogSignInBox.Controls.Add(this.label6);
            this.GroupLogSignInBox.Location = new System.Drawing.Point(217, 17);
            this.GroupLogSignInBox.Name = "GroupLogSignInBox";
            this.GroupLogSignInBox.Size = new System.Drawing.Size(290, 133);
            this.GroupLogSignInBox.TabIndex = 15;
            this.GroupLogSignInBox.TabStop = false;
            this.GroupLogSignInBox.Text = "groupBox1";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(85, 96);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(89, 22);
            this.textBox3.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            // 
            // LogSignInBtn
            // 
            this.LogSignInBtn.Location = new System.Drawing.Point(194, 21);
            this.LogSignInBtn.Name = "LogSignInBtn";
            this.LogSignInBtn.Size = new System.Drawing.Size(90, 60);
            this.LogSignInBtn.TabIndex = 8;
            this.LogSignInBtn.Text = "Cerrar";
            this.LogSignInBtn.UseVisualStyleBackColor = true;
            this.LogSignInBtn.Click += new System.EventHandler(this.LogSignInBtn_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(85, 59);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(89, 22);
            this.textBox2.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(85, 23);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(89, 22);
            this.textBox4.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "label6";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(566, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 510);
            this.panel1.TabIndex = 16;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 553);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GroupLogSignInBox);
            this.Controls.Add(this.ListaConectados);
            this.Controls.Add(this.ConectadosBtn);
            this.Controls.Add(this.ConexionActual);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DisconectBtn);
            this.Controls.Add(this.GroupConsultasBox);
            this.Controls.Add(this.SigInBtn);
            this.Controls.Add(this.LogInBtn);
            this.Controls.Add(this.Connect_btn);
            this.Name = "FormPrincipal";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupConsultasBox.ResumeLayout(false);
            this.GroupConsultasBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).EndInit();
            this.GroupLogSignInBox.ResumeLayout(false);
            this.GroupLogSignInBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Button LogInBtn;
        private System.Windows.Forms.GroupBox GroupConsultasBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton UsuariBtn;
        private System.Windows.Forms.RadioButton PartMaxBtn;
        private System.Windows.Forms.RadioButton PuntMaxBtn;
        private System.Windows.Forms.Button EnviarBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SigInBtn;
        private System.Windows.Forms.Button DisconectBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel ConexionActual;
        private System.Windows.Forms.DataGridView ListaConectados;
        private System.Windows.Forms.Button ConectadosBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox GroupLogSignInBox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LogSignInBtn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
    }
}

