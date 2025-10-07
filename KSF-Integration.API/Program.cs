using KSF_Integration.API.Services;
using KSF_Integration.API.Services.Interfaces;
using KSF_Integration.API.Servises;
using KSF_Integration.API.Servises.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("KsefClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Ksef:BaseAddress"]!);
});

builder.Services.AddScoped<IAuthChallengeService, AuthChallengeService>();
builder.Services.AddScoped<IAuthTokenRequestBuilder, AuthTokenRequestBuilder>();
builder.Services.AddScoped<ICertificateProcessService, CertificateProcessService>();
builder.Services.AddScoped<ISignService, SignService>();


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
