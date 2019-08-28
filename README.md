### Coding Exercise - Software Engineer in Test

This is a pre-interview exercise for a company I applied to.  There are 4 parts to this exercise which is pretty extensive for a pre-interview problem. It asks for overall testing strategy, coding API tests, performance tests, and UI/integration tests.  
1.  Briefly describe your overall strategy for testing the API. Questions you might have, different types of ways you might consider testing the
API, characteristics and functionality you would be looking to verify, etc.
2.  Code (in your language of choice) a utility that will loop through the patients in the mock data, invoke the API and verify the results.
3. Code (in your language or or package/tool choice) a test to generate large concurrent invocations of the API - for performance testing.
4. Assume a web page exists that displays a single patient and the list of medications for that patient. It’s a React.js SPA - and the user has already logged in selected a patient. Describe how you might use Selenium (or a similar tool) - to load the expected patient data from JSON - and verify that the contents of the web page is correct (correct patient, number of medications, etc.)

Health Data Analytics Institute Coding Exercise

The goal of the exercise is to demonstrate knowledge of testing strategy, testing approaches and automation coding. Time spent on the exercise should be limited to under 2 hours.


<b>REST API to be Tested </b><Br>
An API to return a patient given a patient ID, and optionally return a list of medications for the patient. <br>
GET http://test.hdai.com/api/patient/{id},(includemedications)

| Input Parameter    | Type      | Validation | Required           |
|--------------------|-----------|------------|--------------------|
| id                 | Nuneric   | 11 digits  | Yes                |
| includemedications | character | Y or N     | No (default false) |

| Output         | Type    | Validation                            |
|----------------|---------|---------------------------------------|
| id             | Nuneric | 11 digits                             |
| LastName       | String  | Max length of 50                      |
| FirstName      | String  | Max length of 50                      |
| Medicationname | String  | Max length of 150                     |
| StartDate      | Date    |                                       |
| EndDate        | Date    | does not precede start date, optional |

<b>Response</b>
200
- Headers
- Content-Type:application/json

Sample Body
```json
[
 {
  "id": 1,
  "FirstName": "Smith", 
  "LastName": "Fred",
 },
 {
  "id": 2,
  "FirstName": "Johnson", 
  "LastName": 
  "Sally", 
  "Medications":[
   {
    "MedicationID":1, 
    "MedicationName":'Acetaminophone', 
    "StartDate":'05/14/2019'
   },
   {
    "MedicationID":2, 
    "MedicationName":'Tylenol', 
    "StartDate":'05/15/2019', 
    "EndDate":'05/21/2019'
   }
 ]
}
]
```

<b>Response</b>
401
- Headers
- Content-Type:application/json 
 
Body
```json
{
"error": "error.unauthorized"
}
```

<b>To Be Completed</b>

1.  Briefly describe your overall strategy for testing the API. Questions you might have, different types of ways you might consider testing the
API, characteristics and functionality you would be looking to verify, etc.
2.  Code (in your language of choice) a utility that will loop through the patients in the mock data, invoke the API and verify the results.
3. Code (in your language or or package/tool choice) a test to generate large concurrent invocations of the API - for performance testing.
4. Assume a web page exists that displays a single patient and the list of medications for that patient. It’s a React.js SPA - and the user has already logged in selected a patient. Describe how you might use Selenium (or a similar tool) - to load the expected patient data from JSON - and verify that the contents of the web page is correct (correct patient, number of medications, etc.)
 
Mock Data

```json
"Patients":

[
 {
  "LastName":"Smith", 
  "FirstName":"Fred", 
  "Medications":[
   {
    "MedicationName:"morphine", 
    "Dose":"2mg", 
    "StartDate":"10/10/15"
   },
  {
   "MedicationName":"acetaminophen", 
   "Dose":"325mg", 
   "StartDate":"10/11/15", 
   "StopDate:"10/14/15"
  },
  {
   "MedicationName":"furosemide", 
   "Dose":"20mg", 
   "StartDate":"10/31/15"
  }
 ]
},
{
 "LastName":"Jones", 
 "FirstName":"Sally", 
 "Medications":[
  {
   "MedicationID":1, 
   "MedicationName":"morphine", 
   "Dose":"1mg", 
   "StartDate":"10/11/15"
  },
  {
   "MedicationID":2, 
   "MedicationName":"coumadin", 
   "Dose":"5mg", 
   "StartDate:"10/15/15"
  },
  {
   "MedicationID":3, 
   "MedicationName":"lovenox", 
   "Dose":"100mg/mL",
   "StartDate":"10/31/15", 
   "StopDate":"11/2/15"
  }
 ]
}
]
```
# Solution

