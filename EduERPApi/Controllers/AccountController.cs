using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        int Rfc2898KeygenIterations = 100;
        int AesKeySizeInBits = 128;

        UnitOfWork _unitOfWork;
        IConfiguration _cfg;
        public AccountController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
            _cfg = cfg;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateUser(LoginDataDTO data)
        {
            var RepoObj = _unitOfWork.AccountRepo as IRawRepo<LoginDataDTO, LoginResultDTO>;
            if (RepoObj != null)
            {
                LoginResultDTO Res = RepoObj.ExecuteRaw(data);
                //Encrypt the UserID
                if (Res != null)
                {
                    //Res.HeaderUserId = Res.UserId.ToString();//EncryptPlainText(Res.UserId.ToString());
                    GenerateLoginToken(Res);
                    return Ok(new { Status = 1, Data = Res });
                }
                else
                {
                    return BadRequest(new { Status = 0, Data = 401, Message = "Error In Login" });
                }
            }
            else
            {
                return BadRequest(new { Status = 0, Data = 402, Message = "Error In Login" });
            }

        }

        [Authorize]
        [HttpPost("token")]
        public async Task<IActionResult> GetAccessToken(TokenRequestDTO dto)
        {
            try
            {
                string SavedUserId=ReadUserIDFromJWTToken();
                UserOrgInfoDTO userOrgInfoDto = new UserOrgInfoDTO()
                {
                    UserId = Guid.Parse(SavedUserId),
                    OrgId = dto.SelOrgId,
                };
                Guid ExtractedUserOrgMapId=((IRawRepo<UserOrgInfoDTO, Guid>)_unitOfWork.UserOrgMapRepoImpl).ExecuteRaw(userOrgInfoDto);
                if(ExtractedUserOrgMapId == Guid.Empty)
                {
                    return BadRequest(new { Status = 0, Data = 408, Message = "Error In input data" });
                }
                string token = GenerateAccessToken(dto.SelOrgId.ToString(), SavedUserId,ExtractedUserOrgMapId, dto);
                if(token != null) {
                    return Ok(new { Status = 1, Data = token });
                }
                else
                {
                    return BadRequest(new { Status = 0, Data = 408, Message = "Error In token generation" });
                }
                
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 408, Message = "Error In token generation" });
        }

        private string GenerateAccessToken(string OrgId,string UserID,Guid UserOrgMapId,TokenRequestDTO inp)
        {
            List<AppUserFeatureRoleMapDTO> RoleList = GetUserRoles(UserOrgMapId);
            string secret = _cfg.GetSection("JwtConfig:SigningKey").Value;
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, UserOrgMapId.ToString()));
            claims.Add(new Claim("FeatureRoleData", JsonSerializer.Serialize(RoleList)));
            claims.Add(new Claim("UserId", UserID));
            claims.Add(new Claim("OrgId", OrgId));
            
            foreach (var role in RoleList)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.FeatureRoleId.ToString()));
            }
            var token = new JwtSecurityToken(issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void GenerateLoginToken(LoginResultDTO inp)
        {
            string secret = _cfg.GetSection("JwtConfig:SigningKey").Value;
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            //claims.Add(new Claim("HeaderUserId", inp.HeaderUserId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, inp.UserEmail));
            claims.Add(new Claim("UserId", inp.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role,"LoggedInUser"));            
            var token = new JwtSecurityToken(issuer: _cfg["JwtConfig:Issuer"],
                audience: _cfg["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            inp.JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        }
       

        private List<AppUserFeatureRoleMapDTO> GetUserRoles(Guid UserId)
        {
            return _unitOfWork.AppUserFeatureRoleMapRepo.GetByParentId(UserId).ToList();
        }

        /*private string EncryptPlainText(string plainText)
        {
            try
            {
                string Secret = _cfg["HeaderInfoConfig:Secret"];
                string Salt = _cfg["HeaderInfoConfig:Salt"];
                byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes(plainText);
                byte[] cipherText = null;
                byte[] PasswordBytes = System.Text.Encoding.Unicode.GetBytes(Secret);
                byte[] SaltBytes = System.Text.Encoding.Unicode.GetBytes(Salt);

                using (Aes aes = new AesManaged())
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = AesKeySizeInBits;
                    int KeyStrengthInBytes = aes.KeySize / 8;
                    System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                    new System.Security.Cryptography.Rfc2898DeriveBytes(PasswordBytes, SaltBytes, Rfc2898KeygenIterations);
                    aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                    aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                        }
                        cipherText = ms.ToArray();
                        return Convert.ToBase64String(cipherText);

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            }

        private string Decrypt(string EncodedcipherText)
        {
            try
            {
                string Secret = _cfg["HeaderInfoConfig:Secret"];
                string Salt = _cfg["HeaderInfoConfig:Salt"];
                byte[] cipherText = Convert.FromBase64String(EncodedcipherText);


                byte[] PasswordBytes = System.Text.Encoding.Unicode.GetBytes(Secret);
                byte[] SaltBytes = System.Text.Encoding.Unicode.GetBytes(Salt);

                using (Aes aes = new AesManaged())
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = AesKeySizeInBits;
                    int KeyStrengthInBytes = aes.KeySize / 8;
                    System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                    new System.Security.Cryptography.Rfc2898DeriveBytes(PasswordBytes, SaltBytes, Rfc2898KeygenIterations);
                    aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                    aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherText, 0, cipherText.Length);
                        }
                        byte[] plainTextBytes = ms.ToArray();
                        string UserId=System.Text.Encoding.Unicode.GetString(plainTextBytes);
                        return UserId;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }*/

        private string ReadUserIDFromJWTToken()
        { 
            StringValues AuthString;
            bool AuthStringPresent = Request.Headers.TryGetValue("Authorization", out AuthString);
            if (!AuthStringPresent)
            {
                return null;
            }
            string[] ValueArray = AuthString.ToArray();
            if (ValueArray.Length == 0)
            {
                return null;
            }
            string HeaderValue = ValueArray[0];
            string[] AuthHeaderValues = HeaderValue.Split(' ');
            var stream = AuthHeaderValues[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
            string UserId = jti;//Decrypt(jti);
            return UserId;
        }
    }
}
