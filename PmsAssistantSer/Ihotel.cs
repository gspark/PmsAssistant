using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PmsAssistant
{
    public class Ihotel
    {
        private static readonly string BASE_ADDRESS = "http://119.29.215.133:8090";
        private static readonly string LOGIN = "http://119.29.215.133:8090/ipmsthef/loginCenter";
        private HttpClient httpClient;

        public Ihotel()
        {
            httpClient = new HttpClient() { BaseAddress = new Uri(BASE_ADDRESS) };
        }

        public async Task<bool> login()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"password", "1234"},
                {"userName", "MY"},
                {"typeCode", "CQYSK-F"},
                {"flex.messaging.request.language", "zh_CN"}
            });

            //await异步等待回应
            var response = await httpClient.PostAsync(LOGIN, content);

            //await异步
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return true;
        }
    }
}
