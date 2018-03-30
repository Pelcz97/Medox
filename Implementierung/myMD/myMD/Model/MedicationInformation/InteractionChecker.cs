using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.Model.MedicationInformation
{
    public class InteractionChecker : IInteractionChecker
    {
        const string RxCUIbyName = "https://rxnav.nlm.nih.gov/REST/rxcui?name=";
        
        public IList<string> GetInteractions(IList<IMedication> medications)
        {
            return null;
        }

        public IList<string> GetRxNormIDs(IList<IMedication> medications){
            return null;
        }

        public async void GetRxNormID(IMedication medication){
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            // Assemble the URI for the REST API Call.
            //string uri = RxCUIbyName + medication.Name;
            string uri = RxCUIbyName + "aspirin";

            // Execute the REST API call.
            response = await client.GetAsync(uri);

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(contentString);
            //return contentString;

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
    }
}
