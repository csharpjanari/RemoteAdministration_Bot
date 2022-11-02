using RemoteAdministration_Bot;
using RemoteAdministration_Bot.Handlers;
using Telegram.Bot;


var botClient = new TelegramBotClient(Configuration.BotToken);
var me = await botClient.GetMeAsync();

using var cts = new CancellationTokenSource();

botClient.StartReceiving(
        updateHandler: UpdateHandlerAsync.Handler,
        pollingErrorHandler: ErrorHandlerAsync.Handler,
        cancellationToken: cts.Token
        );

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();
