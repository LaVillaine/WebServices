# WebServices

_Reference Links_
- https://www.hanselman.com/blog/AzureDevOpsContinuousBuildDeployTestWithASPNETCore22PreviewInOneHour.aspx
- https://forums.xamarin.com/discussion/20800/proper-way-to-use-xamarin-forms-with-restfull-web-services-and-backend-data
- https://www.guru99.com/restful-web-services.html
- https://medium.com/@maheshi.gunarathne1994/building-a-restful-api-with-asp-net-web-api-and-sql-server-ce7873d5b331
- https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/create-an-ajax-wcf-asp-net-client
- https://www.codeproject.com/questions/1032296/get-data-from-wcf-service-in-json-format-and-bind
- https://www.c-sharpcorner.com/UploadFile/736ca4/display-wcf-data-service-into-html-page/

#

_Raw PUT Format_
- PUT /BookService.svc/Catalogue/1 HTTP/1.1
- User-Agent: Fiddler
- Host: localhost:55105
- Content-Type: application/json
- Content-Length: 1024
- 
- {"Title":"Robinson Crusoe","Author": "Daniel Defoe","Publisher": null}

#

_Raw POST Format_
- POST /BookService.svc/Catalogue HTTP/1.1
- User-Agent: Fiddler
- Host: localhost:55105
- Content-Type: application/json
- Content-Length: 1024
- 
- {"Title": "Great Expectations","Author": null,"Publisher": null}

#

_Raw GET Format_
- GET /BookService.svc/Catalogue HTTP/1.1
- User-Agent: Fiddler
- Host: localhost:55105
- Content-Type: application/json

#

_Raw DELETE Format_
- DELETE /BookService.svc/Catalogue/0 HTTP/1.1
- User-Agent: Fiddler
- Host: localhost:55105
- Content-Type: application/json
- Content-Length: 1024
