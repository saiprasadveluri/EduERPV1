using Azure.Core;
using DocumentFormat.OpenXml.Spreadsheet;
using EduERPApi.Data;
using EduERPApi.DTO;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public (LoginResultDTO, bool) ValidateUser(LoginDataDTO data)
        {
            (LoginResultDTO dto, bool Success) Res = _unitOfWork.AccountRepo.GetLoginResult(data);
            if(!String.IsNullOrEmpty(Res.dto.UserDetailsJson))
            {
               SysAdminData sysData= JsonSerializer.Deserialize<SysAdminData>(Res.dto.UserDetailsJson);
                if(sysData.IsSysAdmin==1)
                {
                    LoginResultDTO loginResultDto = new LoginResultDTO()
                    {
                        JwtToken = GetSysAdminAccessToken(Res.dto.UserId),
                        UserId = Res.dto.UserId,
                        IsSysAdmin=true
                    };
                    return (loginResultDto, true);
                }
            }
            //Process Non Sys Admins
            GenerateLoginToken(Res.dto);
            return Res;
        }

        public string GetAccessToken(TokenRequestDTO dto)
        {
            string SavedUserId = ReadUserIDFromJWTToken();
            Guid orgMapId=_unitOfWork.UserOrgMapRepoImpl.GetSelUserOrgMapIdObj(new UserOrgInfoDTO()
            {
                OrgId = dto.SelOrgId,
                UserId = Guid.Parse(SavedUserId)
            });
            var UserOrgMapObj=_unitOfWork.UserOrgMapRepoImpl.GetById(orgMapId);
            
            return GenerateAccessToken(dto.SelOrgId.ToString(), SavedUserId, orgMapId, UserOrgMapObj.IsOrgAdmin);
        }

        public string GetSysAdminAccessToken(Guid UserId)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, UserId.ToString()));
            claims.Add(new Claim("IsSysAdmin", "1"));
            claims.Add(new Claim("UserId", UserId.ToString()));
           return PrepareToken(claims);
        }


        private string GenerateAccessToken(string OrgId, string UserID, Guid UserOrgMapId,int IsOrgAdmin)
        {
            List<AppUserFeatureRoleMapDTO> RoleList=null;
            if (IsOrgAdmin != 1)
            {
                RoleList = GetUserRoles(UserOrgMapId);
                if (RoleList.Count == 0)
                {
                    throw new Exception("Access Roles Empty");
                }
            }
            
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, UserOrgMapId.ToString()));
            if(RoleList!=null)
                claims.Add(new Claim("FeatureRoleData", JsonSerializer.Serialize(RoleList)));
            if (IsOrgAdmin == 1)
                claims.Add(new Claim("IsOrgAdmin", "1"));
            claims.Add(new Claim("UserId", UserID));
            claims.Add(new Claim("OrgId", OrgId));
            
            return PrepareToken(claims);
        }

        private string PrepareToken(List<Claim> claims)
        {
            string secret = _cfg.GetSection("JwtConfig:SigningKey").Value;
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
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
            claims.Add(new Claim(ClaimTypes.Role, "LoggedInUser"));
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

        private string ReadUserIDFromJWTToken()
        {
            StringValues AuthString;
            bool AuthStringPresent = _context.Context.Request.Headers.TryGetValue("Authorization", out AuthString);

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
