using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoGRAPHService;
using JetBrains.Annotations;

namespace AGServiceAPI
{
    public abstract class AGServiceBase
    {
        [NotNull] protected readonly string Uri;
        [NotNull] protected readonly HttpClient client = new HttpClient();

        protected const string DT_JSON_FORMAT = "yyyyMMdd-HHmm";
        protected const string HEADER_TOKEN = "AG-TOKEN";

        protected AGServiceBase([NotNull] string uri)
        {
            if (!uri.EndsWith("/")) uri += "/";
            Uri = uri;
        }

        protected async Task<T> exec<T>(string methodName, Dictionary<string, object> arguments)
        {
            if (!client.DefaultRequestHeaders.Contains(HEADER_TOKEN))
                throw new InvalidDataException("Call 'Login' method first");

            var convertedArguments = new Dictionary<string, string>();
            foreach (var item in arguments)
            {
                if (item.Value == null) continue;
                
                if (item.Value is Guid[] guids)
                {
                    if (guids.Any())
                        convertedArguments.Add(item.Key, guids.Select(c => c.ToString()).Aggregate((acc, sel) => acc + "," + sel));
                } else if (item.Value is string[] strings)
                {
                    if (strings.Any())
                        convertedArguments.Add(item.Key, strings.Aggregate((acc, sel) => acc + "," + sel));
                } else if (item.Value is DateTime datetime)
                    convertedArguments.Add(item.Key, datetime.ToString(DT_JSON_FORMAT));
                else
                    convertedArguments.Add(item.Key, item.Value.ToString());
            }

            var response = (await client.PostAsync(Uri + methodName, new FormUrlEncodedContent(convertedArguments))).checkResponse();
            return (await response.Content.ReadAsStringAsync()).FromJson<T>();
        }
    }
}