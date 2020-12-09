using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatLichKham.webservice;


namespace DatLichKham
{
   
    public partial class FormDanhMuc : Form
    {
        DateTime now = DateTime.Today;
        
        public int dem=0;
        public String TenBacSi = "";
        public String ngay1 = "";
        public String Ngay = "";
        public String Gio = "";
        public int MaLichKham=0;
        public String ma1;

        public SqlConnection con = new SqlConnection("Data Source=DESKTOP-E06EJ47;Initial Catalog=DatKhamBenh;Integrated Security=True");
        public  WebService1 context = new WebService1();
        private SqlCommand command = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        public FormDanhMuc()
        {
            InitializeComponent();
            con.Open();

           
        }

      

        private void FormDanhMuc_Load(object sender, EventArgs e)
        {
           FillCombo("select MaKhoa,TenKhoa from KHOA", comboBox1, "MaKhoa", "TenKhoa");
            comboBox1.SelectedIndex = -1;
     

        }
        
        public DataTable readDatathroughAdapter(string query)
        {
            try
            {
                DataTable tblName = new DataTable();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                command.Connection = con;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
                con.Close();

                return tblName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {

         

            SqlCommand cmd = new SqlCommand(sql);
            SqlDataAdapter da;//= new SqlDataAdapter(cmd,);

            //dataGridViewDichGia.DataSource = table;
            DataTable table = new DataTable();
            table = readDatathroughAdapter(sql);
            //  da.Fill(table);
            // da.Dispose();
            cbo.Refresh();
            cbo.DataSource = table;
            table = readDatathroughAdapter(sql);
            cbo.DataSource = table;

            cbo.ValueMember = ma; //Trường giá trị
            cbo.DisplayMember = ten  ; //Trường hiển thị


        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
          
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tenKhoa = "";
            tenKhoa = comboBox1.Text.ToString();
       
            FillCombo("SELECT MaBacSi, TenBacSi from BacSi, Khoa where BacSi.MaKhoa=Khoa.MaKhoa and Khoa.TenKhoa=N'" + tenKhoa + "'", comboBox2, "MaBacSi", "TenBacSi");
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tenBacSi = "";
            tenBacSi = comboBox2.Text.ToString();
            String ngay =now.Year+"-"+ now.Month+"-"+now.Day;
            dem++;
          
            FillCombo("SELECT MaLichKham, Ngay from LichKhamBenh,BacSi  where Ngay>='" + ngay + "' and LichKhamBenh.MaBacSi=BacSi.MaBacSi and BacSi.TenBacSi =N'" + tenBacSi + "'", comboBox3, "MaLichKham", "Ngay");
            comboBox3.SelectedIndex = -1;
    
        }
     

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tenBacSi = "";
            tenBacSi = comboBox2.Text.ToString();
            ngay1 = comboBox3.Text.ToString();

            if (dem!=1){
            FillCombo("SELECT MaLichKham, Gio from LichKhamBenh,BacSi  where Ngay='" + ngay1 + "' and LichKhamBenh.MaBacSi=BacSi.MaBacSi and BacSi.TenBacSi =N'" + tenBacSi + "'", comboBox4, "MaLichKham", "Gio");
            comboBox4.SelectedIndex = -1;
            }
            con.Close();
           
        }



        private void buttonDatLich_Click(object sender, EventArgs e)
        {
            
            TenBacSi = comboBox2.Text.ToString();
            Ngay = comboBox3.Text.ToString();
            Gio = comboBox4.Text.ToString();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
           // ds = context.getdata("select * from LichKhamBenh");
            ds = context.getdata("select MaLichKham from LichKhamBenh,BacSi where BacSi.MaBacSi=LichKhamBenh.MaBacSi and BacSi.TenBacSi='" + TenBacSi + "' and LichKhamBenh.Ngay='" + Ngay + "' and LichKhamBenh.Gio ='" + Gio + "'");
            dt = ds.Tables[0];
            MaLichKham = Convert.ToInt32(dt.Rows[0]["MaLichKham"].ToString());
         //   ma1 = MaLichKham.ToString();
            MessageBox.Show("mã lịch khám từ form bác sĩ:"+MaLichKham.ToString());
            FormDatLich datlich = new FormDatLich(MaLichKham.ToString());
            datlich.Show();

        }
    }
}
