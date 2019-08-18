using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;
using TestProject.dataObjects;

namespace TestProject.stepDefinitions
{
    [Binding]
    public class PatientSteps
    {
        private RestClient restClient;
        private RestRequest restRequest;
        private IRestResponse restResponse;
        List<Patient> expectedPatientData = new List<Patient>();
        Patient patientResponse;
        string baseUrl = "http://test.hdai.com/api/patient/";

        [Given(@"Load the expected test data from json file '(.*)'")]
        public void GivenLoadTheExpectedTestDataFromJsonFile(string dataFile)
        {
            var dataDir = @"C:\Users\JTam\Source\Repos\TestProject\TestProject\bin\Debug\netcoreapp2.1\testData";
            string json = File.ReadAllText(@dataDir + "\\" + dataFile);
            expectedPatientData = JsonConvert.DeserializeObject<List<Patient>>(json);
        }

        [Given(@"I want to find details for patientID = '(.*)' and includemedications = '(.*)'")]
        public void GivenIWantToFindDetailsForPatientIDAndIncludemedications(string id, string includeMeds)
        {
            restClient = new RestClient(baseUrl);
            switch (includeMeds)
            {
                case "N":   
                case "Y":
                    restRequest = new RestRequest(id + "/" + includeMeds, Method.GET);
                    break;
                default:
                    restRequest = new RestRequest(id, Method.GET);
                    break;
            }
        }

        [When(@"I retrieve the details for the patient")]
        public void WhenIRetrieveTheDetailsForThePatient()
        {
            restResponse = restClient.Execute(restRequest);
            patientResponse = new JsonDeserializer().Deserialize<Patient>(restResponse);
        }

        [Then(@"the result should should show details for '(.*)' '(.*)'")]
        public void ThenTheResultShouldShouldShowDetailsFor(string fName, string lName)
        {
            Assert.AreEqual(fName, patientResponse.fName);
            Assert.AreEqual(lName, patientResponse.lname);

            // For brevity, above assertion is based on user supplied name from feature file.
            // TODO: Read patients.json data file as expected data and compare with patientResponse obj          
        }
    }
}
