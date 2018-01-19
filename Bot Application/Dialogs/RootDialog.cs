using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;

namespace Bot_Application.Dialogs
{
    [Serializable]
    [LuisModel("66e2cca5-362c-42c4-81ef-6e3d2a95e6df", "58be75eb94384005bf644f843e718ded")]
    public class RootDialog : LuisDialog<object>
    {
        //public Task StartAsync(IDialogContext context)
        //{
        //    context.Wait(MessageReceivedAsync);

        //    return Task.CompletedTask;
        //}

        //private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        //{
        //    var activity = await result as Activity;

        //    // calculate something for us to return
        //    int length = (activity.Text ?? string.Empty).Length;

        //    // return our reply to the user
        //    await context.PostAsync($"You sent {activity.Text} which was {length} characters");

        //    context.Wait(MessageReceivedAsync);
        //}

    
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Lo siento no te entiendo.'{result.Query}'. Escribe 'help' o 'ayuda' si necesitas ayuda.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("search")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Bienvenido al buscador de gifts! Estamos analizando tu mensaje: '{message.Text}'...");

        }

        [LuisIntent("search-gif")]
        public async Task Reviews(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {

            var message = await activity;
            await context.PostAsync($"Bienvenido al buscador de gifts! Estamos analizando tu mensaje: '{message.Text}'...");

        }

        [LuisIntent("help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hola! Intenta buscar algo así 'dame un gif de starwars', '' or 'muestrame un gif de starwars'");

            context.Wait(this.MessageReceived);
        }

    }
}