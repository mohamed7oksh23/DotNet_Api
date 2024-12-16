using DotNet_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Api.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly MyDbContext context;

        public ProductRepo(MyDbContext myDbContext)
        {
            context = myDbContext;
        }

        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var productid = context.Products.FirstOrDefault(d => d.ID == id);
            context.Products.Remove(productid!);
            context.SaveChanges();
        }

        public List<Product> Get()
        {
            List<Product> product = context.Products.ToList();
            return product;
        }

        public Product GetbyId(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.ID == id);
            return product!;
        }

        public void Update(Product product)
        {
            context.Update(product);
            context.SaveChanges();
        }

    }
}
