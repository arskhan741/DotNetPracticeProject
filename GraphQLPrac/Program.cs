using GraphQLPrac.Schema.Mutations;
using GraphQLPrac.Schema.Queries;
using GraphQLPrac.Schema.Subscription;
using GraphQLPrac.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

 
// Add services to the DI container
services.AddControllers();
services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

string connectionString = builder.Configuration.GetConnectionString("default");
services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlite(connectionString));


var app = builder.Build();

// Add middleware to the request pipeline
app.UseRouting();  

// Websockets should always be used after routing
app.UseWebSockets();

// Add GraphQL to endpoints
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.Run();
