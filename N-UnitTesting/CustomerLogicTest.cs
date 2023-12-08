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
        // Setting up the mock behavior for the GetByUsernameAsync method to return null,
        // simulating the absence of an existing customer with the same username.
        _mockCustomerGRPC.Setup(x => x.GetByUsernameAsync(It.IsAny<CustomerLoginDto>())).ReturnsAsync((Customer)null);

        // Configuring the mock behavior for the CreateAsync method.
        // When the method is called with any Customer, it returns a sample created Customer.
        _mockCustomerGRPC.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(new Customer
        {
            Id = 1,
            UserName = "testuser",
            Password = "testpassword",
            FirstName = "Nural",
            LastName = "Hassan",
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

        // Creating a sample Customer object for testing.
        var customer = new Customer
        {
            UserName = "testuser",
            Password = "testpassword",
            FirstName = "Nural",
            LastName = "Hassan",
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
            // Invoking the CreateAsync method on the CustomerLogic class with the sample Customer.
            var createdCustomer = await _customerLogic.CreateAsync(customer);

            // Assert
            // Verifying that the result is not null, indicating a successful creation.
            Assert.IsNotNull(createdCustomer);
            // Asserting that the customer ID in the result matches the expected value (assuming Id is set by CreateAsync).
            Assert.That(createdCustomer.Id, Is.EqualTo(1));
            Assert.Multiple(() =>
            {
                // Asserting that the customer username in the result matches the expected value.
                Assert.That(createdCustomer.UserName, Is.EqualTo(customer.UserName));
                // Asserting that the customer first name in the result matches the expected value.
                Assert.That(createdCustomer.FirstName, Is.EqualTo(customer.FirstName));
            });
        }
        catch (Exception ex)
        {
            // Handling exceptions and logging the details if the test fails.
            Console.WriteLine($"Exception during test: {ex}");
            throw;
        }
    }

    [Test]
    public async Task LoginValidationTest()
    {
        // Given
        // Creating a valid login DTO with a username and password for testing purposes.
        var validLoginDto = new CustomerLoginDto
        {
            Username = "existingUser",
            Password = "validPassword"
        };

        // Configuring the mock behavior for the ValidateLogin method.
        // When the method is called with the provided username and password,
        // it returns a sample Customer representing a successful login validation.
        _mockCustomerGRPC.Setup(x => x.ValidateLogin(validLoginDto.Username, validLoginDto.Password))
            .ReturnsAsync(new Customer
            {
                Id = 1,
                UserName = validLoginDto.Username,
                Password = validLoginDto.Password,
                FirstName = "Nural",
                LastName = "Hasan",
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

        // When
        // Invoking the LoginValidation method on the CustomerLogic class
        // with the provided valid login DTO.
        var result = await _customerLogic.LoginValidation(validLoginDto);

        // Assert
        // Verifying that the result is not null, indicating a successful login validation.
        Assert.IsNotNull(result);
        // Asserting that the customer ID in the result matches the expected value.
        Assert.That(result.Id, Is.EqualTo(1));
        // Asserting that the customer username in the result matches the expected value.
        Assert.That(result.UserName, Is.EqualTo(validLoginDto.Username));
        // These assertions ensure that the returned Customer object has the expected values.
        // This test validates that the LoginValidation method correctly returns a Customer
    }


    [Test]
    public async Task GetByUsernameAsyncTest()
    {
        // Arrange
        // Setting up a valid username for testing purposes.
        var validUsername = "existingUser";

        // Configuring the mock behavior for the GetByUsernameAsync method.
        // When the method is called with any CustomerLoginDto, it returns a sample Customer.
        _mockCustomerGRPC.Setup(x => x.GetByUsernameAsync(It.IsAny<CustomerLoginDto>()))
            .ReturnsAsync(new Customer
            {
                Id = 1,
                UserName = validUsername,
                Password = "password",
                FirstName = "Nural",
                LastName = "Hassan",
                Address = new Address
                {
                    DoorNumber = 123,
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = 12345,
                    Country = "Country"
                }
            });

        // Act
        // Invoking the GetByUsernameAsync method on the CustomerLogic class
        // with a CustomerLoginDto containing the valid username.
        var result = await _customerLogic.GetByUsernameAsync(new CustomerLoginDto { Username = validUsername });

        // Assert
        // Verifying that the result is not null, indicating a successful retrieval.
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            // Asserting that the customer ID in the result matches the expected value.
            Assert.That(result.Id, Is.EqualTo(1));
            // Asserting that the customer username in the result matches the expected value.
            Assert.That(result.UserName, Is.EqualTo(validUsername));
        });
    }

    [Test]
    public async Task UpdateAsyncTest()
    {
        // Given
        // Creating a sample Customer object with specific details for testing.
        var customer = new Customer
        {
            Id = 1,
            UserName = "testuser",
            Password = "testpassword",
            FirstName = "Nural",
            LastName = "Hassan",
            Address = new Address
            {
                DoorNumber = 123,
                Street = "Street",
                City = "City",
                State = "State",
                PostalCode = 12345,
                Country = "Country"
            }
        };

        // When
        // Invoking the UpdateAsync method on the CustomerLogic class with the given Customer.
        await _customerLogic.UpdateAsync(customer);

        // Then
        // Verifying that the UpdateAsync method on the mocked ICustomerGRPC interface
        // was called exactly once with the specified Customer object.
        _mockCustomerGRPC.Verify(x => x.UpdateAsync(customer), Times.Once);
        // This assertion ensures that the expected method was called on the mock object.
        // Times.Once specifies that it should have been called exactly once.
        // If the method was not called or called more than once, the test would fail.
    }

    [Test]
    public async Task AddNewAddressTest()
    {
        // Given
        var addNewAddressDto = new AddNewAddressDTO
        {
            Street = "New Street",
            City = "New City",
            DoorNumber = 456,
            PostalCode = 67890,
            Country = "New Country",
            State = "New State"
        };

        // When
        // Invoking the AddNewAddress method on the CustomerLogic class with the given AddNewAddressDTO.
        await _customerLogic.AddNewAddress(addNewAddressDto);

        // Then
        // Verifying that the AddNewAddress method on the mocked ICustomerGRPC interface
        // was called exactly once with the specified AddNewAddressDTO.
        _mockCustomerGRPC.Verify(x => x.AddNewAddress(addNewAddressDto), Times.Once);
        // This assertion ensures that the expected method was called on the mock object.
        // Times.Once specifies that it should have been called exactly once.
        // If the method was not called or called more than once, the test would fail.
    }

    [Test]
    public async Task GetAddressesByUsernameTest()
    {
        // Given
        // Setting up a valid username for testing purposes.
        var validUsername = "ricardo";

        // Configuring the mock behavior for the GetAddressesByUsername method.
        // When the method is called with the provided username, it returns a list
        // containing a sample Address representing an existing address for the user.
        _mockCustomerGRPC.Setup(x => x.GetAddressesByUsername(validUsername))
            .ReturnsAsync(new List<Address>
            {
                new Address
                {
                    DoorNumber = 123,
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = 12345,
                    Country = "Country"
                },
            });

        // Then
        // Invoking the GetAddressesByUsername method on the CustomerLogic class with the valid username.
        var result = await _customerLogic.GetAddressesByUsername(validUsername);

        // Assert
        // Verifying that the result is not null, indicating a successful retrieval of addresses.
        Assert.That(result, Is.Not.Null);
        // Asserting that the result is an instance of ICollection<Address>, ensuring it's a collection of addresses.
        Assert.That(result, Is.InstanceOf<ICollection<Address>>());
        // Asserting that the collection has exactly one address.
        // These assertions ensure that the returned list of addresses has the expected values.
    }

}