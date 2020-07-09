using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace video_rental_Assign_N
{
    public class RentVideo : VideoDetails
    {
        //delete the rental video 
        public int delRentMovie(int RentID) {
            if (RentID > 0)
            {
                //if the we add the record by mistake of rental video then 
                Sql_Permission("delete from rentTable where ID=" + RentID + "");
                MessageBox.Show("Rental Video Is Deleted");
                return 1;
            }
            else {
                MessageBox.Show("select the Rental video to delete ");
                return 0;
            }

        }

        //this code is used to check about the customer how much video he has booked 
        public int countcoustomer(int custID) {
            DataTable tbl = new DataTable();
            tbl = Sql_searchPermission("select * from rentTable where CID=" + custID + " and returnDate='Book'");
            return tbl.Rows.Count;
        }

        //this code is used to check how much video is already booked of this id
        public int CountVideo(int VideoID) {
            DataTable tbl = new DataTable();
            tbl = Sql_searchPermission("select * from rentTable where VID=" + VideoID + " and returnDate='Book'");
            return tbl.Rows.Count;
        }


        //get the copies details 
        public int getCopies(int VideoID) {
            DataTable tbl = new DataTable();
            tbl = Sql_searchPermission("select * from Video where ID=" + VideoID + "");
            return Convert.ToInt32(tbl.Rows[0]["Copies"].ToString());
        }


        //get the rent charges of the video 
        public int getCharges(int VideoID)
        {
            DataTable tbl = new DataTable();
            tbl = Sql_searchPermission("select * from Video where ID=" + VideoID + "");
            return Convert.ToInt32(tbl.Rows[0]["Cost"].ToString());
        }


        public int BookVideo(int custID,int VideoID,String BookDate) {
            if (countcoustomer(custID) < 2)
            {
                if (CountVideo(VideoID) < getCopies(VideoID))
                {
                    Sql_Permission("insert into rentTable values("+custID+","+VideoID+",'"+BookDate+"','Book')");
                    MessageBox.Show("Video is Booked By Customer ");
                    return 1;
                }
                else {
                    MessageBox.Show("All video are booked ");
                    return 0;
                }


            }
            else {
                MessageBox.Show("You already have 2 video on rent ");
                return 0;
            } 

        }




        public int ReturnVideo(int RID,int custID, int VideoID, String BookDate,String ReturnDate)
        {
            //get the difference between 
            //get the difference in days between 2 dates and get  the cost from the database 
            DateTime start = Convert.ToDateTime(BookDate);
            DateTime endDate = Convert.ToDateTime(ReturnDate);

            String diff2 = (endDate - start).TotalDays.ToString();
            //convert the string value to double 
            double d = Convert.ToDouble(diff2);
            //pass the roud off value to calculate 
            double days = Math.Round(d);

            //get the cost of the video 
            DataTable tbl = new DataTable();
            if (d == 0)
            {
                days = 1;
            }

            int cost = getCharges(Convert.ToInt32(VideoID));

            int payment = Convert.ToInt32(days) * cost;



            Sql_Permission("update rentTable set CID=" + custID + ",VID=" + VideoID + ",BookDate='" + BookDate + "',returnDate='"+ReturnDate+"' where ID="+RID+"");
            MessageBox.Show("Video is Booked By Customer and Your charges is $"+payment );
            return 1;        
        }

        //return the wholedata from the video 
        public DataTable VideoData() {
            DataTable tbl = new DataTable();
            tbl=Sql_searchPermission("select * from Video");
            return tbl;

        }

        //return the wholedata from the customer 
        public DataTable CusotmerData()
        {
            DataTable tbl = new DataTable();
            tbl = Sql_searchPermission("select * from registerCustomer");
            return tbl;

        }


        //return the wholedata from the rental 
        public DataTable RentalData()
        {
            DataTable tbl = new DataTable();
            tbl = Sql_searchPermission("select * from rentTable");
            return tbl;

        }

        public void bestVideo() {
            DataTable tblData = new DataTable();
            tblData =Sql_searchPermission("select * from Video");
            int x = 0, y = 0, cunt = 0;
            String Name = "";
            for (x = 0; x < tblData.Rows.Count; x++)
            {
                DataTable tbl = new DataTable();
                tbl = Sql_searchPermission("select * from rentTable where VID=" + Convert.ToInt32(tblData.Rows[x]["ID"].ToString()) + "");

                if (tbl.Rows.Count > cunt)
                {
                    Name = tblData.Rows[x]["Title"].ToString();
                    cunt = tbl.Rows.Count;
                }

            }
            MessageBox.Show("Best Video of the Store  is " + Name);

        }



        public void bestCustomer()
        {
            DataTable tblData = new DataTable();
            tblData = Sql_searchPermission("select * from registerCustomer");
            int x = 0, y = 0, cunt = 0;
            String Name = "";
            for (x = 0; x < tblData.Rows.Count; x++)
            {
                DataTable tbl = new DataTable();
                tbl = Sql_searchPermission("select * from rentTable where CID=" + Convert.ToInt32(tblData.Rows[x]["ID"].ToString()) + "");

                if (tbl.Rows.Count > cunt)
                {
                    Name = tblData.Rows[x]["Name"].ToString();
                    cunt = tbl.Rows.Count;
                }

            }
            MessageBox.Show("Best Cusotmer of the Store  is " + Name);

        }



    }
}
