using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using myMD.Model.DatabaseModel;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using Xamarin.Forms.Internals;

namespace myMD.Model.ParserModel
{
    public class ImageToTextParser : FileToDatabaseParser
    {
            // **********************************************
            // *** Update or verify the following values. ***
            // **********************************************

            // Replace the subscriptionKey string value with your valid subscription key.
            const string subscriptionKey = "ab36f4ca191d43ba827560d037333842";

            // Replace or verify the region.
            //
            // You must use the same region in your REST API call as you used to obtain your subscription keys.
            // For example, if you obtained your subscription keys from the westus region, replace 
            // "westcentralus" in the URI below with "westus".
            //
            // NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
            // a free trial subscription key, you should not need to change this region.
            const string uriBaseOCR = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/ocr";
            const string uriBaseHR = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/recognizeText";
            

            /// <summary>
            /// Gets the text visible in the specified image file by using the Computer Vision REST API.
            /// </summary>
            /// <param name="file">The image file.</param>
            public static async Task<string> MakeOCRRequest(byte[] file)
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Request parameters.
                string requestParameters = "language=unk&detectOrientation=true";

                // Assemble the URI for the REST API Call.
                string uri = uriBaseOCR + "?" + requestParameters;

                HttpResponseMessage response;

                // Request body. Posts a locally stored JPEG image.
                //byte[] byteData = GetImageAsByteArray(file);
                
                using (ByteArrayContent content = new ByteArrayContent(file))
                {
                    // This example uses content type "application/octet-stream".
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    // Execute the REST API call.
                    response = await client.PostAsync(uri, content);

                    // Get the JSON response.
                    string contentString = await response.Content.ReadAsStringAsync();
                    
                    return TextOnly(contentString);
                }
            }

        /// <summary>
        /// Gets the handwritten text from the specified image file by using the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file with handwritten text.</param>
        public static async Task<string> ReadHandwrittenText(byte[] file)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameter. Set "handwriting" to false for printed text.
            string requestParameters = "handwriting=true";

            // Assemble the URI for the REST API Call.
            string uri = uriBaseHR + "?" + requestParameters;

            HttpResponseMessage response = null;

            // This operation requrires two REST API calls. One to submit the image for processing,
            // the other to retrieve the text found in the image. This value stores the REST API
            // location to call to retrieve the text.
            string operationLocation = null;

            // Request body. Posts a locally stored JPEG image.

            ByteArrayContent content = new ByteArrayContent(file);

            // This example uses content type "application/octet-stream".
            // You can also use "application/json" and specify an image URL.
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            // The first REST call starts the async process to analyze the written text in the image.
            response = await client.PostAsync(uri, content);

            // The response contains the URI to retrieve the result of the process.
            if (response.IsSuccessStatusCode)
            {
                operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
                Debug.WriteLine(operationLocation);
            }
            else
            {
                // Display the JSON error data.
                Debug.WriteLine("\nError:\n");
                Debug.WriteLine(JsonPrettyPrint(await response.Content.ReadAsStringAsync()));
                return (await response.Content.ReadAsStringAsync());
            }

            // The second REST call retrieves the text written in the image.
            //
            // Note: The response may not be immediately available. Handwriting recognition is an
            // async operation that can take a variable amount of time depending on the length
            // of the handwritten text. You may need to wait or retry this operation.
            //
            // This example checks once per second for ten seconds.
            string contentString;
            int i = 0;
            do
            {
                await Task.Delay(1000);
                response = await client.GetAsync(operationLocation);
                contentString = await response.Content.ReadAsStringAsync();
                ++i;
            }
            while (i < 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1);

            if (i == 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1)
            {
                Debug.WriteLine("\nTimeout error.\n");
            }

            // Display the JSON response.
            Debug.WriteLine("\nResponse:\n");
            Debug.WriteLine(JsonPrettyPrint(contentString));
            return HandwrittentextOnly(contentString);
        }


            /// <summary>
            /// Returns the contents of the specified file as a byte array.
            /// </summary>
            /// <param name="file">The image file to read.</param>
            /// <returns>The byte array of the image data.</returns>
            static byte[] GetImageAsByteArray(MediaFile file)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    return memoryStream.ToArray();
                }
            }


            /// <summary>
            /// Formats the given JSON string by adding line breaks and indents.
            /// </summary>
            /// <param name="json">The raw JSON string to format.</param>
            /// <returns>The formatted JSON string.</returns>
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

        static string TextOnly(string json){
            JObject response = JObject.Parse(json);

            string result = "";

            for (int region = 0; region < response["regions"].Count(); region++) {
                for (int line = 0; line < response["regions"][region]["lines"].Count(); line++) {
                    for (int word = 0; word < response["regions"][region]["lines"][line]["words"].Count(); word++){
                        result += (string)response["regions"][region]["lines"][line]["words"][word]["text"] + " ";
                    }
                    result += Environment.NewLine;
                }
                result += Environment.NewLine;
                result += Environment.NewLine;
            }

            return result;
        }

        static string HandwrittentextOnly(string json)
        {
            JObject response = JObject.Parse(json);

            string result = "";

            for (int line = 0; line < response["recognitionResult"]["lines"].Count(); line++)
            {
                result += (string)response["recognitionResult"]["lines"][line]["text"];
                result += Environment.NewLine;
            }

            return result;
        }

        protected override void Init(string file)
        {
            throw new NotImplementedException();
        }

        protected override Doctor ParseDoctor()
        {
            throw new NotImplementedException();
        }

        protected override DoctorsLetter ParseLetter()
        {
            throw new NotImplementedException();
        }

        protected override IList<Medication> ParseMedications()
        {
            throw new NotImplementedException();
        }

        protected override Profile ParseProfile()
        {
            throw new NotImplementedException();
        }

        public void GenerateDoctorsLetter(string DoctorsName, string DoctorsField, DateTime LetterDate, string Diagnosis, IEntityDatabase db){
            Doctor eqDoc = new Doctor();
            eqDoc.Name = DoctorsName;
            eqDoc.Field = DoctorsField;

            IDoctor doc = db.GetDoctor(eqDoc);
            Doctor doctor;
            //F黦e neuen Arzt in die Datenbank ein, falls dieser dort noch nicht existiert
            if (doc == null)
            {
                doctor = eqDoc;
                db.Insert(doctor);
            }
            else
            {
                doctor = doc.ToDoctor();
            }

            DoctorsLetter letter = new DoctorsLetter();
            letter.Diagnosis = Diagnosis;
            letter.Date = LetterDate;
            db.Insert(letter);
            //Erstelle Beziehungen zwischen den geparsten Entitäten
            //letter.Profile = profile;
            letter.DatabaseDoctor = doctor;

            db.Update(letter);

        }
    }
}