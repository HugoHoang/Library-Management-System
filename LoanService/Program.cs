using Microsoft.EntityFrameworkCore;
using LoanService.Data;
using LoanService.SyncDataServices.Http;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LoanDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("LoansConn")));
builder.Services.AddScoped<ILoanRepo, LoanRepo>();
builder.Services.AddHttpClient<IBookDataClient, HttpBookDataClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
PrepDb.PrepPopulation(app);
app.Run();
