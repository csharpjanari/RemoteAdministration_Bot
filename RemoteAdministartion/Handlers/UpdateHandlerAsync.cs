using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Diagnostics;

namespace RemoteAdministration_Bot.Handlers
{
    class UpdateHandlerAsync
    {
        public static async Task Handler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                _ => UnknownUpdateHandler(botClient, update)
            };
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            Console.WriteLine($"{message.Chat.Username ?? "Anonymus"}   |   {message.Text}");

            if (message.Type != MessageType.Text)
                return;

            var action = message.Text!.Split(' ')[0] switch
            {
                "/Shutdown" => ShutdownPC(),
                "/Reboot" => RebootPC(),
                _ => Usage(botClient, message)
            };

            Console.WriteLine($"The message was sent with id: {message.MessageId}");



            static Task ShutdownPC()
            {
                Process.Start("cmd", "/c shutdown -s -f -t 00");

                return Task.CompletedTask;
            }



            static Task RebootPC()
            {
                Process.Start("shutdown", "-r -f -t 00");

                return Task.CompletedTask;
            }
        }        



        static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
        {
            const string usage = "Select one of the operations : \n" +
                                 "/Shutdown - shutting down the PC \n" +
                                 "/Reboot - rebooting PC";

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: usage,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }



        private static Task UnknownUpdateHandler(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }
    }
}
