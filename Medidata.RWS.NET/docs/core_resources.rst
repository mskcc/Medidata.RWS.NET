============================================
Core Resources
============================================

Clinical Data
=============
Often times you'll want to work with the clinical data of your studies. The Medidata.RWS.NET library provides 
several requests for extracting clinical data (via RAVE "Clinical Views") or POSTing clinical data to the 
RAVE platform.

----------------------
ClinicalStudiesRequest
----------------------
Returns a list of EDC studies (as a ``RWSStudies`` object). Excludes studies that you (the authenticated user) are not associated with.

.. code-block:: c#

	using Medidata.RWS.Core.Requests.Implementations;

	//Create a connection
	var connection = new RwsConnection("innovate", "username", "password"); // authentication required

	//Send the request / get a response
	var response = connection.SendRequest(new ClinicalStudiesRequest()) as RWSStudies;

	//Write the study list to the console
	foreach (var s in response)
	{
	    Console.Write(s.OID + "\r\n");
	}
	//Mediflex(Prod)
	//Mediflex(Dev)
	//PlaceboTest(Prod)
	//...

--------------------
StudySubjectsRequest
--------------------
Returns a listing of all the subjects in a study (as a ``RWSSubjects`` object), optionally including those currently inactive or deleted.
Clinical data for the subjects is not included in the response.

Parameters
----------
TBA

.. code-block:: c#

	using Medidata.RWS.Core.Requests.Implementations;

	//Create a connection
	var connection = new RwsConnection("innovate", "username", "password"); // authentication required

	//Send the request / get a response
	var response = connection.SendRequest(new StudySubjectsRequest("Mediflex", "Prod")) as RWSSubjects;

	//Write each subject key to the console
	foreach (var s in response)
	{
	    Console.Write(s.SubjectKey + "\r\n");
	}
	// SUBJECT001
	// SUBJECT002
	// SUBJECT003
	// ...