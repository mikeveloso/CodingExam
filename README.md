# Coding Exam - IP/Domain Workers

An API that takes an IP/Domain as parameters and gather information from multiple sources returning a single result.
This API farms from individual portions of the lookup to a various API workers who performs the action and then combine
The results and return as a single payload.

API Report Sources
---
- ViewDNS.info
- VirusTotal.com
- whoisxmlapi.com
- ipstack.com

Prerequisites
---
- .Net Core 2.1.1
- .Net Framework 4.5 or later
- Asp.Net Web API
- Visual Studio 2019
- XUnitTest

NUGet Packages
---
- NLog.Extensions.Logging 1.6.1
- Swashbuckle.AspNetCore.Swagger 5.1.0
- Swashbuckle.AspNetCore.SwaggerGen 5.1.0
- Swashbuckle.AspNetCore.SwaggerUI
- RestSharp 106.10.1
- VirusTotalNet 2.0.0
- Moq 4.13.1
- coverlet.collector 1.0.10

Download and Running the solution
---
- clone repo
- Open the "CodingExam"
- In the "Solution Explorer", right click the solution file and select "Properties"
- In the "Pproperties Window", under "Startup Project" click the "Multiple startup projects"
- In the "Action" dropdown, Make sure to select from the "Start" for the following projects

  - CodingExamApi
  - CodingExamAbuseContact
  - CodingExamDnsLookup
  - CodingExamDnsPropagation
  - CodingExamDomainAvailability
  - CodingExamGeoLocation
  - CodingExamIpHistoryCheck
  - CodingExamPing
  - CodingExamPortScan
  - CodingExamRdap
  - CodingExamReverseIp
  - CodingExamVirusTotal
  - CodingReverseDNS
  
- Then click the "OK" button to save the changes.
- Press CTRL+F5 or control F5 to Run the "CodingExamApi" and all the API Workers

Unit Testing
---

Unit Test is not properly setup for properly testing due to time constraints.
But the basic idea is implemented.
  

