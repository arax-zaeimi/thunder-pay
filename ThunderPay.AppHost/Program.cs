var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ThunderPay_ApiService>("apiservice");

builder.AddProject<Projects.ThunderPay_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
