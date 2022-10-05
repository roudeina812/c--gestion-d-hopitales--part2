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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-ROUDEIN;Initial Catalog=orojet csharp;Integrated Security=True");
            cnx.Open();
            SqlCommand cmd = new SqlCommand("select matricule,depotDate,Acte from EMPLOYEE,BULLTIN where matricule=" + textBox1.Text + " AND depotDate =@date", cnx);
            cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            bool trouve = false;
            while (dr.Read())
            {
                int a = int.Parse(dr[2].ToString());
                if (a==1)
                {
                    trouve = true;
                    this.Hide();
                    Form9 f9 = new Form9();
                    f9.Show();

                }
                else
                    MessageBox.Show("votre dmande est refusée !!!");
            }
               if(trouve==false)
                label4.Text = "Coordonnées Invalides !!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.Show();
            Visible = false;

        }
    }
}
