using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace MSSQLStoreAPI.Models
{
    [Table("Category", Schema = "dbo")]
    [Comment("Category table data")]
    public class Category
    {

        public int CategoryId { get; set; } //กำหนด PK แบบที่ 1

        // [Column("CategoryId")] //ชื่อ Column ที่ต้องการให้ Entityframework ไป generate ออกมาว่าเป็นชื่ออะไร
        // public int Id { get; set; } //กำหนด PK แบบที่ 2

        // [Key]
        // [Column("CategoryId")] 
        // public int CategoryNumber { get; set; } //กำหนด PK แบบที่ 3

        [Required] //Not null
        [Column("CatergoryName", TypeName = "varchar(64)", Order = 1)]
        public string CategoryName { get; set; }

        [Required]
        [Column(Order = 2)] //default จะเรียงให้ตามลำดับ แต่บาง DB จะเรียงตามตัวอักษร ถ้าเอาชัวร์ ใส่ order ไว้ก็ดี
        public int CategoryStatus { get; set; }


    }
}