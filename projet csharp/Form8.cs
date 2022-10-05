using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace projet_csharp
{
    public partial class Form8 : Form
    {
        public SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-ROUDEIN;Initial Catalog=orojet csharp;Integrated Security=True");

        public SqlCommand cmd;
        public SqlDataReader dr;

        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { cnx.Open();
            if (textBox1.Text == "" || textBox2.Text == "")
                label3.Text = "Entrer Vos Coordonees !!";
            else
            { cmd = new SqlCommand("select matricule,cin from employee where matricule=" + textBox1.Text + " AND cin=" + textBox2.Text, cnx);
             
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    this.Hide();
                    Form7 f7 = new Form7();
                    f7.Show();
                }
                dr.Close();
                
                cmd = new SqlCommand("select IDENTIFIANT, PASWORD from ADMIN where IDENTIFIANT =" + textBox1.Text + " AND PASWORD=" + textBox2.Text, cnx);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { 
                    this.Hide();
                    Form1 form = new Form1();
                    form.Show(); 
                }
                dr.Close();
                cmd = new SqlCommand("select id,pwd from AGENT_SOCIAL where id=" + textBox1.Text + " AND pwd=" + textBox2.Text, cnx);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    this.Hide();
                    Form10 f10 = new Form10();
                    f10.Show();
                }
                else
                    label3.Text = "Login ou Mot de Passe Incorrect !";

                cnx.Close();}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
