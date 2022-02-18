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

namespace GymSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AMMAR MEER\Documents\ansdbsd.mdf;Integrated Security=True;Connect Timeout=30");
        public int Id;
       
        private void getmembersdata()
        {

            SqlCommand cmd = new SqlCommand("Select* from Addmemdata", conn);
            //Datatable dt = new Datatable();
            DataTable dt=new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();

            dataGridView2.DataSource = dt;
        }

       // connectionclass cs = new connectionclass();

        private void resetdata()
        {
            textBoxFname.Clear();

            textBoxADDRESS.Clear();
            textBoxdob.Clear();
            textBoxDOJ.Clear();
            textBoxCONTACT.Clear();
            textBoxFname.Focus();
            cmbGENDER.Items.Clear();
            cmbGYMTIME.Items.Clear();
            cmbMEMTIME.Items.Clear();
        }
        private bool Isvalid()
        {
            if (textBoxFname.Text == string.Empty)
            {
                MessageBox.Show("Please write Full Name in order to proceede", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                connectionclass obj = new connectionclass();
                
                

                SqlCommand cmd = new SqlCommand("Insert into Addmemdata VALUES (@fullName, @gender ,@address, @Dob,  @DateOfJoin, @Gymtiming, @Contact, @MemshipTime)", conn);
                cmd.CommandType = CommandType.Text;

                // cmd.Parameters.AddWithValue("@id", textBoxName.Text);
                cmd.Parameters.AddWithValue("@fullName", textBoxFname.Text);
                cmd.Parameters.AddWithValue("@gender", cmbGENDER.Text);
                cmd.Parameters.AddWithValue("@address", textBoxADDRESS.Text);
                cmd.Parameters.AddWithValue("@Dob", textBoxdob.Text);
                cmd.Parameters.AddWithValue("@DateOfJoin", textBoxDOJ.Text);
                cmd.Parameters.AddWithValue("@Gymtiming", cmbGYMTIME.Text);
                cmd.Parameters.AddWithValue("@Contact", textBoxCONTACT.Text);
                cmd.Parameters.AddWithValue("@MemshipTime", cmbMEMTIME.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Saved Successfully in Database, Click OK to Continue", "SAVED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getmembersdata();
                resetdata();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Please select an image";
            opf.Filter = "jpeg(*.jpeg; *.png;*.bmp;)|*.jpeg; *.png; *.bmp;";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(opf.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            opf.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Update Addmemdata SET  fullName=@fullName, gender=@gender ,@address=@address, Dob=@Dob, DateOfJoin=@DateOfJoin, Gymtiming=@Gymtiming, Contact=@Contact, MemshipTime=@MemshipTime Where Id=@Id", conn);
                    cmd.CommandType = CommandType.Text;


                    cmd.Parameters.AddWithValue("@fullName", textBoxFname.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbGENDER.Text);
                    cmd.Parameters.AddWithValue("@address", textBoxADDRESS.Text);
                    cmd.Parameters.AddWithValue("@Dob", textBoxdob.Text);
                    cmd.Parameters.AddWithValue("@DateOfJoin", textBoxDOJ.Text);
                    cmd.Parameters.AddWithValue("@Gymtiming", cmbGYMTIME.Text);
                    cmd.Parameters.AddWithValue("@Contact", textBoxCONTACT.Text);
                    cmd.Parameters.AddWithValue("@Memshiptime", cmbMEMTIME.Text);
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Updated Successfully, Click OK to Proceede", "UPDATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getmembersdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Action Stoped","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a member from the data given below to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                SqlCommand cmd = new SqlCommand("Delete from Addmemdata  Where Id=@Id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", this.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Deleted Successfully!, Click OK to Proceede", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getmembersdata();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                textBoxFname.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                cmbGENDER.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                textBoxADDRESS.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                textBoxdob.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                textBoxDOJ.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                cmbGYMTIME.Text = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                textBoxCONTACT.Text = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();
                cmbMEMTIME.Text = dataGridView2.SelectedRows[0].Cells[8].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getmembersdata();
        }
    }
}
