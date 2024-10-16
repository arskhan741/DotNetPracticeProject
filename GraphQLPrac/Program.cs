using GraphQLPrac.Schema.Mutations;
using GraphQLPrac.Schema.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    ; 

var app = builder.Build();

// Add middleware to the request pipeline
app.UseRouting();  

// Add GraphQL to endpoints
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.Run();
