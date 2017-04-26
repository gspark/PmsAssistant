using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PmsAssistant
{
    public class Ihotel
    {
        private const string BASE_ADDRESS = "http://119.29.215.133:8090";
        private const string LOGIN = "http://119.29.215.133:8090/ipmsthef/loginCenter";
        private HttpClient httpClient;

        private string logCode;

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
            var ret = await response.Content.ReadAsStringAsync();
            if (ret.Contains("faultCode"))
            {
                return false;
            }
            logCode = ret;
            return true;
        }
    }
}
