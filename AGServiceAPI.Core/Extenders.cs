using System;
using System.IO;
using System.Net.Http;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace AGServiceAPI
{
    internal static class Extenders
    {
        [NotNull]
        internal static HttpResponseMessage checkResponse([NotNull] this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException(response.ReasonPhrase);
            return response;
        }

        static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        
        internal static T FromJson<T>([NotNull] this string content)
        {
            return JsonSerializer.Create(jsonSerializerSettings).Deserialize<T>(new JsonTextReader(new StringReader(content)));
        }
    }
}