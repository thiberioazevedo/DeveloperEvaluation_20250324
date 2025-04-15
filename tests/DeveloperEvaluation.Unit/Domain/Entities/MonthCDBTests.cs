using DeveloperEvaluation.Domain.Entities;
using Xunit;

public class MonthCDBTests
{

    [Fact(DisplayName = "Calculate should set GrossValue correctly")]
    public void Given_MonthCDB_When_CalculateCalled_Then_ShouldSetGrossValueCorrectly()
    {
        // Arrange
        var cdb = new CDB(1000, 12);

        // Act
        cdb.GenerateMonths(108, 0.9m);
        var MonthCDBList = cdb.MonthCDBCollection.ToList();

        // Assert
        Assert.Equal((decimal)1009.72, MonthCDBList[0].GrossValue);
        Assert.Equal((decimal)2.19, Math.Round(MonthCDBList[0].TaxAmount, 2));
        Assert.Equal((decimal)1007.53, Math.Round(MonthCDBList[0].NetValue, 2));

        Assert.Equal((decimal)1019.53, Math.Round(MonthCDBList[1].GrossValue, 2));
        Assert.Equal((decimal)4.40, Math.Round(MonthCDBList[1].TaxAmount, 2));
        Assert.Equal((decimal)1015.14, Math.Round(MonthCDBList[1].NetValue, 2));

        Assert.Equal((decimal)1070.06, Math.Round(MonthCDBList[6].GrossValue, 2));
        Assert.Equal((decimal)14.01, Math.Round(MonthCDBList[6].TaxAmount, 2));
        Assert.Equal((decimal)1056.05, Math.Round(MonthCDBList[6].NetValue, 2));
    }

    [Fact(DisplayName = "Calculate should set TaxAmount correctly")]
    public void Given_MonthCDB_When_CalculateCalled_Then_ShouldSetTaxAmountCorrectly()
    {
        // Arrange
        var cdb = new CDB(1000, 6);
        var monthCDB = new MonthCDB(1, cdb);
        monthCDB.Calculate();

        // Act
        var expectedTaxAmount = (monthCDB.GrossValue - monthCDB.InitialValue) * 0.225m;

        // Assert
        Assert.Equal(expectedTaxAmount, monthCDB.TaxAmount);
    }

    [Fact(DisplayName = "Calculate should set NetValue correctly")]
    public void Given_MonthCDB_When_CalculateCalled_Then_ShouldSetNetValueCorrectly()
    {
        // Arrange
        var cdb = new CDB(1000, 6);
        var monthCDB = new MonthCDB(1, cdb);

        // Act
        monthCDB.Calculate();

        // Assert
        var expectedNetValue = monthCDB.GrossValue - monthCDB.TaxAmount;
        Assert.Equal(expectedNetValue, monthCDB.NetValue);
    }

    [Fact(DisplayName = "Calculate should set all properties correctly")]
    public void Given_MonthCDB_When_CalculateCalled_Then_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var cdb = new CDB(1000, 6);

        // Act
        cdb.GenerateMonths(108, (decimal)0.9);
        var monthCDB = cdb.MonthCDBCollection.FirstOrDefault();

        // Assert
        Assert.Equal(1000m, monthCDB.InitialValue);
        Assert.Equal(0.225m, monthCDB.TaxPercentage);
        Assert.True(monthCDB.GrossValue > monthCDB.InitialValue);
        Assert.True(monthCDB.NetValue < monthCDB.GrossValue);
    }

    [Theory]
    [InlineData(5, 0.225)]
    [InlineData(12, 0.20)]
    [InlineData(18, 0.175)]
    [InlineData(25, 0.15)]
    public void SetTaxPercentage_ShouldSetCorrectTaxPercentage(int month, decimal expectedTaxPercentage)
    {
        // Arrange
        var cdb = new CDB(1000, month);

        // Act
        cdb.GenerateMonths(108, 0.9m);
        var monthCDB = cdb.MonthCDBCollection.LastOrDefault();

        // Assert
        Assert.Equal(expectedTaxPercentage, monthCDB.TaxPercentage);
    }
}