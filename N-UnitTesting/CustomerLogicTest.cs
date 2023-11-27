using Application.gRPCcon.Costumer;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Moq;

namespace N_UnitTesting;

public class CustomerLogicTest
{
   private CustomerLogic _customerLogic; 
   // using mock for simulating the behaviour of the object/class inside
   // and to avoid relying to another services 
   private Mock<ICustomerGRPC> _mockCustomerGRPC;
        
    [SetUp]
    public void Setup()
    {
        _mockCustomerGRPC = new Mock<ICustomerGRPC>();
        _customerLogic = new CustomerLogic(_mockCustomerGRPC.Object);
    }

    [Test]
    public async Task CreateAsyncTesting()
    {
        // Arrange
        _mockCustomerGRPC.Setup(x => x.GetByUsernameAsync(It.IsAny<CustomerLoginDto>())).ReturnsAsync((Customer)null);
        _mockCustomerGRPC.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(new Customer
        {
            Id = 1,
            UserName = "testuser",
            Password = "testpassword",
            FirstName = "John",
            LastName = "Doe",
            Address = new Address
            {
                DoorNumber = 123,
                Street = "Test Street",
                City = "Test City",
                State = "Test State",
                PostalCode = 12345,
                Country = "Test Country"
            }
        });

        var customer = new Customer
        {
            UserName = "testuser",
            Password = "testpassword",
            FirstName = "John",
            LastName = "Doe",
            Address = new Address
            {
                DoorNumber = 123,
                Street = "Test Street",
                City = "Test City",
                State = "Test State",
                PostalCode = 12345,
                Country = "Test Country"
            }
        };

        try
        {
            // Act
            var createdCustomer = await _customerLogic.CreateAsync(customer);

            // Assert
            Assert.IsNotNull(createdCustomer);
            Assert.AreEqual(1, createdCustomer.Id); // Assuming Id is set by the CreateAsync method
            Assert.AreEqual(customer.UserName, createdCustomer.UserName);
            Assert.AreEqual(customer.FirstName, createdCustomer.FirstName);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during test: {ex}");
            throw;
        }
    }

    
    [Test]
    public async Task LoginValidationTest()
    {
        // Arrange
        CustomerLoginDto validLoginDto = new CustomerLoginDto
        {
            Username = "existingUser",
            Password = "validPassword"
        };

        _mockCustomerGRPC.Setup(x => x.ValidateLogin(validLoginDto.Username, validLoginDto.Password))
            .ReturnsAsync(new Customer
            {
                Id = 1,
                UserName = validLoginDto.Username,
                Password = validLoginDto.Password,
                FirstName = "John",
                LastName = "Doe",
                Address = new Address
                {
                    DoorNumber = 123,
                    Street = "Test Street",
                    City = "Test City",
                    State = "Test State",
                    PostalCode = 12345,
                    Country = "Test Country"
                }
            });

        // Act
        var result = await _customerLogic.LoginValidation(validLoginDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual(validLoginDto.Username, result.UserName);
        
    }

    [Test]
    public async Task GetByUsernameAsyncTest()
    {
        // Arrange
        var validUsername = "existingUser";

        _mockCustomerGRPC.Setup(x => x.GetByUsernameAsync(It.IsAny<CustomerLoginDto>()))
            .ReturnsAsync(new Customer
            {
                Id = 1,
                UserName = validUsername,
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                Address = new Address
                {
                    DoorNumber = 123,
                    Street = "Test Street",
                    City = "Test City",
                    State = "Test State",
                    PostalCode = 12345,
                    Country = "Test Country"
                }
            });

        // Act
        var result = await _customerLogic.GetByUsernameAsync(new CustomerLoginDto { Username = validUsername });

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual(validUsername, result.UserName);
        
    }
}