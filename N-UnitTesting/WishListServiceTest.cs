using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.gRPCcon.WishList;
using Domain.DTOs;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace Application.Logic.Tests
{
    [TestFixture]
    public class WishListLogicTests
    {
        private Mock<IWIshListGRPC> wishGrpcMock;
        private WishListLogic wishListLogic;

        [SetUp]
        public void Setup()
        {
            wishGrpcMock = new Mock<IWIshListGRPC>();
            wishListLogic = new WishListLogic(wishGrpcMock.Object);
        }

        [Test]
        public async Task AddToWishListAsyncTest()
        {
            // Arrange
            var validDto = new AddToWishListDTO(1, "testUser");

            // Act
            await wishListLogic.AddToWishListAsync(validDto);

            // Assert
            wishGrpcMock.Verify(mock => mock.AddToWishList(validDto), Times.Once);
        }

        [Test]
        public void AddToWishListAsyncException()
        {
            // Arrange
            AddToWishListDTO nullDto = null;

            // Act and Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await wishListLogic.AddToWishListAsync(nullDto));
            Assert.AreEqual("Order is null", ex.Message);
        }

        [Test]
        public async Task GetWishListAsync_WithValidUsername()
        {
            // Arrange
            var validUsername = "testUser";

            // Act
            await wishListLogic.GetWishListAsync(validUsername);

            // Assert
            wishGrpcMock.Verify(mock => mock.GetWishListAsync(validUsername), Times.Once);
        }

        [Test]
        public void GetWishListAsync_WithNullUsername()
        {
            // Arrange
            string nullUsername = null;

            // Act and Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await wishListLogic.GetWishListAsync(nullUsername));
            Assert.AreEqual("Customer id is null", ex.Message);
        }
    }
}
