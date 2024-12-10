using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using EduERPApi.Infra;

namespace EduERPApi.Data
{
    public static class DataSeedExtention
    {
        
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            SeedAppModules(modelBuilder);
            SeedModuleFaetures(modelBuilder);
            SeedFeatureRole(modelBuilder);
        }

        private static void SeedAppModules(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationModule>().HasData(
                new ApplicationModule()
                {
                    ModuleId = new Guid(RoleConstents.SCHOOL_GUID),
                    ModuleName = "School",
                    Status = 1
                },
                new ApplicationModule()
                {
                    ModuleId = new Guid(RoleConstents.COLLEGE_GUID),
                    ModuleName = "College",
                    Status = 1
                },
                new ApplicationModule()
                {
                    ModuleId = new Guid(RoleConstents.UNIVERSITY_GUID),
                    ModuleName = "University",
                    Status = 1
                }
                );
        }

        private static void SeedModuleFaetures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModuleFeature>().HasData(
                new ModuleFeature()
                {
                    FeatureId=new Guid(RoleConstents.SCHOOL_STUDENT_MANAGEMENT_GUID),
                    ModuleId=new Guid(RoleConstents.SCHOOL_GUID),
                    FeatureName=RoleConstents.STUDENT_MANAGEMENT_FEATURE,
                    Status=1
                },
                new ModuleFeature()
                {
                    FeatureId = new Guid(RoleConstents.SCHOOL_FEE_MANAGEMENT_GUID),
                    ModuleId = new Guid(RoleConstents.SCHOOL_GUID),
                    FeatureName = RoleConstents.FEE_MANAGEMENT_FEATURE,
                    Status = 1
                },
                new ModuleFeature()
                {
                    FeatureId = new Guid(RoleConstents.COLLEGE_STUDENT_MANAGEMENT_GUID),
                    ModuleId = new Guid(RoleConstents.COLLEGE_GUID),
                    FeatureName = RoleConstents.STUDENT_MANAGEMENT_FEATURE,
                    Status = 1
                },
                new ModuleFeature()
                {
                    FeatureId = new Guid(RoleConstents.COLLEGE_FEE_MANAGEMENT_GUID),
                    ModuleId = new Guid(RoleConstents.COLLEGE_GUID),
                    FeatureName = RoleConstents.FEE_MANAGEMENT_FEATURE,
                    Status = 1
                },
                new ModuleFeature()
                {
                    FeatureId = new Guid(RoleConstents.UNIVERSITY_STUDENT_MANAGEMENT_GUID),
                    ModuleId = new Guid(RoleConstents.UNIVERSITY_GUID),
                    FeatureName = RoleConstents.STUDENT_MANAGEMENT_FEATURE,
                    Status = 1
                },
                new ModuleFeature()
                {
                    FeatureId = new Guid(RoleConstents.UNIVERSITY_FEE_MANAGEMENT_GUID),
                    ModuleId = new Guid(RoleConstents.UNIVERSITY_GUID),
                    FeatureName = RoleConstents.FEE_MANAGEMENT_FEATURE,
                    Status = 1
                }
                );

        }

        private static void SeedFeatureRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeatureRole>().HasData(
                new FeatureRole()
                {
                    AppRoleId=new Guid(RoleConstents.SCHOOL_STUDENT_MANAGEMENT_ADMIN_ROLE_GUID),
                    FeatureId=new Guid(RoleConstents.SCHOOL_STUDENT_MANAGEMENT_GUID),
                    RoleName= RoleConstents.ROLE_ADMIN
                },
                new FeatureRole()
                {
                    AppRoleId = new Guid(RoleConstents.SCHOOL_STUDENT_MANAGEMENT_STUDENT_ROLE_GUID),
                    FeatureId = new Guid(RoleConstents.SCHOOL_STUDENT_MANAGEMENT_GUID),
                    RoleName = RoleConstents.ROLE_STUDENT
                },
                new FeatureRole()
                {
                    AppRoleId = new Guid(RoleConstents.COLLEGE_STUDENT_MANAGEMENT_ADMIN_ROLE_GUID),
                    FeatureId = new Guid(RoleConstents.COLLEGE_STUDENT_MANAGEMENT_GUID),
                    RoleName = RoleConstents.ROLE_ADMIN
                },
                new FeatureRole()
                {
                    AppRoleId = new Guid(RoleConstents.COLLEGE_STUDENT_MANAGEMENT_STUDENT_ROLE_GUID),
                    FeatureId = new Guid(RoleConstents.COLLEGE_STUDENT_MANAGEMENT_GUID),
                    RoleName = RoleConstents.ROLE_STUDENT
                },
                new FeatureRole()
                {
                    AppRoleId = new Guid(RoleConstents.SCHOOL_FEE_ADMIN_ROLE_GUID),
                    FeatureId = new Guid(RoleConstents.SCHOOL_FEE_MANAGEMENT_GUID),
                    RoleName = RoleConstents.ROLE_ADMIN
                }

                );
        }
    }
}
