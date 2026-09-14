using Microsoft.AspNetCore.Mvc;
using ProductBrowser.Controllers;
using ProductBrowser.Dtos;
using ProductBrowser.Models;
using ProductBrowser.Repositories;

namespace ProductBrowser.Tests.Controllers;

public class ProductsControllerTests
{
    [Fact]
    public void GetAll_ShouldReturnOk()
    {
        // Arrange
        var repository = new InMemoryProductRepository();
        var controller = new ProductsController(repository);

        // Act
        var result = controller.GetProducts();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAll_ShouldReturnProducts()
    {
        // Arrange
        var repository = new InMemoryProductRepository();

        repository.Add(new Product
        {
            Kod = "P100",
            Nazwa = "Monitor",
            Cena = 1000m
        });

        var controller = new ProductsController(repository);

        // Act
        var result = controller.GetProducts();

        var okResult =
            Assert.IsType<OkObjectResult>(result.Result);

        var products =
            Assert.IsAssignableFrom<IEnumerable<Product>>(
                okResult.Value
            );

        // Assert
        Assert.Contains(
            products,
            p => p.Kod == "P100"
        );
    }

    [Fact]
    public void Add_ShouldReturn201Created()
    {
        // Arrange
        var repository = new InMemoryProductRepository();
        var controller = new ProductsController(repository);

        var dto = new CreateProductDto
        {
            Kod = "P200",
            Nazwa = "Laptop",
            Cena = 3500m
        };

        // Act
        var result = controller.Add(dto);

        // Assert
        var objectResult =
            Assert.IsType<ObjectResult>(result.Result);

        Assert.Equal(201, objectResult.StatusCode);
    }

    [Fact]
    public void Add_ShouldReturnCreatedProduct()
    {
        // Arrange
        var repository = new InMemoryProductRepository();
        var controller = new ProductsController(repository);

        var dto = new CreateProductDto
        {
            Kod = "P300",
            Nazwa = "Klawiatura",
            Cena = 299.99m
        };

        // Act
        var result = controller.Add(dto);

        var objectResult =
            Assert.IsType<ObjectResult>(result.Result);

        var product =
            Assert.IsType<Product>(objectResult.Value);

        // Assert
        Assert.True(product.Id > 0);
        Assert.Equal("P300", product.Kod);
        Assert.Equal("Klawiatura", product.Nazwa);
        Assert.Equal(299.99m, product.Cena);
    }

    [Fact]
    public void Add_ShouldAddProductToRepository()
    {
        // Arrange
        var repository = new InMemoryProductRepository();
        var controller = new ProductsController(repository);

        var dto = new CreateProductDto
        {
            Kod = "P400",
            Nazwa = "Mysz",
            Cena = 150m
        };

        // Act
        controller.Add(dto);

        var products = repository.GetAllProducts();

        // Assert
        Assert.Contains(
            products,
            p => p.Kod == "P400"
        );
    }
}