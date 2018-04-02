using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myMD.Model.MedicationInformation
{
    public class Translator : ITranslator
    {
        // Replace the subscriptionKey string value with your valid subscription key.
        const string subscriptionKey = "427ecc271f6d4e6686d0db7307b4e51a";

        static string host = "https://api.microsofttranslator.com";
        static string path = "/V2/Http.svc/Translate";

        public async Task<IList<string>> TranslateText(IList<string> input)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (string text in input){
                list.Add(new KeyValuePair<string, string>(text, "de-de"));
            }

            IList<string> resultList = new List<string>();

            foreach (KeyValuePair<string, string> i in list)
            {
                string uri = host + path + "?to=" + i.Value + "&text=" + System.Net.WebUtility.UrlEncode(i.Key);

                HttpResponseMessage response = await client.GetAsync(uri);

                string result = await response.Content.ReadAsStringAsync();
                // NOTE: A successful response is returned in XML. You can extract the contents of the XML as follows.
                // var content = XElement.Parse(result).Value;
                resultList.Add((string)XElement.Parse(result).Value);
            }

            return resultList;
        }
    }
}
