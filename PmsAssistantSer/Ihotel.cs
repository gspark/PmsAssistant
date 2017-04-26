using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PmsAssistant
{
    public class Ihotel
    {
        private const string BaseAddress = "http://119.29.215.133:8090";
        private const string LogIn = "http://119.29.215.133:8090/ipmsthef/loginCenter";
        private const string LogOut = "http://119.29.215.133:8090/ipmsthef/loginCenter";
        private readonly HttpClient _httpClient;

        private string logCode;

        public Ihotel()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(BaseAddress) };
        }

        public async Task<bool> Login()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"password", "1234"},
                {"userName", "MY"},
                {"typeCode", "CQYSK-F"},
                {"flex.messaging.request.language", "zh_CN"}
            });

            //await异步等待回应
            var response = await _httpClient.PostAsync(LogIn, content);

            //await异步
            var ret = await response.Content.ReadAsStringAsync();
            if (ret.Contains("faultCode"))
            {
                return false;
            }
            logCode = ret;
            return true;
        }

        public async Task<bool> Logout()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"password", "1234"},
                {"userName", "MY"},
                {"typeCode", "CQYSK-F"},
                {"flex.messaging.request.language", "zh_CN"}
            });

            //await异步等待回应
            var response = await _httpClient.PostAsync(LogOut, content);

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
