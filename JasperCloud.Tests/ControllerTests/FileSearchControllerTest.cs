namespace JasperCloud.Tests.ControllerTests;

[TestClass]
public class FileSearchControllerTest
{
    [TestMethod]
    public async Task FileSearchReturnsNoneWhenNoFiles()
    {
        // Arrange
        var controller = new FileSearchController();

        // Act
        await controller.FindFiles("", ""); 

        // Assert
        Assert.AreEqual(0, controller.fileNames.Count);
    }
}