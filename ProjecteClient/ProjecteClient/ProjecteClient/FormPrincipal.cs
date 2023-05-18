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
using System.Threading;

namespace ProjecteClient
{
    public partial class FormPrincipal : Form
    {
        Socket server;
        Thread atender;
        int id;
        bool conectado = false, loggued = false;
        string nom, contra,contra2;
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void AtenderServidor()
        {
            byte[] msg = new byte[80];
            byte[] msg2 = new byte[80];
            string mensaje;
            string[] respuesta;
            while (true)
            {

                //Array.Clear(msg, 0, msg.Length);
                //Recibimos la respuesta del servidor
                server.Receive(msg);

                mensaje = Encoding.ASCII.GetString(msg).Split('\0')[0];
                respuesta = mensaje.Split('/');
                try {
                    
                int codigo = Convert.ToInt32(respuesta[0]);
                

                int registrado;
                    switch (codigo)
                    {
                        case 1://Loguearse
                            registrado = Convert.ToInt32(respuesta[1]);
                            if (registrado == 1)
                            {
                                MessageBox.Show("Error al iniciar sesion, usuario o contraseña incorrecta, prueba a registrarte");
                                this.nom = "";
                                this.contra = "";
                                this.contra2 = "";
                                this.Invoke(new Action(() =>
                                {
                                    label7.Text = "Usuario: "+textBox4.Text;
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    textBox4.Text = "";
                                     
                                }));
                            }
                            else
                            {
                                this.Invoke(new Action(() =>
                                    {
                                        MessageBox.Show("Bienvenido " + textBox4.Text);
                                        EnviarBtn.Enabled = true;
                                        textBox2.Text = "";
                                        textBox3.Text = "";
                                        textBox4.Text = "";
                                        GroupLogSignInBox.Visible = false;
                                    }));
                                this.nom = "";
                                this.contra = "";
                                this.contra2 = "";
                                this.loggued = true;
                            }
                            break;
                        case 2://Registrarse
                            registrado = Convert.ToInt32(mensaje);
                            if (registrado == 2)
                            {
                                MessageBox.Show("Usuario registrado correctamente");
                                this.Invoke(new Action(() =>
                                    {
                                        textBox2.Text = "";
                                        textBox3.Text = "";
                                        textBox4.Text = "";
                                        GroupLogSignInBox.Visible = false;
                                    }));
                                this.nom = "";
                                this.contra = "";
                                this.contra2 = "";
                            }
                            else if (registrado == 1)
                            {
                                MessageBox.Show("Ya estas registrado, Inicia Sesión o escoge otro nombre");
                            }
                            break;
                        case 3://Consulta 1
                               //Recibimos la respuesta del servidor

                           // server.Receive(msg2);
                            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                            string[] c1 = mensaje.Split('/');
                            MessageBox.Show("Los jugadores con mayor puntuacion son: " + c1[2] + " con puntuacion: " + c1[3]);
                            mensaje = "";
                            break;
                        case 4://Consulta 2
                            //Recibimos la respuesta del servidor
                            //server.Receive(msg2);
                            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                            MessageBox.Show("La partida con duracion máxima jugada por  " + textBox1.Text + " es: " + mensaje);
                            mensaje = "";
                            break;
                        case 5://Consulta 3
                            //Recibimos la respuesta del servidor
                            //server.Receive(msg2);
                            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];


                            if (mensaje == "SI")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    textBox1.Text = "";
                                    MessageBox.Show("El usuario" + textBox1 + " existe");
                                }));
                            }
                            else
                                MessageBox.Show("El usuario no existe");
                            break;
                        case 6://ListaConectados
                            //Recibimos la respuesta del servidor  Juan/Maria/Carlos/
                            //server.Receive(msg2);
                            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                            string[] Conectados = mensaje.Split('/');
                            for (int i=0;i< Conectados.Length-1;i++) {
                                Conectados[i] = Conectados[i + 1];
                            }
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Conectados", typeof(string));

                            foreach (string nombre in Conectados)
                            {
                                DataRow row = dt.NewRow();
                                row["Conectados"] = nombre;
                                dt.Rows.Add(row);
                            }
                            this.Invoke(new Action(() =>
                            {
                                ListaConectados.DataSource = dt;
                                ListaConectados.Refresh();
                            }));
                            Array.Clear(Conectados, 0, Conectados.Length);
                            break;
                        }
                } 
                catch (System.FormatException) {
                    mensaje = "";
                    Array.Clear(respuesta,0,respuesta.Length);
                }
            }
        }
        private void Connect_btn_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9095);

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
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
            ConexionActual.BackColor = Color.Green;
            SigInBtn.Enabled = true;
            LogInBtn.Enabled = true;
            conectado = true;
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
            }
            else if (PartMaxBtn.Checked)
            {
                string mensaje = "4/" + textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if(UsuariBtn.Checked)
            {
                // Enviamos nombre 
                string mensaje = "5/"+ textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
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
            atender.Abort();
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            SigInBtn.Enabled = false;
            LogInBtn.Enabled = false;
            EnviarBtn.Enabled = false;
            DisconectBtn.Enabled = false;
            ConexionActual.BackColor = Color.Gray;
            this.conectado = false;
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
        }

      

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.conectado == true)
            {
                string mensaje = "0/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
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
