using GraphQLPrac.Repositories;
using GraphQLPrac.Schema.Mutations;
using GraphQLPrac.Schema.Queries;
using GraphQLPrac.Schema.Subscription;
using GraphQLPrac.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

// Add Graph QL 
services.AddControllers();
services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

// Add SQL Connection
services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Home"))
           .EnableSensitiveDataLogging() // Optional: log sensitive data
           .LogTo(Console.WriteLine, LogLevel.Information); // Log to console
});

// Add services to the DI container
services.AddScoped<CourseRepository>();
services.AddScoped<Query>();


var app = builder.Build();

// Add middleware to the request pipeline
app.UseRouting();  

// Websockets should always be used after routing
app.UseWebSockets();

// Add GraphQL to endpoints
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.Run();
