using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace video_rental_Assign_N
{
    public partial class Form1 : Form
    {
        RentVideo rgCustomer = new RentVideo();
        int rntID=0,optn=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void add_Ncusto_Click(object sender, EventArgs e)
        {
            rgCustomer.registerCustomer(customer_name.Text,customer_contact.Text,customer_address.Text);
            resetAll();

        }

        private void issue_Nmovie_Click(object sender, EventArgs e)
        {
            if (!pkcustomer.Text.Equals("") && !pkmovie.Text.Equals(""))
            {


                rgCustomer.BookVideo(Convert.ToInt32(pkcustomer.Text), Convert.ToInt32(pkmovie.Text), dateTimePicker1.Value.ToShortDateString());
                
            }
            else {
                MessageBox.Show("Select the Video or Cusotmer to book video on rent ");
            }
            resetAll();
        }

        private void delete_Ncusto_Click(object sender, EventArgs e)
        {
            if (!pkcustomer.Text.ToString().Equals(""))
            {
                rgCustomer.delCustomer(Convert.ToInt32(pkcustomer.Text));
                resetAll();
            }
            else {
                MessageBox.Show("select the Cusotmer to delete ");
            }
        }


        //this method is used to reset all the variable 
        public void resetAll() {
            customer_address.Text = "";
            customer_contact.Text = "";
            customer_name.Text = "";
            pkcustomer.Text = "";

            video_title.Text = "";
            video_ratting.Text= "";
            video_plot.Text = "";
            video_copies.Text = "";
            video_cost.Text = "";
            video_year.Text = "";
            pkmovie.Text = "";
            video_genre.Text = "";
        }

        private void update_Ncusto_Click(object sender, EventArgs e)
        {
            //code to edit the details of the customer 
            rgCustomer.EditCustomer(Convert.ToInt32(pkcustomer.Text), customer_name.Text, customer_contact.Text, customer_address.Text);
            resetAll();

        }

        private void video_year_TextChanged(object sender, EventArgs e)
        {
            try {
                //dislay the cost of the price of the video after adding the year of the video
                DateTime dateNow = DateTime.Now;

                int Currentyear = dateNow.Year;
                int cost = 0;

                int diffYear = Currentyear - Convert.ToInt32(video_year.Text);
                // MessageBox.Show(diff.ToString());
                if (diffYear >= 5)
                {
                    cost = 2;
                }
                else if (diffYear >= 0 && diffYear < 5)
                {
                    cost = 5;
                }

                video_cost.Text = "" + cost;
            }
            catch (Exception ex) {
                    
            }



        }

        private void add_NVideo_Click(object sender, EventArgs e)
        {
            //add the video into the database 
            rgCustomer.addVideo(video_title.Text,video_ratting.Text,video_year.Text,video_cost.Text,video_copies.Text,video_plot.Text,video_genre.Text);
            resetAll();
        }

        private void delete_NVideo_Click(object sender, EventArgs e)
        {
            //code to delete the video only if the video is not booked by any customer 
            if (!pkmovie.Text.Equals("")) {
                rgCustomer.delVideo(Convert.ToInt32(pkmovie.Text));
            }
            else {
                MessageBox.Show("You must have to select the video ");
            }
            resetAll();

        }

        private void update_NVideo_Click(object sender, EventArgs e)
        {
            rgCustomer.EditVideo(Convert.ToInt32(pkmovie.Text),video_title.Text,video_ratting.Text,video_year.Text,video_cost.Text,video_copies.Text,video_plot.Text,video_genre.Text); ;
            resetAll();
        }

        private void delete_Nmovie_Click(object sender, EventArgs e)
        {
            if (rntID > 0)
            {
                rgCustomer.delRentMovie(rntID);

            }
            else {
                MessageBox.Show("select the rental video to delete ");
            }
            resetAll();
        }

        private void customer_record_show_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = rgCustomer.CusotmerData();
            dataGridView1.DataSource=tbl;
            optn = 1;
        }

        private void rental_record_show_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = rgCustomer.RentalData();
            dataGridView1.DataSource = tbl;
            optn = 2;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (optn == 1)
            {
                //means cusotmer data 
                pkcustomer.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                customer_name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                customer_contact.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                customer_address.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();


            }
            else if (optn == 2)
            {
                //measn rental data 
                rntID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                pkcustomer.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                pkmovie.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


            }
            else if (optn == 3)
            {
                //measn video data 
                pkmovie.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                video_title.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                video_ratting.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
                video_year.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                video_cost.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                video_copies.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                video_plot.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                video_genre.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
            else
            {
                MessageBox.Show("select an option to view record ");
            }

            optn = 0;

        }

        private void return_Nmovie_Click(object sender, EventArgs e)
        {
            rgCustomer.ReturnVideo(rntID, Convert.ToInt32(pkcustomer.Text), Convert.ToInt32(pkmovie.Text), dateTimePicker1.Text, dateTimePicker2.Text);

        }

        private void best_customer_Click(object sender, EventArgs e)
        {
            //show the best cusotmer of the video store 
            rgCustomer.bestCustomer();
        }

        private void best_movie_Click(object sender, EventArgs e)
        {
            //show the best video of teh store 
            rgCustomer.bestVideo();

        }

        private void video_record_show_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = rgCustomer.VideoData();
            dataGridView1.DataSource = tbl;
            optn = 3;
        }
    }
}
