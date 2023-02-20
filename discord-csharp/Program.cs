using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Serilog;

namespace discord_training_bot;

public class Program
{

    private readonly string pinEmoji = ":pushpin:";
    private readonly string removePinEmoji = ":no_entry_sign:";
    static void Main()
    {
        var program = new Program();
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        program.MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
        var logFactory = new LoggerFactory().AddSerilog();
        var clientCfg = new DiscordConfiguration()
        {
            Token = File.ReadAllText("token.txt"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged,
            LoggerFactory = logFactory
        };
        
        var client = new DiscordClient(clientCfg);

        client.MessageReactionAdded += ReactionAddedHandler;

        await client.ConnectAsync();
        await Task.Delay(-1);
    }

    private async Task ReactionAddedHandler(DiscordClient client, MessageReactionAddEventArgs eventArgs)
    {
        if (eventArgs.Emoji.Equals(DiscordEmoji.FromName(client, pinEmoji)))
        {
            // await client.SendMessageAsync(eventArgs.Channel, $"Pin!");
            if (!eventArgs.Message.Pinned)
            {
                Log.Information("Pin {messageId} in {channelId}.", eventArgs.Message.Id, eventArgs.Channel.Id);
                await eventArgs.Message.PinAsync();
                await eventArgs.Message.CreateReactionAsync(DiscordEmoji.FromName(client, pinEmoji));
            }
        } else if (eventArgs.Emoji.Equals(DiscordEmoji.FromName(client, removePinEmoji)))
        {
            var pinReactionCount = eventArgs.Message.Reactions.Count(reaction =>
                reaction.Emoji.Equals(DiscordEmoji.FromName(client, pinEmoji))) - 1; // Minus 1 because of self-reaction
            if (eventArgs.Message.Pinned && pinReactionCount <= 1)
            {
                Log.Information("Unpin {messageId} in {channelId}.", eventArgs.Message.Id, eventArgs.Channel.Id);
                await eventArgs.Message.UnpinAsync();
                await eventArgs.Message.DeleteReactionsEmojiAsync(DiscordEmoji.FromName(client, pinEmoji));
                await eventArgs.Message.DeleteReactionsEmojiAsync(DiscordEmoji.FromName(client, removePinEmoji));

            }
            else
            {
                // FIXME This is a really bad magic number implementation
                Log.Information("Cannot unpin {messageId} in {channelId}.", eventArgs.Message.Id, eventArgs.Channel.Id);
                if (pinReactionCount > 2) // Greater than 2 because of user-reaction + self-reaction
                {
                    // Minus 1 because of user-reaction + self-reaction
                    await eventArgs.Channel.SendMessageAsync(
                        $"Cannot unpin message. There are still {pinReactionCount-1} others who want to have it pinned.");
                }
                await eventArgs.Message.DeleteReactionsEmojiAsync(DiscordEmoji.FromName(client, removePinEmoji));
            }
        }
    }

    private async Task MessageCreatedHandler(DiscordClient client, MessageCreateEventArgs eventArgs)
    {
        Log.Debug("Message {Message}", eventArgs.Message);
    }
}