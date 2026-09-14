using ProductBrowser.Models;
using ProductBrowser.Repositories;

namespace ProductBrowser.Tests.Repositories;

public class InMemoryProductRepositoryTest
{
    [Fact]
    public void Add_ShouldAddProduct()
    {
        // Arrange
        var repository = new InMemoryProductRepository();

        var product = new Product
        {
            Kod = "TEST001",
            Nazwa = "Produkt testowy",
            Cena = 100m
        };

        // Act
        var result = repository.Add(product);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("TEST001", result.Kod);
        Assert.Equal("Produkt testowy", result.Nazwa);
        Assert.Equal(100m, result.Cena);
    }

    [Fact]
    public void GetAll_ShouldContainAddedProduct()
    {
        // Arrange
        var repository = new InMemoryProductRepository();

        var product = new Product
        {
            Kod = "P100",
            Nazwa = "Monitor",
            Cena = 999.99m
        };

        repository.Add(product);

        // Act
        var products = repository.GetAllProducts();

        // Assert
        Assert.Contains(
            products,
            p => p.Kod == "P100"
        );
    }

    [Fact]
    public void Add_ShouldAssignDifferentIds()
    {
        // Arrange
        var repository = new InMemoryProductRepository();

        var first = new Product
        {
            Kod = "A",
            Nazwa = "Produkt A",
            Cena = 10m
        };

        var second = new Product
        {
            Kod = "B",
            Nazwa = "Produkt B",
            Cena = 20m
        };

        // Act
        var firstResult = repository.Add(first);
        var secondResult = repository.Add(second);

        // Assert
        Assert.NotEqual(
            firstResult.Id,
            secondResult.Id
        );
    }

}