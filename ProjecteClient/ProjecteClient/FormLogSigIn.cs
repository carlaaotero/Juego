using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjecteClient
{
    public partial class FormLogSigIn : Form
    {
        int id;
        string nom, contra,contra2;
        public FormLogSigIn()
        {
            InitializeComponent();
        }
        public string GetNom()
        {
            return this.nom;
        }
        public string GetContra()
        {
            return this.contra;
        }
        public string GetContra2()
        {
            return this.contra2;
        }
        public void SetId(int n)
        {
            this.id = n;   
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (this.id == 1)
            {
                groupBox1.Text = "Log In";
                label3.Visible = false;
                textBox3.Visible = false;
            }

            else if (this.id == 2)
            {
                groupBox1.Text = "Sign In";
                label3.Text = "contraseña";
            }  
            label1.Text = "nombre";
            label2.Text = "contraseña";
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            if(this.id==2)
            {
                int i=0;
                this.contra = textBox2.Text;
                this.contra2 = textBox3.Text;
                this.nom = textBox1.Text;

                if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
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
                    Close();
            }
            else if(this.id == 1)
            {
                this.nom = textBox1.Text;
                this.contra = textBox2.Text;
                Close();
            }
        }
    }
}