1) The approach I have taken in testing the HDAI Patient Rest API is to use C# utilizing RESTSharp library
and Behavior Driven Development (BDD) using SpecFlow. There are many other approaches such as using SOAPUI, Swagger, RestAssured libraries (java), etc. but since I already have my development environment for Microsoft Visual Studio set up, it was quicker for me. I chose BDD because it is an Agile process that encourages collaboration between business owners, developers, and testers.
Test Scenarios should include some basic validations such as validating response (i.e. HTTP Status code OK), Content type (application/json), fields (types, validation requirements, and whether optional). Since these are basic validations and to keep my answer short, they will not be addressed here.
2) Note: The baseUrl http://test.hdai.com/api/patient/ is not accessible to the public. Although this is not a blocker for developing the test code, it does make it impossible to validate whether the below tests work.
The first step is to create a feature file that contains the feature being tested and scenarios. The feature file is in a natural language that non-developers are able to read.

```c#
Feature: Patient
In order see patient details
As a user
I want to retrieve the patient information and medication

Background:
Given Load the expected test data from json file 'patients.json'

@API
Scenario Outline: Retrieve Patient Info
Given I want to find details for patientID = '<id>' and includemedications =
'<includemedications>'
When I retrieve the details for the patient
Then the result should show details for '<firstName>' '<lastName>'
 
 Examples:
| description                | id | includemedications | firstName | lastName |
|----------------------------|----|--------------------|-----------|----------|
| patient1 medication = null | 1  | null               | Fred      | Smith    |
| patient1medication=N       | 1  | N                  | Fred      | Smith    |
| patient1medication=Y       | 1  | Y                  | Fred      | Smith    |
| patient2medication=Y       | 2  | Y                  | Sally     | Jones    |
```
In BDD, each step (the Given, When, Then) corresponds to methods in a step definition file.

```c#
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json; using System.Collections.Generic; using System.IO;
using TechTalk.SpecFlow; using TestProject.dataObjects;

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
   var dataDir = @"C:\Users\{myUserName}\Source\Repos\TestProject\TestProject\bin\Debug\netcoreapp2.1\testData";
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
   
 [When(@"I retrieve the details for the patient")] public void WhenIRetrieveTheDetailsForThePatient() 
 {
  restResponse = restClient.Execute(restRequest);
  patientResponse = new JsonDeserializer().Deserialize<Patient>(restResponse); 
 }

 [Then(@"the result should show details for '(.*)' '(.*)'")]
 public void ThenTheResultShouldShouldShowDetailsFor(string fName, string lName) 
 {
  Assert.AreEqual(fName, patientResponse.fName); Assert.AreEqual(lName, patientResponse.lname);
  // For brevity, above assertion is based on user supplied name from feature file.
  // TODO: Read patients.json data file as expected data and compare with patientResponse obj
 }
 }
}

```

In the step “Given Load the expected test data from json file 'patients.json'”, the mock/expected data is read from the patients.json file. <br>

The step “Given I want to find details for patientID = '<id>' and includemedications = '<includemedications>'” sets up the RestClient and RestRequest. <br>
 
The step “When I retrieve the details for the patient” executes the RestClient and stores the response as a deserialized json in a Patient object. The Patient object is a (POCO – plain old C# object). <br>

The step “Then the result should show details for '<firstName>' '<lastName>'” validates the data. Due to brevity and availability of time, the Then validation only validates the patient name (first and last). The TODO is to validate the expected patients.json file against the rest json response. <br>

Running the scenario file will run a test for each row in the Examples table.
All of the code above is available at https://github.com/jtam100/restSharp <br>

3) Note: The baseUrl http://test.hdai.com/api/patient/ is not accessible to the public. <br>

For performance testing, I have decided to use Apache JMeter because it is an open sourced tool with less need for writing code while still providing many benefits such as integration with popular CI/CD tools. The steps are as follows:
1) Add a Http Header Manager to your Test Plan with Content-Type set to application/json
2) Add thread group to the test plan setting the number of threads to 10, ramp up period to 10
seconds, and loop count to 10
3) Set up the HTTP request Defaults with servername = test.hdai.com/api
4) Under the Thread Group, create the HTTP Request for patient 1
5) Add Listeners for View Result Tree
6) Under the Thread Group, create the HTTP Request for patient 1 w/ medications
7) Add Listeners for View Result Tree
8) Save and execute by using the green run button
<br>

4) Assuming we have already validated the REST API using the code from step 2, the selenium UI test Assertion should be of what is displayed on UI against the expected data from the call to the REST API.

```c#
Background:
       Given User is logged in
@UI
Scenario Outline: Retrieve Patient Info
Given I want to find details for patientID = '<id>' and includemedications =
'<includemedications>'
When I retrieve the details for the patient
Then the result should show details that patient
Examples:
| description                | id | includemedications |
|----------------------------|----|--------------------|
| patient1 medication = null | 1  | Y                  |
```
The BDD feature file is very similar to the API test. In this case in the When statement, the selenium script will also navigate to the patient info screen based on the @UI tag. The Then statement validate the expected results ( from REST API) against the actual (from UI).
