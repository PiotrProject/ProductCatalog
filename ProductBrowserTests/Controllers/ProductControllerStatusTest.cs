using Microsoft.AspNetCore.Mvc;
using ProductBrowser.Controllers;
using ProductBrowser.Dtos;
using ProductBrowser.Models;
using ProductBrowser.Repositories;

namespace ProductBrowser.Tests.Controllers;

public class ProductsControllerStatusTests
{
    [Fact]
    public void Add_ValidProduct_ShouldReturn201Created()
    {
        // Arrange
        var repository = new InMemoryProductRepository();
        var controller = new ProductsController(repository);

        var dto = new CreateProductDto
        {
            Kod = "TEST-201",
            Nazwa = "Produkt testowy",
            Cena = 100m
        };

        // Act
        var result = controller.Add(dto);

        // Assert
        var createdResult =
            Assert.IsType<CreatedAtActionResult>(
                result.Result
            );

        Assert.Equal(201, createdResult.StatusCode);

        var product =
            Assert.IsType<Product>(
                createdResult.Value
            );

        Assert.True(product.Id > 0);
        Assert.Equal("TEST-201", product.Kod);
        Assert.Equal("Produkt testowy", product.Nazwa);
        Assert.Equal(100m, product.Cena);

        Assert.Equal(
            nameof(ProductsController.GetById),
            createdResult.ActionName
        );
    }


    [Fact]
    public void GetById_ProductDoesNotExist_ShouldReturn404NotFound()
    {
        // Arrange
        var repository = new InMemoryProductRepository();
        var controller = new ProductsController(repository);

        // Act
        var result = controller.GetById(999999);

        // Assert
        var notFoundResult =
            Assert.IsType<NotFoundObjectResult>(
                result.Result
            );

        Assert.Equal(
            404,
            notFoundResult.StatusCode
        );
    }


    [Fact]
    public void Add_DuplicateCode_ShouldReturn409Conflict()
    {
        // Arrange
        var repository = new InMemoryProductRepository();

        repository.Add(new Product
        {
            Kod = "DUPLICATE",
            Nazwa = "Pierwszy produkt",
            Cena = 100m
        });

        var controller =
            new ProductsController(repository);

        var dto = new CreateProductDto
        {
            Kod = "DUPLICATE",
            Nazwa = "Drugi produkt",
            Cena = 200m
        };

        // Act
        var result = controller.Add(dto);

        // Assert
        var conflictResult =
            Assert.IsType<ConflictObjectResult>(
                result.Result
            );

        Assert.Equal(
            409,
            conflictResult.StatusCode
        );
    }
}