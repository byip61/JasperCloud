using Moq;
using JasperCloud.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JasperCloud.UnitTests;

[TestClass()]
public class FileSearchControllerTest
{
    [TestMethod]
    public async Task FileSearchFindsFooItems()
    {
        // Arrange
        var controller = new FileSearchController();

        // Act
        await controller.FindFiles("foo", "C://bar/");

        // Assert
        Assert.AreNotEqual(0, controller.fileNames.Count);
    }
}