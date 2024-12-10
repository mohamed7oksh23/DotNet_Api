using DotNet_Api.Models;

namespace DotNet_Api.Repositories
{
    public interface IProductRepo
    {
        public void Create(Product product);

        public void Update(Product product);

        public void Delete(int id);

        public List<Product> Get();
        
        public Product GetbyId(int id);

        public Product ProductfromDb(int id);
    }
}
