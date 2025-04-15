using DeveloperEvaluation.Domain.Entities;
using FluentValidation.Results;
using Xunit;

namespace DeveloperEvaluation.Tests
{
    public class CDBTests
    {
        [Fact]
        public void Calculate_ShouldPopulateMonthCDBCollection()
        {
            // Arrange
            var cdb = new CDB(1000m, 12);

            // Act
            cdb.GenerateMonths(10m, 100m);

            // Assert
            Assert.NotNull(cdb.MonthCDBCollection);
            Assert.Equal(12, cdb.MonthCDBCollection.Count);
            Assert.All(cdb.MonthCDBCollection, month => Assert.True(month.NetValue > 0));
        }

        [Fact]
        public void GetValidationResult_ShouldReturnValidationResult()
        {
            // Arrange
            var cdb = new CDB(1000m, 12);

            // Act
            cdb.GenerateMonths(10m, 100m);
            ValidationResult result = cdb.GetValidationResult();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Calculate_ShouldSetCorrectValuesForCDIAndTB()
        {
            // Arrange
            var cdb = new CDB(1000m, 6);

            // Act
            cdb.GenerateMonths(12m, 80m);

            // Assert
            Assert.Equal(12m, cdb.CDI);
            Assert.Equal(80m, cdb.TB);
        }
    }
}