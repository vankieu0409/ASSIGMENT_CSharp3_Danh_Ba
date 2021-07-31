using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ASSIGMENT_Danh_Ba.Models
{
    [Table("Nguoi")]
    public class Nguoi
    {
        [Key]
        public Guid IdNguoi { get; set; }
        [StringLength(30)]
        public string? Ho { get; set; }
        [StringLength(30)]
        public string? TenDem { get; set; }
        [StringLength(30)]
        public string Ten { get; set; }
        public int NamSinh { get; set; }
        public int GioiTinh { get; set; }
        public ICollection<DanhBa> DanhBas { get; set; }

        public Nguoi()
        {
            DanhBas = new HashSet<DanhBa>();
        }

    }
}