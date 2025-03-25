using DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace DeveloperEvaluation.Tests
{
    public class MonthCDBTests
    {
        [Fact]
        public void MonthCDB_ShouldInitializeCorrectly()
        {
            // Arrange
            const int month = 1;
            const decimal initialValue = 1000m;
            var cdb = new CDB(1000m, 12, 0.05m, 0.02m);

            // Act
            var monthCDB = new MonthCDB(month, initialValue, cdb);

            // Assert
            monthCDB.Month.Should().Be(month);
            monthCDB.InitialValue.Should().Be(initialValue);
            monthCDB.CDB.Should().Be(cdb);
        }
    }
}
