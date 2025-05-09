using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;


namespace EduERPApi.Data
{
    public class EduERPDbContext:DbContext
    {
        public DbSet<ApplicationModule> ApplicationModules { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ModuleFeature> ModuleFeatures { get; set; }
        public DbSet<OrgnizationFeatureSubscription> OrgnizationFeatureSubscriptions { get; set; }
        public DbSet<FeatureRole> FeatureRoles { get; set; }
        public DbSet<MainCourse> MainCourses { get; set; }
        public DbSet<CourseSpecialization> CourseSpecializations { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<AppUserFeatureRoleMap> AppUserFeatureRoleMaps { get; set; }
        public DbSet<UserOrgMap> UserOrgMaps { get; set; }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StreamSubjectMap> StreamSubjectMaps { get; set; }

        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<AcdYear> AcdYears { get; set; }
        public DbSet<StudentYearStreamMap> StudentYearStreamMaps { get; set; }
        public DbSet<FeeHeadMaster> FeeHeadMasters { get; set; }
        public DbSet<FeeMaster> FeeMasters { get; set; }
        public DbSet<Chalan> Chalans { get; set; }
        public DbSet<ChalanLineInfo> ChalanLineInfos { get; set; }
        public DbSet<FeeConcession> FeeConcessions { get; set; }

        public DbSet<FeeCollection> FeeCollections { get; set; }
        public DbSet<FeeCollectionLineItem> FeeCollectionLineItems { get; set; }
        public DbSet<StudentLanguage> StudentLanguages { get; set; }

        public DbSet<SubjectTopic> SubjectTopics { get; set; }

        public DbSet<Question> Questions { get; set; }
        
        public DbSet<QuestionChoice> QuestionChoices { get; set; }

        public DbSet<ExamType> ExamTypes { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<Exam> Exams { get; set; }

        public DbSet<StudentExamScheduleMap> StudentExamScheduleMaps { get; set; }
        public EduERPDbContext(DbContextOptions opts) :base(opts)
        {
           
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence("EduERPSequence",(act)=>
            {
                act.IncrementsBy(1);
                act.StartsAt(1);
            });

            modelBuilder.Entity<ApplicationModule>().HasIndex(p => p.ModuleName).IsUnique();

            modelBuilder.Entity<ModuleFeature>().HasIndex(p => new { p.ModuleId, p.FeatureName }).IsUnique();

            modelBuilder.Entity<ModuleFeature>().HasOne<ApplicationModule>(f => f.ParentModule)
                                                .WithMany(m => m.ModuleFeatures)
                                                .HasForeignKey(f => f.ModuleId)
                                                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Organization>().HasIndex(p => p.OrgName).IsUnique();
            modelBuilder.Entity<ModuleFeature>().HasOne<ApplicationModule>(f => f.ParentModule)
                                                .WithMany(m => m.ModuleFeatures)
                                                .HasForeignKey(f => f.ModuleId)
                                                .OnDelete(DeleteBehavior.NoAction);

            //Composite Primary Key on OrgnizationFeatureSubscription
            modelBuilder.Entity<OrgnizationFeatureSubscription>().HasKey(s => new { s.FeatureId, s.OrgId });
            modelBuilder.Entity<ModuleFeature>().HasMany<OrgnizationFeatureSubscription>(f => f.OrgFeatureSubscriptions)
                                                .WithOne(sub => sub.CurrentFeature).HasForeignKey(f => f.FeatureId)
                                                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Organization>().HasMany<OrgnizationFeatureSubscription>(o => o.OrgFeatureSubscriptions)
                                                .WithOne(sub => sub.CurrentOrganization).HasForeignKey(f => f.OrgId)
                                                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<FeatureRole>().HasIndex(p => new { p.FeatureId, p.RoleName }).IsUnique();
            modelBuilder.Entity<ModuleFeature>().HasMany<FeatureRole>(m => m.CurrentFeatureRoles)
                                                    .WithOne(role => role.ParentFeature)
                                                    .HasForeignKey(role => role.FeatureId)
                                                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MainCourse>().HasIndex(p => new { p.OrgId, p.CourseName }).IsUnique();
            modelBuilder.Entity<Organization>().HasMany<MainCourse>(org => org.CurrentCourses)
                                                .WithOne(course => course.ParentOrganization)
                                                .HasForeignKey(course => course.OrgId);

            modelBuilder.Entity<CourseSpecialization>().HasIndex(p => new { p.MainCourseId, p.SpecializationName }).IsUnique();
            modelBuilder.Entity<MainCourse>().HasMany<CourseSpecialization>(mc => mc.Specializations)
                                              .WithOne(spl => spl.ParentCourse)
                                              .HasForeignKey(spl => spl.MainCourseId)
                                              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseDetail>().HasIndex(p => new { p.SpecializationId, p.Year,p.Term }).IsUnique();
            modelBuilder.Entity<CourseSpecialization>().HasMany<CourseDetail>(cd => cd.CourseDetailList)
                                                        .WithOne(spl => spl.ParentSpecialization)
                                                        .HasForeignKey(p => p.SpecializationId)
                                                        .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserInfo>().HasIndex(ui => ui.UserEmail).IsUnique();

            modelBuilder.Entity<AppUserFeatureRoleMap>().HasIndex(p => new { p.UserOrgMapId, p.FeatureRoleId }).IsUnique();
            modelBuilder.Entity<FeatureRole>().HasMany<AppUserFeatureRoleMap>(fr => fr.UserFeatureRoleMapList)
                                               .WithOne(UFRoleApp => UFRoleApp.ParentFeatureRole)
                                               .HasForeignKey(obj => obj.FeatureRoleId)
                                               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserOrgMap>().HasMany<AppUserFeatureRoleMap>(fr => fr.CurUserRoles)
                                               .WithOne(UFRoleApp => UFRoleApp.ParentUserOrgMap)
                                               .HasForeignKey(obj => obj.UserOrgMapId)
                                               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserOrgMap>().HasIndex(p => new { p.UserId, p.OrgId }).IsUnique();
            modelBuilder.Entity<Organization>().HasMany<UserOrgMap>(obj => obj.UserOrgMapList)
                                                .WithOne(obj => obj.CurOrganization)
                                                .HasForeignKey(obj => obj.OrgId)
                                                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserInfo>().HasMany<UserOrgMap>(obj => obj.UserOrgMapList)
                                                .WithOne(obj => obj.CurUserInfo)
                                                .HasForeignKey(obj => obj.UserId)
                                                .OnDelete(DeleteBehavior.NoAction);
            
            
            modelBuilder.Entity<Subject>().HasIndex(p=>new { p.SubjectName, p.OrgId }).IsUnique();

            modelBuilder.Entity<Organization>().HasMany<Subject>(org => org.OrgSubjects)
                                                .WithOne(p => p.ParentOrganization)
                                                .HasForeignKey(p => p.OrgId)
                                                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StreamSubjectMap>().HasIndex(p => new { p.StreamId, p.SubjectId }).IsUnique();

            modelBuilder.Entity<Subject>().HasMany<StreamSubjectMap>(p => p.StreamSubjectMapsList)
                            .WithOne(p => p.ParentSubject)
                            .HasForeignKey(p => p.SubjectId)
                            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseDetail>().HasMany<StreamSubjectMap>(p => p.StreamSubjectMapsList)
                            .WithOne(p => p.ParentStream)
                            .HasForeignKey(p => p.StreamId)
                            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserInfo>().HasMany(u=>u.StudentInfoList)
                .WithOne(s=>s.CurUserInfo)
                .HasForeignKey(p=>p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Organization>().HasMany<StudentInfo>(p => p.OrgStudents)
                .WithOne(p => p.ParentOrganization).
                HasForeignKey(p => p.OrgId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentInfo>().HasIndex(p => new {p.OrgId,p.RegdNumber}).IsUnique();

            modelBuilder.Entity<StudentYearStreamMap>().HasIndex(p => new { p.StudentId, p.AcdYearId,p.CourseStreamId }).IsUnique();
            
            modelBuilder.Entity<StudentInfo>().HasMany< StudentYearStreamMap >(s=>s.StudentCourseStreams)
                .WithOne(p=>p.ParentStudent).HasForeignKey(p => p.StudentId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AcdYear>().HasIndex(a => a.AcdYearText).IsUnique();
            
            modelBuilder.Entity<CourseDetail>().HasMany(p=>p.StudentYearStreamMapList).
                WithOne(p=>p.ParentCourseStream).HasForeignKey(p => p.CourseStreamId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AcdYear>().HasMany(y => y.StudentYearStreamMapsList).WithOne(p => p.ParentAcdYear)
                .HasForeignKey(p => p.AcdYearId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Organization>().HasMany<FeeHeadMaster>(p => p.OrgFeeHeads)
                .WithOne(p => p.ParentOrg).HasForeignKey(p => p.OrgId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FeeHeadMaster>().HasIndex(p => new { p.OrgId, p.FeeHeadName }).IsUnique();

            modelBuilder.Entity<FeeHeadMaster>().HasMany<FeeMaster>(fh=>fh.ChildFeeRecords).WithOne(p => p.ParentFeeHead)
                .HasForeignKey(p=>p.FHeadId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeeMaster>().HasMany(p=>p.ChalanLines).WithOne(p => p.ParentFeeMaster).HasForeignKey(p=>p.FeeId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeeMaster>().Property(p => p.CourseDetailId).IsRequired(false);
            modelBuilder.Entity<Chalan>().HasMany(p => p.ChalanLines).
                WithOne(p=>p.ParentChln).HasForeignKey(p => p.ChlId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasSequence<int>("ChalanNumberSeq").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<StudentYearStreamMap>().HasMany(p => p.FeeConcessions)
                .WithOne(p=>p.SSMap).HasForeignKey(p=>p.MapId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeeMaster>().HasMany(p => p.FeeConcessions)
                .WithOne(p => p.ParentFeeId).HasForeignKey(p => p.FeeId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Chalan>().HasMany(p=>p.FeeCollections).WithOne(p=>p.ParentChalan)
                .HasForeignKey(p=>p.ChlnId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FeeMaster>().HasMany(p => p.FeeCollectionLineItems)
                .WithOne(p => p.ParentFeeMaster).
                HasForeignKey(p => p.FeeId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeeCollection>().HasMany(p => p.FeeCollectionLineItems)
                .WithOne(p => p.ParentFeeCollection).
                HasForeignKey(p => p.ColId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Subject>().HasIndex(p => new {p.OrgId,p.SubjCode }).IsUnique(true);
            modelBuilder.Entity<StudentLanguage>().HasIndex(s => new {s.LangNumber,s.StudentYearStreamMapId}).IsUnique(true);
            modelBuilder.Entity<StudentLanguage>().HasIndex(s => new { s.LangNumber, s.StudentYearStreamMapId,s.SubjectMapId }).IsUnique(true);

            modelBuilder.Entity<SubjectTopic>().HasIndex(st => new { st.SubId, st.TopicCode }).IsUnique(true);
            modelBuilder.Entity<Question>().HasIndex(q => new { q.TopicID, q.QTitle}).IsUnique(true);

            modelBuilder.Entity<ExamType>().HasIndex(et => new { et.MainCourseId, et.ExamTypeName }).IsUnique(true);

            modelBuilder.Entity<ExamSchedule>().HasIndex(sc => new { sc.ExamId, sc.StreamSubjectMapId }).IsUnique(true);
            //Seed Data.
            modelBuilder.SeedData();
        }
    }
}
