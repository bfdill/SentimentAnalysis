Thank you for downloading Sentiment Analysis Vivekn.

This project is a wrapper for this online tool: http://sentiment.vivekn.com/

The service returns a sentiment value of Positive, Negative, or Neutral.

It should run out-of-the-box with no configuration required using code similar to this:
namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SentimentAnalysis.Vivekn;

    internal class Program
    {
        private static void Main(string[] args)
        {
            ProcessSingle().Wait();
            ProcessBatch().Wait();
            Console.ReadLine();
        }

        private static async Task ProcessSingle()
        {
            var service = ServiceFactory.Build();
            var result = await service.GetSentimentAsync("The quick brown fox jumps over the lazy dog.");

            Console.WriteLine(result.Success ? result.Sentiment.ToString() : result.Error);
        }

        private static async Task ProcessBatch()
        {
            var service = ServiceFactory.Build();

            // when processing a batch, you need to include an identifier
            // the batch method takes a dictionary of string/string
            // the first string is the id, the second string is the text to be analyzed
            // I'm using A/B for my ids, they could be any valid string though
            // random reviews from metacritic.com
            var request = new Dictionary<string, string>
            {
                { "A", "Spielberg continues to do intense visually extraordinary film and this one is no exception. It is a meticulous story of a piece of The Cold War weaving two stories together into a prisoner exchange along a bridge in Berlin. Adding The Coen Brothers brings pieces of their highly stylized view of film making into the production in service to the story, not the other way around as sometimes happens in Coen films. Yes, Tom Hanks holds this story together but Mark Rylance is fabulous as well in what should bring him a Best supporting Actor nomination. The dialogue is tight yet textured for the era. Have you figured out I really liked it yet?" },
                { "B", "How how how is it even possible that these movies keep getting made? The same hack that did the later Paranormal flicks - which got worse and worse and worse - is not directing this piece of trash. Folks STAY HOME. Rent a scary movie if that's what you're looking for. $15 to see this is unconscionable." }
            };
            var response = await service.GetBatchSentimentAsync(request);

            foreach (var item in response)
            {
                Console.WriteLine("Id: {0} - Result: {1}", item.Key, item.Value.Success ? item.Value.Sentiment.ToString() : item.Value.Error);
            }
        }
    }
}