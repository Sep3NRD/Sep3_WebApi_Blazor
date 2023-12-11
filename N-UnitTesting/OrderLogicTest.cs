using Application.gRPCcon.Order;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Grpc.Core;
using Moq;

namespace N_UnitTesting;

public class OrderLogicTest
{
    private OrderLogic _orderLogic;
    private Mock<IOrderGRPC> _mockOrderGRPC;
    
    [SetUp]
    public void Setup()
    {
        _mockOrderGRPC = new Mock<IOrderGRPC>();
        _orderLogic = new OrderLogic(_mockOrderGRPC.Object);
    }
    
    [Test]
    public async Task CreateAsyncTest()
    {
        // Arrange
        _orderLogic = new OrderLogic(_mockOrderGRPC.Object);

        var createOrderDto = new CreateOrderDto
        {
            OrderDate = "2023-12-11",
            totalPrice = 2000.0,
            Items = new List<Item> { new Item
            {
                Name = "Pre-Built v1",
                Description = "Gaming Computer",
                Category = "Pre-Built",
                Price = 2000.0,
                Stock = 10
            } },
            username = "username",
            addressId = 1
        };

        // Act
        await _orderLogic.CreateAsync(createOrderDto);

        // Assert
        _mockOrderGRPC.Verify(x => x.CreateAsync(It.IsAny<CreateOrderDto>()), Times.Once);
    }

    [Test]
    public async Task ConfirmAsync_ShouldCallGrpcServiceWithCorrectParameters()
    {
        // Arrange
       _orderLogic = new OrderLogic(_mockOrderGRPC.Object);

        // Act
        await _orderLogic.ConfirmAsync(2); // Replace with the orderId you want to test

        // Assert
        _mockOrderGRPC.Verify(x => x.ConfirmAsync(2), Times.Once);
        
    }
    
    
    
    [Test]
    public async Task GetAllAsyncTest()
    {
        //Arrange
        _mockOrderGRPC.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Order>()); 
        
        //Act
        _orderLogic = new OrderLogic(_mockOrderGRPC.Object);
        var result = await _orderLogic.GetAllAsync();
        
        //Assert
        Assert.IsNotNull(result);
    }
    
    [Test]
    public async Task GetAsync_GrpcError_ShouldThrowException()
    {
        //Arrange
        _mockOrderGRPC.Setup(x => x.GetAllAsync())
            .ThrowsAsync(new RpcException(new Status(StatusCode.Internal, "Internal Server Error")));
        //Act
        _orderLogic= new OrderLogic(_mockOrderGRPC.Object);
        //Assert
        Assert.ThrowsAsync<RpcException>(async () => await _orderLogic.GetAllAsync());
    }

}