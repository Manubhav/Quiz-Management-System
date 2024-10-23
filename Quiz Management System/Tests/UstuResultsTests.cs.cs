using NUnit.Framework;
using Quiz_Management_System;
using Quiz_Management_System.Models;
using System.Data;
using System.Threading.Tasks;

[TestFixture]
public class UstuResultsTests
{
    private UstuResults ustuResults;

    [SetUp]
    public void Setup()
    {
        ustuResults = new UstuResults();
    }

    [Test]
    public async Task GetResultsAsync_ShouldReturnDataTable_WhenValidInput()
    {
        // Arrange
        string subject = "Math";
        string group = "Group A";

        // Act
        DataTable result = await ustuResults.GetResultsAsync(subject, group);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Rows.Count > 0, "Expected results to be returned");
    }
}
