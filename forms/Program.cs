using forms.Data;
using forms.Dto.FormAuthoring;
using forms.GraphQL;
using forms.Mapping;
using forms.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===== Data Layer =====
// EF Core - MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// MongoDB Atlas
builder.Services.AddSingleton(new MongoDbContext(builder.Configuration));

// ===== Service Layer =====
// Register Repositories and Services
builder.Services.AddRepositories();
builder.Services.AddServices();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ===== GraphQL Configuration =====
builder.Services
    .AddGraphQLServer()
    .AddQueries() // All GraphQL queries
    .AddMutations() // All GraphQL mutations
    .AddErrorFilter<GraphQLErrorFilter>()
    .ModifyRequestOptions(o => { o.IncludeExceptionDetails = false; });

// ===== CORS =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ===== Build App =====
var app = builder.Build();

// ===== Middleware Pipeline =====
app.UseCors("AllowAll");
app.MapGraphQL("/graphql");

app.Run();