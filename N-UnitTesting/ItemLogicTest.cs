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
    
    
    [Test]
    public async Task GetByIdAsyncTest()
    {
        // Arrange
        var id = 1;

        _mockItemGRPC.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Item
            {
                ItemId = id,
                Name = "Nvidia GTX 3060",
                Description = "Gaming graphics card",
                Category = "GPU",
                Price = 200.0,
                Stock = 10
                
            });

        // Act
        var result = await _itemLogic.GetByIdAsync(id);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.ItemId);
        //I THINK THERE SHOULD BE 1 MORE BUT IM NOT SURE HAT
    }
}