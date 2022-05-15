using EF_PostgreSQL.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EF_PostgreSQL.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors("CorsSample")]
    public class studentController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var message = "";
        //    using (var context = new studentDbContext())
        //    {
        //        var student = new student
        //        {
        //            name = "Rector",
        //            age = 12
        //        };
        //        context.students.Add(student);
        //        var i = context.SaveChanges();
        //        message = i > 0 ? "数据写入成功" : "数据写入失败";
        //    }
        //    return Ok(message);
        //}

        private readonly studentDbContext _dbContext;
        public studentController(studentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var message = "";
            try
            {
                using (_dbContext)
                {
                    _dbContext.Database.EnsureDeleted();
                    _dbContext.Database.EnsureCreated();
                    _dbContext.SaveChanges();
                    message = "数据表更新成功！";
                }
            }
            catch (Exception)
            {
                message = "数据表更新失败！";
                throw;
            }
            return Ok(message);
        }

        [HttpGet]
        public IActionResult Insert(string name, int age)
        {
            var message = "";
            using (_dbContext)
            {
                var k = _dbContext.Set<student>().Where(s => (s.name == name && s.age == age)).ToList();
                if (k.Count > 0)
                {
                    message = "该名称已存在！";
                    return BadRequest(message);
                }
                else
                {
                    var student = new student
                    {
                        name = name,
                        age = age
                    };
                    _dbContext.students.Add(student);
                    var i = _dbContext.SaveChanges();
                    message = i > 0 ? "数据写入成功" : "数据写入失败";
                    return Ok(message);
                }
            }
        }

        [HttpGet]
        public IActionResult SelectAll()
        {
            using (_dbContext)
            {
                var list = _dbContext.students.ToList();
                return Ok(list);
            }
        }

        [HttpGet]
        public IActionResult SelectById(int id)
        {
            using (_dbContext)
            {
                var list = _dbContext.students.Find(id);
                return Ok(list);
            }
        }

        [HttpGet]
        public List<student> SelectByName(string name)
        {
            var list = _dbContext.students.Where(s => s.name == name).ToList();
            return list;
        }

        [HttpGet]
        public List<student> SelectByNameAndAge(string name, int age)
        {
            var list = _dbContext.students.Where(s => (s.name == name && s.age == age)).ToList();
            return list;
        }

        [HttpGet]
        public IActionResult DeleteByNameAndAge(string name, int age)
        {
            var list = _dbContext.students.Where(s => (s.name == name && s.age == age)).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    _dbContext.students.Remove(item);
                }
                _dbContext.SaveChanges();
            }
            return Ok("");
        }

        [HttpGet]
        public IActionResult UpdateByNameAndAge(string name, int age, string newName, int newAge)
        {
            var list = _dbContext.students.Where(s => (s.name == name && s.age == age)).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.name = newName;
                    item.age = newAge;
                }
                _dbContext.SaveChanges();
            }
            return Ok("");
        }
    }
}
