using Microsoft.AspNetCore.Routing;

namespace ThunderPay.Api.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
