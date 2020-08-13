using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamTestAppDataLibrary.Models;

namespace XamTestAppDataLibrary.Services
{
    public class HttpJsonContactsDataSource : IContactsDataSource
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url;
        public HttpJsonContactsDataSource(string url)
        {
            _url = url ?? throw new System.ArgumentNullException(nameof(url));
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            using var result = await _httpClient.GetAsync(_url);
            result.EnsureSuccessStatusCode();
            var stream = await result.Content.ReadAsStreamAsync();

            var output = DeserializeJsonFromStream<List<Contact>>(stream);

            return output;
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || !stream.CanRead)
            {
                return default;
            }

            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);
            var jsonSerializer = new JsonSerializer();
            var output = jsonSerializer.Deserialize<T>(jsonTextReader);
            return output;
        }

    }
}
