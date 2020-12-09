using DatLichKham.webservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatLichKham
{
    public partial class FormDatLich : Form
    {
        public WebService1 context = new WebService1();
        FormDanhMuc dat = new FormDanhMuc();

        public FormDatLich(String MaDatLich)
        {
            InitializeComponent();
            this.Text = MaDatLich;
        }

        private void FormDatLich_Load(object sender, EventArgs e)
        {

        }

        private void buttonXacNhan_Click(object sender, EventArgs e)
        {
            String kq;
            MessageBox.Show("Mã lịch khám nhảy sang form đặt="+ this.Text);
            context.DatLich(textBoxTenBenhNhan.Text.ToString(), textBoxSDT.Text.ToString(), textBoxGioiTinh.Text.ToString(), textBoxDiaChi.Text.ToString(), int.Parse(this.Text), 0);
          
        }
    }
}
