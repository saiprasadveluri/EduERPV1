﻿// <auto-generated />
using System;
using EduERPApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EduERPApi.Migrations
{
    [DbContext(typeof(EduERPDbContext))]
    [Migration("20241110221849_User_FeatureRole_Adjstments")]
    partial class User_FeatureRole_Adjstments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EduERPApi.Data.AppUserFeatureRoleMap", b =>
                {
                    b.Property<Guid>("AppUserRoleMapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeatureRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserOrgMapId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppUserRoleMapId");

                    b.HasIndex("FeatureRoleId");

                    b.HasIndex("UserOrgMapId", "FeatureRoleId")
                        .IsUnique();

                    b.ToTable("AppUserFeatureRoleMaps");
                });

            modelBuilder.Entity("EduERPApi.Data.ApplicationModule", b =>
                {
                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ModuleId");

                    b.HasIndex("ModuleName")
                        .IsUnique();

                    b.ToTable("ApplicationModules");

                    b.HasData(
                        new
                        {
                            ModuleId = new Guid("0942b7b7-e7e2-4964-bc09-be5cb46e2524"),
                            ModuleName = "School",
                            Status = 1
                        },
                        new
                        {
                            ModuleId = new Guid("c4b4cd0e-30e2-4800-889f-a71920c481a6"),
                            ModuleName = "College",
                            Status = 1
                        },
                        new
                        {
                            ModuleId = new Guid("13a01c28-632f-4734-b9b4-a3b3c10f47ee"),
                            ModuleName = "University",
                            Status = 1
                        });
                });

            modelBuilder.Entity("EduERPApi.Data.CourseDetail", b =>
                {
                    b.Property<Guid>("CourseDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpecializationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("CourseDetailId");

                    b.HasIndex("SpecializationId", "Year", "Term")
                        .IsUnique();

                    b.ToTable("CourseDetails");
                });

            modelBuilder.Entity("EduERPApi.Data.CourseSpecialization", b =>
                {
                    b.Property<Guid>("CourseSpecializationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MainCourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SpecializationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("CourseSpecializationId");

                    b.HasIndex("MainCourseId", "SpecializationName")
                        .IsUnique();

                    b.ToTable("CourseSpecializations");
                });

            modelBuilder.Entity("EduERPApi.Data.FeatureRole", b =>
                {
                    b.Property<Guid>("AppRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("AppRoleId");

                    b.HasIndex("FeatureId", "RoleName")
                        .IsUnique();

                    b.ToTable("FeatureRoles");

                    b.HasData(
                        new
                        {
                            AppRoleId = new Guid("76f8e76e-714a-432c-9fe8-9d02665b47a7"),
                            FeatureId = new Guid("9e231229-8aa4-4ccb-b5c2-8960e7c62d34"),
                            RoleName = "STUDENT_MANAGEMENT Admin",
                            Status = 0
                        },
                        new
                        {
                            AppRoleId = new Guid("a71b43e4-a7f7-4e21-9ab6-2a03657bc9ec"),
                            FeatureId = new Guid("9e231229-8aa4-4ccb-b5c2-8960e7c62d34"),
                            RoleName = "STUDENT_MANAGEMENT Sudent",
                            Status = 0
                        });
                });

            modelBuilder.Entity("EduERPApi.Data.MainCourse", b =>
                {
                    b.Property<Guid>("MainCourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationInYears")
                        .HasColumnType("int");

                    b.Property<int>("IsSpecializationsAvailable")
                        .HasMaxLength(500)
                        .HasColumnType("int");

                    b.Property<Guid>("OrgId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("MainCourseId");

                    b.HasIndex("OrgId", "CourseName")
                        .IsUnique();

                    b.ToTable("MainCourses");
                });

            modelBuilder.Entity("EduERPApi.Data.ModuleFeature", b =>
                {
                    b.Property<Guid>("FeatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FeatureName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("FeatureId");

                    b.HasIndex("ModuleId", "FeatureName")
                        .IsUnique();

                    b.ToTable("ModuleFeatures");

                    b.HasData(
                        new
                        {
                            FeatureId = new Guid("9e231229-8aa4-4ccb-b5c2-8960e7c62d34"),
                            FeatureName = "Student Management",
                            ModuleId = new Guid("0942b7b7-e7e2-4964-bc09-be5cb46e2524"),
                            Status = 1
                        },
                        new
                        {
                            FeatureId = new Guid("2f82a7cd-8a0c-4043-9f20-222f554ca241"),
                            FeatureName = "Fee Management",
                            ModuleId = new Guid("0942b7b7-e7e2-4964-bc09-be5cb46e2524"),
                            Status = 1
                        },
                        new
                        {
                            FeatureId = new Guid("adb42ecc-3032-400a-89b1-c05a685f3daa"),
                            FeatureName = "Student Management",
                            ModuleId = new Guid("c4b4cd0e-30e2-4800-889f-a71920c481a6"),
                            Status = 1
                        },
                        new
                        {
                            FeatureId = new Guid("15c5c626-5585-4f7d-aec2-c8986816dada"),
                            FeatureName = "Fee Management",
                            ModuleId = new Guid("c4b4cd0e-30e2-4800-889f-a71920c481a6"),
                            Status = 1
                        },
                        new
                        {
                            FeatureId = new Guid("76b219bb-b72a-4125-8ec3-37aabb15c7b5"),
                            FeatureName = "Student Management",
                            ModuleId = new Guid("13a01c28-632f-4734-b9b4-a3b3c10f47ee"),
                            Status = 1
                        });
                });

            modelBuilder.Entity("EduERPApi.Data.Organization", b =>
                {
                    b.Property<Guid>("OrgId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OrgAddress")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("OrgModuleType")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrgName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimaryEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrgId");

                    b.HasIndex("OrgName")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("EduERPApi.Data.OrgnizationFeatureSubscription", b =>
                {
                    b.Property<Guid>("FeatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrgId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("SubId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FeatureId", "OrgId");

                    b.HasIndex("OrgId");

                    b.ToTable("OrgnizationFeatureSubscriptions");
                });

            modelBuilder.Entity("EduERPApi.Data.StreamSubjectMap", b =>
                {
                    b.Property<Guid>("StreamSubjectMapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StreamSubjectMapId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("StreamId", "SubjectId")
                        .IsUnique();

                    b.ToTable("StreamSubjectMaps");
                });

            modelBuilder.Entity("EduERPApi.Data.Subject", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrgId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SubjectId");

                    b.HasIndex("OrgId");

                    b.HasIndex("SubjectName", "OrgId")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("EduERPApi.Data.UserInfo", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserDetailsJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("UserId");

                    b.HasIndex("UserEmail")
                        .IsUnique();

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("EduERPApi.Data.UserOrgMap", b =>
                {
                    b.Property<Guid>("UserOrgMapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrgId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserOrgMapId");

                    b.HasIndex("OrgId");

                    b.HasIndex("UserId", "OrgId")
                        .IsUnique();

                    b.ToTable("UserOrgMaps");
                });

            modelBuilder.Entity("EduERPApi.Data.AppUserFeatureRoleMap", b =>
                {
                    b.HasOne("EduERPApi.Data.FeatureRole", "ParentFeatureRole")
                        .WithMany("UserFeatureRoleMapList")
                        .HasForeignKey("FeatureRoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EduERPApi.Data.UserOrgMap", "ParentUserOrgMap")
                        .WithMany("CurUserRoles")
                        .HasForeignKey("UserOrgMapId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentFeatureRole");

                    b.Navigation("ParentUserOrgMap");
                });

            modelBuilder.Entity("EduERPApi.Data.CourseDetail", b =>
                {
                    b.HasOne("EduERPApi.Data.CourseSpecialization", "ParentSpecialization")
                        .WithMany("CourseDetailList")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentSpecialization");
                });

            modelBuilder.Entity("EduERPApi.Data.CourseSpecialization", b =>
                {
                    b.HasOne("EduERPApi.Data.MainCourse", "ParentCourse")
                        .WithMany("Specializations")
                        .HasForeignKey("MainCourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentCourse");
                });

            modelBuilder.Entity("EduERPApi.Data.FeatureRole", b =>
                {
                    b.HasOne("EduERPApi.Data.ModuleFeature", "ParentFeature")
                        .WithMany("CurrentFeatureRoles")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentFeature");
                });

            modelBuilder.Entity("EduERPApi.Data.MainCourse", b =>
                {
                    b.HasOne("EduERPApi.Data.Organization", "ParentOrganization")
                        .WithMany("CurrentCourses")
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentOrganization");
                });

            modelBuilder.Entity("EduERPApi.Data.ModuleFeature", b =>
                {
                    b.HasOne("EduERPApi.Data.ApplicationModule", "ParentModule")
                        .WithMany("ModuleFeatures")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentModule");
                });

            modelBuilder.Entity("EduERPApi.Data.OrgnizationFeatureSubscription", b =>
                {
                    b.HasOne("EduERPApi.Data.ModuleFeature", "CurrentFeature")
                        .WithMany("OrgFeatureSubscriptions")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EduERPApi.Data.Organization", "CurrentOrganization")
                        .WithMany("OrgFeatureSubscriptions")
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CurrentFeature");

                    b.Navigation("CurrentOrganization");
                });

            modelBuilder.Entity("EduERPApi.Data.StreamSubjectMap", b =>
                {
                    b.HasOne("EduERPApi.Data.CourseDetail", "ParentStream")
                        .WithMany("StreamSubjectMapsList")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EduERPApi.Data.Subject", "ParentSubject")
                        .WithMany("StreamSubjectMapsList")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentStream");

                    b.Navigation("ParentSubject");
                });

            modelBuilder.Entity("EduERPApi.Data.Subject", b =>
                {
                    b.HasOne("EduERPApi.Data.Organization", "ParentOrganization")
                        .WithMany("OrgSubjects")
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentOrganization");
                });

            modelBuilder.Entity("EduERPApi.Data.UserOrgMap", b =>
                {
                    b.HasOne("EduERPApi.Data.Organization", "CurOrganization")
                        .WithMany("UserOrgMapList")
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EduERPApi.Data.UserInfo", "CurUserInfo")
                        .WithMany("UserOrgMapList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CurOrganization");

                    b.Navigation("CurUserInfo");
                });

            modelBuilder.Entity("EduERPApi.Data.ApplicationModule", b =>
                {
                    b.Navigation("ModuleFeatures");
                });

            modelBuilder.Entity("EduERPApi.Data.CourseDetail", b =>
                {
                    b.Navigation("StreamSubjectMapsList");
                });

            modelBuilder.Entity("EduERPApi.Data.CourseSpecialization", b =>
                {
                    b.Navigation("CourseDetailList");
                });

            modelBuilder.Entity("EduERPApi.Data.FeatureRole", b =>
                {
                    b.Navigation("UserFeatureRoleMapList");
                });

            modelBuilder.Entity("EduERPApi.Data.MainCourse", b =>
                {
                    b.Navigation("Specializations");
                });

            modelBuilder.Entity("EduERPApi.Data.ModuleFeature", b =>
                {
                    b.Navigation("CurrentFeatureRoles");

                    b.Navigation("OrgFeatureSubscriptions");
                });

            modelBuilder.Entity("EduERPApi.Data.Organization", b =>
                {
                    b.Navigation("CurrentCourses");

                    b.Navigation("OrgFeatureSubscriptions");

                    b.Navigation("OrgSubjects");

                    b.Navigation("UserOrgMapList");
                });

            modelBuilder.Entity("EduERPApi.Data.Subject", b =>
                {
                    b.Navigation("StreamSubjectMapsList");
                });

            modelBuilder.Entity("EduERPApi.Data.UserInfo", b =>
                {
                    b.Navigation("UserOrgMapList");
                });

            modelBuilder.Entity("EduERPApi.Data.UserOrgMap", b =>
                {
                    b.Navigation("CurUserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
