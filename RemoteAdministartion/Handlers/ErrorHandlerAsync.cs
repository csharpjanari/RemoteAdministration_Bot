

using System.Diagnostics;
using Telegram.Bot;

namespace RemoteAdministration_Bot.Handlers
{
    internal class ErrorHandlerAsync
    {
        public static async Task Handler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
        }
    }
}
