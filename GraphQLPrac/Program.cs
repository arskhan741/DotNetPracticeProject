using GraphQLPrac.Schema.Mutations;
using GraphQLPrac.Schema.Queries;
using GraphQLPrac.Schema.Subscription;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions(); 



var app = builder.Build();

// Add middleware to the request pipeline
app.UseRouting();  

// Websockets should always be used after routing
app.UseWebSockets();

// Add GraphQL to endpoints
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.Run();
