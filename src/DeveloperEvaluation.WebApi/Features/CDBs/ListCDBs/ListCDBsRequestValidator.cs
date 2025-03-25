using FluentValidation;

namespace DeveloperEvaluation.WebApi.Features.CDBs.ListCDBs;

public class ListCDBsRequestValidator : ListPaginationRequestValidator<ListCDBsRequest>
{
    public ListCDBsRequestValidator()
    {

    }
}
