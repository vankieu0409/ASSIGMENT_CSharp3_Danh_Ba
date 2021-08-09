using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ASSIGMENT_Danh_Ba.Interface;
using ASSIGMENT_Danh_Ba.Models;
using ASSIGMENT_Danh_Ba.Sevices;

namespace ASSIGMENT_Danh_Ba
{
    public partial class ASSIGRMENTS : Form
    {
        private IServices Sv;
        private ICheck check;
        private List<DanhBa> lstDanhBas;
        private List<Nguoi> lstNguoi;
        private string erorrmes=" Thông báo về việc ";
        private string Erorr = "Thông báo của UBND xã Tuân Chính";
        private int flag = -1;
        private Guid idWhenClick;
        private int rowIndex1;

        public ASSIGRMENTS()
        {
            
            Sv = new Services();
            check = new CheckEveryThing();
            lstDanhBas = new List<DanhBa>();
            lstNguoi = new List<Nguoi>();
            InitializeComponent();
            NamSinh();
            rbtn_Nam.Checked = true;
            rbtn_nam_search.Checked = true;
            LoadDatabase();
            
        }

        bool CheckEveryThing()
        {
            if (check.checkNull(txt_Ten.Text) || check.checkNull(cbox_namsinh.Text))
            {
                MessageBox.Show(erorrmes+" Bạn Không được để trống Tên và năm sinh", Erorr);
                return false;
            }

            if (check.checkString(txt_Ten.Text) || check.checkString(txt_ho.Text) || check.checkString(txt_tendem.Text)
                || check.checkString(txt_Email.Text) || check.checkString(txt_note.Text))
            {
                MessageBox.Show( erorrmes+" bạn Phải Nhập chữ hoặc ký tự vào các Ô Họ,tên Đệm,tên. \n Không được nhập Số!! 😒😒😒 ", Erorr);
                return false;
            }

            if (check.checkNumber(txt_SDT_1.Text) || check.checkNumber(txt_SDT_2.Text) ||
                check.checkNumber(cbox_namsinh.Text))
            {
                MessageBox.Show(erorrmes+" bạn Phải nhập Số vào Các ô Số điên thoại và Phải Chọn năm Sinh\n Không Được Nhập Chữ!! 😒😒😒");
                return false;
            }

            return true;
        }

        void NamSinh()
        {
            foreach (var x in Sv.getYearofBirth())
            {
                cbox_namsinh.Items.Add(x);
            }
        }

