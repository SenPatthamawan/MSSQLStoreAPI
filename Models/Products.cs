using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MSSQLStoreAPI.Models
{
    [Table("Products", Schema = "dbo")]
    [Comment("ตารางเก็บข้อมูลสินค้า")]

    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [MaxLength(64)] // เช็คฝั่ง API ถ้าเกินจะมี message เตือนออกมา ทำให้เราไม่ต้องเขียน validate เอง
        [Column(TypeName = "varchar(64)", Order = 1)] //varchar(64) ถ้าใส่เกินก็จะ query ไม่ผ่าน แต่จะไม่มี message เตือน
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)", Order = 2)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(Order = 3)]
        public int UnitInStock { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)", Order = 4)]
        public string ProductPicture { get; set; }

        [Column(Order = 5)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(Order = 6)]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [ForeignKey("CatergoryInfo")] //ช่วยยืนยันว่า Data ของทั้ง 2 ข้อมูลผูกกันและมีความสอดคล้องกัน ถ้าลบข้อมูลตารางนึง ก็จะถามหาอีกตารางนึงด้วย
        public int CategoryId { get; set; }
        public virtual Category CatergoryInfo { get; set; } //CategoryId ก็จะได้ join กันทั้ง 2 Table (ForeignKey)

        [NotMapped] //เอา CatergoryName มาโชว์ แต่ไม่เขียนลงไปใน Product อ่านแค่ในโค้ดอย่างเดียว ถึงจะมีความสัมพันธ์แต่จะไม่ได้ข้อมูลมา (จะเป็น null รอไว้) เป็นแค่ variable/Properties เราต้อง join table ใน controller เอง
        public string CatergoryName { get; set; } //ไม่ต้องใส่ ForeignKey ก็ได้ เพราะต้องไป join เอาค่ามาใส่เองอยู่ดี แต่ทำ FK เพราะยืนยัน Data ของทั้ง 2 Table ว่ามีความสอดคล้องกัน

    }
}