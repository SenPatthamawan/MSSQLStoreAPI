using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSSQLStoreAPI.Models;

namespace MSSQLStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //https://localhost5001/api/Products

    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        //Read Product
        [HttpGet]
        public ActionResult<Products> GetAll()
        {
            //Linq
            //แบบอ่านทั้งหมด
            // var allProducts = _context.Products.ToList();

            //LINQ With Condition
            //หาสินค้าราคาสูงที่สุด 2 ชิ้นแรก เรียงจากราคาสูงไปต่ำ
            //อ่านเพิ่มเติม https://docs.microsoft.com/en-us/ef/core/querying/
            // var allProducts = _context.Products
            //                 .Where(p => p.CategoryId != 0)
            //                 .OrderByDescending(p => p.UnitPrice)
            //                 .Take(2)
            //                 .ToList();

            //LINQ Raw SQL
            //อ่านเพิ่มเติม https://docs.microsoft.com/en-us/ef/core/querying/raw-sql

            // var allProducts = _context.Products
            //                 .FromSqlRaw("SELECT TOP 2 * FROM Products ORDER BY ProductID DESC")
            //                 .ToList();

            //LINQ with Join
            //แบบกำหนดเงื่อนไข
            //ดึงสินค้าเรียงจากราคาสูงสุด-ไปต่ำสุด 3 รายการแรก
            //อ่านเพิ่มเติม https://docs.microsoft.com/en-us/ef/core/querying/complex-query-operators
            var allProducts = (
                from category in _context.Categories
                join product in _context.Products
                on category.CategoryId equals product.CategoryId
                where category.CategoryStatus == 1
                orderby product.UnitPrice descending
                select new
                {
                    product.ProductID,
                    product.ProductName,
                    product.UnitPrice,
                    product.UnitInStock,
                    product.ProductPicture,
                    product.CreatedDate,
                    product.ModifiedDate,
                    category.CategoryName,
                    category.CategoryStatus
                }

            ).ToList();

            return Ok(allProducts);
        }

    }
}