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

*Example:*

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

This is the equivalent of calling:
``https://{subdomain}.mdsol.com/studies/{study-oid}/Subjects[?status=all&include={inactive|inactiveAndDeleted}]``

Parameters
----------
.. |br| raw:: html

   <br />

+-------------------------------------+-----------------------------------------------------------------------------+-------------+
| Parameter                           | Description                                                                 | Mandatory?  |
+=====================================+=============================================================================+=============+
| {study-oid}                         | The study name.                                                             | Yes         |
+-------------------------------------+-----------------------------------------------------------------------------+-------------+
| status={**true** | **false**}       | If true, add subject level workflow status to  |br|                         |             |
|                                     | the response (if present).                                                  | No          |
+-------------------------------------+-----------------------------------------------------------------------------+-------------+
| include= |br|\                      | Will include active, inactive and/or deleted |br|                           | No          |
| {**inactive** | **deleted** | |br|\ | subjects in the response.                                                   |             |
| **inactiveAndDeleted**}             |                                                                             |             |
+-------------------------------------+-----------------------------------------------------------------------------+-------------+
| subjectKeyType= |br|\               | Whether RWS should return the unique |br|                                   | No          |
| {**SubjectName** | **SubjectUUID**} | identifier (UUID) or the subject name.                                      |             |
+-------------------------------------+-----------------------------------------------------------------------------+-------------+
| links={**true** | **false**}        | If true, includes "deep link"(s) (e.g. URLs) to the |br|                    | No          |
|                                     | subject page in Rave in the response.                                       |             |
+-------------------------------------+-----------------------------------------------------------------------------+-------------+

*Example:*

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

Clinical View Datasets
======================

In addition to the above requests, Medidata RAVE Web Services allows for the extraction of clinical data in the form of 
"Clinical Views" - that is, RAVE database views. There are 3 "Datasets" available that represent different subsets of 
clinical data for your studies:

-------------------
StudyDatasetRequest
-------------------
Clinical data in ODM format for the given study / environment. 

TBA

---------------------
SubjectDatasetRequest
---------------------
Clinical data in ODM format for the given study / environment for a single subject.

TBA

---------------------
VersionDatasetRequest
---------------------
Clinical data in ODM format for the given study / environment for a single RAVE study version for all subjects.

TBA