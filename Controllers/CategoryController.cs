using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSSQLStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using MSSQLStoreAPI.Authentication;

namespace MSSQLStoreAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")] //https://localhost5001/api/Category
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        //Read Catergories
        [HttpGet]
        public ActionResult<Category> GetAll()
        { //ชื่อฟังก์ชั่น เป็นชื่ออะไรก็ได้ GetAll() จะรอ return กลับมา
            var allCategory = _context.Categories.ToList(); //Categories มาจากในไฟล์ AppDbContext
            return Ok(allCategory);
        }

        //Get Category by ID
        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var Category = _context.Categories.Where(c => c.CategoryId == id);
            return Ok(Category);
        }

        //Create new Catergory
        [HttpPost]
        public ActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(category.CategoryId);
        }

        //Update Category
        [HttpPut]
        public ActionResult Update(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            _context.Update(category);
            _context.SaveChanges();
            return Ok(category);
        }

        //Delete Category
        [HttpDelete("{id}")]//ถ้ามี parameter 1 ตัวก็ไม่ต้องใส่ id ในนี้ก็ได้
        public ActionResult Delete(int id)
        {
            var categoryToDelete = _context.Categories.Where(c => c.CategoryId == id).FirstOrDefault(); //c เป็นตัวรับค่า เขียนด้วยรูปแบบ Linq //method FirstOrDefault() 5ถ้าไม่พบจะเอาค่าเริ่มต้นของ Field นั้นมาใช้งาน
            if(categoryToDelete == null){
                return NotFound();
            }
            _context.Remove(categoryToDelete);
            _context.SaveChanges();
            
            return Ok(categoryToDelete);
        }
    }
}