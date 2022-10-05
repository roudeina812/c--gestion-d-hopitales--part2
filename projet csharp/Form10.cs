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
    public partial class Form10 : Form
    {
        public SqlConnection con = new SqlConnection("Data Source=DESKTOP-ROUDEIN;Initial Catalog=orojet csharp;Integrated Security=True");
        public SqlCommand cmd;
        public SqlDataReader dr;
        public DataTable dt;
        public Form10()
        {
            InitializeComponent();
            BulletinsGridView();
            fillAnneeCombo();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        void BulletinsGridView()
        {

            con.Open();
            cmd = new SqlCommand("select * from BULLTIN", con);
            dr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();


        }

        void BulletinsGridViewSearch()
        {
            con.Open();
            cmd = new SqlCommand("select * from BULLTIN where idCIN_Emp='" + numCin.Text + "' and year(depotDate)='" + anneeCombo.Text + "'", con);
            dr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        void BulletinsGridViewSearch2()
        {

            con.Open();
            cmd = new SqlCommand("select * from BULLTIN where num='" + numBulletinSearch.Text + "' and depotDate='" + dateTimePicker1.Value.ToString() + "'", con);
            dr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        void fillAnneeCombo()
        {
            string query = "select DISTINCT Year (depotDate) as year from BULLTIN";
            cmd = new SqlCommand(query, con);
            con.Open();
            dt = new DataTable();
            dt.Columns.Add("year", typeof(string));
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
        }
        void updatePlafond(double p)
        {
            con.Open();
            SqlCommand cmd3 = new SqlCommand("update EMPLOYEEE set REMBOURSEMENT ='" + p + "' where cin='" + numCin.Text + "'", con);
            cmd3.ExecuteNonQuery();
            MessageBox.Show("Plafond modifié");
            con.Close();
            BulletinsGridView();
        }

        void updateResponse(string rep)
        {
            con.Open();
            SqlCommand cmd4 = new SqlCommand("update BULLTIN set Acte ='" + rep + "' where idCIN_Emp='" + numCin.Text + "'", con);
            cmd4.ExecuteNonQuery();
            MessageBox.Show("Réponse modifié");
            con.Close();
            BulletinsGridView();
        }

        int sommeRM()
        {
            cmd = new SqlCommand("select fraisActe from BULLTIN where idCIN_EMP='" + numCin.Text + "' and year(depotDate)='" + anneeCombo.Text + "'", con);
            con.Open();
            dt = new DataTable();
            dt.Columns.Add("fraisActe", typeof(string));
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            string[] result = new string[dt.Rows.Count];

            int nb = dt.Rows.Count;
            for (int i = 0; i < nb; i++)
            {
                DataRow dr1 = dt.Rows[i];
                for (int j = 0; j < dr1.ItemArray.Length; j++)
                {
                    result[i] = dr1[j].ToString();
                }

            }


            double somme = 0;
            for (int i = 0; i < result.Length; i++)
            {
                string num = string.Empty;
                string s = result[i];
                foreach (char c in s)
                {
                    if (c >= '0' && c <= '9')
                    {
                        num = string.Concat(num, c.ToString());
                    }
                }
                double frs = double.Parse(num);
                somme += frs;
            }
            con.Close();
            return (int)somme;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                numBulletin.Text = row.Cells["num"].Value.ToString();
                dateDepot.Text = row.Cells["depotDate"].Value.ToString();
                acte.Text = row.Cells["acte"].Value.ToString();
                frais.Text = row.Cells["fraisActe"].Value.ToString();
                numCin.Text = row.Cells["idCIN_Emp"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (numBulletin.Text == "")
            {
                MessageBox.Show("Veuillez SVP entrez le numéro du bulletin!");
            }
            else
            {
                con.Open();
                cmd = new SqlCommand("delete from BULLTIN where num='" + numBulletin.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bulletin supprimé avec succès");
                con.Close();
                BulletinsGridView();
                numBulletin.Text = "";
                dateDepot.Text = "";
                acte.Text = "";
                frais.Text = "";
                numCin.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (numBulletin.Text == "" || dateDepot.Text == "" || acte.Text == "" || frais.Text == "" || numCin.Text == "")
            {
                MessageBox.Show("Veuillez remplir les champs vacants svp!");
            }
            else
            {
                con.Open();
                cmd = new SqlCommand("update BULLTIN set depotDate='" + dateDepot.Text + "',acte='" + acte.Text + "',fraisActe='" + frais.Text + "',idCIN_Emp='" + numCin.Text + "' where num='" + numBulletin.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bulletin modifié avec succès");
                con.Close();
                BulletinsGridView();
                numBulletin.Text = "";
                dateDepot.Text = "";
                acte.Text = "";
                frais.Text = "";
                numCin.Text = "";
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (CinEmpListe.Text == "" || anneeCombo.Text == "")
            {
                MessageBox.Show("Veuillez remplir les champs svp !");
            }
            else
            {
                BulletinsGridViewSearch();
                numCin.Text = "";
                anneeCombo.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            if (numBulletinSearch.Text == "" || dateTimePicker1.Value.ToString() == "")
            {
                MessageBox.Show("Veuillez donner le numéro du bulletin et la date de dépot simultanément!");
            }
            else
            {
                cmd = new SqlCommand("select * from BULLTIN where num =" + numBulletinSearch.Text + "and dateDepot =" + dateTimePicker1.Value.ToString() + ";", con);
                dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);
                }
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            double taux_rembours = 0.3;
            string fraisActe = frais.Text;
            string numericString = string.Empty;
            foreach (char c in fraisActe)
            {
                if (c >= '0' && c <= '9')
                {
                    numericString = string.Concat(numericString, c.ToString());
                }
            }
            double fraisPayes = double.Parse(numericString);
            double rembours_total = fraisPayes * taux_rembours;
            string rembours = rembours_total.ToString();
            MessageBox.Show("Le remboursement mutuel du bulletin n° " + numBulletin.Text + " est égal à " + rembours + " DT");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int sm = sommeRM();
            string query = "select grade from EMPLOYEE where idCIN='" + numCin.Text + "'";
            SqlCommand cmd2 = new SqlCommand(query, con);
            con.Open();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("grade", typeof(int));
            SqlDataReader dr2 = cmd2.ExecuteReader();
            int gr;
            double restePlafond;
            while (dr2.Read())
            {
                gr = int.Parse(dr2["grade"].ToString());

                MessageBox.Show(gr.ToString());
                if (gr == 1)
                {
                    restePlafond = 1800 - sm;
                    if (restePlafond > 0)
                    {
                        con.Close();
                        updateResponse("acceptée");
                        con.Open();
                    }
                    else
                    {
                        con.Close();
                        updateResponse("refusée");
                        con.Open();
                    }
                    con.Close();
                    updatePlafond(restePlafond);
                    con.Open();
                }
                else if (gr == 2)
                {
                    restePlafond = 1400 - sm;
                    if (restePlafond > 0)
                    {
                        con.Close();
                        updateResponse("acceptée");
                        con.Open();
                    }
                    else
                    {
                        con.Close();
                        updateResponse("refusée");
                        con.Open();
                    }
                    con.Close();
                    updatePlafond(restePlafond);
                    con.Open();
                }
                else if (gr == 3)
                {
                    restePlafond = 1000 - sm;
                    if (restePlafond > 0)
                    {
                        con.Close();
                        updateResponse("acceptée");
                        con.Open();
                    }
                    else
                    {
                        con.Close();
                        updateResponse("refusée");
                        con.Open();
                    }
                    con.Close();
                    updatePlafond(restePlafond);
                    con.Open();
                }
                else
                {
                    restePlafond = 600 - sm;
                    if (restePlafond > 0)
                    {
                        con.Close();
                        updateResponse("acceptée");
                        con.Open();
                    }
                    else
                    {
                        con.Close();
                        updateResponse("refusée");
                        con.Open();
                    }
                    con.Close();
                    updatePlafond(restePlafond);
                    con.Open();
                }

            }

            dt2.Load(dr2);
            dr2.Close();
            con.Close();

        }

        
    }
}
