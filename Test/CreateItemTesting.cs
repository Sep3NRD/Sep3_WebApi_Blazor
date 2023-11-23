using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;

namespace TestProject1;
[TestFixture]
public class CreateItemTesting
{
    public void AddProduct_ValidProduct_RedirectToAdminDashboard()
    {
         var productServiceMock = new Mock<ProductService.ProductServiceClient>();
        productServiceMock.Setup(x => x.AddProduct(It.IsAny<AddProductRequest>(), It.IsAny<CallOptions>()))
                          .Returns(new AddProductResponse { Success = true });

        var adminController = new AdminController(productServiceMock.Object);

        var newProduct = new Product
        {
            ProductName = "Test Product",
            Description = "This is a test product.",
            Price = 19.99m,
            QuantityInStock = 100
        };

        // Act
        var result = adminController.AddProduct(newProduct) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("AdminDashboard", result.ActionName);
    
}