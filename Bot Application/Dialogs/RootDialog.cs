using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using System.Collections.Generic;

namespace Bot_Application.Dialogs
{
    [Serializable]
    [LuisModel("c921f687-ac4c-4db7-b957-2e21b1f27781", "e23edb5849bf4287935045666d5ad963")]
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
            string message = $"Mis respuestas son limitadas. Formula las preguntas correctas.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        private Task MessageReceived(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }

        [LuisIntent("search")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            string tags = message.Text;

            var giphy = new Giphy("J3T4XARDCw5TEVLJ8hZNCtaOUh0v5YLf");
            var searchParameter = new SearchParameter()
            {
                Query = tags,
                Limit = 1
            };
            // Returns gif results
            var gifResult = await giphy.GifSearch(searchParameter);

            AnimationCard gifCard = new AnimationCard()
            {
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "https://media2.giphy.com/media/" + gifResult.Data[0].Id + "/giphy.gif"
                    }
                }
            };

            var reply = context.MakeMessage();

            reply.Text = "Aqui tienes tu dosis de fanservice...";
            reply.Attachments = new List<Attachment>();
            reply.Attachments.Add(gifCard.ToAttachment());

            await context.PostAsync(reply);
        }

        [LuisIntent("topsecret")]
        public async Task TopSecret(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            AnimationCard gifCard = new AnimationCard()
            {
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "https://i.imgur.com/ocYxZg0.gif"
                    }
                }
            };

            var reply = context.MakeMessage();

            reply.Text = "(＾ω＾)";
            reply.Attachments = new List<Attachment>();
            reply.Attachments.Add(gifCard.ToAttachment());

            await context.PostAsync(reply);
        }
    }
}