var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello GitHUb Actions!    e-spec is live with latest code changes.");

app.Run();
