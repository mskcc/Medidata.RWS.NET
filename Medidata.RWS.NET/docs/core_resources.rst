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
Clinical data in ODM format for the given study / environment. This data can be optionally filtered by a specific Form.

This is the equivalent of calling:
``https://{subdomain}.mdsol.com/studies/{project}({environment})/datasets/{ regular|raw }?{options}``

or, to filter the data by form:
``https://{subdomain}.mdsol.com/studies/{project}({environment})/datasets/{ regular|raw }/{ formoid }?{options}``

*Example:*

.. code-block:: c#

	using Medidata.RWS.Core.Requests
	using Medidata.RWS.Core.Requests.Datasets;

	//Create a connection
	var connection = new RwsConnection("innovate", "username", "password"); // authentication required

	//Send the request / get a response
	var response = connection.SendRequest(new StudyDatasetRequest("Mediflex", "Prod", dataset_type: "regular")) as RWSResponse;

	//Write the XML response to the console (see XML below)
	Console.Write(response.RawXMLString());


.. code-block:: xml

    <?xml version="1.0" encoding="utf-8"?>
    <ODM FileType="Snapshot" FileOID="92747321-c8b3-4a07-a874-0ecb53153f20" CreationDateTime="2017-06-05T13:09:33.202-00:00" ODMVersion="1.3" xmlns:mdsol="http://www.mdsol.com/ns/odm/metadata" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns="http://www.cdisc.org/ns/odm/v1.3">
       <ClinicalData StudyOID="Mediflex(Prod)" MetaDataVersionOID="1">
            <SubjectData SubjectKey="1">
                <SiteRef LocationOID="1" />
                <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                    <FormData FormOID="CHEM" FormRepeatKey="1">
                        <ItemGroupData ItemGroupOID="CHEM_LOG_LINE">
                            <ItemData ItemOID="CHEM.DATECOLL" Value="2015-04-25T14:09:00" />
                        </ItemGroupData>
                    </FormData>
                </StudyEventData>
            </SubjectData>
        </ClinicalData>
        <ClinicalData StudyOID="Mediflex(Prod)" MetaDataVersionOID="1">
            <SubjectData SubjectKey="2">
                <SiteRef LocationOID="1" />
                <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                    <FormData FormOID="CHEM" FormRepeatKey="1">
                        <ItemGroupData ItemGroupOID="CHEM_LOG_LINE">
                            <ItemData ItemOID="CHEM.DATECOLL" Value="2015-04-13T16:34:00" />
                        </ItemGroupData>
                    </FormData>
                </StudyEventData>
            </SubjectData>
        </ClinicalData>
        <ClinicalData StudyOID="Mediflex(Prod)" MetaDataVersionOID="1">
            <SubjectData SubjectKey="3">
                <SiteRef LocationOID="1" />
                <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                    <FormData FormOID="CHEM" FormRepeatKey="1">
                        <ItemGroupData ItemGroupOID="CHEM_LOG_LINE">
                            <ItemData ItemOID="CHEM.DATECOLL" Value="2015-05-09T18:52:00" />
                        </ItemGroupData>
                    </FormData>
                </StudyEventData>
            </SubjectData>
        </ClinicalData>
        ...
    </ODM>


---------------------
SubjectDatasetRequest
---------------------
Clinical data in ODM format for the given study / environment for a single subject. Similar to ``StudyDatasetRequest``,
this data can be optionally filtered by a specific Form.

This is the equivalent of calling:
``https://{subdomain}.mdsol.com/studies/{project}({environment})/subjects/{ subjectkey }/datasets/{ regular|raw }?{options}``

or, to filter the data by form:
``https://{subdomain}.mdsol.com/studies/{project}({environment})/subjects/{ subjectkey }/datasets/{ regular|raw }/{ formoid }?{options}``


.. code-block:: c#

    using Medidata.RWS.Core.Requests
    using Medidata.RWS.Core.Requests.Datasets;

    //Create a connection
    var connection = new RwsConnection("innovate", "username", "password"); // authentication required

    //Send the request / get a response
    var response = connection.SendRequest(new SubjectDatasetRequest("Mediflex", "Prod", subject_key: "1", dataset_type: "regular")) as RWSResponse;

    //Write the XML response to the console (see XML below)
    Console.Write(response.RawXMLString());


