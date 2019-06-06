# idp_auth_pkce
identity server test with spa and Authorization Code type and PKCE

## Tasks
  1. security : https://securityheaders.com/

### Idp features
  * ~~Idp.1: change login inputs  
    ~~- now: username & password => correct: email & password (size user name might be duplicated)  
  * Idp.2: enable 3rd-party login (Google, Facebook)   
    - make sure if a user logins first time through 3rd party credential, store it in my idp database  
  * Idp.3: customize login view  
    - add register page link (if a user does not have account for my idp)
### Resource Server features
  * RS.1: implement it
