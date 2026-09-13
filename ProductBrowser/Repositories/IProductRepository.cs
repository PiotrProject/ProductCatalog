using ProductBrowser.Models;

namespace ProductBrowser.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product Add (Product product);
    }
}
