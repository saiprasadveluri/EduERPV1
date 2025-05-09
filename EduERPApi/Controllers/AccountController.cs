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
using EduERPApi.BusinessLayer;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        //int Rfc2898KeygenIterations = 100;
        //int AesKeySizeInBits = 128;

        //UnitOfWork _unitOfWork;
        IConfiguration _cfg;
        private Business _businessObj;
        public AccountController(Business businessObj, IConfiguration cfg)
        {
            //_unitOfWork = unitOfWork;
            _businessObj = businessObj;
            _cfg = cfg;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateUser(LoginDataDTO data)
        {
            (LoginResultDTO Res, bool Success) = _businessObj.ValidateUser(data);
            if (Success)
            {
                return Ok(new { Status = 1, Data = Res });
            }
            else
            {
                return BadRequest(new { Status = 0, Data = 401, Message = "Error In Login" });
            }
        }

        [Authorize]
        [HttpPost("token")]
        public async Task<IActionResult> GetAccessToken(TokenRequestDTO dto)
        {
            try
            {
                string token=_businessObj.GetAccessToken(dto);
                    return Ok(new { Status = 1, Data = token });                
               
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 408, Message = "Error In token generation" });
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

        
    }
}
