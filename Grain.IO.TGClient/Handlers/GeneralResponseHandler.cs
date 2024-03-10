using Grain.IO.TGClient.Models;
using Grain.IO.TGClient.Parsers;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Grain.IO.TGClient.Handlers;

public interface IGeneralResponseHandler
{
    void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}

public class GeneralResponseHandler : IGeneralResponseHandler
{
    private readonly IOptions<IConfigurationModel> _configModel;

    public GeneralResponseHandler(IOptions<IConfigurationModel> configModel)
    {
        _configModel = configModel;
    }

    public async void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(update.Message.Chat, "Whisky notificator Grain.IO has started", cancellationToken: cancellationToken);

        var whiskys = WhiskyModelParserV1.Parse(_configModel.Value.TamdhuWhiskyPath);

        if(whiskys == null || whiskys.Count == 0)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, "Nothing parsed");
            return;
        }


        //TODO: add buttons
    }
}
