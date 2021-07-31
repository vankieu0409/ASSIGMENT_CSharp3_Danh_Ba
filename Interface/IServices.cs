using System.Collections.Generic;
using ASSIGMENT_Danh_Ba.Models;

namespace ASSIGMENT_Danh_Ba.Interface
{
    public interface IServices
    {
        List<Nguoi> getListNguoi();
        List<DanhBa> getlListDanhBa();
        void ActiveDB();
        string Them(Nguoi Nguoi, DanhBa DB);
        string sua(Nguoi Nguoi, DanhBa DB);
        string Xoa(Nguoi Nguoi, DanhBa DB);
        string[] getYearofBirth();
        void QuestionSave();
     


    }
}