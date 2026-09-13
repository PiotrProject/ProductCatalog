using Microsoft.AspNetCore.Mvc;
using ProductBrowser.Dtos;
using ProductBrowser.Models;
using ProductBrowser.Repositories;

namespace ProductBrowser.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Ok(products);
        }
        [HttpPost]
        public ActionResult<Product> Add(CreateProductDto product)
        {
            var newProduct = new Product
            {
                Kod = product.Kod,
                Nazwa = product.Nazwa,
                Cena = product.Cena
            };
            var createdProduct = _productRepository.Add(newProduct);
            return StatusCode(StatusCodes.Status201Created, createdProduct);
        }
    }
}
