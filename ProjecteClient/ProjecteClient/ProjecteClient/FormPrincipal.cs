using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ProjecteClient
{
    public partial class FormPrincipal : Form
    {
        Socket server;
        int id;
        string nom, contra,contra2;
        public FormPrincipal()
        {
            InitializeComponent();
        }
        
        private void Connect_btn_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9090);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            ConexionActual.BackColor = Color.Green;
            SigInBtn.Enabled = true;
            LogInBtn.Enabled = true;
            
            DisconectBtn.Enabled = true;
        }

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            if (PuntMaxBtn.Checked)
            {
                string mensaje = "3/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                          
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("El jugador con mejor puntuación es: " + mensaje);
                mensaje = "";
            }
            else if (PartMaxBtn.Checked)
            {
                string mensaje = "4/" + textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("La partida con duracion máxima jugada por  " + textBox1.Text + " es: " + mensaje);
                mensaje = "";
            }
            else if(UsuariBtn.Checked)
            {
                // Enviamos nombre 
                string mensaje = "5/"+ textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];


                if (mensaje == "SI")
                {
                    MessageBox.Show("El usuario"+textBox1+" existe");
                    textBox1.Text = "";
                }
                else
                    MessageBox.Show("El usuario no existe");
            }
        }

        private void SigInBtn_Click(object sender, EventArgs e)
        {
            
            GroupLogSignInBox.Visible = true;
            label6.Visible = true;
            textBox3.Visible = true;
            GroupLogSignInBox.Text = "Registrarse";
            LogSignInBtn.Text = "Registrarse";
            this.id = 2;
            label4.Text = "Nombre";
            label5.Text = "Contraseña";
            label6.Text = "Contraseña";
        }

        private void DisconectBtn_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            SigInBtn.Enabled = false;
            LogInBtn.Enabled = false;
            EnviarBtn.Enabled = false;
            DisconectBtn.Enabled = false;
            ConexionActual.BackColor = Color.Gray;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConexionActual.BackColor = Color.Gray;
            SigInBtn.Enabled = false;
            LogInBtn.Enabled = false;
            EnviarBtn.Enabled = false;
            DisconectBtn.Enabled = false;
            GroupLogSignInBox.Visible = false;
            ConexionActual.BackColor = Color.Gray;
            ListaConectados.RowHeadersVisible = false;

        }

        private void ConectadosBtn_Click(object sender, EventArgs e)
        {
            // Enviamos nombre 
            string mensaje = "6/";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);


            //Recibimos la respuesta del servidor  Juan/Maria/Carlos/
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            string[] Conectados = mensaje.Split('/');
            DataTable dt = new DataTable();
            dt.Columns.Add("Conectados",typeof(string));

            foreach(string nombre in Conectados)
            {
                DataRow row = dt.NewRow();
                row["Conectados"] = nombre;
                dt.Rows.Add(row);
            }
            ListaConectados.DataSource = dt;
            ListaConectados.Refresh();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {

        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void LogSignInBtn_Click(object sender, EventArgs e)
        {
            if (this.id == 2) {
                this.nom = textBox4.Text;
                this.contra = textBox2.Text;
                this.contra2 = textBox3.Text;
                if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Uno de los campos está vacio");
                }
                else if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("Contraseñas diferentes por favor introduce denuevo la contraseña");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    string mensaje = "2/" + this.nom + "/" + this.contra;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor
                   byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    MessageBox.Show(mensaje);

                    this.nom = "";
                    this.contra = "";
                    this.contra2 = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    GroupLogSignInBox.Visible = false;
                    
                }
            }
            if(this.id ==1)
            {
                this.nom = textBox4.Text;
                this.contra = textBox2.Text;
                if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Uno de los campos está vacio");
                }
                else
                {
                    string mensaje = "1/" + this.nom + "/" + this.contra;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    if (mensaje == "No")
                    {
                        MessageBox.Show("Error con usuario o contraseña intentelo denuevo");
                        this.nom = "";
                        this.contra = "";
                        this.contra2 = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                    else { 
                        MessageBox.Show(mensaje);
                        EnviarBtn.Enabled = true;
                        this.nom = "";
                        this.contra = "";
                        this.contra2 = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        GroupLogSignInBox.Visible = false;
                        
                    }
                }
            }
        }

        private void LogInBtn_Click(object sender, EventArgs e)
        {
           

            GroupLogSignInBox.Visible = true;
            label6.Visible = false;
            textBox3.Visible = false;
            GroupLogSignInBox.Text = "loguearse";
            LogSignInBtn.Text = "loguearse";
            this.id = 1;
            label4.Text = "Nombre";
            label5.Text = "Contraseña";
        }
    }
}