.. code-block:: xml

    <?xml version="1.0" encoding="utf-8"?>
    <ODM FileType="Snapshot" FileOID="9035596c-f090-4030-860a-0ed27a4e3d03" CreationDateTime="2017-06-05T13:28:39.325-00:00" ODMVersion="1.3" xmlns:mdsol="http://www.mdsol.com/ns/odm/metadata" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns="http://www.cdisc.org/ns/odm/v1.3">
    <ClinicalData StudyOID="Mediflex(Prod)" MetaDataVersionOID="1">
        <SubjectData SubjectKey="1">
            <SiteRef LocationOID="1" />
            <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                <FormData FormOID="CHEM" FormRepeatKey="1">
                    <ItemGroupData ItemGroupOID="CHEM_LOG_LINE">
                        <ItemData ItemOID="CHEM.DATECOLL" Value="2015-04-25T16:09:00" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID="Mediflex(Prod)" MetaDataVersionOID="1">
        <SubjectData SubjectKey="1">
            <SiteRef LocationOID="1" />
            <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                <FormData FormOID="ABX" FormRepeatKey="1">
                    <ItemGroupData ItemGroupOID="ABX_LOG_LINE">
                        <ItemData ItemOID="ABX.ABXDATE" Value="2017-04-25" />
                        <ItemData ItemOID="ABX.MODALITY" Value="2" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID="Mediflex(Prod)" MetaDataVersionOID="1">
        <SubjectData SubjectKey="1">
            <SiteRef LocationOID="1" />
            <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                <FormData FormOID="BONEMARROW" FormRepeatKey="1">
                    <ItemGroupData ItemGroupOID="BONEMARROW_LOG_LINE">
                        <ItemData ItemOID="BONEMARROW.VISITDAT" Value="2015-04-24" />
                        <ItemData ItemOID="BONEMARROW.CHEMSAMPLE" Value="1" />
                        <ItemData ItemOID="BONEMARROW.BMPB_COLLECT" Value="1" />
						...
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
	...
    </ODM>


---------------------
VersionDatasetRequest
---------------------
Clinical data in ODM format for the given study / environment for a single RAVE study version for all subjects.
Similar to ``StudyDatasetRequest``, this data can be optionally filtered by a specific Form.

This is the equivalent of calling:
``https://{subdomain}.mdsol.com/studies/{project}({environment})/versions/{ version_id }/datasets/{ regular|raw }?{options}``

or, to filter the data by form:
``https://{subdomain}.mdsol.com/studies/{project}({environment})/versions/{ version_id }/datasets/{ regular|raw }/{ formoid }?{options}``


.. code-block:: c#

    using Medidata.RWS.Core.Requests
    using Medidata.RWS.Core.Requests.Datasets;

    //Create a connection
    var connection = new RwsConnection("innovate", "username", "password"); // authentication required

    //Send the request / get a response
    var response = connection.SendRequest(new VersionDatasetRequest(project_name: "Mediflex", environment_name: "Dev", version_oid: "999")) as RWSResponse;

    //Write the XML response to the console (see XML below)
    Console.Write(response.RawXMLString());

*Note the **MetaDataVersionOID** value in the XML response.*

.. code-block:: xml

    <?xml version="1.0" encoding="utf-8"?>
    <ODM FileType="Snapshot" FileOID="9035596c-f090-4030-860a-0ed27a4e3d03" CreationDateTime="2017-06-05T13:28:39.325-00:00" ODMVersion="1.3" xmlns:mdsol="http://www.mdsol.com/ns/odm/metadata" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns="http://www.cdisc.org/ns/odm/v1.3">
    <ClinicalData StudyOID="Mediflex(Dev)" MetaDataVersionOID="999">
        <SubjectData SubjectKey="1">
            <SiteRef LocationOID="1" />
            <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                <FormData FormOID="CHEM" FormRepeatKey="1">
                    <ItemGroupData ItemGroupOID="CHEM_LOG_LINE">
                        <ItemData ItemOID="CHEM.DATECOLL" Value="2015-04-25T16:09:00" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID="Mediflex(Dev)" MetaDataVersionOID="999">
       <SubjectData SubjectKey="2">
            <SiteRef LocationOID="1" />
            <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                <FormData FormOID="CHEM" FormRepeatKey="1">
                    <ItemGroupData ItemGroupOID="CHEM_LOG_LINE">
                        <ItemData ItemOID="CHEM.DATECOLL" Value="2016-04-25T16:09:00" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID="Mediflex(Dev)" MetaDataVersionOID="999">
        <SubjectData SubjectKey="3">
            <SiteRef LocationOID="1" />
            <StudyEventData StudyEventOID="SCREENING" StudyEventRepeatKey="1">
                <FormData FormOID="CHEM" FormRepeatKey="1">
                    <ItemGroupData ItemGroupOID="CHEM_LOG_LINE">
                        <ItemData ItemOID="CHEM.DATECOLL" Value="2017-04-25T16:09:00" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
	...
    </ODM>