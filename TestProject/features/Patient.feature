Feature: Patient
	In order see patient details
	As a user
	I want to retireve the patient infpormation and medication 

Background:
	Given Load the expected test data from json file 'patients.json'

@API
Scenario Outline: Retrieve Patient Info
	Given I want to find details for patientID = '<id>' and includemedications = '<includemedications>'
	When I retrieve the details for the patient
	Then the result should should show details for '<firstName>' '<lastName>'

Examples:
| description                | id | includemedications | firstName | lastName |
| patient1 medication = null | 1  | null               | Fred      | Smith    |
| patient1 medication = N    | 1  | N                  | Fred      | Smith    |
| patient1 medication = Y    | 1  | Y                  | Fred      | Smith    |
| patient2 medication = Y    | 2  | Y	 			   | Sally     | Jones    |