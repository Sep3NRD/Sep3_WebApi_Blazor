using Application.gRPCcon.Item;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Moq;

namespace N_UnitTesting;

public class ItemLogicTest
{
    private ItemLogic _itemLogic;
    private Mock<IItemGRPC> _mockItemGRPC;
    
    [SetUp]
    public void Setup()
    {
        _mockItemGRPC = new Mock<IItemGRPC>();
        _itemLogic = new ItemLogic(_mockItemGRPC.Object);
    }

    [Test]
    public async Task CreateAsyncTesting()
    {
        _mockItemGRPC.Setup(x => x.CreateAsync(It.IsAny<Item>())).ReturnsAsync(new Item
        {
            Name = "nametest",
            Description = "descrpitontest",
            Category = "categorytest",
            Price = 20.0,
            Stock = 10
        });

        var item = new Item 
        {
            Name = "nametest",
            Description = "descrpitontest",
            Category = "categorytest",
            Price = 20.0,
            Stock = 10
        };

        try
        {
            //Act
            var createdItem = await _itemLogic.CreateAsync(item);
            
            //Assert
            Assert.IsNotNull(createdItem);
            // Assert.AreEqual(1,createdItem.ItemId);
            Assert.AreEqual(item.Name, createdItem.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception during test: {e}");
            throw;
        }
    }
}