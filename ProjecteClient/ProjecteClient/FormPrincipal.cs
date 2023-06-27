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
        int id, numinvitados, participantes=0, puerto = 50057;
        bool conectado = false, loggued = false;
        string nom, contra, contra2, MiPartida, InvitacionPartida = "-1", MiColor; //strings para almacenar nombres, contraseñas, y partidas, InvitacionPartidas inicialmente es "-1" para indicar que no poseemos
                                                                                   //partida, cuando aceptamos partida se actualiza 
        char lider = 'P';//Char que indica si es lider o Jugador
        List<PictureBox> MisPictureBox = new List<PictureBox>();
        public FormPrincipal()
        {
            InitializeComponent();
        }
        
        private void AtenderServidor()
        {
            byte[] msg = new byte[80];
            string mensaje;
            string[] respuesta;
            while (true)
            {
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
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    textBox4.Text = "";

                                }));
                            }
                            else
                            {
                                this.Invoke(new Action(() =>
                                {
                                    label7.Text = textBox4.Text;
                                    //EnviarBtn.Enabled = true;
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    textBox4.Text = "";
                                    GroupLogSignInBox.Visible = false;
                                    ChatGlobalBox.Visible = true;
                                    GlobalScreen.AppendText("Servidor: se te ha añadido al chat global " + Environment.NewLine);
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
                            
                            break;
                        case 4://Consulta 2
                            
                            break;
                        case 5://Consulta 3
                            
                            break;
                        case 6://ListaConectados
                               //Recibimos la respuesta del servidor  Juan/Maria/Carlos/
                            string[] Conectados = mensaje.Split('/');
                            for (int i = 0; i < Conectados.Length - 1; i++)
                            {
                                Conectados[i] = Conectados[i + 1];
                            }
                            
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Conectados", typeof(string));

                            foreach (string nombre in Conectados)
                            {
                                if(nombre!="")
                                {
                                    DataRow row = dt.NewRow();
                                    row["Conectados"] = nombre;
                                    dt.Rows.Add(row);
                                }
                            }
                            this.Invoke(new Action(() =>
                            {
                                ListaConectados.DataSource = dt;
                                ListaConectados.Refresh();
                            }));
                            Array.Clear(Conectados, 0, Conectados.Length);
                            break;
                        case 7://Notificacion de Invitacion a partida
                            this.InvitacionPartida = respuesta[1];
                            this.MiPartida = respuesta[1];
                            this.Invoke(new Action(() =>
                            {
                                InvitacionBox.Visible = true;
                                InvitacionLbl2.Text = respuesta[2];
                                ChatLocalBox.Visible = true;
                            }));
                            break;
                        case 8://protocolo de respuesta a partida
                            if (respuesta[1] == "1")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    LocalScreen.AppendText("Servidor: se ha añadido a " + respuesta[2] + Environment.NewLine);
                                    participantes++;
                                }));
                            }
                            if (respuesta[1] == "2")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    LocalScreen.AppendText("Servidor: " + respuesta[2] + " ha rechazado la invitación" + Environment.NewLine);
                                }));
                            }
                            if (respuesta[1] == "3")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    LocalScreen.AppendText("Servidor: tu partida está llena, no invites a más personas" + Environment.NewLine);
                                }));
                            }
                            if (respuesta[1] == "4") {
                                this.MiPartida = respuesta[2];
                                this.InvitacionPartida = respuesta[2];
                                this.Invoke(new Action(() =>
                                {
                                    LocalScreen.AppendText("Servidor: se ha añadido al grupo número " + respuesta[2] + Environment.NewLine);
                                    LocalScreen.AppendText("Servidor: se ha añadido a " + label7.Text + Environment.NewLine);
                                }));
                            }
                            if (respuesta[1] == "5")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    GlobalScreen.AppendText("Servidor: todas las personas que has invitado te han rechazado, intentalo de nuevo" + Environment.NewLine);
                                    ChatLocalBox.Visible = false;
                                    EmpezarPartidaBtn.Visible = false;
                                }));
                            }
                            if (respuesta[1] == "6")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    GlobalScreen.AppendText("Servidor: No hay partidas disponibles, intentalo más tarde");
                                    ChatLocalBox.Visible = false;
                                }));
                            }
                            break;
                        case 9://protocolo para mensajes globales y locales
                            if (respuesta[1] == "1")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    GlobalScreen.AppendText(respuesta[2] + ": " + respuesta[3] + Environment.NewLine);
                                }));
                            }
                            else if (respuesta[1] == "2")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    LocalScreen.AppendText(respuesta[2] + ": " + respuesta[3] + Environment.NewLine);
                                }));
                            }
                            break;
                        //Cases para el juego
                        case 10://
                            MiColor = respuesta[1];//Me guardo el color
                            this.Invoke(new Action(() =>
                            {
                                Color c = ColorTranslator.FromHtml(MiColor);
                                PColorBox.BackColor = c;
                                GrupoJuegoBox.Visible = true;
                                //GrupoJuegoBox.Enabled = false;
                                LocalScreen.AppendText("Servidor: ¡Que empieze la partida!" + Environment.NewLine);
                            }));
                            break;
                        case 11:
                            if (respuesta[1] == "1")
                            {
                                this.Invoke(new Action(() =>
                                {
                                    GrupoJuegoBox.Enabled = true;
                                    LocalScreen.AppendText("Servidor: Tu turno" + Environment.NewLine);
                                }));

                            }
                            else if (respuesta[1] == "-1") {
                                this.Invoke(new Action(() =>
                                {
                                    LocalScreen.AppendText("Servidor: jugadores insuficientes" + Environment.NewLine);
                                }));
                            }
                            else
                            {
                                string elemento = respuesta[1];
                                this.Invoke(new Action(() =>
                                {
                                    string color = respuesta[2];
                                    CambiaElColorEnGroupBox(this, "GrupoJuegoBox",respuesta[1],color);
                                }));
                            }
                            break;
                        case 12:
                            this.Invoke(new Action(() =>
                            {
                                if(respuesta[1]=="1")
                                {
                                    LocalScreen.AppendText("Servidor: La partida ha acabado, ha ganado " + respuesta[2] + Environment.NewLine);
                                    LocalScreen.AppendText("Servidor: El lider puede empezar otra partida" + Environment.NewLine);
                                    ReiniciarColores(this);
                                    MisPictureBox.Clear();
                                    GrupoJuegoBox.Visible = false;
                                    EmpezarPartidaBtn.Enabled = true;
                                }
                                else if (respuesta[1] == "2")
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        LocalScreen.AppendText("Servidor: La partida ha resultado en empate, puedes empezar un nuevo juego " + respuesta[2] + Environment.NewLine);
                                        ReiniciarColores(this);
                                        MisPictureBox.Clear();
                                        GrupoJuegoBox.Visible = false;
                                        EmpezarPartidaBtn.Enabled = true;
                                    }));
                                }
                            }));
                            break;
                    }
                }
                catch (System.FormatException) {
                    mensaje = "";
                    Array.Clear(respuesta, 0, respuesta.Length);
                }
            }
        }
        
        public void CambiaElColorEnGroupBox(FormPrincipal formPrincipal, string groupName, string posicion, string color)
        {
            GroupBox groupBox = formPrincipal.Controls.OfType<GroupBox>().FirstOrDefault(g => g.Name == groupName);
            if (groupBox != null)
            {
                CambiaElColor(groupBox, posicion, color);
            }
        }

        public void CambiaElColor(GroupBox groupBox, string posicion, string color)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    if (pictureBox.Name == posicion)
                    {
                        Color c = ColorTranslator.FromHtml(color);
                        pictureBox.BackColor = c;
                    }
                }
            }
        }


        public void ReiniciarColores(FormPrincipal FormPrincipal)
        {
            for (char i = 'A'; i <= 'F'; i++) 
            {
                for (char j = '1'; j <= '6'; j++) 
                {
                    string pos = Convert.ToString(i) + Convert.ToString(j);
                    string col = "#FF6600";
                    CambiaElColorEnGroupBox(FormPrincipal, "GrupoJuegoBox", pos, col);
                }
            }
        }

        private void Connect_btn_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, puerto);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                //MessageBox.Show("Conectado");

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
            DisconectBtn.Enabled = true;
            conectado = true;
            Connect_btn.Enabled = false;
        }

        

        private void SigInBtn_Click(object sender, EventArgs e)
        {
            
            GroupLogSignInBox.Visible = true;
            label6.Visible = true;
            textBox3.Visible = true;
            GroupLogSignInBox.Text = "Registrate, introduce usuario, contraseña y repite la contraseña";
            LogSignInBtn.Text = "Registrarse";
            this.id = 2;
            label4.Text = "Usuario";
            label5.Text = "Contraseña";
            label6.Text = "Contraseña";
        }

        private void DisconectBtn_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/"+this.MiPartida;
            Invitadoslbl.Text = "";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            SigInBtn.Enabled = false;
            LogInBtn.Enabled = false;
            //EnviarBtn.Enabled = false;
            DisconectBtn.Enabled = false;
            ConexionActual.BackColor = Color.Gray;
            this.conectado = false;
            GroupLogSignInBox.Visible = false;
            InvitacionBox.Visible = false;
            InvitacionBox.Visible = false;
            this.numinvitados = 0;
            this.MiPartida = "-1";
            this.InvitacionPartida = "-1";
            Connect_btn.Enabled = true;
            LogInBtn.Enabled = true;
            SigInBtn.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReiniciarColores(this);
            //Botones
            SigInBtn.Enabled = false;
            LogInBtn.Enabled = false;
           //EnviarBtn.Enabled = false;
            DisconectBtn.Enabled = false;
            EmpezarPartidaBtn.Visible = false;

            //Group Box
            GroupLogSignInBox.Visible = false;
            InvitacionBox.Visible = false;
            GrupoJuegoBox.Visible = false;
            GrupoJuegoBox.Enabled = true;
            ChatGlobalBox.Visible = false;
            ChatLocalBox.Visible = false;

            //Paneles
            

            //Labels
            Invitadoslbl.Text = "";
            //DataGridsView
            ListaConectados.RowHeadersVisible = false;
        }

        private void GlobalBtn_Click(object sender, EventArgs e)
        {
            string mensaje = "8/"+label7.Text+"/"+GlobalMsg.Text;

            GlobalMsg.Text = "";
            // Enviamos al servidor el mensaje
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void LocalBtn_Click(object sender, EventArgs e)
        {
            string mensaje = "9/" +this.MiPartida + "/" +label7.Text + "/" +LocalMsg.Text+"/";
            
            LocalMsg.Text = "";
            // Enviamos al servidor el mensaje
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void EmpezarPartidaBtn_Click(object sender, EventArgs e)
        {
            string mensaje = "10/" + this.MiPartida;
            // Enviamos al servidor empezar partida
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            EmpezarPartidaBtn.Enabled = false;
        }
        public bool ComprobarGane(PictureBox p,List<PictureBox> lp) //Funcion para comprobar si un usuario ha ganado su partida
        {
            bool Coinciden3= false;
            foreach (PictureBox p2 in lp) 
            {
                if (p2.Name != p.Name)
                {
                    //Comprobamos si son circundantes
                    bool cerca2 = ComprobarCercania(p, p2);
                    if (cerca2 == true)
                    {
                        //Si lo son, busca algun otro circundante que coincida con dicha recta 
                        foreach (PictureBox p3 in lp)
                        {
                            if ((p3.Name != p2.Name) && (p3.Name != p.Name))
                            {
                                bool cerca3 = ComprobarTresEnRaya(p,p2,p3);
                                if (cerca3 == true)
                                {
                                    Coinciden3 = true;
                                    break;
                                }
                            }
                        }
                        if (Coinciden3 == true)
                            break;
                    }
                }
            }
            
            return Coinciden3;
        }
        public string TraducemePosiciones(string posicion) 
        {
            /* Traduce las posciones A1,A2,A3... F1,F2,F3 a enteros*/
            char letra = posicion[0];

            string numero = posicion[1].ToString();

            int valLetra = letra - 'A' + 1;
            return valLetra.ToString() + "," + numero;
        }

        public bool ComprobarTresEnRaya(PictureBox p1, PictureBox p2, PictureBox p3)
        {
            int x1, x2, x3, y1, y2, y3;
            bool TresEnRaya = false;
            
            string PC1 = TraducemePosiciones(p1.Name);
            string PC2 = TraducemePosiciones(p2.Name);
            string PC3 = TraducemePosiciones(p3.Name);

            string[] coords1 = PC1.Split(',');
            string[] coords2 = PC2.Split(',');
            string[] coords3 = PC3.Split(',');
            
            
            x1 = Convert.ToInt32(coords1[0]);
            x2 = Convert.ToInt32(coords2[0]);
            x3 = Convert.ToInt32(coords3[0]);

            y1 = Convert.ToInt32(coords1[1]);
            y2 = Convert.ToInt32(coords2[1]);
            y3 = Convert.ToInt32(coords3[1]);

            bool cercaAp1 = ComprobarCercania(p3, p1);
            bool cercaAp2 = ComprobarCercania(p3, p2);

            if ((cercaAp1 == true) || (cercaAp2 == true))
            {
                int dify1 = (y3 - y1);
                int dify2 = (y3 - y2);

                int difx1 = (x3 - x1);
                int difx2 = (x3 - x2);
                if ((difx1 != 0) && (difx2 !=0))
                { 
                    int pendiente1 =  dify1/difx1;
                    int pendiente2 =  dify2/difx2 ;
                    if (pendiente1 == pendiente2) 
                    {
                        TresEnRaya = true;
                    }
                }
                else if((difx1==0) &&(difx2==0))
                {
                    int[] yorden = new int[3];
                    yorden = GetYMasBaja(y1,y2,y3);

                    int ybaja1, ybaja2, yalta;
                    ybaja1 = yorden[0];
                    ybaja2 = yorden[1];
                    yalta = yorden[2];

                    bool Consecutivos = ((ybaja2 == ybaja1 + 1) && (yalta == ybaja1 + 2));
                    if(Consecutivos==true)
                    {
                        TresEnRaya = true;
                    }
                }
            }
            return TresEnRaya;
        }

        private void Invitadoslbl_Click(object sender, EventArgs e)
        {
            Invitadoslbl.Text = "";
        }

        public int[] GetYMasBaja(int y1, int y2, int y3)
        {//Retorna un vector ordenado de la y mas baja a la mas alta
            int[] yorden = { y1, y2, y3 };
            Array.Sort(yorden);
            return yorden;
        }

        public bool ComprobarCercania(PictureBox Pseleccionado,PictureBox Pespecifico)//Funcion para comprobar si dos pictureBox son circundantes 
        {
            /* Comprueba si los dos picture box son cercanos, es decir su distancia es menor a 1, usando pitagoras*/
            int x, y,x2,y2;
            bool Coinciden = false;
            string coordenada = TraducemePosiciones(Pseleccionado.Name);
            string[] coords = coordenada.Split(',');

            x = Convert.ToInt32(coords[0]);
            y = Convert.ToInt32(coords[1]);

            string coordenada2 = TraducemePosiciones(Pespecifico.Name);
            string[] coords2 = coordenada2.Split(',');

            x2 = Convert.ToInt32(coords2[0]);
            y2 = Convert.ToInt32(coords2[1]);

            int difx = x2 - x;
            int dify = y2 - y;

            double Modulo = Math.Sqrt(Math.Pow(difx,2)+Math.Pow(dify,2));
            if ((Modulo <= Math.Sqrt(2)))
            {
                Coinciden = true;
            }
            return Coinciden;
        }
        
        private void PonerFichaClick(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                string col = "#FF6600";
                Color c = ColorTranslator.FromHtml(col);
                if (pictureBox.BackColor == c)
                {
                    Color micolor = ColorTranslator.FromHtml(MiColor);
                    pictureBox.BackColor = micolor;

                    MisPictureBox.Add(pictureBox);
                    bool Win = ComprobarGane(pictureBox, MisPictureBox);
                    if (Win == false)
                    {
                        string mensaje = "11/" + this.MiPartida + "/" + pictureBox.Name + "/" + MiColor;
                        GrupoJuegoBox.Enabled = false;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                    }
                    else
                    {
                        string mensaje = "12/" + this.MiPartida + "/" + this.label7.Text;
                        GrupoJuegoBox.Enabled = false;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                    }
                }
                else
                {
                    MessageBox.Show("Esa posicion está cogida");
                }
            }
        }
        private void GameAcceptBtn_Click(object sender, EventArgs e)
        {
            // Enviamos la respuesta positiva 
            string mensaje = "7/1/"+this.InvitacionPartida;
            InvitacionBox.Visible = false;
            AnadirInvitadoBtn.Visible = false;
            ListaConectados.Enabled = false;
            label8.Visible = false;
            Invitadoslbl.Visible = false;
            LocalScreen.AppendText("Servidor: pon una T en caso de que nadie tenga el turno" + Environment.NewLine);

            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void RejectGameBtn_Click(object sender, EventArgs e)
        {
            // Enviamos la respuesta negativa 
            string mensaje = "7/2/"+this.InvitacionPartida+"/"+label7.Text+"/"+InvitacionLbl2.Text;
            InvitacionBox.Visible = false;
            ChatLocalBox.Visible = false;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void ListaConectados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Añadimos invitados a la partida
            if (this.numinvitados == 0)
            {
                Invitadoslbl.Text = ListaConectados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                this.numinvitados++;
            }
            else 
            {
                if (this.numinvitados <= 3) 
                {
                    Invitadoslbl.Text = Invitadoslbl.Text + "/" + ListaConectados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    this.numinvitados++;
                }
                if (this.numinvitados > 3)
                    MessageBox.Show("No puedes invitar a más de 3 personas");                    
            }
        }

        private void AnadirInvitadoBtn_Click(object sender, EventArgs e)
        {
            // Enviamos la invitacion 
            string mensaje = "6/"+this.InvitacionPartida+"/"+Convert.ToString(this.numinvitados)+"/"+Invitadoslbl.Text;
            Invitadoslbl.Text = "";
            ChatLocalBox.Visible = true;
            this.numinvitados = 0;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            EmpezarPartidaBtn.Visible = true;
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
                SigInBtn.Enabled = false;
                LogInBtn.Enabled = false;
            }
        }

        private void LogInBtn_Click(object sender, EventArgs e)
        {
            GroupLogSignInBox.Visible = true;
            label6.Visible = false;
            textBox3.Visible = false;
            GroupLogSignInBox.Text = "Introduce usuario y contraseña";
            LogSignInBtn.Text = "loguearse";
            this.id = 1;
            label4.Text = "Usuario";
            label5.Text = "Contraseña";
        }
    }
}
