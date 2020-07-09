using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace video_rental_Assign_N
{
    public class RegisterCustomer
    {

        //object of the Sql Class
        SqlConnection sqlcntn;

        //object of the SQL Command 
        SqlCommand sqlcmd;

        //object of the SQL Data Adapter
        SqlDataReader sqlDataReader;


        //connection string 
        String location = "Data Source=DESKTOP-HKD1BEO\\SQLEXPRESS;Initial Catalog=RentalAssign;Integrated Security=True";



//use the permision to insert delete or update 

        public void Sql_Permission(String query)
        {
            sqlcntn = new SqlConnection(location);
            sqlcntn.Open();
            sqlcmd = new SqlCommand(query, sqlcntn);
            sqlcmd.ExecuteNonQuery();
            sqlcntn.Close();
        }


        //permission to read the data from the daabase 
        public DataTable Sql_searchPermission(String qry)
        {
            DataTable tbl = new DataTable();

            sqlcntn = new SqlConnection(location);

            sqlcntn.Open();

            sqlcmd = new SqlCommand(qry, sqlcntn);

            sqlDataReader = sqlcmd.ExecuteReader();

            tbl.Load(sqlDataReader);

            sqlcntn.Close();

            return tbl;
        }

        public int registerCustomer(String Name, String contact,String Address) {
            if (!Name.Equals("") && !contact.Equals("") && !Address.Equals(""))
            {
                //insert the data by getting the permission 
                Sql_Permission("insert into registerCustomer values('" + Name + "','" + contact + "','" + Address + "')");
                MessageBox.Show("Record is saved ");
                return 1;
            }
            else {
                MessageBox.Show("Fill all values to ");
                return 0;
            }
        }

        //delete the customer only if he doesn't have any video on rent 
        public int delCustomer(int delCust) {
            if (delCust > 0)
            {
                DataTable tbl = new DataTable();
                tbl = Sql_searchPermission("select * from rentTable where CID=" + delCust + " and returnDate='Book'");
                if (tbl.Rows.Count > 0)
                {
                    MessageBox.Show("We can't delete this customer ");
                    return 0;
                }
                else
                {
                    Sql_Permission("delete from registerCustomer where ID=" + delCust + "");
                    return 1;
                }
                
            }
            else {
                MessageBox.Show("select the Customer to delete");
                return 0;
            }
        }

        //update the customer details like contact or address 

        public int EditCustomer(int delCust,String Name,String Contact,String Address) {

            //if all the details are filled 
            if (!Name.Equals("") && !Contact.Equals("") && !Address.Equals("")) {
                Sql_Permission("update registerCustomer set Name='"+Name+"', Contact='"+Contact+"',Address='"+Address+"' where ID="+delCust+"");
                MessageBox.Show("Record is Updated ");

                return 1;
            }
            else {
                MessageBox.Show("fill the proper details to update or select the customer ");
                return 0;
            }    

        }



    }

}
