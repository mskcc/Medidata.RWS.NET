
============================================
Topics
============================================

Authentication
==============
In order to use Medidata RAVE web services, you must authenticate your web service requests. You can do this by supplying both a RAVE username and RAVE password parameter when you establish a ``RwsConnection`` object. This username/password should reference a dedicated RAVE account you intend to use for web service activities.

For example:

.. code-block:: c#

	using Medidata.RWS.Core.Requests;
	var connection = new RwsConnection("innovate", "rwsUser1", "password1");

The above code will create a ``RwsConnection`` object and point it to the innovate RAVE instance (subdomain) - ``https://innovate.mdsol.com``. The username and password you provide are concatenated, base64-encoded, and passed in the Authorization HTTP header each time you make a request using the connection object, as follows:

``Authorization: Basic cndzVXNlcjE6cGFzc3dvcmQx``

**Do not share your username / password in publicly accessible areas such GitHub, client-side code, and so forth.** Authentication is only required when establishing a new ``RwsConnection`` object. The Medidata.RWS.NET library will then automatically send the appropriate Authorization header for each request made with this connection object.
API requests without authentication will fail.

Errors
======

Medidata RAVE web services uses HTTP response codes to indicate success or failure of an API request. Generally speaking, HTTP codes in the ranges below mean the following:

- 2xx - success
- 4xx - an error that failed given the information provided
- 5xx - an error with Medidata's servers

Not all errors map cleanly onto HTTP response codes, however. Medidata usually attempts to return a "RWS Reason Code" in addition to the conventional HTTP code to explain in more detail about what went wrong. To see a full listing of these codes, refer to: `Rave Web Services Error Responses - Complete List <https://learn.mdsol.com/api/rws/rave-web-services-error-responses-complete-list-95587425.html>`_

---------------
Handling errors
---------------

The Medidata.RWS.NET library can raise an exception for a variety reasons, such as invalid parameters, authentication errors, and network unavailability. We recommend writing code that gracefully handles all possible API exceptions.