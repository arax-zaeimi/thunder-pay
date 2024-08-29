namespace ThunderPay.Api.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
