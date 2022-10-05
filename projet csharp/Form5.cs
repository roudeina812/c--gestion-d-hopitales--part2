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
    public partial class Form5 : Form
    {
        Form3 f3;
        public SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-ROUDEIN;Initial Catalog=orojet csharp;Integrated Security=True");
        public SqlDataReader Reader;
        
        public Form5(Form3 frm3)
        {
            InitializeComponent();
            this.f3 = frm3;

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            cnx.Open();
            try
            {    Reader.Close();
                int matricul = int.Parse(textBox13.Text);
                SqlCommand cmd = new SqlCommand("Update EMPLOYEE set nom=@nom ,prenom=@prenom, cin=@cin , dateNais=@dateNais ,adresse=@adresse ,grade=@grade , numTel=@numTel , codeCNAM=@codeCNAM , etatCivil=@etatCivil , nomConjoint=@nomConjoint , prenomConjoint=@prenomConjoint , nbEnfant=@nbEnfant where matricule="+matricul+";", cnx);
                
                cmd.Parameters.AddWithValue("@nom", textBox1.Text);
                cmd.Parameters.AddWithValue("@prenom", textBox3.Text);
                cmd.Parameters.AddWithValue("@cin", int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("@dateNais", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@adresse", textBox5.Text);
                cmd.Parameters.AddWithValue("@grade", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@numTel", int.Parse(textBox12.Text));
                cmd.Parameters.AddWithValue("@codeCNAM", int.Parse(textBox11.Text));
                cmd.Parameters.AddWithValue("@etatCivil", textBox10.Text);
                cmd.Parameters.AddWithValue("@nomConjoint", textBox9.Text);
                cmd.Parameters.AddWithValue("@prenomConjoint", textBox8.Text);
                cmd.Parameters.AddWithValue("@nbEnfant", int.Parse(textBox7.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("employéé modifié avec succés");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            cnx.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            Visible = false;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            cnx.Open();
            textBox13.Text = f3.textBoxForm3.Text;
            SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEE", cnx);
            Reader = cmd.ExecuteReader();
            while (Reader.Read())
            {
                dataGridView1.Rows.Add(Reader[0], Reader[1], Reader[2], Reader[3], Reader[4], Reader[5], 
                    Reader[6], Reader[7], Reader[8], Reader[9], Reader[10], Reader[11], Reader[12]);
            }
            cnx.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["nom"].Value.ToString();
                textBox3.Text = row.Cells["prenom"].Value.ToString();
                textBox2.Text = row.Cells["cin"].Value.ToString();
                textBox5.Text = row.Cells["adresse"].Value.ToString();
                textBox4.Text = row.Cells["grade"].Value.ToString();
                textBox12.Text = row.Cells["numTel"].Value.ToString();
                textBox11.Text = row.Cells["codeCNAM"].Value.ToString();
                textBox10.Text = row.Cells["etatCivil"].Value.ToString();
                textBox9.Text = row.Cells["nomConjoint"].Value.ToString();
                textBox8.Text = row.Cells["prenomConjoint"].Value.ToString();
                textBox7.Text = row.Cells["nbEnfant"].Value.ToString();
            }
        }
    }
}
