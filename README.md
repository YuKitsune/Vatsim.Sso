# Vatsim.Sso
A .NET Standard implementation of VATSIM SSO (Single Sign On).

![](https://img.shields.io/nuget/dt/Vatsim.Sso.svg?style=flat)

## Example Usage

```cs
public ActionResult Login()
{
    // Set the base url (Use "http://sso.hardern.net/server/api/" for testing and development)
    Sso.SetBaseUrl("https://cert.vatsim.net/sso/api/");
    
    // Get the token
    TokenResponse res = Vatsim.Sso.GetRequestToken("myConsumerKey", "myConsumerSecret", this.Url.Action("LoginReturnAsync", "Account", null, Uri.UriSchemeHttps));
    
    // Store the token secret in a session
    HttpContext.Session.SetString("TokenSecret", res.Token.TokenSecret);
    
    // Redirect to the VATSIM SSO login page with our token
    return this.Redirect($"https://cert.vatsim.net/sso/auth/pre_login/?oauth_token={res.Token.Token}");
}

public async Task<ActionResult> LoginReturnAsync(string oauth_token, string oauth_verifier)
{
    if (string.IsNullOrEmpty(oauth_token)) throw new ArgumentNullException(nameof(oauth_token));
    if (string.IsNullOrEmpty(oauth_verifier)) throw new ArgumentNullException(nameof(oauth_verifier));

    // Get our token secret back
    string secret = HttpContext.Session.GetString("TokenSecret");
    
    // Get the user details
	Vatsim.Sso.SetBaseUrl("https://cert.vatsim.net/sso/api/");
	UserResponse res = Vatsim.Sso.GetUserData("myConsumerKey", "myConsumerSecret", "https://cert.vatsim.net/sso/api/", oauth_verifier, oauth_token, secret);

	// Todo: Process the user accordingly
}
```
