using DeveloperEvaluation.Application.CDBs.CreateCDB;
using Bogus;

namespace DeveloperEvaluation.Unit.Domain;

public static class CreateCDBHandlerTestData
{
    private static readonly Faker<CreateCDBCommand> createCDBHandlerFaker = new Faker<CreateCDBCommand>()
        .RuleFor(u => u.Months, f => f.Random.Number(1, 999))
        .RuleFor(u => u.Value, f => f.Random.Number(1, 99999));

    public static CreateCDBCommand GenerateValidCommand()
    {
        return createCDBHandlerFaker.Generate();
    }
}
