using DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace DeveloperEvaluation.Tests
{
    public class CDBTests
    {
        [Fact]
        public void CDB_ShouldInitializeCorrectly()
        {
            // Arrange
            const decimal value = 1000m;
            const int months = 12;
            const decimal cdi = 0.05m;
            const decimal tb = 0.02m;

            // Act
            var cdb = new CDB(value, months, cdi, tb);

            // Assert
            cdb.Value.Should().Be(value);
            cdb.Months.Should().Be(months);
            cdb.CDI.Should().Be(cdi);
            cdb.TB.Should().Be(tb);
        }

        [Fact]
        public void GenerateMonthCDBCollection_ShouldGenerateCorrectNumberOfMonths()
        {
            // Arrange
            var cdb = new CDB(1000m, 12, 0.05m, 0.02m);

            // Act
            cdb.Calculate(0.05m, 0.02m);

            // Assert
            cdb.MonthCDBCollection.Should().HaveCount(12);
        }

        [Theory]
        [InlineData(1, 0.225)]
        [InlineData(2, 0.225)]
        [InlineData(3, 0.225)]
        [InlineData(4, 0.225)]
        [InlineData(5, 0.225)]
        [InlineData(6, 0.225)]
        [InlineData(7, 0.20)]
        [InlineData(8, 0.20)]
        [InlineData(9, 0.20)]
        [InlineData(10, 0.20)]
        [InlineData(11, 0.20)]
        [InlineData(12, 0.20)]
        [InlineData(13, 0.175)]
        [InlineData(14, 0.175)]
        [InlineData(15, 0.175)]
        [InlineData(16, 0.175)]
        [InlineData(17, 0.175)]
        [InlineData(18, 0.175)]
        [InlineData(19, 0.175)]
        [InlineData(20, 0.175)]
        [InlineData(21, 0.175)]
        [InlineData(22, 0.175)]
        [InlineData(23, 0.175)]
        [InlineData(24, 0.175)]
        [InlineData(25, 0.15)]

        public void GenerateMonthCDBCollection_SetTaxPercentage(int months, decimal taxPercentage)
        {
            // Arrange
            var cdb = new CDB(1000m, months, 0.05m, 0.02m);

            // Act
            cdb.Calculate(1, 1);

            // Assert
            cdb.TaxPercentage.Should().Be(taxPercentage);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void GetValidationResult_ShouldReturnValidResult(int months, bool isValid)
        {
            // Arrange
            var cdb = new CDB(1000m, months, 0.05m, 0.02m);
            cdb.Calculate(1, 1);

            // Act
            var validationResult = cdb.GetValidationResult();

            // Assert
            validationResult.IsValid.Should().Be(isValid);
        }

        [Fact]
        public void Calculate()
        {
            // Arrange
            var cdb = new CDB(1000m, 3, 0.05m, 0.02m);

            // Act
            cdb.Calculate(0.05m, 0.02m);
            var monthCDBCollectionList = cdb?.MonthCDBCollection?.ToList();

            // Assert
            cdb?.Value.Should().Be(1000);
            cdb?.Months.Should().Be(3);
            cdb?.CDI.Should().Be(0.05m);
            cdb?.TB.Should().Be(0.02m);
            cdb?.GrossValue.Should().Be(1002.10m);
            cdb?.TaxPercentage.Should().Be(0.225m);
            cdb?.TaxAmount.Should().Be(225.47m);
            cdb?.NetValue.Should().Be(776.63m);

            monthCDBCollectionList?.Count.Should().Be(3);

            monthCDBCollectionList?[0].Month.Should().Be(1);
            monthCDBCollectionList?[0].InitialValue.Should().Be(1000m);
            monthCDBCollectionList?[0].FinalValue.Should().Be(1000.70m);

            monthCDBCollectionList?[1].Month.Should().Be(2);
            monthCDBCollectionList?[1].InitialValue.Should().Be(1000.70m);
            monthCDBCollectionList?[1].FinalValue.Should().Be(1001.40m);

            monthCDBCollectionList?[2].Month.Should().Be(3);
            monthCDBCollectionList?[2].InitialValue.Should().Be(1001.40m);
            monthCDBCollectionList?[2].FinalValue.Should().Be(1002.10m);
        }
    }
}
