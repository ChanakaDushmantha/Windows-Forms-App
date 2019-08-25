using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Windows_Forms_App
{
    public partial class Form1 : Form
    {
        MySqlConnection mycon = new MySqlConnection("server=localhost;user id = root; database=Company"); 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (mycon)
            {
                MySqlCommand cmd = new MySqlCommand("Select * from employee", mycon);
                mycon.Open();
                using (MySqlDataReader mrdr = cmd.ExecuteReader())
                {
                    DataTable empTb1 = new DataTable();
                    empTb1.Columns.Add("Emp_ID");
                    empTb1.Columns.Add("Emp_Name");
                    empTb1.Columns.Add("Salary");

                    while (mrdr.Read())
                    {
                        DataRow drow = empTb1.NewRow();

                        drow["Emp_ID"] = mrdr["Emp_ID"];
                        drow["Emp_Name"] = mrdr["Emp_Name"];
                        drow["Salary"] = mrdr["Salary"];
                        empTb1.Rows.Add(drow);
                    }
                    dataGridView1.DataSource = empTb1;
                }
                mycon.Close();
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            using (mycon)
            {
                MySqlCommand cmd = new MySqlCommand("Select * from employee", mycon);
                mycon.Open();
                using (MySqlDataReader mrdr = cmd.ExecuteReader())
                {
                    DataTable empTb1 = new DataTable();
                    empTb1.Columns.Add("Emp_ID");
                    empTb1.Columns.Add("Emp_Name");
                    empTb1.Columns.Add("Salary");

                    while (mrdr.Read())
                    {
                        DataRow drow = empTb1.NewRow();

                        drow["Emp_ID"] = mrdr["Emp_ID"];
                        drow["Emp_Name"] = mrdr["Emp_Name"];
                        drow["Salary"] = mrdr["Salary"];
                        empTb1.Rows.Add(drow);
                    }
                    dataGridView1.DataSource = empTb1;
                }
            }
            mycon.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(textBox1.Text);
            string nm = textBox2.Text;
            int sl = Int32.Parse(textBox3.Text);

            MySqlCommand cmd2 = new MySqlCommand("INSERT INTO employee (Emp_ID, Emp_Name, Salary) VALUES ('"+ id +"','"+ nm +"','"+ sl +"')", mycon);
            mycon.Open();
            cmd2.ExecuteNonQuery();
            mycon.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using (mycon)
            {
                int id = Int32.Parse(textBox1.Text);

                MySqlCommand cmd3 = new MySqlCommand("DELETE from employee where Emp_ID =" + id,mycon);
                mycon.Open();
                cmd3.ExecuteNonQuery();
                int result = cmd3.ExecuteNonQuery();
                if (result > 1)
                {
                    System.Windows.Forms.MessageBox.Show("Delete failed");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("The Employee Name and Salary of " + id + " has been Deleted Successfully");
                }
                mycon.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(textBox1.Text);
            string nm = textBox2.Text;
            int sl = Int32.Parse(textBox3.Text);

            MySqlCommand cmd4 = new MySqlCommand("UPDATE `employee` SET `Emp_ID`='" + id + "',`Emp_Name`='" + nm + "',`Salary`=" + sl + " WHERE Emp_ID='" + id + "'", mycon);
            mycon.Open();
            cmd4.ExecuteNonQuery();

            int result = cmd4.ExecuteNonQuery();
            if (result < 1)
            {
                System.Windows.Forms.MessageBox.Show("Update failed");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("The Employee Name and Salary of " + id + " has been Updated Successfully");
            }
            mycon.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (mycon)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {

                }
                else
                {
                    int id = Int32.Parse(textBox1.Text);

                    MySqlCommand cmd5 = new MySqlCommand("Select Emp_Name, Salary from employee where Emp_ID =" + id, mycon);

                    mycon.Open();
                    MySqlDataReader DR1 = cmd5.ExecuteReader();
                    if (DR1.Read())
                    {
                        textBox2.Text = DR1.GetValue(0).ToString();
                        textBox3.Text = DR1.GetValue(1).ToString();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Employee not found");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                    }

                    mycon.Close();
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(dataGridView1.Rows.Count)-1;
            label4.Text = "Total Number of Employees: " + Convert.ToString(count);
        }
    }
}
