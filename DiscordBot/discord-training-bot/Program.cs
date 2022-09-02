// See https://aka.ms/new-console-template for more information

using Discord;
using Discord.WebSocket;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace discord_training_bot;

public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient _client;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public async Task MainAsync()
    {
        var logConfig = new LoggingConfiguration();
        var logFile = new FileTarget("logfile") {FileName = "discord-training.log"};
        var logConsole = new ConsoleTarget("logconsole");
        logConfig.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
        logConfig.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);
        LogManager.Configuration = logConfig;
        
        _client = new DiscordSocketClient();

        _client.Log += Log;

        //  You can assign your bot token to a string, and pass that in to connect.
        //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
        // var token = "";
        var token = File.ReadAllText("token.txt");

        // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
        // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
        // var token = File.ReadAllText("token.txt");
        // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
    
    private Task Log(LogMessage msg)
    {
        Logger.Info(msg.ToString());
        return Task.CompletedTask;
    }
}