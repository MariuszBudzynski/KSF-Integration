using KSeF.Client.Api.Services;
using KSeF.Client.Core.Interfaces;
using KSeF.Client.DI;
using KSF_Integration.API.Services;
using KSF_Integration.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKSeFClient(options =>
{
    options.BaseUrl = KsefEnviromentsUris.TEST;
});

builder.Services.AddScoped<ICertificateProcessService, CertificateProcessService>();
builder.Services.AddScoped<ISignatureService, SignatureService>();


var app = builder.Build();

//Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

await app.RunAsync();
