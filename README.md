# Alx.FacebookLogin
---
[![License](http://img.shields.io/:license-MIT-blue.svg)](https://licenses.nuget.org/MIT) [![NuGet Badge](https://buildstats.info/nuget/Alx.FacebookLogin)](https://www.nuget.org/packages/Alx.FacebookLogin/) 

Easy to use ASP.NET Core middleware that allows more control over the Facebook login compared to the built-in implementation.

## Installation
```powershell
Install-Package Alx.FacebookLogin
```
## Usage
1. Add the following snippet on the root level of your appsettings.json and replace the placeholders with your Facebook app settings:
```json
"Authentication": {
    "Facebook": {
      "AppId": "{YOUR-APP-ID}",
      "AppSecret": "{YOUR-APP-SECRET}",
      "RedirectUri": "{YOUR-REDIRECT-URI}"
    }
  }
```
2. In Startup.cs, include a reference to the package:
```cs
using Alx.FacebookLogin;
```
3. In the ConfigureServices method, add the line:
```cs
services.AddFacebookLogin();
```
4. In the Configure method, add:
```cs
app.UseFacebookLogin();
```
5. Create a controller SessionController.cs and inject FacebookLoginService in the constructor:
```cs
using Alx.FacebookLogin;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly FacebookLoginService fbService;

        public SessionController(FacebookLoginService fbService)
        {
            this.fbService = fbService;
        }

        [HttpGet("facebook")]
        public IActionResult LoginWithFacebook()
        {
            var userData = fbService.UserData; // contains user ID, name, email (if provided) and picture url
            //Do stuff with the userData object
            return Ok();
        }
    }
}
```
Once the backend is set up, all you need to do on the frontend is to call this endpoint with the code provided by Facebook. To do that:
1. Replace the placeholders in the following URL with your Facebook app settings and make a request
```
https://www.facebook.com/v6.0/dialog/oauth?client_id=${YOUR-APP-ID}&scope=email&redirect_uri=${YOUR-REDIRECT-URI}
```
2. If the user has granted access, you will recieve a code in the provided redirect URI. Make a GET request to the controller with a query parameter 'code', like ```{BACKEND-URL}/api/session/facebook?code={FACEBOOK-CODE}```

## Configuration
Coming shortly...

## Examples
Coming shortly...
