using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Endpoints.Financial;

namespace HospitalApi.Summaries.Account
{
    public class ChangeAccountBalanceSummary : Summary<ChangeAccountBalanceEndpoint>
    {
        public ChangeAccountBalanceSummary()
        {
            Summary = "Updates an existing account balance in the system";
            Description = "Updates an existing account balance in the system";
            Response<AccountResponse>(201, "Patient was successfully updated");
            Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
        }
    }
}