        void LoadDatabase()
        {
            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }

        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (CheckEveryThing() == false)
            {
                return;
            }
            else
            {
                Nguoi nguoi = new Nguoi();
                nguoi.IdNguoi = Guid.NewGuid();
                nguoi.Ho = txt_ho.Text;
                nguoi.TenDem = txt_tendem.Text;
                nguoi.Ten = txt_Ten.Text;
                nguoi.NamSinh = Convert.ToInt16(cbox_namsinh.Text);
                nguoi.GioiTinh = rbtn_Nam.Checked ? 1 : 0;
                DanhBa danhBa = new DanhBa();
                danhBa.IdDanhBa = Guid.NewGuid();
                danhBa.SDT1 = txt_SDT_1.Text;
                danhBa.SDT2 = txt_SDT_2.Text;
                danhBa.Email = txt_Email.Text;
                danhBa.Note = txt_note.Text;
                danhBa.IdNguoi = nguoi.IdNguoi;
                MessageBox.Show(Sv.Them(nguoi, danhBa), Erorr);
                LoadDatabase();
                flag = 1;
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (CheckEveryThing() == false)
            {
                return;
            }
            else
            {
                idWhenClick = Sv.getListNguoi().Where(c => c.Ten == txt_Ten.Text).Select(c => c.IdNguoi).FirstOrDefault();
                Nguoi nguoi = Sv.getListNguoi().Where(c => c.IdNguoi == idWhenClick).FirstOrDefault();
                nguoi.Ho = txt_ho.Text;
                nguoi.TenDem = txt_tendem.Text;
                nguoi.Ten = txt_Ten.Text;
                nguoi.NamSinh = Convert.ToInt16(cbox_namsinh.Text);
                nguoi.GioiTinh = rbtn_Nam.Checked ? 1 : 0;

                var idDB = Sv.getlListDanhBa().Where(c => c.IdNguoi == idWhenClick).Select(c => c.IdDanhBa).FirstOrDefault();
                DanhBa danhBa = Sv.getlListDanhBa().Where(c => c.IdDanhBa == idDB).FirstOrDefault();
                danhBa.SDT1 = txt_SDT_1.Text;
                danhBa.SDT2 = txt_SDT_2.Text;
                danhBa.Email = txt_Email.Text;
                danhBa.Note = txt_note.Text;
                if (MessageBox.Show("Bạn Có muốn Sửa thông tin  Không??", Erorr, MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    MessageBox.Show(Sv.sua(nguoi, danhBa), Erorr);
                }

                LoadDatabase();
                flag = 2;
            }
        }

       

        private void dgv_DanhBa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
            //1. Lấy Index Rows Khi bấm vào Gird
            int rowIndex = e.RowIndex;
            rowIndex1 = e.RowIndex;
            if (rowIndex == lstNguoi.Count) return;
            // 2. Lấy giá trị tại cột ID
              
            // 3. có 2 cách để tìm được dối tượng
            // 3.1- Dùng Service dể tìm
            // 3.2 là sử dụng cách lấy giá trị tại cột
            txt_ho.Text = dgv_DanhBa.Rows[rowIndex].Cells[0].Value.ToString();
            txt_tendem.Text = dgv_DanhBa.Rows[rowIndex].Cells[1].Value.ToString();
            txt_Ten.Text = dgv_DanhBa.Rows[rowIndex].Cells[2].Value.ToString();
            cbox_namsinh.SelectedIndex = cbox_namsinh.FindString(dgv_DanhBa.Rows[rowIndex].Cells[3].Value.ToString());
            rbtn_Nam.Checked = dgv_DanhBa.Rows[rowIndex].Cells[4].Value.ToString() == "Nam" ? true : false;
            rbtn_Nu.Checked = dgv_DanhBa.Rows[rowIndex].Cells[4].Value.ToString() == "Nữ" ? true : false;
            txt_SDT_1.Text = dgv_DanhBa.Rows[rowIndex].Cells[5].Value.ToString();
            txt_SDT_2.Text = dgv_DanhBa.Rows[rowIndex].Cells[6].Value.ToString();
            txt_Email.Text = dgv_DanhBa.Rows[rowIndex].Cells[7].Value.ToString();
            txt_note.Text = dgv_DanhBa.Rows[rowIndex].Cells[8].Value.ToString();
          //  idWhenClick =Sv.getListNguoi().Where(c=>c.Ho==txt_ho.Text&&c.TenDem==)
          idWhenClick = Sv.getListNguoi().Where(c => c.Ten == txt_Ten.Text).Select(c => c.IdNguoi).FirstOrDefault();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            idWhenClick = Sv.getListNguoi().Where(c => c.Ten == txt_Ten.Text).Select(c => c.IdNguoi).FirstOrDefault();
            var nguoi = Sv.getListNguoi().Where(c => c.IdNguoi == idWhenClick).FirstOrDefault();
            var danhBa = Sv.getlListDanhBa().Where(c => c.IdNguoi == idWhenClick).FirstOrDefault();
            if (MessageBox.Show(erorrmes+ " bạn có muốn Xóa thông tin và Liên Hệ\n của người này không?", Erorr,
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show(Sv.Xoa(nguoi, danhBa), Erorr);
                flag = 3;
            }

            LoadDatabase();
            
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn Muốn Lưu Chứ?", Erorr, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Sv.QuestionSave();
                MessageBox.Show("Lưu Danh bạ Thành Công!", Erorr);
                flag = -1;
            }

        }

