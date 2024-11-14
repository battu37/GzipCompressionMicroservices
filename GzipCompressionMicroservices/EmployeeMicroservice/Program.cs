using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// refer /c/6715f46a-44d4-8013-9b42-f2b1f8a3b28c

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;  // Enable compression even for HTTPS requests
});

// Optional: Set compression level
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
        options.Level = System.IO.Compression.CompressionLevel.Optimal; // You can also use Fastest
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable Gzip compression
app.UseResponseCompression();

app.UseAuthorization();

app.MapControllers();

app.Run();
