using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.ComTypes;

namespace ASSIGMENT_Danh_Ba.Models
{
    [Table("DANHBA")]
    public class DanhBa
    {
        [Key]
        public Guid IdDanhBa { get; set; }
        public int? SDT1 { get; set; }
        public int? SDT2 { get; set; }
        [StringLength(30)]
        public string? Email { get; set; }
        [StringLength(30)]
        public string? Note { get; set; }

        public Guid IdNguoi { get; set; }
        [ForeignKey("IdNguoi")] public Nguoi Nguois { get; set; }

    }
}