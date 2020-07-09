using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace video_rental_Assign_N
{
   public class VideoDetails: RegisterCustomer
    {
        //register the video in the portal 
        public int addVideo(String title,String ratting,String year,String Cost,String Copies,String Plot,String genre) {
            if (!title.Equals("") && !ratting.Equals("") && !year.Equals("") && !Cost.Equals("") && !Copies.Equals("") && !Plot.Equals("") && !genre.Equals("")) {
                Sql_Permission("insert into Video values('"+title+"','"+ratting+"','"+year+"','"+Cost+"','"+Copies+"','"+Plot+"','"+genre+"')");
                MessageBox.Show("video is reorded ");
                return 1;
            }
            else {
                MessageBox.Show("fill the All Details ");
                return 0;
            }    
        }

        public int EditVideo(int delVideo,String title, String ratting, String year, String Cost, String Copies, String Plot, String genre)
        {
            if (!title.Equals("") && !ratting.Equals("") && !year.Equals("") && !Cost.Equals("") && !Copies.Equals("") && !Plot.Equals("") && !genre.Equals(""))
            {
                Sql_Permission("update Video set Title='" + title + "',Ratting='" + ratting + "',Year='" + year + "',Cost='" + Cost + "',Copies='" + Copies + "',Plot='" + Plot + "',Genre='" + genre + "' where ID="+delVideo+"");
                MessageBox.Show("video record is Uodated ");
                return 1;
            }
            else
            {
                MessageBox.Show("fill the All Details ");
                return 0;
            }
        }

        public int delVideo(int delVideo)
        {
            if (delVideo>0 )
            {
                DataTable tbl = new DataTable();

                tbl = Sql_searchPermission("select * from rentTable where VID="+delVideo+" and returnDate='Book'");
                if (tbl.Rows.Count > 0)
                {
                    MessageBox.Show("video is already booked by a customer ");
                    return 0;
                }
                else
                {
                    Sql_Permission("delete from Video where ID=" + delVideo + "");
                    MessageBox.Show("video is reorded ");
                    return 1;
                }
            }
            else
            {
                MessageBox.Show("fill the All Details ");
                return 0;
            }
        }


    }
}
