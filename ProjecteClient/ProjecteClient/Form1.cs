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
    public partial class Form1 : Form
    {
        Socket server;
        string nom, contra;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Connect_btn_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("10.192.213.110");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
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
            Form2 f1 = new Form2();
            f1.SetId(2);
            f1.ShowDialog();

            this.nom = f1.GetNom();
            this.contra = f1.GetContra();


            string mensaje = "2/" + this.nom + this.contra;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //MessageBox.Show("Bienvenido " + this.nom + " con contraseña: " + this.contra);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }

        private void LogInBtn_Click(object sender, EventArgs e)
        {
            Form2 f1 = new Form2();
            f1.SetId(1);
            f1.ShowDialog();

            this.nom = f1.GetNom();
            this.contra = f1.GetContra();


            string mensaje = "1/" + this.nom + this.contra;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //MessageBox.Show("Bienvenido " + this.nom + " con contraseña: " + this.contra);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }
    }
}
