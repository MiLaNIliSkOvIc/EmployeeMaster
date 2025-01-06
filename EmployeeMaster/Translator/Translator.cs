using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Translator
{
    using EmployeeMaster.Administrator.SettingsScreen;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class Translator
        {
            private static readonly HttpClient httpClient = new HttpClient();

            public static async Task<string> TranslateAsync(string message)
            {
            string targetLanguage = SettingsScreen.Language;
            if (SettingsScreen.Language == "sr-RS")
                targetLanguage = "hr";

            string apiKey = "123231";
            string url = $"https://translation.googleapis.com/language/translate/v2?key={apiKey}";

                
                var content = new StringContent(JsonConvert.SerializeObject(new
                {
                    q = message,
                    target = targetLanguage
                }), Encoding.UTF8, "application/json");

                
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

              
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(jsonResponse);
                    return result.data.translations[0].translatedText;
                }

                
                return message;
            }

        public static string Translate(string message)
        {
            string targetLanguage = SettingsScreen.Language;
            if (SettingsScreen.Language == "sr-RS")
                targetLanguage = "hr";

            string apiKey = "AIzaSyCCuUBHKVgDODtg7_p6lnWtuXodNOJGfwI";
            string url = $"https://translation.googleapis.com/language/translate/v2?key={apiKey}";

            
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                q = message,
                target = targetLanguage
            }), Encoding.UTF8, "application/json");

            
            HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

            
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = response.Content.ReadAsStringAsync().Result;  
                dynamic result = JsonConvert.DeserializeObject(jsonResponse);
                return result.data.translations[0].translatedText;
            }

            
            return message;
        }

    }
    


}