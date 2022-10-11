using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helper;
using System.IO;
using Service.DTO;
using FoodyAPI.Helper;
using Service.Services.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [AllowAnonymous, Route("account")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserService _service;
        public LoginController(IUserService service)
        {
            _service = service;
        }
        [Route("google-login")]
        [HttpPost]
        public async Task<ActionResult> TokenLogin([FromBody]string tokenId)
        {
            //var tokenId = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijk1NTEwNGEzN2ZhOTAzZWQ4MGM1NzE0NWVjOWU4M2VkYjI5YjBjNDUiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXpwIjoiMjE5MTI5OTI3MjU5LXYxcHUzZG43a3EyMGZrYnBvOTUxa2hpbDY5cGhsMWNyLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiYXVkIjoiMjE5MTI5OTI3MjU5LXYxcHUzZG43a3EyMGZrYnBvOTUxa2hpbDY5cGhsMWNyLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwic3ViIjoiMTAwMjQ0NDgxNTAwNjYxNzc3OTM4IiwiaGQiOiJmcHQuZWR1LnZuIiwiZW1haWwiOiJ0cnVuZ250c2UxNTExMzRAZnB0LmVkdS52biIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJhdF9oYXNoIjoiV3k5SGdNTExrdkRZMnNLVUZJTlNKdyIsIm5hbWUiOiJOZ3V5ZW4gVGhhbmggVHJ1bmcgKEsxNSBIQ00pIiwicGljdHVyZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS9hL0FMbTV3dTJncTlIRGg1Vy1pT3EzSk4tWE1FU2pkN1VCaFFlWHlILW5pVktiPXM5Ni1jIiwiZ2l2ZW5fbmFtZSI6Ik5ndXllbiBUaGFuaCBUcnVuZyIsImZhbWlseV9uYW1lIjoiKEsxNSBIQ00pIiwibG9jYWxlIjoiZW4tR0IiLCJpYXQiOjE2NjUzNjY1MjMsImV4cCI6MTY2NTM3MDEyMywianRpIjoiNTZlMDdiYmVkZGQ2OGJmN2Y0YTUzMDBmYWQxNzc4M2Y4YzhiNTg5YiJ9.iAGedPUuDTMqZD9Z_CFbHMSNODdvO6EVvOf6ceZkV36A8SKTOrW3S9obFIfjCbO54Xn1JHtlPOOCcbJtNFBcywR5PQ-H6BXQIDJNnlTckpQHiaGeQXtwi400d8T7WZYTZboLWLeuk8sddFjQoh9sd9glb5Yv5-CerBy5m9R6fyFp2UgzboL5Qiw2OYwuIjWRCjMp-i5KFtXvZhg1UTOtEW3LUbd-EDw0R6URRIhH98sWQETT9AWEnk0t3g2IaNhfwj8vQQB_TgtvhZLLXkTZHIK-WWZ3o1FeSmrGwOSCcyfi7hdYwYbdKsm46xcFArRfUvld1__VcY9dfMIVM5mJTA";//Request["idToken"];
            var settings = new GoogleJsonWebSignature.ValidationSettings() { Audience = new List<string>() { Constants.GOOGLE_CLIENT_ID } };

            User user = new Service.DTO.User();
            try
            {
                //retrieve data
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(tokenId, settings);
                user.Email = validPayload.Email;
                user.Name = validPayload.Name;
                user.Image = validPayload.Picture;
                //login
                user = _service.LoginAsync(user).Result;
                //set user to session
                if(user != null)
                {
                    var session = HttpContext.Session;
                    SessionExtension.Set(session, "login-user", user);
                } else
                {
                    return Ok(StatusCodes.Status401Unauthorized);
                }
                
            }
            catch
            {
                return Ok(StatusCodes.Status403Forbidden);
            }

            // Obviously return something useful, not null.
            return Ok(user);
        }
    }
    
}
