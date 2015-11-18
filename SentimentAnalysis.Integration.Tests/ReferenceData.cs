namespace SentimentAnalysis.Integration.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Core.Domain;

    public class ReferenceData
    {
        public static IList<HybridResult> CaseData { get; }

        static ReferenceData()
        {
            CaseData = new List<HybridResult>()
            {
                new HybridResult() {Confidence = 100m, Error = null, Id = "A", Score = 1.1m, Sentiment = Sentiment.Positive, Text = "My daughter (8) and I both enjoy watching this show. She got interested in Flash from her classmates at school and now awaits eagerly each week for the new episode. I had never read a Flash comic book growing up so was totally unfamiliar with it. The great part is that my daughter has now gotten me really hooked on this show. The plot lines are well devised and the show is entertaining and well-paced. Nuclear Man was probably the best episode so far with a great combination of action and good acting. The character of Dr Wells is brilliantly crafted as alternately good and evil, and Grant Gutsin gives a brilliantly nuanced performance as Barry Allen. Cant wait to find out more about the origin story as the season advances and the story of the death of Barry's mother Nora is developed further. Hope this show continues for several more seasons."},
                new HybridResult() {Confidence = 2m, Error = null, Id = "B", Score = 1.1m, Sentiment = Sentiment.Positive, Text = ""},
                new HybridResult() {Confidence = 100m, Error = null, Id = "C", Score = 19m, Sentiment = Sentiment.Negative, Text = "The pilot was mediocre and the second episode got so much worse. The writing is unbelievably heavy handed. \"You can do it, Barry\" is not exactly a motivational speech that would inspire anyone to overcome self doubt. Oops, spoiler, the writing is literally that bad. Apparently there are enough fan boys watching to give this how an 8.6 rating - that's even sadder than the quality of the show. Complete waste of time. Oh look, ten lines of text are required, but this show isn't worth ten lines of effort so I'll bore you with crappy writing, much like the pathetic character motivations and ham-fisted plot arcs."},
                new HybridResult() {Confidence = 2m, Error = null, Id = "D", Score = 1.1m, Sentiment = Sentiment.Negative, Text = ""},
                new HybridResult() {Confidence = 2m, Error = null, Id = "E", Score = 1.1m, Sentiment = Sentiment.Neutral, Text = ""},
                new HybridResult() {Confidence = 2m, Error = null, Id = "F", Score = 1.1m, Sentiment = Sentiment.Neutral, Text = ""}
            };
        }

        public static HybridResult PostiviteResultOne => CaseData[0];

        public static HybridResult PostiviteResultTwo => CaseData[1];

        public static HybridResult NegativeResultOne => CaseData[2];

        public static HybridResult NegativeResultTwo => CaseData[3];

        public static HybridResult NeutralResultOne => CaseData[4];

        public static HybridResult NeutralResultTwo => CaseData[5];
    }
}