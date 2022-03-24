using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<MyHub>("/chat");
app.Run();

public class MyHub : Hub
{
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while(true)
        {
            yield return DateTime.Now;
            await Task.Delay(5000, cancellationToken);
        }
    }
}