        private void txt_Search_ten_TextChanged(object sender, EventArgs e)
        {
            //txt_Search_ten.Text = "";
            LoadDatabaseSearch(txt_Search_ten.Text);

        }

        void LoadDatabaseSearch(string acc)
        {
            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           where (a.Ten == acc || b.SDT1 == acc || b.SDT2 == acc)
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }



        private void txt_Seach_SDT_TextChanged(object sender, EventArgs e)
        {
            LoadDatabaseSearch(txt_Seach_SDT.Text);
        }

        private void txt_Search_ten_MouseDown(object sender, MouseEventArgs e)
        {
            txt_Search_ten.Text = "";
        }

        private void txt_Seach_SDT_MouseDown(object sender, MouseEventArgs e)
        {
            txt_Seach_SDT.Text = "";
        }

        private void btn_Viettel_Click(object sender, EventArgs e)
        {

            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           where (b.SDT1.StartsWith("03") || b.SDT2.StartsWith("03") || b.SDT1.StartsWith("098") ||
                                  b.SDT2.StartsWith("098"))
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }

        private void btn_vina_Click(object sender, EventArgs e)
        {
            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           where (b.SDT1.StartsWith("08") || b.SDT2.StartsWith("08"))
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           where (b.SDT1.StartsWith("07") || b.SDT2.StartsWith("07"))
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }

        private void btnSapXepAtoZ_Click(object sender, EventArgs e)
        {
            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           orderby a.Ten
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }

        private void btn_xeptenNguoc_Click(object sender, EventArgs e)
        {
            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           orderby a.Ten descending
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }

        private void rbtn_nam_search_CheckedChanged(object sender, EventArgs e)
        {

            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           where (a.GioiTinh == 1)
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }

        private void rbtn_nu_search_CheckedChanged(object sender, EventArgs e)
        {
            lstNguoi = Sv.getListNguoi(); // đổ dữ liệu vài lis Hiện tại
            lstDanhBas = Sv.getlListDanhBa();
            var ListBig = (from a in lstNguoi
                           join b in lstDanhBas on a.IdNguoi equals b.IdNguoi
                           where (a.GioiTinh == 0)
                           select new
                           { a.Ho, a.TenDem, a.Ten, a.NamSinh, a.GioiTinh, b.SDT1, b.SDT2, b.Email, b.Note }).ToList();
            dgv_DanhBa.ColumnCount = 9;
            dgv_DanhBa.Columns[0].Name = " Họ ";
            dgv_DanhBa.Columns[1].Name = " Tên Đệm";
            dgv_DanhBa.Columns[2].Name = " Tên";
            dgv_DanhBa.Columns[3].Name = " Năm Sinh";
            dgv_DanhBa.Columns[4].Name = " Giới Tính";
            dgv_DanhBa.Columns[5].Name = " SĐT 1";
            dgv_DanhBa.Columns[6].Name = " SĐT 2";
            dgv_DanhBa.Columns[7].Name = " Email";
            dgv_DanhBa.Columns[8].Name = " Ghi Chú";
            dgv_DanhBa.Rows.Clear();
            foreach (var x in ListBig)
            {
                dgv_DanhBa.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh,
                    x.GioiTinh == 1 ? "Nam" : x.GioiTinh == 0 ? "Nữ" : "",
                    x.SDT1, x.SDT2, x.Email, x.Note);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_Ten.Enabled = true;
            txt_ho.Text = "";
            txt_tendem.Text = "";
            txt_Ten.Text = "";
            cbox_namsinh.SelectedIndex = 0;
            rbtn_Nam.Checked = true;
            txt_SDT_1.Text = "";
            txt_SDT_2.Text = "";
            txt_Email.Text = "";
            txt_note.Text = "";
        }
        
        private void ASSIGRMENTS_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (flag == 1|| flag == 2|| flag == 3)
            {
                if (MessageBox.Show("Bạn vừa thay đổi Dữ liệu của Danh bạ mà chưa Lưu.\n Bạn Có muốn lưu lại không?",
                    Erorr,
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Sv.QuestionSave();
                }
            }
        }
    }
}
