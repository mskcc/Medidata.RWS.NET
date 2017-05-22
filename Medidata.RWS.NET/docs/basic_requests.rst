
============================================
Basic Requests
============================================

Medidata.RWS.NET provides some basic diagnostic / health check API requests out of the box.

VersionRequest()
================
Returns the RWS version number. Specifically, this is the textual response returned when calling ``https://{ subdomain }.mdsol.com/RaveWebServices/version``.

.. code-block:: c#

	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate"); // no authentication required

	//Send the request / get a response
	var response = connection.SendRequest(new VersionRequest()) as RWSTextResponse;

	//Write the response text to the console
	Console.Write(response.ResponseText);
	//1.15.0

BuildVersionRequest()
=====================
Returns the internal RWS build number. Specifically, this is the textual response returned when calling ``https://{ subdomain }.mdsol.com/RaveWebServices/version/build``.

.. code-block:: c#

	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate"); // no authentication required

	//Send the request / get a response
	var response = connection.SendRequest(new BuildVersionRequest()) as RWSTextResponse;

	//Write the response text to the console
	Console.Write(response.ResponseText);
	//5.6.5.335

TwoHundredRequest()
===================
Returns the html document (along with a 200 HTTP response code) that contains information about the MAuth configuration of Rave Web Services with the given configuration.
Specifically, this is the html response returned when calling ``https://{ subdomain }.mdsol.com/RaveWebServices/twohundred``.

.. code-block:: c#

	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate"); // no authentication required

	//Send the request / get a response
	var response = connection.SendRequest(new TwoHundredRequest()) as RWSTextResponse;

	//Write the response text to the console
	Console.Write(response.ResponseText);
	//<!DOCTYPE html>\r\n<html>\r\n<head><script..........

CacheFlushRequest()
===================
Send a request to flush the RWS cache. Typically, this is used to immediately implement configuration changes in RWS.
Under normal circumstances, this request is unnecessary as RAVE and RWS manage their own caching mechanisms automatically.
Specifically, this is the equivalent of calling ``https://{ subdomain }.mdsol.com/RaveWebServices/webservice.aspx?CacheFlush``.

.. code-block:: c#

	using Medidata.RWS.Core.Requests;

	//Create a connection
	var connection = new RwsConnection("innovate", "username", "password"); // authentication is required

	//Send the request / get a response
	var response = connection.SendRequest(new CacheFlushRequest()) as RWSResponse;

	//Write the response text to the console
	Console.Write(response.IsTransactionSuccessful);
	//true