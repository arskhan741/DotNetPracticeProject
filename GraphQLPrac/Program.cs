using GraphQLPrac.Schema;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>(); 

var app = builder.Build();

// Add middleware to the request pipeline
app.UseRouting();  // This is required for routing to work

// Optional: Add authorization middleware if needed
// app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();  // This maps GraphQL endpoint
    // Add other endpoints if necessary
});

app.MapGet("/", () => "Hello World!");

app.Run();
