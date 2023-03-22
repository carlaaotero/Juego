
namespace ProjecteClient
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.UsuariBtn = new System.Windows.Forms.RadioButton();
            this.PartMaxBtn = new System.Windows.Forms.RadioButton();
            this.PuntMaxBtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.EnviarBtn = new System.Windows.Forms.Button();
            this.SigInBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(39, 22);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(112, 75);
            this.Connect_btn.TabIndex = 0;
            this.Connect_btn.Text = "Conectar";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // LogInBtn
            // 
            this.LogInBtn.Location = new System.Drawing.Point(182, 123);
            this.LogInBtn.Name = "LogInBtn";
            this.LogInBtn.Size = new System.Drawing.Size(112, 75);
            this.LogInBtn.TabIndex = 5;
            this.LogInBtn.Text = "Log In";
            this.LogInBtn.UseVisualStyleBackColor = true;
            this.LogInBtn.Click += new System.EventHandler(this.LogInBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EnviarBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.UsuariBtn);
            this.groupBox1.Controls.Add(this.PartMaxBtn);
            this.groupBox1.Controls.Add(this.PuntMaxBtn);
            this.groupBox1.Location = new System.Drawing.Point(366, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 176);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(124, 127);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // UsuariBtn
            // 
            this.UsuariBtn.AutoSize = true;
            this.UsuariBtn.Location = new System.Drawing.Point(36, 81);
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
            this.PartMaxBtn.Location = new System.Drawing.Point(36, 54);
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
            this.PuntMaxBtn.Location = new System.Drawing.Point(36, 27);
            this.PuntMaxBtn.Name = "PuntMaxBtn";
            this.PuntMaxBtn.Size = new System.Drawing.Size(229, 21);
            this.PuntMaxBtn.TabIndex = 0;
            this.PuntMaxBtn.TabStop = true;
            this.PuntMaxBtn.Text = "jugador con maxima puntuacion";
            this.PuntMaxBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "nombre Jugador";
            // 
            // EnviarBtn
            // 
            this.EnviarBtn.Location = new System.Drawing.Point(426, 21);
            this.EnviarBtn.Name = "EnviarBtn";
            this.EnviarBtn.Size = new System.Drawing.Size(82, 53);
            this.EnviarBtn.TabIndex = 5;
            this.EnviarBtn.Text = "Enviar";
            this.EnviarBtn.UseVisualStyleBackColor = true;
            this.EnviarBtn.Click += new System.EventHandler(this.EnviarBtn_Click);
            // 
            // SigInBtn
            // 
            this.SigInBtn.Location = new System.Drawing.Point(39, 123);
            this.SigInBtn.Name = "SigInBtn";
            this.SigInBtn.Size = new System.Drawing.Size(112, 75);
            this.SigInBtn.TabIndex = 7;
            this.SigInBtn.Text = "Registrarse";
            this.SigInBtn.UseVisualStyleBackColor = true;
            this.SigInBtn.Click += new System.EventHandler(this.SigInBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 450);
            this.Controls.Add(this.SigInBtn);
            this.Controls.Add(this.LogInBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Connect_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Button LogInBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton UsuariBtn;
        private System.Windows.Forms.RadioButton PartMaxBtn;
        private System.Windows.Forms.RadioButton PuntMaxBtn;
        private System.Windows.Forms.Button EnviarBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SigInBtn;
    }
}

