using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using EduERPApi.Infra;

namespace EduERPApi.RepoImpl
{
    public class StudentInfoRepoImpl:IRepo<StudentInfoDTO>
    {
        EduERPDbContext _context;
        public StudentInfoRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(StudentInfoDTO item)
        {
            //Get FeatureRoleID
            var OrgObj=_context.Organizations.FirstOrDefault(obj=>obj.OrgId == item.OrgId);
            if (OrgObj != null)
            {
                Guid ModuleId = OrgObj.OrgModuleType;
                var ModuleFeatureObj = _context.ModuleFeatures.FirstOrDefault(p => p.ModuleId == ModuleId && p.FeatureName== RoleConstents.STUDENT_MANAGEMENT_FEATURE);
                if (ModuleFeatureObj != null)
                {
                    Guid FeatureId = ModuleFeatureObj.FeatureId;
                    var FeatureRoleObj = _context.FeatureRoles.FirstOrDefault(p => p.FeatureId == FeatureId && p.RoleName == RoleConstents.ROLE_STUDENT);
                    if (FeatureRoleObj != null)
                    {
                        UserInfo userInfo = new UserInfo()
                        {
                            UserId = Guid.NewGuid(),
                            DisplayName = item.Name,
                            UserEmail = item.Email,
                            Password = item.Password,
                            Status = item.Status
                        };
                        _context.UserInfos.Add(userInfo);

                        StudentInfo studentInfo = new StudentInfo()
                        {
                            OrgId = item.OrgId.Value,
                            Name = item.Name,
                            Address = item.Address,
                            DateOfBirth = item.DateOfBirth,
                            DateOfJoining = item.DateOfJoining,
                            RegdNumber = item.RegdNumber,
                            UserId = userInfo.UserId,
                            StudentId = Guid.NewGuid()
                        };
                        _context.StudentInfos.Add(studentInfo);

                        StudentYearStreamMap studentYearStreamMap = new StudentYearStreamMap()
                        {
                            StudentYearStreamMapId= Guid.NewGuid(),
                            AcdYearId = item.AcdYearId,
                            CourseStreamId = item.StreamId,
                            StudentId = studentInfo.StudentId
                        };
                        _context.StudentYearStreamMaps.Add(studentYearStreamMap);
                        
                        //Update student Language Data....
                        UpdateLanguages(item, studentYearStreamMap.StudentYearStreamMapId);

                        UserOrgMap mapObj = new UserOrgMap()
                        {
                            OrgId = item.OrgId.Value,
                            UserId = userInfo.UserId,
                            UserOrgMapId = Guid.NewGuid()
                        };
                        _context.UserOrgMaps.Add(mapObj);

                        _context.AppUserFeatureRoleMaps.Add(new AppUserFeatureRoleMap()
                        {
                            AppUserRoleMapId= Guid.NewGuid(),
                            FeatureRoleId = FeatureRoleObj.AppRoleId,
                            UserOrgMapId = mapObj.UserOrgMapId
                        });
                        
                        return studentInfo.StudentId;
                    }
                }
            }
            return Guid.Empty;
        }


        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public StudentInfoDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, StudentInfoDTO item)
        {
            throw new NotImplementedException();
        }

        private void UpdateLanguages(StudentInfoDTO dto,Guid StuMapID)
        {
            if (dto.parsedLangData != null)
            {
                dto.LangData = new List<StudentLanguagesDTO>();

             var temp=from ssmObj in _context.StreamSubjectMaps 
                      join subObj in _context.Subjects on ssmObj.SubjectId equals subObj.SubjectId
                      select new {ssmObj.StreamSubjectMapId,subObj.SubjCode,ssmObj.StreamId};

                foreach(var langData in dto.parsedLangData)
                {
                    var MappedSubId = (from obj in temp
                                       where obj.SubjCode.ToUpper() == langData.Lang.ToUpper() && obj.StreamId == dto.StreamId
                                       select obj.StreamSubjectMapId).FirstOrDefault();
                    if(MappedSubId!=default(Guid))
                    {
                        /*StudentLanguagesDTO studentLanguagesDTO = new StudentLanguagesDTO()
                        {
                            LangNumber = langData.LangNumber,
                            StudentYearStreamMapId = StuMapID,
                            SubjectMapId = MappedSubId,
                            StudentLangId = Guid.NewGuid()
                        };*/
                        StudentLanguage studentLanguage = new()
                        {
                            LangNumber = langData.LangNumber,
                            StudentYearStreamMapId = StuMapID,
                            SubjectMapId = MappedSubId,
                            StudentLangId = Guid.NewGuid()
                        };
                        _context.StudentLanguages.Add(studentLanguage);
                        
                        //dto.LangData.Add(studentLanguagesDTO);
                    }
                    else
                    {
                        throw new Exception("Invalid Language Code error");
                    }
                }
            }
        }
    }
}
