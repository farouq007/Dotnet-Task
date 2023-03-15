using DotnetTaskAPI.Services.Abstract;
using DotnetTaskAPI.Services.Concrete;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();
builder.Services.AddAutoMapper(typeof(Program));

static CosmosClient InitializeCosmosClientInstance(IConfigurationSection configurationSection)
{
    var account = configurationSection["Account"];
    var key = configurationSection["Key"];
    var client = new CosmosClient(account, key);
    return client;
}

builder.Services.AddSingleton<CosmosClient>(InitializeCosmosClientInstance(builder.Configuration.GetSection("CosmosDb")));

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();