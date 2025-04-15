using DeveloperEvaluation.Application.CDBs.CreateCDB;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using DeveloperEvaluation.ORM.Repositories;

namespace DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateCDBHandler"/> class.
/// </summary>
public class CreateCDBHandlerTests : BaseTest<CDB>
{
    private readonly ICDBRepository _cDBRepository;
    private readonly IMapper _mapper;
    private readonly CreateCDBHandler _handler;

    public CreateCDBHandlerTests()
    {
        _cDBRepository = new CDBRepository(BaseTestHelpers.GetCreateDefaultContextInstance());
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCDBHandler(_cDBRepository, _mapper);
    }

    public override CDB CreateEntityDefaultInstance()
    {
        return new CDB(1, 2);
    }

    public override IRepository<CDB> CreateRepositoryInstance()
    {
        return _cDBRepository;
    }

    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCDBCommand();

        var cDB = CommandToEntity(command);
        _mapper.Map<CDB>(command).Returns(cDB);
        _mapper.Map<CreateCDBResult>(cDB).Returns(EntityToResult(cDB));

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    static CDB CommandToEntity(CreateCDBCommand createCDBCommand)
    {
        return new CDB(createCDBCommand.Value, createCDBCommand.Months);
    }

    static CreateCDBResult EntityToResult(CDB cDB)
    {
        return new CreateCDBResult
        {
            Id = cDB.Id
        };
    }
}
