Coding Exercise - Software Engineer in Test

[Company Name] Coding Exercise

The goal of the exercise is to demonstrate knowledge of testing strategy, testing approaches and automation coding. Time spent on the exercise should be limited to under 2 hours.


REST API to be Tested
An API to return a patient given a patient ID, and optionally return a list of medications for the patient. GET http://test.company-name.com/api/patient/{id},(includemedications)

Input Parameter	Type	Validation	Required
id	Numeric	11 digits	Yes
includemedications	character	Y or N	No (default false)

Output	Type	Validation
id	Numeric	11 digits
LastName	String	Max length of 50
FirstName	String	Max length of 50
Medicationname	String	Max length of 150
StartDate	Date	
EndDate	Date	does not precede start date, optional

Response

200

Headers
Content-Type:application/json

Sample Body
 
[
{
"id": 1,
"FirstName": "Smith", "LastName": "Fred",
},
{
"id": 2,
"FirstName": "Johnson", "LastName": "Sally", "Medications":[
{
"MedicationID":1, "MedicationName":'Acetaminophone', "StartDate":'05/14/2019'
},
{
 










]



Response

401
 
"MedicationID":2, "MedicationName":'Tylenol', "StartDate":'05/15/2019', "EndDate":'05/21/2019'
}
]
}
 

Headers
Content-Type:application/json

Body



{
"error": "error.unauthorized"
}




To Be Completed

1.  Briefly describe your overall strategy for testing the API. Questions you might have, different types of ways you might consider testing the
API, characteristics and functionality you would be looking to verify, etc.
2.  Code (in your language of choice) a utility that will loop through the patients in the mock data, invoke the API and verify the results.
3. Code (in your language or or package/tool choice) a test to generate large concurrent invocations of the API - for performance testing.
4. Assume a web page exists that displays a single patient and the list of medications for that patient. It’s a React.js SPA - and the user has already logged in selected a patient. Describe how you might use Selenium (or a similar tool) - to load the expected patient data from JSON - and verify that the contents of the web page is correct (correct patient, number of medications, etc.)
 
Mock Data



"Patients":

[
{
"LastName":"Smith", "FirstName":"Fred", "Medications":[
{
"MedicationName:"morphine", "Dose":"2mg", "StartDate":"10/10/15"
},
{
"MedicationName":"acetaminophen", "Dose":"325mg", "StartDate":"10/11/15", "StopDate:"10/14/15"
},
{
"MedicationName":"furosemide", "Dose":"20mg", "StartDate":"10/31/15"
}
]
},
{
"LastName":"Jones", "FirstName":"Sally", "Medications":[
{
"MedicationID":1, "MedicationName":"morphine", "Dose":"1mg", "StartDate":"10/11/15"
},
{
"MedicationID":2, "MedicationName":"coumadin", "Dose":"5mg", "StartDate:"10/15/15"
},
{
"MedicationID":3, "MedicationName":"lovenox", "Dose":"100mg/mL","StartDate":"10/31/15", "StopDate":"11/2/15"
}
 
]
}
]
