using ProductBrowser.Models;

namespace ProductBrowser.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product Add (Product product);
        Product? GetById(int id);
        bool ExistsByCode(string code);
    }
}
