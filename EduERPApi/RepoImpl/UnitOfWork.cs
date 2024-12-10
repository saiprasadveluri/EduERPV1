using EduERPApi.AdhocData;
using EduERPApi.AdhocImpl;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace EduERPApi.RepoImpl
{
    public class UnitOfWork
    {
        EduERPDbContext _context;
        IDbContextTransaction _transaction;
        IRepo<UserInfoDTO> _userInfoRepo;
        IRepo<UserFeatureRoleDTO> _userFeatureRoleRepo;
        IRepo<OrganizationDTO> _organizationRepo;
        IRepo<MainCourseDTO> _mainCourseRepo;
        IRepo<SpecializationsDTO> _specializationsRepo;
        IRepo<CourseDetailDTO> _courseDetailRepo;
        IRepo<LoginDataDTO> _accountRepo;
        IRepo<AppUserFeatureRoleMapDTO> _appUserFeatureRoleMapRepo;
        IRepo<FeatureRoleDTO> _featureRoleRepo;
        IRepo<OrgnizationFeatureSubscriptionDTO> _orgnizationFeatureSubscriptionRepo;
        IRepo<UserOrgMapDTO> _userOrgMapRepo;
        IRepo<ModuleFeatureDTO> _moduleFeatureRepo;
        IRepo<StudentInfoDTO> _studentInfoRepo;
        IRepo<FeeHeadMasterDTO> _feeHeadMasterRepo;

        IRepo<FeeMasterDTO> _feeMasterRepo;
        IRepo<AcdYearDTO> _acdYearRepo;
        
        IRepo<StudentYearStreamMapDTO> _studentYearStreamMapRepo;
        IAdhocLogicRepo _adhocLogicRepo;
        IRepo<ChalanDTO> _chalanRepo;
        IRepo<ChalanInfoDTO> _chalanLineInfoRepo;
        IRepo<SubjectDTO> _subjectRepo;

        IRepo<StreamSubjectMapDTO> _streamSubjectMapRepo;
        IRepo<StudentLanguagesDTO> _studentLanguagesRepo;
        IRepo<SubjectTopicDTO> _subjectTopicRepo;
        IRepo<QuestionDTO> _questionRepo;
        IRepo<QuestionChoiceDTO> _questionChoiceRepo;

        IRepo<ExamTypeDTO> _examTypeRepo;

        IRepo<ExamDTO> _examRepo;


        IRepo<ExamScheduleDTO> _examScheduleRepo;

        public IRepo<ExamScheduleDTO> ExamScheduleRepo
        {
            get
            {
                if (_examScheduleRepo == null)
                {
                    _examScheduleRepo = new ExamScheduleRepoImpl(_context);
                }
                return _examScheduleRepo;
            }
        }
        public IRepo<ExamDTO> ExamRepo
        {
            get
            {
                if (_examRepo == null)
                {
                    _examRepo = new ExamRepoImpl(_context);
                }
                return _examRepo;
            }
        }

        public IRepo<ExamTypeDTO> ExamTypeRepo
        {
            get
            {
                if (_examTypeRepo == null)
                {
                    _examTypeRepo = new ExamTypeRepoImpl(_context);
                }
                return _examTypeRepo;
            }
        }

        public IRepo<QuestionChoiceDTO> QuestionChoiceRepo
        {
            get
            {
                if (_questionChoiceRepo == null)
                {
                    _questionChoiceRepo = new QuestionChoiceRepoImpl(_context);
                }
                return _questionChoiceRepo;
            }
        }
        public IRepo<QuestionDTO> QuestionRepo
        {
            get
            {
                if (_questionRepo == null)
                {
                    _questionRepo = new QuestionRepoImpl(_context);
                }
                return _questionRepo;
            }
        }

        public IRepo<SubjectTopicDTO> SubjectTopicRepo
        {
            get
            {
                if (_subjectTopicRepo == null)
                {
                    _subjectTopicRepo = new SubjectTopicRepoImpl(_context);
                }
                return _subjectTopicRepo;
            }
        }
        public IRepo<StudentLanguagesDTO> StudentLanguagesRepo
        {
            get
            {
                if (_studentLanguagesRepo == null)
                {
                    _studentLanguagesRepo = new StudentLanguageRepoImpl(_context);
                }
                return _studentLanguagesRepo;
            }
        }
        public IRepo<StreamSubjectMapDTO> StreamSubjectMapRepo
        {
            get
            {
                if (_streamSubjectMapRepo == null)
                {
                    _streamSubjectMapRepo = new StreamSubjectMapRepoImpl(_context);
                }
                return _streamSubjectMapRepo;
            }
        }

        public IRepo<SubjectDTO> SubjectRepo
        {
            get {
                if (_subjectRepo == null)
                {
                    _subjectRepo = new SubjectRepoImpl(_context);
                }
                return _subjectRepo;
            }
        }
        public IRepo<ChalanInfoDTO> ChalanLineInfoRepo
        {
            get
            {
                if (_chalanLineInfoRepo == null)
                {
                    _chalanLineInfoRepo = new ChalanInfoRepoImpl(_context);
                }
                return _chalanLineInfoRepo;
            }
        }

        public IRepo<ChalanDTO> ChalanRepo
        {
            get
            {
                if (_chalanRepo == null)
                {
                    _chalanRepo = new ChalanRepoImpl(_context);
                }
                return _chalanRepo;
            }
        }
        public IAdhocLogicRepo AdhocLogicRepo
        {
            get
            {
                if (_adhocLogicRepo == null)
                {
                    _adhocLogicRepo = new AdhocProcessImpl(_context);
                }
                return _adhocLogicRepo;
            }
        }
        public IRepo<StudentYearStreamMapDTO> StudentYearStreamMapRepo
        {
            get
            {
                if (_studentYearStreamMapRepo == null)
                {
                    _studentYearStreamMapRepo = new StudentYearStreamMapRepoImpl(_context);
                }
                return _studentYearStreamMapRepo;
            }
        }
       
        public IRepo<AcdYearDTO> AcdYearRepo
        {
            get
            {
                if (_acdYearRepo == null)
                {
                    _acdYearRepo = new AcdYearRepoImpl(_context);
                }
                return _acdYearRepo;
            }
        }
        public IRepo<FeeMasterDTO> FeeMasterRepoImpl
        {
            get
            {
                if (_feeMasterRepo == null)
                {
                    _feeMasterRepo = new FeeMasterRepoImpl(_context);
                }
                return _feeMasterRepo;
            }
        }
        public IRepo<FeeHeadMasterDTO> FeeHeadMasterRepoImpl
        {
            get
            {
                if (_feeHeadMasterRepo == null)
                {
                    _feeHeadMasterRepo = new FeeHeadMasterRepoImpl(_context);
                }
                return _feeHeadMasterRepo;
            }
        }
        public IRepo<StudentInfoDTO> StudentInfoRepo
        {
            get
            {
                if (_studentInfoRepo == null)
                {
                    _studentInfoRepo = new StudentInfoRepoImpl(_context);
                }
                return _studentInfoRepo;
            }
        }

        public IRepo<ModuleFeatureDTO> ModuleFeatureRepo
        {
            get
            {
                if (_moduleFeatureRepo == null)
                {
                    _moduleFeatureRepo = new ModuleFeatureRepoImpl(_context);
                }
                return _moduleFeatureRepo;
            }
        }
        public IRepo<UserOrgMapDTO> UserOrgMapRepoImpl
        {
            get
            {
                if (_userOrgMapRepo == null)
                {
                    _userOrgMapRepo = new UserOrgMapRepoImpl(_context);
                }
                return _userOrgMapRepo;
            }
        }

        public IRepo<OrgnizationFeatureSubscriptionDTO> OrgnizationFeatureSubscriptionRepo
        {
            get
            {
                if (_orgnizationFeatureSubscriptionRepo == null)
                {
                    _orgnizationFeatureSubscriptionRepo = new OrgnizationFeatureSubscriptionRepoImpl(_context);
                }
                return _orgnizationFeatureSubscriptionRepo;
            }
        }
        public IRepo<FeatureRoleDTO> FeatureRoleRepo
        {
            get
            {
                if (_featureRoleRepo == null)
                {
                    _featureRoleRepo = new FeatureRoleImpl(_context);
                }
                return _featureRoleRepo;
            }
        }

        public IRepo<AppUserFeatureRoleMapDTO> AppUserFeatureRoleMapRepo
        {
            get
            {
                if (_appUserFeatureRoleMapRepo == null)
                {
                    _appUserFeatureRoleMapRepo = new AppUserFeatureRoleMapImpl(_context);
                }
                return _appUserFeatureRoleMapRepo;
            }
        }
        public IRepo<LoginDataDTO> AccountRepo
        {
            get
            {
                if (_accountRepo == null)
                {
                    _accountRepo = new AccountRepoImpl(_context);
                }
                return _accountRepo;
            }
        }
        public IRepo<CourseDetailDTO> CourseDetailRepo
        {
            get
            {
                if (_courseDetailRepo == null)
                {
                    _courseDetailRepo = new CourseDetailRepoImpl(_context);
                }
                return _courseDetailRepo;
            }
        }

        public IRepo<SpecializationsDTO> CourseSpecialzationsRepo
        {
            get
            {
                if (_specializationsRepo == null)
                {
                    _specializationsRepo = new CourseSpecialzationsRepoImpl(_context);
                }
                return _specializationsRepo;
            }
        }
        public IRepo<MainCourseDTO> MainCourseRepo
        {
            get
            {
                if (_mainCourseRepo == null)
                {
                    _mainCourseRepo = new MainCourseRepoImpl(_context);
                }
                return _mainCourseRepo;
            }
        }

        public IRepo<OrganizationDTO> OrganizationRepo
        {
            get
            {
                if (_organizationRepo == null)
                {
                    _organizationRepo = new OrgRepoImpl(_context);
                }
                return _organizationRepo;
            }
        }

        public IRepo<UserInfoDTO> UserInfoRepo { 
            get
            {
                if(_userInfoRepo == null)
                {
                    _userInfoRepo = new UserInfoRepoImpl(_context);
                }
                return _userInfoRepo;
            }
        }

        public IRepo<UserFeatureRoleDTO> UserFeatureRoleRepo
        {
            get
            {
                if (_userFeatureRoleRepo == null)
                {
                    _userFeatureRoleRepo = new UserFeatureRoleIImpl(_context);
                }
                return _userFeatureRoleRepo;
            }
        }
        public UnitOfWork(EduERPDbContext context)
        {
            _context = context;
        }

        public bool SaveAction()
        {
            _context.SaveChanges();
            return true;
        }

        public void BeginTransaction()
        {
            _transaction= _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if(_transaction!=null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _transaction;
        }
    }
}
