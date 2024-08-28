var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.ThunderPay_Api>("api");

builder.AddProject<Projects.ThunderPay_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(api);

builder.Build().Run();
