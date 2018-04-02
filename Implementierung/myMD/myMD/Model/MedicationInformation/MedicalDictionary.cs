using System;
using System.Diagnostics;
using System.Net.Http;
using System.Xml.Linq;

namespace myMD.Model.MedicationInformation
{
    public class MedicalDictionary : IMedicalDictionary
    {

        const string url1 = "https://www.dictionaryapi.com/api/references/medical/v2/xml/";
        const string url2 = "?key=001a4d6f-43df-42aa-a652-e085ade599dc";


        public async void GetDefinitions(string term){
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            // Assemble the URI for the REST API Call.
            string uri = url1 + "antibiotics" + url2;

            // Execute the REST API call.
            response = await client.GetAsync(uri);

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();

            //Parse response
            /*XDocument xdoc = XDocument.Parse(contentString);

            if (xdoc.Element("rxnormdata").Element("idGroup").Descendants().Count() == 1)
            {
                return null;
            }*/

            Debug.WriteLine(contentString);
        }
    }
}
