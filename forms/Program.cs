using forms.Data;
using forms.Dto;
using forms.GraphQL.Mutations;
using forms.GraphQL.Queries;
using forms.Mapping;
using forms.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF-Core  MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// MongoDB - Atlas
builder.Services.AddSingleton(new MongoDbContext(builder.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<UserQuery>()       // now Query has real fields
    .AddMutationType<UserMutation>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = "docs";
    });
}

app.UseCors("AllowAll");
app.MapControllers();
app.MapGraphQL("/graphql");
app.Run();