# idp_auth_pkce
identity server test with spa and Authorization Code type and PKCE

## Tasks
  * **1. security**: 
    - **HSTS** (if possible. might too much??): https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.2&tabs=visual-studio#http-strict-transport-security-protocol-hsts 
    - **Content Security Policy**: https://securityheaders.com/
    - **XSS policy and SQL Injection policy**: make sure all input are sanitize and enforce validation (html, js, backend, data stored in db)
      - use parameterized query (like @id, @keyword) or query builder method (like Where, OrderBy ...) of Entity Framework: don't use raw query!!!
    - **XSRF policy**: use token for every post request
    - **OWASP**: use free version to make sure there is no any security hole.

### Idp features
  * **Idp.1**: ~~change login inputs~~<br>
    - now: username & password => correct: email & password (size user name might be duplicated)<br>
  * **Idp.2**: ~~enable 3rd-party login (Google, Facebook)~~<br>
    - make sure if a user logins first time through 3rd party credential, ~~store it in my idp database~~<br>
    - storing user credential in my idp does violate OAuth protocol, the nitty gritty point is that user credential is not exposed to outside external idp (not to local idp and client). therefore, you just need to store oidc (subjectid) of user to your local idp. is that correct?
  * **Idp.3**: ~~customize login view~~<br>
    - add register page link (if a user does not have account for my idp)<br>
  * **Idp.4**: ~~is it possible without consent page?~~<br>
    - currently clients and users are internal so don't need consent at all.
### Resource Server (CatalogApi) features
    - don't apply DDD since this api only does query stuff.
  * **1**: implement Infrastructure Layer.
    - ~~DataEntity~~
    - DbContext
      - ~~entities~~
      - ~~relationships~~
      - index (later based on query)
    - ~~Specification (per filter and search item)~~
    - ~~SpecificationBuilder (class which create and put each specification together)~~
    - ~~QueryBuilder (class which build query based on query string using specification builder)~~
    - ~~Repository~~
  * **2**: implement Application Layer (coordinate Specification and Repository)
    - decouple DataEntity (infrastructure) and DTO (UI); only return DTO (this ends up json)
    - implement service per request/endpoint
  * **3**: implement UI Layer 
    - implement Controller (maybe using Swagger): should i use HETEOS??
  * **4**: implement DI (Autfac)
    - caveat: read docs to make sure how Autofac and ASP.NET Core work together
    - create module per endpoint or controller to organize registration of components and its service
  * **5**: Unit Test
    - only test public method (not private method. this is covered by when testing the public method which uses the private method)
    - ~~i still dont appreciate why Unit Test is important~~
      - but docs say following:
        - can reduce the number of integration test by unit test (how??)
          - i think this is because if you do without any unit test, you have to do integration test and fixes the errors and repeat this process over and over again. let's say, if you need to do integration test five times to implement a feature, and it takes 5sec per testing. so the total time comes to 25sec. on the other hands, if you implement implement unit test to implement the feature, you can use the unit test to fix the erros. and let's say you had to do unit testing 5 times and plus integration test only once. and unit testing takes 1sec. the total comes to 5 + 5 = 10sec. you saved your time and also you can get all benefits come from unit testing (like below) and also drawbacks such as cost for preparing unit test and test data. 
        - avoid regression error (i see this point)
        - facilitate design following SOLID principle (i also see this point)
  * **6**: Integration Test
    - implement per endpoint
      - use Bogus to create test data
      - WebApplicationFactory to create TestServer and Client (request)
      - validate the resonse 
# Error handlings and tips
  * **NullReferenceException**: reference variable is not initiated (not primitive)
      - enforce null check
  * **IQueryable without Where method** : memory burst?? this is because all data in db moves into memory if data is huge, memory got bloated and throws exception.
      - use pagination or Where clause to limit how many data are moved into memory
