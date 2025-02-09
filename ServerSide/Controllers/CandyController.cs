using Domain.Data_Transfare_Object;
using Domain.DataModels;
using Domain.DataModels.ServerSide.Models.DatabaseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerSide.Models.DatabaseModel;
using ServerSide.Repository;


namespace ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandyController : ControllerBase
    {
        private readonly CandyRepository _Candy;
        private readonly CatigoryRepository _Category;
        public CandyController(CandyRepository Candy,CatigoryRepository Category)
        {
            _Candy = Candy;
            _Category = Category;
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var ret = _Category.Delete(id);
            return ret.IsSucceeded ? Ok(ret.Comment) : BadRequest(ret.Comment);
        }
        [HttpGet("GetCategorys")]
        public async Task<IActionResult> GetCategorys()
        {
            var Catigorys = _Category.GetAll();
            return Ok(Catigorys);
        }
        [HttpGet("GetCandies")]
        public async Task<IActionResult> GetCandies()
        {
            return Ok(await _Candy.GetCandies());
        }
        
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]AddCategoryDto Category)
        {
            CandyCategoryModel NewCategory = new()
            {
                Id = 0,
                Title = Category.Title,
            };
            var ret = _Category.Insert(NewCategory);
            return Ok(ret);
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryData req)
        {

            var NewModel = new CandyCategoryModel()
            {
                Id = req.Id,
                Title = req.Title,
            };
            var ret = _Category.Update(NewModel);
            return ret.IsSucceeded ? Ok(ret.Comment) : BadRequest(ret.Comment);
        }
        [HttpPut("UpdateCandy")]
        public async Task<IActionResult> UpdateCandy([FromBody]CandyData req)
        {
            
            var NewModel = new CandyModel()
            {
                Id =req.Id,
                Name = req.Name,
                Image = req.Image,
                Price = req.Price,
                CategoryId=req.CategoryId
            };
            var ret =  _Candy.Update(NewModel);
            return ret.IsSucceeded ? Ok(ret.Comment) : BadRequest(ret.Comment);
        }
        [HttpPost("AddCandy")]
        public async Task<IActionResult> AddCandy([FromBody] CandyData req)
        {
            if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var NewModel = new CandyModel()
            {
                Name = req.Name,
                Image = req.Image,
                Price = req.Price,
                CategoryId = req.CategoryId
            };
            var ret = _Candy.Insert(NewModel);
            Console.WriteLine(ret.Comment);
            if(ret.IsSucceeded)
            {
                return Ok(ret.Comment);

            }
            else
            {
                return BadRequest(ret.Comment);
            }
        }
        [HttpDelete("DeleteCandy/{id}")]
        public async Task<IActionResult> DeleteCandy(int id)
        {
            var ret = _Candy.Delete(id);
            return ret.IsSucceeded ? Ok(ret.Comment) : BadRequest(ret.Comment);
        }

         [HttpGet("GetCandisByCategory/{id}")]
        public async Task<IActionResult> GetCandisByCategory(int id)
        {
            
            var ret = await _Candy.GetCandiesByCategoryIdAsync(id);
            bool IsSuc = ret.Count >0  ? true : false;
            if (IsSuc)
            {
                return Ok(ret);
            }
            else
            {
                return BadRequest("there Is no Candis in category  or null");

            }
        }
    }
}
