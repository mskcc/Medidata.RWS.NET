
============================================
Getting Started
============================================

Installation
============
Install Medidata.RWS.NET via NuGet: 
``nuget install Medidata.RWS.NET``

or, via the Package Manager Console: 
``Install-Package Medidata.RWS.NET``

Basic Example
===============
At the most basic level, communicating with Medidata RAVE using Medidata.RWS.NET involves:

1. Creating a connection.

.. code-block:: c#

	using Medidata.RWS.Core.Requests;
	var connection = new RwsConnection("innovate", "RAVE username", "RAVE password");

The above code will create a RwsConnection object to the "innovate" RAVE instance (subdomain) - https://innovate.mdsol.com. You'd substitute your RAVE instance subdomain here.
The RAVE username and RAVE password parameters should reference a dedicated RAVE account you intend to use for web service activities.

2. Creating a request.

.. code-block:: c#

	using Medidata.RWS.Core.Requests.Datasets;
	var datasetRequest = new SubjectDatasetRequest("MediFlex", "PROD", subject_key: "SUBJECT001", formOid: "HEM");

3. Sending the request.

.. code-block:: c#

	var response = connection.SendRequest(datasetRequest) as RWSResponse;

4. Dealing with the response/exception.

.. code-block:: c#

	Console.Write(response.RawXMLString());

5. Putting this all together, we have the following.

.. code-block:: c#

	using Medidata.RWS.Core.Requests.Datasets;
	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate", "RAVE username", "RAVE password");

	//Create a request
	var datasetRequest = new SubjectDatasetRequest("MediFlex", "PROD", subject_key: "SUBJECT001", formOid: "HEM");

	//Send the request / get a response
	var response = connection.SendRequest(datasetRequest) as RWSResponse;

	//Write the response XML string to the console
	Console.Write(response.RawXMLString());

The above steps outline how to retrieve data for **SUBJECT001**'s **HEM** form in the **MediFlex** study (specifically, the **PROD** environment).

Miscellaneous Configuration
===========================
When working with Medidata.RWS.NET, you may wish to configure the connection to RWS beyond the default settings. Options for doing so are described below.

-----------------
Virtual Directory
-----------------
When you make requests to RAVE Web services, the URL you communicate with follows a specific pattern. The domain name will always be "mdsol.com", and the protocol will always be "https://".
The sub-domain & virtual directory are configurable. The default virtual directory is "RaveWebServices". This means that when you create a new connection to innovate using the default virtual directory, you'll get the following:

.. code-block:: c#

	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate", "RAVE username", "RAVE password");
	connection.base_url; // 'https://innovate.mdsol.com/RaveWebServices'

If instead you'd like to change the virtual directory to a custom one, you can pass an additional parameter through the RwsConnection constructor:

.. code-block:: c#

	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate", "RAVE username", "RAVE password", "CustomVirtualDirectory");
	connection.base_url; // 'https://innovate.mdsol.com/CustomVirtualDirectory'

Note that this is provided as a convenience, and the default virtual directory will be preferable in most scenarios.

--------
Timeouts
--------
If supplied, the timeout of the request in milliseconds. If the request takes longer than the timeout value, an exception will be thrown.

.. code-block:: c#

	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate", "RAVE username", timeout: 1000)
