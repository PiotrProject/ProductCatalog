using ProductBrowser.Models;

namespace ProductBrowser.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Kod = "P001", Nazwa = "Produkt 1", Cena = 10.99m },
            new Product { Id = 2, Kod = "P002", Nazwa = "Produkt 2", Cena = 15.49m },
        };
        private object _lock = new();
        public IEnumerable<Product> GetAllProducts()
        {
            lock (_lock)
            {
                return _products.ToList();
            }
        }
        public Product Add(Product product)
        {
            lock (_lock)
            {
                product.Id = _products.Any() ? _products.Max(x => x.Id) + 1 : 1;
                _products.Add(product);
                return product;
            }
        }
    }
}
