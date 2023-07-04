using Grpc.Net.Client;
using MagicOnion.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.ConfigureEndpointDefaults(endpointOptions =>
    {
        endpointOptions.Protocols = HttpProtocols.Http2;
    });
});
builder.Services.AddGrpc();
builder.Services.AddMagicOnion();

var app = builder.Build();

app.MapMagicOnionHttpGateway(
    "_",
    app.Services.GetRequiredService<MagicOnionServiceDefinition>().MethodHandlers,
    GrpcChannel.ForAddress(@"http://localhost:5000"));
app.MapMagicOnionSwagger(
    "swagger",
    app.Services.GetRequiredService<MagicOnionServiceDefinition>().MethodHandlers,
    "/_/");
app.MapMagicOnionService();
app.Run();
