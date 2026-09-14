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

        public ProductsController(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET /api/products
        // 200 OK
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products =
                _productRepository.GetAllProducts();

            return Ok(products);
        }

        // GET /api/products/1
        // 200 OK
        // 404 Not Found
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id)
        {
            var product =
                _productRepository.GetById(id);

            if (product is null)
            {
                return NotFound(new
                {
                    message =
                        $"Produkt o Id {id} nie istnieje."
                });
            }

            return Ok(product);
        }

        // POST /api/products
        // 201 Created
        // 400 Bad Request - automatycznie przez ApiController
        // 409 Conflict
        [HttpPost]
        public ActionResult<Product> Add(
            CreateProductDto product)
        {
            if (_productRepository.ExistsByCode(product.Kod))
            {
                return Conflict(new
                {
                    message =
                        $"Produkt o kodzie '{product.Kod}' już istnieje."
                });
            }

            var newProduct = new Product
            {
                Kod = product.Kod,
                Nazwa = product.Nazwa,
                Cena = product.Cena
            };

            var createdProduct =
                _productRepository.Add(newProduct);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProduct.Id },
                createdProduct
            );
        }
    }
}