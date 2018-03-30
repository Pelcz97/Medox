using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using myMD.ModelInterface.DataModelInterface;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Internals;

namespace myMD.Model.MedicationInformation
{
    public class InteractionChecker : IInteractionChecker
    {
        const string RxCUIbyName = "https://rxnav.nlm.nih.gov/REST/rxcui?name=";
        const string InteractionsURL = "https://rxnav.nlm.nih.gov/REST/interaction/list.json?rxcuis=";

        public async Task<IList<InteractionPair>> GetInteractions(IList<IMedication> medications)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            // Assemble the URI for the REST API Call.
            string uri = InteractionsURL;
            foreach (IMedication med in medications){
                string rxcui = await GetRxNormID(med);
                if (rxcui != null){
                    uri += rxcui + "+";
                }
            }

            if (uri.Length == InteractionsURL.Length){
                return null;
            }

            // Execute the REST API call.
            response = await client.GetAsync(uri);

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();

            //Debug.WriteLine(contentString);
            //Debug.WriteLine(JsonPrettyPrint(contentString));

            var pairs = ResultOnly(contentString);

            return pairs;
        }

        public async Task<IList<string>> GetRxNormIDs(IList<IMedication> medications) {
            IList<string> resultList = new List<string>();

            foreach (IMedication medication in medications) {
                var val = await GetRxNormID(medication);
                if (val != null){
                    resultList.Add(val);
                }
            }

            return resultList;
        }

        public async Task<string> GetRxNormID(IMedication medication){
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            // Assemble the URI for the REST API Call.
            string uri = RxCUIbyName + medication.Name;

            // Execute the REST API call.
            response = await client.GetAsync(uri);

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();

            //Parse response
            XDocument xdoc = XDocument.Parse(contentString);

            if (xdoc.Element("rxnormdata").Element("idGroup").Element("rxnormId").Descendants().Count() == 0){
                return null;
            }

            var result = xdoc.Element("rxnormdata").Element("idGroup").Element("rxnormId").Value;
            return result;
        }

        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            string INDENT_STRING = "    ";
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < json.Length; i++)
            {
                var ch = json[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && json[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        static IList<InteractionPair> ResultOnly(string json)
        {
            Debug.WriteLine(JsonPrettyPrint(json));

            JObject response = JObject.Parse(json);

            IList<InteractionPair> pairs = new List<InteractionPair>();

            for (int group = 0; group < response["fullInteractionTypeGroup"].Count(); group++)
            {
                for (int type = 0; type < response["fullInteractionTypeGroup"][group]["fullInteractionType"].Count(); type++)
                {
                    string med1 = (string)response["fullInteractionTypeGroup"][group]["fullInteractionType"][type]["minConcept"][0]["name"];
                    string med2 = (string)response["fullInteractionTypeGroup"][group]["fullInteractionType"][type]["minConcept"][1]["name"];

                    for (int p = 0; p < response["fullInteractionTypeGroup"][group]["fullInteractionType"][type]["interactionPair"].Count(); p++)
                    {
                        var description = (string)response["fullInteractionTypeGroup"][group]["fullInteractionType"][type]["interactionPair"][p]["description"];
                        pairs.Add(new InteractionPair(med1, med2, description));
                    }
                }
            }

            return pairs;
        }
    }
}
