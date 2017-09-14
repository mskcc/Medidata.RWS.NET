============================================
Using Builders
============================================

When communicating with Medidata RAVE Web Services, your data payloads will take the form of ODM XML - or `Operational Data Model <http://www.cdisc.org/odm/>`_ XML documents.

It's important to understand that RWS expects the XML data you send to conform to the ODM format - malformed or otherwise improperly formatted XML
won't be processed. Since creating these data structures manually can be time consuming and tedious, Medidata.RWS.NET provides several "Builder" classes
to help.

Basic Example - Register a Subject
==================================

By way of example, let's say you want to register a subject onto a RAVE study using RWS. In order to do this, you'll need:

1. An authenticated connection to RWS
2. An XML document that represents the POST request you intend to make, which will include:

  - A Study OID (study)
  - A LocationOID (site)
  - A SubjectKey (subject)

3. A way to deal with the response after the request is sent

The ``ODMBuilder`` class allows developers to build out the ODM XML documents required for transmission using a simple to use, fluent interface.
Using the above example, let's create a new ``ODMBuilder`` instance to register a subject:

.. code-block:: c#

    var odmObject = new ODMBuilder().WithClinicalData("MediFlex", cd =>
        cd.WithSubjectData("SUBJECT001", "SITE01", sd =>
            sd.WithTransactionType(TransactionType.Insert)));

After instantiating the ``ODMBuilder`` class, you'll notice that you have access to chain-able methods which allow you to construct the object appropriate
for your use case. Since we are registering a subject, we supplied a Study OID (``Mediflex``), Subject Key (``SUBJECT001``), and Site (``SITE01``). 

Each of the nested methods used (e.g. ``WithClinicalData``, ``WithSubjectData``, and ``WithTransactionType``) map to the specific XML node we want
to construct. 

To see the XML string representation of what we've got so far, you can use the ``AsXMLString()`` method, which will convert the ODM object 
you constructed into an XML string.

For example:

.. code-block:: c#

    string registrationXml = new ODMBuilder().WithClinicalData("MediFlex", cd =>
        cd.WithSubjectData("SUBJECT001", "SITE01", sd =>
            sd.WithTransactionType(TransactionType.Insert))).AsXMLString();

would produce:

.. code-block:: xml

	<?xml version="1.0" encoding="utf-16"?>
	<ODM xmlns:mdsol="http://www.mdsol.com/ns/odm/metadata" FileType="Transactional" Granularity="All" FileOID="1d84fb20-1959-45bf-b9c4-cf2ad7a4273d" CreationDateTime="2017-09-14T15:01:50.8441121-04:00" AsOfDateTime="0001-01-01T00:00:00" ODMVersion="1.3" xmlns="http://www.cdisc.org/ns/odm/v1.3">
	  <ClinicalData StudyOID="MediFlex" MetaDataVersionOID="1">
		<SubjectData SubjectKey="SUBJECT001" TransactionType="Insert">
		  <SiteRef LocationOID="SITE01" />
		</SubjectData>
		<AuditRecords />
		<Signatures />
		<Annotations />
	  </ClinicalData>
	</ODM>

Using this ODM conformant XML, you could now POST it to RAVE by wrapping it in a ``PostDataRequest`` object:

.. code-block:: c#

	RwsConnection conn = new RwsConnection("innovate", "username", "password");
	var registrationRequest = new PostDataRequest(registrationXml);
	var response = conn.SendRequest(registrationRequest) as RWSPostResponse;

	//If successful, SUBJECT001 should be registered in SITE01 for the Mediflex study.