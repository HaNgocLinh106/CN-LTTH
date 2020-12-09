using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Windows.Forms;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public SqlConnection con = new SqlConnection("Data Source=DESKTOP-E06EJ47;Initial Catalog=DatKhamBenh;Integrated Security=True");
        private SqlCommand command = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
        public DataSet getdata(string queryy)
        {
            DataSet ds = new DataSet();

            //   connection.ConnectionString = strConString;
            // connection.Open();
            con.Open();

            SqlCommand com = new SqlCommand(queryy, con);
           
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.SelectCommand = com;
            

            da.Fill(ds);
            con.Close();
            
            return ds;

        }

        public string execute(string cmd)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(cmd, con);
                com.ExecuteNonQuery();
                con.Close();
                return "ok";
            }
            catch (Exception)
            {
                return "no";
            }
        }


        [WebMethod]
        public string DatLich(string tennguoibenh, string SDT, string gioitinh, string diachi,string hinhthuc, int MaTuBacSi, int MaTuGoiKham )
        {
            string thongbao;
            string thongbao2;
       
            if (MaTuBacSi != 0)
            {
                thongbao = execute("insert into NguoiBenh(tennguoibenh,SDT,gioitinh,diachi) values ('" + tennguoibenh + "','" + SDT + "','" + gioitinh + "','" + diachi + "' )");
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                // ds = context.getdata("select * from LichKhamBenh");
                ds = getdata("select top 1 MaNguoiBenh from NguoiBenh order by MaNguoiBenh desc ");
                dt = ds.Tables[0];
                int MaNguoiBenh = 0;
              
                MaNguoiBenh = Convert.ToInt32(dt.Rows[0]["MaNguoiBenh"].ToString());
                MessageBox.Show(MaNguoiBenh.ToString());
                thongbao2 = execute("insert into LichKhamBenh_NguoiBenh(MaLichKham,MaNguoiBenh,HinhThuc) values ('" + MaTuBacSi + "','" + MaNguoiBenh  + "' ,'" + hinhthuc + "')");
              
            }
            if (MaTuGoiKham != 0)
            {
                thongbao = execute("insert into NguoiBenh(tennguoibenh,SDT,gioitinh,diachi) values ('" + tennguoibenh + "','" + SDT + "','" + gioitinh + "','" + diachi + "' )");
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                // ds = context.getdata("select * from LichKhamBenh");
                ds = getdata("select top 1 MaNguoiBenh from NguoiBenh order by MaNguoiBenh desc ");
                dt = ds.Tables[0];
                int MaNguoiBenh = 0;
                thongbao2 = execute("insert into :LichKhamGoi_NguoiBenh(MaLichKhamGoi,MaNguoiBenh) values ('" + MaTuGoiKham + "','" + MaNguoiBenh + "' ,'" + hinhthuc + "')");
                return thongbao2;
            }

            return "ok";
        }

    }


}
