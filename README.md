# Sentiment Analysis #

This project was developed to simplify the sentiment analysis of text.  It is meant to be easily extensible with additional implementations.  For the initial release, there are two different sentiment analysis engines programmed in.

* [Azure Machine Learning](https://azure.microsoft.com/en-us/documentation/articles/machine-learning-apps-text-analytics/)
* [Vivekn Sentiment Analysis](http://sentiment.vivekn.com/)

## Setup ##

The code can be cloned and built from GitHub or installed via NuGet.

```powershell
PM> Install-Package SentimentAnalysis.AzureMachineLearning
OR
PM> Install-Package SentimentAnalysis.Vivekn
```

The Azure Machine Learning implementation requires the use of an API Key and allows 10,000 free lookups a month.  If you need more than that, you have to pay them.  Not me, I'm not affiliated with them in any way, shape, or form.  Their implementation was one of many that came up while I was researching Sentiment Analysis.

This means you will need to [request that from them](https://azure.microsoft.com/en-us/documentation/articles/machine-learning-apps-text-analytics/) and put it in the config file under this setting AzureMachineLearning:BasicAuthenticationCredentials.

```xml
<appSettings>
    <add key="AzureMachineLearning:BasicAuthenticationCredentials" value="AccountKey:{YOUR ACCOUNT KEY HERE}" />
</appSettings>
``` 
## Use ##

Single item requests
```csharp
var service = ServiceFactory.Build();

var results = await service.GetSentimentAsync("String to test");
```

Batch requests
```csharp
var service = ServiceFactory.Build();

var request = new Dictionary<string, string>
                    {
                        { "A", "Data to be tested" },
                        { "B", "Data to be tested" }
                    };

var batchResult = await service.GetBatchSentimentAsync(request);
```

## Credits ##

This project is a rebasing of [work](https://github.com/dalealleshouse/AzureTextAnalytics) done by my friend and mentor [Dale Alleshouse](http://www.hideoushumpbackfreak.com/).

## Assistance/Questions/Etc ##
Most of the code has covering unit tests, so reviewing those may provide insight to call/result processing.  Pull requests and feature requests are welcome as is feedback of any kind.

Thanks and enjoy,
Ben