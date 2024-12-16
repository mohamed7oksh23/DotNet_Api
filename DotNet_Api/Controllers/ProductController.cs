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
            Product productfromdb = productrepo.GetbyId(product.ID);
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
                return RedirectToAction("GetProducts", new { Message = "Product Created Successfully" });
                //return CreatedAtAction("GetProducts", new { id = product.ID }, new { Message = "Product created successfully" });                
                //return Created($"/{GetProducts()}", new { Message = "Product Created Successfully" });
            }
        }

        [HttpPut]                                                       //Update Products
        [Route("{id}")]
        public IActionResult Updateproduct(int id,Product product)
        {
            var existproduct = productrepo.GetbyId(id);
            if (existproduct == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }
            else if (product.Name == null)
            {
                return BadRequest(new { Message = "Product Name cannot be null" });
            } 
            else if (product.Price == 0)
            {
                return BadRequest(new { Message = "Product Price cannot be null" });
            }
            else if (product.Description == null)
            {
                return BadRequest(new { Message = "Product Description cannot be null" });
            }
            existproduct.Name = product.Name;
            existproduct.Description = product.Description;
            existproduct.Price = product.Price;
            productrepo.Update(existproduct);
            return StatusCode(201, new { Message = "Product updated successfully", existproduct });
        }

        [HttpDelete]                                                    //Delete Product
        [Route("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = productrepo.GetbyId(id);
            if(product == null)
            {
                return NotFound(new {Message = "Product Invalid, The Product is not Existed"});
            }
            productrepo.Delete(id);
            return StatusCode(200, new { Message = "Deleted Successfully" });
        }
    }
}
