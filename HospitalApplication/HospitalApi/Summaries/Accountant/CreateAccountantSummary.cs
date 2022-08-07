using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Endpoints.Accountant;

namespace HospitalApi.Summaries.Accountant;

public class CreateAccountantSummary : Summary<CreateAccountantEndpoint>
{
    public CreateAccountantSummary()
    {
        Summary = "Creates a new accountant in the system";
        Description = "Creates a new accountant in the system";
        Response<AccountantResponse>(201, "Accountant was successfully created");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}

