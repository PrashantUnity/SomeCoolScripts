# Keep in Mind that i am Getting Cors Issue If I Used this Application of Server and Client Seperately In Different Project
```razor
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

app.UseCors();
app.MapHub<ChatHub>("/chathub");
app.Run();
public class ChatHub : Hub
{
    public async Task SendMessage(string User, string message)
    {
        await Clients.All.SendAsync("RecieveMessage", User, message);
    }
}
```

# Server Code
```razor
using BlazorApp3.Page;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});


var app = builder.Build(); 
app.UseHttpsRedirection(); 
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp3.Client._Imports).Assembly);

app.UseCors();
app.MapHub<ChatHub>("/chathub");
app.Run();
public class ChatHub : Hub
{
    public async Task SendMessage(string User, string message)
    {
        await Clients.All.SendAsync("RecieveMessage", User, message);
    }
}
```
# Webassembly Code

```razor
@page "/"

@using Microsoft.AspNetCore.SignalR.Client
@inject NavigationManager NavigationManager

@if (IsConnected)
{
    <input type="text" @bind="user" />
    <input type="text" @bind="message" />
    <button @onclick="Send">Send</button>
    <ul>
        @foreach (var item in messageList)
        {
            <li>@item</li>
        }
    </ul>
}
else
{
    <p>Connecting...</p>
}

@code {
    HubConnection connection;
    List<string> messageList = new();
    string user;
    string message;
    [Inject] public NavigationManager nav { get; set; }

    protected override async Task OnInitializedAsync()
    {
        connection = new HubConnectionBuilder()
            .WithUrl(nav.ToAbsoluteUri("/chathub"))
            .Build();

        connection.On<string, string>("RecieveMessage", (receivedUser, receivedMessage) =>
        { 
            messageList.Add($"{receivedUser} : {receivedMessage}");
            StateHasChanged();
        });

        await connection.StartAsync();
    }

    async Task Send()
    {
        if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(message))
        {
            await connection.SendAsync("SendMessage", user, message);
            message = string.Empty;
            StateHasChanged();
        }
    }

    bool IsConnected => connection.State == HubConnectionState.Connected;
}
```
