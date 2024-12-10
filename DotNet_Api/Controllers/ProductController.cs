using DotNet_Api.Models;
using DotNet_Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private IProductRepo productrepo;

        public ProductController(IProductRepo productRepo)
        {
            productrepo = productRepo;
        }

        [HttpGet]                                           //Get All Products
        public IActionResult GetProducts()
        {
            var product = productrepo.Get();
            return Ok(product);
        }

        [HttpGet]                                           //Get By ID
        [Route("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = productrepo.GetbyId(id);
            if (product == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }
            return Ok(product);
        }

        [HttpPost(Name = "CreateProduct")]                      //Create Product
        public IActionResult AddProduct(Product product)
        {
            Product productfromdb = productrepo.ProductfromDb(product.ID);
            if (product == productfromdb)
            {
                //return Conflict(new { Message = "Product is Already Existed"});
                return StatusCode(500, new { Message = "Product is Already Existed" });
            }
            else if (product == null)
            {
                return BadRequest();
            }
            else
            {
                productrepo.Create(product);
                return Created($"/{GetProducts()}", new { Message = "Product Created Successfully" });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Updateproduct(int id,Product product)
        {
            var existproduct = productrepo.GetbyId(id);
            if (existproduct == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }
            existproduct.Name = product.Name;
            existproduct.Description = product.Description;
            existproduct.Price = product.Price;
            productrepo.Update(existproduct);
            return StatusCode(201, product);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = productrepo.GetbyId(id);
            if(product == null)
            {
                return NotFound(new {Message = "Product Invalid"});
            }
            productrepo.Delete(id);
            return StatusCode(201, new { Message = "Deleted Successfully" });
        }
    }
}
