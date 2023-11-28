//
// using Blazor.Services.Http;
// using Blazored.LocalStorage;
// using Blazored.Toast.Services;
// using Domain.Models;
// using Moq;
// using NUnit.Framework;
//
// namespace Blazor.Tests.Services.Http
// {
//     [TestFixture]
//     public class ShoppingCartServiceTests
//     {


//         
//         [Test]
//         public async Task GetAllItems_WhenCartIsEmpty_ReturnsEmptyList()
//         {
//             // Arrange
//             var localStorageMock = new Mock<ILocalStorageService>();
//             var cart = new List<Item>();
//
//             localStorageMock
//                 .Setup(m => m.SetItemAsync("cart", It.IsAny<List<Item>>()))
//                 .Callback<string, List<Item>>((key, value) => cart = value)
//                 .Returns(Task.CompletedTask);
//
//             var toastServiceMock = new Mock<IToastService>();
//             var shoppingCartService = new ShoppingCartService(localStorageMock.Object, toastServiceMock.Object);
//
//             // Act
//             var result = await shoppingCartService.GetAllItems();
//
//             // Assert
//             Assert.IsEmpty(result);
//         }
//
//
//
//
//         [Test]
//         public async Task GetAllItems_WhenCartIsNotEmpty_ReturnsListOfItems()
//         {
//             // Arrange
//             var itemsInCart = new List<Item> { new Item { ItemId = 1, Name = "Product 1" }, new Item { ItemId = 2, Name = "Product 2" } };
//
//             var localStorageMock = new Mock<ILocalStorageService>();
//             localStorageMock.Setup(m => m.GetItemAsync<List<Item>>(It.IsAny<string>())).ReturnsAsync(itemsInCart);
//
//             var toastServiceMock = new Mock<IToastService>();
//             var shoppingCartService = new ShoppingCartService(localStorageMock.Object, toastServiceMock.Object);
//
//             // Act
//             var result = await shoppingCartService.GetAllItems();
//
//             // Assert
//             CollectionAssert.AreEqual(itemsInCart, result);
//         }
//
//         [Test]
//         public async Task DeleteItem_WhenCartIsNull_DoesNotModifyCart()
//         {
//             // Arrange
//             var localStorageMock = new Mock<ILocalStorageService>();
//             localStorageMock.Setup(m => m.GetItemAsync<List<Item>>(It.IsAny<string>())).ReturnsAsync((List<Item>)null);
//
//             var toastServiceMock = new Mock<IToastService>();
//             var shoppingCartService = new ShoppingCartService(localStorageMock.Object, toastServiceMock.Object);
//
//             var itemToDelete = new Item { ItemId = 1, Name = "Product 1" };
//
//             // Act
//             await shoppingCartService.DeleteItem(itemToDelete);
//
//             // Assert
//             // No exception should be thrown, and no modifications to cart should occur.
//         }
//
//         [Test]
//         public async Task DeleteItem_WhenItemExistsInCart_RemovesItemFromCart()
//         {
//             // Arrange
//             var itemToDelete = new Item { ItemId = 1, Name = "Product 1" };
//             var itemsInCart = new List<Item> { itemToDelete, new Item { ItemId = 2, Name = "Product 2" } };
//
//             var localStorageMock = new Mock<ILocalStorageService>();
//             localStorageMock.Setup(m => m.GetItemAsync<List<Item>>(It.IsAny<string>())).ReturnsAsync(itemsInCart);
//
//             var toastServiceMock = new Mock<IToastService>();
//             var shoppingCartService = new ShoppingCartService(localStorageMock.Object, toastServiceMock.Object);
//
//             // Act
//             await shoppingCartService.DeleteItem(itemToDelete);
//
//             // Assert
//             CollectionAssert.DoesNotContain(itemsInCart, itemToDelete);
//         }
//
//         [Test]
//         public async Task Clear_CallsLocalStorageClearAsync()
//         {
//             // Arrange
//             var localStorageMock = new Mock<ILocalStorageService>();
//             var toastServiceMock = new Mock<IToastService>();
//             var shoppingCartService = new ShoppingCartService(localStorageMock.Object, toastServiceMock.Object);
//
//             // Act
//             await shoppingCartService.Clear();
//
//             // Assert
//             localStorageMock.Verify(m => m.ClearAsync(), Times.Once);
//         }
//
//         [Test]
//         public async Task AddToCart_WhenCartIsNull_InitializesNewCart()
//         {
//             // Arrange
//             var itemToAdd = new Item { ItemId = 1, Name = "Product 1" };
//
//             var localStorageMock = new Mock<ILocalStorageService>();
//             localStorageMock.Setup(m => m.GetItemAsync<List<Item>>(It.IsAny<string>())).ReturnsAsync((List<Item>)null);
//
//             var toastServiceMock = new Mock<IToastService>();
//             var shoppingCartService = new ShoppingCartService(localStorageMock.Object, toastServiceMock.Object);
//
//             // Act
//             await shoppingCartService.AddToCart(itemToAdd);
//
//             // Assert
//             localStorageMock.Verify(m => m.SetItemAsync("cart", It.IsAny<List<Item>>()), Times.Once);
//         }
//
//         [Test]
//         public async Task AddToCart_WhenCartIsNotNull_AddsItemToCart()
//         {
//             // Arrange
//             var itemToAdd = new Item { ItemId = 1, Name = "Product 1" };
//             var itemsInCart = new List<Item> { new Item { ItemId = 2, Name = "Product 2" } };
//
//             var localStorageMock = new Mock<ILocalStorageService>();
//             localStorageMock.Setup(m => m.GetItemAsync<List<Item>>(It.IsAny<string>())).ReturnsAsync(itemsInCart);
//
//             var toastServiceMock = new Mock<IToastService>();
//             var shoppingCartService = new ShoppingCartService(localStorageMock.Object, toastServiceMock.Object);
//
//             // Act
//             await shoppingCartService.AddToCart(itemToAdd);
//
//             // Assert
//             CollectionAssert.Contains(itemsInCart, itemToAdd);
//         }
//     }
// }