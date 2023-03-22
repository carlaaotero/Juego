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
    public partial class Form2 : Form
    {
        int id;
        string nom, contra;
        public Form2()
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
        public void SetId(int n)
        {
            this.id = n;   
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (this.id == 1)
                groupBox1.Text = "Log In";
            else if(this.id ==2)
                groupBox1.Text = "Sign In";
            label1.Text = "nombre";
            label2.Text = "contraseña";
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            if(this.id==2)
            {
                Form1 f2 = new Form1();
                //if (textBox1.Text != null && textBox2.Text != null)
                //{
                //f2.SetNom(textBox1.Text);
                //f2.SetContra(textBox2.Text);
                // }
                this.nom = textBox1.Text;
                this.contra = textBox2.Text;
            }
            else if(this.id == 1)
            {
                Form1 f2 = new Form1();
                this.nom = textBox1.Text;
                this.contra = textBox2.Text;
            }
            
            Close();
        }
    }
}
