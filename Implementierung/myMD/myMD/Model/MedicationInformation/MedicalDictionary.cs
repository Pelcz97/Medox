using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myMD.Model.MedicationInformation
{
    public class MedicalDictionary : IMedicalDictionary
    {

        const string url1 = "https://www.dictionaryapi.com/api/references/medical/v2/xml/";
        const string url2 = "?key=001a4d6f-43df-42aa-a652-e085ade599dc";


        public async Task<IList<DictionaryEntry>> GetDefinitions(string term){
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            // Assemble the URI for the REST API Call.
            string uri = url1 + term + url2;

            // Execute the REST API call.
            response = await client.GetAsync(uri);

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();

            //Parse response
            XDocument xdoc = XDocument.Parse(contentString);

            IList<DictionaryEntry> entries = new List<DictionaryEntry>();
            Debug.WriteLine(contentString);

            if (xdoc.Element("entry_list").Descendants().Any())
            {
                foreach (var element in xdoc.Element("entry_list").Descendants("entry")){
                    string expression = (string)element.Element("ew").Value;
                    string definition = (string)element.Element("def").Element("sensb").Element("sens").Element("dt").Value;

                    Debug.WriteLine("Expression: " + expression);
                    Debug.WriteLine("Definition: " + definition);
                    entries.Add(new DictionaryEntry(expression, definition));
                }
            }

            return entries;
        }
    }
}
