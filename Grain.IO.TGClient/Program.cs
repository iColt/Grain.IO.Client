using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;

namespace Grain.IO.TGClient;

class Program
{
    //TODO
    private const string TOKEN = "TOKEN";

    static readonly ITelegramBotClient bot = new TelegramBotClient(TOKEN);
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;
            if (message.Text.Equals("/start", StringComparison.CurrentCultureIgnoreCase))
            {
                await botClient.SendTextMessageAsync(message.Chat, "Whisky notificator Grain.IO has started");
                //TODO: add buttons
                return;
            }
            //TODO : change
            //await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }


    static void Main(string[] args)
    {
        Console.WriteLine("Bot started " + bot.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}