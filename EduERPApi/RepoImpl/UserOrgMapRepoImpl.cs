﻿using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace EduERPApi.RepoImpl
{

    public class UserOrgMapRepoImpl : IRepo<UserOrgMapDTO>,IRawRepo<UserOrgInfoDTO, Guid>
    {
        EduERPDbContext _context;
        public UserOrgMapRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(UserOrgMapDTO item)
        {
            UserOrgMap userOrgMapObj = new UserOrgMap()
            {
                UserOrgMapId = Guid.NewGuid(),
                UserId = item.UserId,
                OrgId = item.OrgId
            };
            _context.UserOrgMaps.Add(userOrgMapObj);
            return userOrgMapObj.UserOrgMapId;
        }

        public bool Delete(Guid key)
        {
            var CurMap = _context.UserOrgMaps.Where(map => map.UserOrgMapId == key).FirstOrDefault();
            if(CurMap != null)
            {
                _context.UserOrgMaps.Remove(CurMap);
                return true;
            }
            return false;
        }

        public UserOrgMapDTO GetById(Guid id)
        {
            return _context.UserOrgMaps.Where(map => map.UserOrgMapId == id)
                .Select(obj => new UserOrgMapDTO()
                {
                     UserOrgMapId = id,
                     OrgId=obj.OrgId,
                     UserId=obj.UserId
                }).FirstOrDefault();

        }

        public bool Update(Guid key, UserOrgMapDTO item)
        {
            var CurMap = _context.UserOrgMaps.Where(map => map.UserOrgMapId == key).FirstOrDefault();
            if(CurMap!=null)
            {
                CurMap.OrgId = item.OrgId;
                CurMap.UserId = item.UserId;
                return true;
            }
            return false ;
        }

        public Guid ExecuteRaw(UserOrgInfoDTO inp)
        {
            var SelUserOrgMapIdObj = _context.UserOrgMaps.FromSql($"Select * from UserOrgMaps where UserId={inp.UserId} AND OrgId={inp.OrgId}").FirstOrDefault();
            if(SelUserOrgMapIdObj != null)
            {
                return SelUserOrgMapIdObj.UserOrgMapId;
            }
            else
                return Guid.Empty;
        }
    }
}
