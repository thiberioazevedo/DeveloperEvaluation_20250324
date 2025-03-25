using AutoMapper;
using MediatR;
using FluentValidation;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Common.Security;
using FluentValidation.Results;

namespace DeveloperEvaluation.Application.CDBs.CreateCDB;

public class CreateCDBHandler : IRequestHandler<CreateCDBCommand, CreateCDBResult>
{
    private readonly ICDBRepository _cDBRepository;
    private readonly IMapper _mapper;

    public CreateCDBHandler(ICDBRepository cDBRepository, IMapper mapper)
    {
        _cDBRepository = cDBRepository;
        _mapper = mapper;
    }

    public async Task<CreateCDBResult> Handle(CreateCDBCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCDBCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cDB = _mapper.Map<CDB>(command);

        cDB.Calculate(GetCDICurrent(), GetTBCurrent());

        var validationResultModel = cDB.Validate();

        if (!validationResultModel.IsValid)
            throw new ValidationException(validationResultModel.ValidationFailureList);

        var createdCDB = await _cDBRepository.CreateAsync(cDB, cancellationToken);
        
        var result = _mapper.Map<CreateCDBResult>(createdCDB);

        return result;
    }

    decimal GetCDICurrent(){
        return (decimal)0.9;
    }

    decimal GetTBCurrent(){
        return 108;
    }
}
