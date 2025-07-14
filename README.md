# SignalR Streaming Server (Minimal API)

A minimalist **ASP.NET Core** application built with the *Minimal API* style that exposes a **server‑to‑client streaming** hub method using SignalR.

```csharp
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
        while (true)
        {
            yield return DateTime.Now;
            await Task.Delay(5000, cancellationToken);
        }
    }
}
```

## How It Works

1. **builder.Services.AddSignalR()** registers SignalR services with dependency injection.
2. **app.MapHub<MyHub>("/chat")** maps the hub endpoint at `/chat`.
3. **MyHub.Streaming** yields the current server time every 5 seconds until the client disconnects or the cancellation token is triggered.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- A development HTTPS certificate (`dotnet dev-certs https --trust`)

## Getting Started

```bash
# Clone and enter the project folder
git clone <your-fork-url> signalr-stream-server
cd signalr-stream-server

# Restore packages, build, and run
dotnet run
```

The server starts on `https://localhost:7056` by default (see `launchSettings.json` if you generated the project with `dotnet new web`).

### Testing

1. **Run the server** with `dotnet run`.
2. **Run the console client** described in [SignalR Streaming Console Client](../signalr-stream-client/README.md) — make sure both client and server use the same `/chat` hub URL.
3. Watch the client print a fresh timestamp every 5 seconds.

---

## NuGet Packages

The following package is added implicitly when you create an ASP.NET Core web project in .NET 8, but you can add it explicitly if needed:

```bash
dotnet add package Microsoft.AspNetCore.SignalR
```

---

## Troubleshooting

| Symptom                           | Likely Cause                                | Fix                                                      |
| --------------------------------- | ------------------------------------------- | -------------------------------------------------------- |
| `System.Net.Sockets.SocketException` when starting | Port already in use                         | Change the port in `Properties/launchSettings.json`      |
| Client never connects             | URL mismatch or firewall                   | Verify the client URL and ensure `https://localhost:7056` (or your chosen port) is accessible |
| `InvalidDataException: Connection closed` | Incompatible SignalR protocol versions       | Ensure both client and server reference compatible package versions |

---

## Contributing

Pull requests and issues are welcome!

## License

This project is licensed under the MIT License. See [`LICENSE`](./LICENSE) for details.
