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
        string nom, contra;
        public FormPrincipal()
        {
            InitializeComponent();
        }
        
        private void Connect_btn_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9068);


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
            EnviarBtn.Enabled = true;
            DisconectBtn.Enabled = true;
        }

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            if (PuntMaxBtn.Checked)
            {
                string mensaje = "3/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                try
                {
                    server.Send(msg);
                }
                finally
                {
                    MessageBox.Show("Error al enviar el mensaje");
                }
                
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("El jugador con mejor puntuación es: " + mensaje);
            }
            else if (PartMaxBtn.Checked)
            {
                string mensaje = "4/" + textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                try
                {
                    server.Send(msg);
                }
                finally
                {
                    MessageBox.Show("Error al enviar el mensaje");
                }
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("La partida con duracion máxima jugada por  " + textBox1.Text + " es: " + mensaje);

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
                    MessageBox.Show("El usuario"+textBox1+" existe");
                else
                    MessageBox.Show("Tu nombre NO bonito. Lo siento.");
            }
        }

        private void SigInBtn_Click(object sender, EventArgs e)
        {
            FormLogSigIn f1 = new FormLogSigIn();
            f1.SetId(2);
            f1.ShowDialog();
            //string id1;
            //Random r = new Random();
            //id1 =Convert.ToString( r.Next(1000,9000));
            this.nom = f1.GetNom();
            this.contra = f1.GetContra();
            string mensaje = "2/"+ this.nom +"/"+ this.contra;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
            

        }

        private void DisconectBtn_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            try
            {
                server.Send(msg);
            }
            finally
            {
                MessageBox.Show("Error al enviar el mensaje");
            }

            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            SigInBtn.Enabled = false;
            LogInBtn.Enabled = false;
            EnviarBtn.Enabled = false;
            DisconectBtn.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConexionActual.BackColor = Color.Gray;
            SigInBtn.Enabled = false;
            LogInBtn.Enabled = false;
            EnviarBtn.Enabled = false;
            DisconectBtn.Enabled = false;

            ListaConectados.RowCount = 8;
            ListaConectados.ColumnCount = 1;
            ListaConectados.ColumnHeadersVisible = false;
            ListaConectados.RowHeadersVisible = false;
            ListaConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

            string[] Nombres = new string[8];
            string nombre = null;
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < mensaje.Length; i++)
                {
                    if (mensaje[i] != '/')
                    {
                        nombre += mensaje[i];
                    }
                    else
                    {
                        Nombres[j] = nombre;
                    }
                }
            }

            //for(int p = 0, p < Nombres.Length; p++)
            //{
            //    ListaConectados.Rows[p].Cells[0].Value = Nombres;
            //}

            //DataGridView
            ListaConectados.Rows[1].Cells[0].Value = "Juan";  
            ListaConectados.Rows[2].Cells[0].Value = "Ernesto";
            ListaConectados.Rows[3].Cells[0].Value = "KK";
        }

        private void LogInBtn_Click(object sender, EventArgs e)
        {
            FormLogSigIn f1 = new FormLogSigIn();
            f1.SetId(1);
            f1.ShowDialog();

            this.nom = f1.GetNom();
            this.contra = f1.GetContra();


            string mensaje = "1/" + this.nom +"/"+ this.contra;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            //try
            //{
            //}
            //finally
            //{
            //    MessageBox.Show("Error al enviar el mensaje");
            //}

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }
    }
}
