# idp_auth_pkce
identity server test with spa and Authorization Code type and PKCE

## Tasks
  1. security : https://securityheaders.com/

### Idp features
  * Idp.1: ~~change login inputs~~<br>
    - now: username & password => correct: email & password (size user name might be duplicated)<br>
  * Idp.2: ~~enable 3rd-party login (Google, Facebook)~~<br>
    - make sure if a user logins first time through 3rd party credential, store it in my idp database<br> 
  * Idp.3: customize login view<br>
    - add register page link (if a user does not have account for my idp)<br>
  * Idp.4: is it possible without consent page?<br>
    - currently clients and users are internal so don't need consent at all.
### Resource Server features
  * RS.1: implement it
