using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ASSIGMENT_Danh_Ba.DBContext;
using ASSIGMENT_Danh_Ba.Interface;
using ASSIGMENT_Danh_Ba.Models;

using Microsoft.EntityFrameworkCore;

namespace ASSIGMENT_Danh_Ba.Sevices
{
    public class Services : IServices
    {
        private List<Nguoi> lstNguoi;
        private List<DanhBa> lstDanhBas;
      
        private DB_CONTEXT_DanhBa db;// kiểu dữ liệu là con của DbContext
        

        public Services()
        {
          
            lstDanhBas = new List<DanhBa>();
            lstNguoi = new List<Nguoi>();
            db = new DB_CONTEXT_DanhBa();
            ActiveDB();
      
        }

        public List<Nguoi> getListNguoi()
        {
            return lstNguoi;
        }

        public List<DanhBa> getlListDanhBa()
        {
            return lstDanhBas;
        }

        public void ActiveDB()
        {

            lstNguoi = db.Nguois.AsNoTracking().ToList();
            lstDanhBas = db.DanhBas.AsNoTracking().ToList();

        }

        public string Them(Nguoi Nguoi, DanhBa DB)
        {
            lstNguoi.Add(Nguoi);
            lstDanhBas.Add(DB);
            db.Nguois.Add(Nguoi);
            db.DanhBas.Add(DB);

            return " THêm thành công";
        }

        public string sua(Nguoi Nguoi, DanhBa DB)
        {
            int temp1 = getIndexNguoi(Nguoi);
            lstNguoi[temp1] = Nguoi;
            int temp2 = getIndexDanhba(DB);
            lstDanhBas[temp2] = DB;
            db.Nguois.Update(Nguoi);
            db.DanhBas.Update(DB);

            return " sửa thành công";
        }

        public string Xoa(Nguoi Nguoi, DanhBa DB)
        {
            int temp1 = getIndexNguoi(Nguoi);
            lstNguoi.RemoveAt(temp1);
            int temp2 = getIndexDanhba(DB);
            lstDanhBas.RemoveAt(temp2);
            db.Nguois.Remove(Nguoi);
            db.DanhBas.Remove(DB);

            return " xóa thành công";
        }

        public string[] getYearofBirth()
        {
            string[] temps = new string[2022 - 1950];
            int temp = 1950;
            for (int i = 0; i < temps.Length; i++)
            {
                temps[i] = temp.ToString();
                temp++;
            }

            return temps;
        }
        public void QuestionSave()
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    db.SaveChanges();
                    saved = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Lưu thất bại", "Error");
                    return;
                }
            }

        }
        private int getIndexNguoi(Nguoi ng)
        {
            int i = lstNguoi.FindIndex(c => c.IdNguoi == ng.IdNguoi);

            return i;
        }

        private int getIndexDanhba(DanhBa dbrr)
        {
            int i = lstDanhBas.FindIndex(c => c.IdDanhBa == dbrr.IdDanhBa);
            return i;
        }
    }
}