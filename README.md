# SRequest
a private library I composed to make my life of making requests easier.

# How to use
```
using SRequest;

List<HTTPRequestHeader> headers = new List<HTTPRequestHeader>();
headers.Add(
  new HTTPRequestHeader()
  {
    Header = "HEADER NAME",
    Value = "HEADER VALUE"
  });
  
List<HTTPRequestCookie> cookies = new List<HTTPRequestCookie>();
cookies.Add(
  new HTTPRequestCookie()
  {
    Cookie = "COOKIE NAME",
    Value = "COOKIE VALUE"
  });
  
List<HTTPRequestParams> parameters = new List<HTTPRequestParams>();
parameters.Add(
  new HTTPRequestParams()
  {
    Param = "username",
    Value = username
  });
  
string value = new HTTPRequest()
                    .MakeRequest(
                        URL,
                        HOST,
                        USER AGENT,
                        CONTENT TYPE,
                        ACCEPT,
                        REFERER,
                        proxy, //Optional
                        parameters,
                        cookies,
                        headers,
                        true //for POST
                    );
```
