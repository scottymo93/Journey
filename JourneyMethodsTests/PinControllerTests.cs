using Journey.Controllers;
using Journey.Data;
using Journey.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JourneyMethodsTests;

public class PinControllerTests
{
    [Fact]
    public async Task GetPinById_ReturnsView_WhenPinExists()
    {
        // Arrange
        var mockService = new Mock<IPinService>();

        var testPin = new Pin
        {
            PinId = 1,
            PinName = "Test Park"
        };

        mockService.Setup(s => s.GetPinById(1))
            .ReturnsAsync(testPin);

        var controller = new PinController(mockService.Object);

        // Act
        var result = await controller.GetPinById(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Pin>(viewResult.Model);

        Assert.Equal(1, model.PinId);
    }
    
}