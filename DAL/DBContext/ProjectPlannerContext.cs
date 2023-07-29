﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public partial class ProjectPlannerContext : DbContext
{
    public ProjectPlannerContext()
    {
    }

    public ProjectPlannerContext(DbContextOptions<ProjectPlannerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Environment> Environments { get; set; }

    public virtual DbSet<PermissionsProfilesWebModule> PermissionsProfilesWebModules { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<ProjectsUser> ProjectsUsers { get; set; }

    public virtual DbSet<TasksStatus> TasksStatuses { get; set; }

    public virtual DbSet<TasksUser> TasksUsers { get; set; }

    public virtual DbSet<TypesModule> TypesModules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserState> UserStates { get; set; }

    public virtual DbSet<WebModule> WebModules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customers_pkey");

            entity.ToTable("customers", "projectplanner");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(255)
                .HasColumnName("customer_email");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name");
        });

        modelBuilder.Entity<Environment>(entity =>
        {
            entity.HasKey(e => e.EnvironmentId).HasName("environments_pkey");

            entity.ToTable("environments", "projectplanner");

            entity.Property(e => e.EnvironmentId).HasColumnName("environment_id");
            entity.Property(e => e.EnvironmentName)
                .HasMaxLength(250)
                .HasColumnName("environment_name");
        });

        modelBuilder.Entity<PermissionsProfilesWebModule>(entity =>
        {
            entity.HasKey(e => e.PermissionProfileWebModuleId).HasName("permissions_profiles_web_modules_pkey");

            entity.ToTable("permissions_profiles_web_modules", "projectplanner");

            entity.Property(e => e.PermissionProfileWebModuleId)
                .HasDefaultValueSql("nextval('projectplanner.permissions_profiles_web_modu_permission_profile_web_module_seq'::regclass)")
                .HasColumnName("permission_profile_web_module_id");
            entity.Property(e => e.PermissionProfileWebModuleAccess)
                .HasDefaultValueSql("false")
                .HasColumnName("permission_profile_web_module_access");
            entity.Property(e => e.PermissionProfileWebModuleCreate)
                .HasDefaultValueSql("false")
                .HasColumnName("permission_profile_web_module_create");
            entity.Property(e => e.PermissionProfileWebModuleDelete)
                .HasDefaultValueSql("false")
                .HasColumnName("permission_profile_web_module_delete");
            entity.Property(e => e.PermissionProfileWebModuleDownload)
                .HasDefaultValueSql("false")
                .HasColumnName("permission_profile_web_module_download");
            entity.Property(e => e.PermissionProfileWebModuleUpdate)
                .HasDefaultValueSql("false")
                .HasColumnName("permission_profile_web_module_update");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.WebModuleId).HasColumnName("web_module_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.PermissionsProfilesWebModules)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profiles_fk");

            entity.HasOne(d => d.WebModule).WithMany(p => p.PermissionsProfilesWebModules)
                .HasForeignKey(d => d.WebModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("web_modules_fk");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("profiles_pkey");

            entity.ToTable("profiles", "projectplanner");

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.ProfileName)
                .HasMaxLength(100)
                .HasColumnName("profile_name");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.ToTable("projects", "projectplanner");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectCustomer).HasColumnName("project_customer");
            entity.Property(e => e.ProjectDirectBoss).HasColumnName("project_direct_boss");
            entity.Property(e => e.ProjectImmediateBoss).HasColumnName("project_immediate_boss");
            entity.Property(e => e.ProjectStatus).HasColumnName("project_status");
            entity.Property(e => e.ProjectTitle)
                .HasColumnType("character varying")
                .HasColumnName("project_title");

            entity.HasOne(d => d.ProjectCustomerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectCustomer)
                .HasConstraintName("projects_customers_fk");

            entity.HasOne(d => d.ProjectDirectBossNavigation).WithMany(p => p.ProjectProjectDirectBossNavigations)
                .HasForeignKey(d => d.ProjectDirectBoss)
                .HasConstraintName("projects_users_direct_boss_fk");

            entity.HasOne(d => d.ProjectImmediateBossNavigation).WithMany(p => p.ProjectProjectImmediateBossNavigations)
                .HasForeignKey(d => d.ProjectImmediateBoss)
                .HasConstraintName("projects_users_immediate_boss_fk");

            entity.HasOne(d => d.ProjectStatusNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectStatus)
                .HasConstraintName("projects_project_status_fk");
        });

        modelBuilder.Entity<ProjectStatus>(entity =>
        {
            entity.HasKey(e => e.ProjectStatusId).HasName("project_status_pkey");

            entity.ToTable("project_status", "projectplanner");

            entity.Property(e => e.ProjectStatusId).HasColumnName("project_status_id");
            entity.Property(e => e.ProjectStatusDescripcion)
                .HasMaxLength(200)
                .HasColumnName("project_status_descripcion");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.ProjectTasksId).HasName("project_tasks_pkey");

            entity.ToTable("project_tasks", "projectplanner");

            entity.Property(e => e.ProjectTasksId).HasColumnName("project_tasks_id");
            entity.Property(e => e.EnvironmentId).HasColumnName("environment_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectTasks)
                .HasMaxLength(300)
                .HasColumnName("project_tasks");
            entity.Property(e => e.ProjectTasksEndDate).HasColumnName("project_tasks_end_date");
            entity.Property(e => e.ProjectTasksObservations)
                .HasMaxLength(300)
                .HasColumnName("project_tasks_observations");
            entity.Property(e => e.ProjectTasksStartDate).HasColumnName("project_tasks_start_date");
            entity.Property(e => e.TaskStatusId).HasColumnName("task_status_id");

            entity.HasOne(d => d.Environment).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.EnvironmentId)
                .HasConstraintName("environments_fk");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("projects_fk");

            entity.HasOne(d => d.TaskStatus).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.TaskStatusId)
                .HasConstraintName("tasks_statuses_fk");
        });

        modelBuilder.Entity<ProjectsUser>(entity =>
        {
            entity.HasKey(e => e.ProjectUser).HasName("projects_users_pkey");

            entity.ToTable("projects_users", "projectplanner");

            entity.Property(e => e.ProjectUser).HasColumnName("project_user");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectsUsers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("project_fk");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectsUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_fk");
        });

        modelBuilder.Entity<TasksStatus>(entity =>
        {
            entity.HasKey(e => e.TaskStatusId).HasName("tasks_statuses_pkey");

            entity.ToTable("tasks_statuses", "projectplanner");

            entity.Property(e => e.TaskStatusId).HasColumnName("task_status_id");
            entity.Property(e => e.TaskStatusName)
                .HasMaxLength(100)
                .HasColumnName("task_status_name");
        });

        modelBuilder.Entity<TasksUser>(entity =>
        {
            entity.HasKey(e => e.TaskUserId).HasName("tasks_users_pkey");

            entity.ToTable("tasks_users", "projectplanner");

            entity.Property(e => e.TaskUserId).HasColumnName("task_user_id");
            entity.Property(e => e.TasksId).HasColumnName("tasks_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Tasks).WithMany(p => p.TasksUsers)
                .HasForeignKey(d => d.TasksId)
                .HasConstraintName("tasks_fk");

            entity.HasOne(d => d.User).WithMany(p => p.TasksUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("tasks_users_fk");
        });

        modelBuilder.Entity<TypesModule>(entity =>
        {
            entity.HasKey(e => e.TypeModuleId).HasName("types_modules_pkey");

            entity.ToTable("types_modules", "projectplanner");

            entity.Property(e => e.TypeModuleId).HasColumnName("type_module_id");
            entity.Property(e => e.TypeModuleName)
                .HasMaxLength(100)
                .HasColumnName("type_module_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users", "projectplanner");

            entity.HasIndex(e => e.UserEmail, "users_user_email_key").IsUnique();

            entity.HasIndex(e => e.UserLogin, "users_user_login_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserAttempts)
                .HasDefaultValueSql("0")
                .HasColumnName("user_attempts");
            entity.Property(e => e.UserDateCreate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("user_date_create");
            entity.Property(e => e.UserDateExpPassword)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("user_date_exp_password");
            entity.Property(e => e.UserDateUpdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("user_date_update");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFirstLogin)
                .HasDefaultValueSql("false")
                .HasColumnName("user_first_login");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(100)
                .HasColumnName("user_login");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .HasColumnName("user_password");
            entity.Property(e => e.UserStateId).HasColumnName("user_state_id");

            entity.HasOne(d => d.UserState).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tasks_fk");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("user_profiles_pkey");

            entity.ToTable("user_profiles", "projectplanner");

            entity.Property(e => e.UserProfileId).HasColumnName("user_profile_id");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.ProfileId)
                .HasConstraintName("tasks_fk");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("users_fk");
        });

        modelBuilder.Entity<UserState>(entity =>
        {
            entity.HasKey(e => e.UserStateId).HasName("user_states_pkey");

            entity.ToTable("user_states", "projectplanner");

            entity.Property(e => e.UserStateId).HasColumnName("user_state_id");
            entity.Property(e => e.UserStateName)
                .HasMaxLength(100)
                .HasColumnName("user_state_name");
        });

        modelBuilder.Entity<WebModule>(entity =>
        {
            entity.HasKey(e => e.WebModuleId).HasName("web_modules_pkey");

            entity.ToTable("web_modules", "projectplanner");

            entity.Property(e => e.WebModuleId).HasColumnName("web_module_id");
            entity.Property(e => e.TypeModuleId).HasColumnName("type_module_id");
            entity.Property(e => e.WebModuleCreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("web_module_create_date");
            entity.Property(e => e.WebModuleDescription)
                .HasMaxLength(100)
                .HasColumnName("web_module_description");
            entity.Property(e => e.WebModuleFather).HasColumnName("web_module_father");
            entity.Property(e => e.WebModuleIcon)
                .HasMaxLength(100)
                .HasColumnName("web_module_icon");
            entity.Property(e => e.WebModuleIndex).HasColumnName("web_module_index");
            entity.Property(e => e.WebModuleTitle)
                .HasMaxLength(100)
                .HasColumnName("web_module_title");
            entity.Property(e => e.WebModuleUrl)
                .HasMaxLength(100)
                .HasColumnName("web_module_url");

            entity.HasOne(d => d.TypeModule).WithMany(p => p.WebModules)
                .HasForeignKey(d => d.TypeModuleId)
                .HasConstraintName("types_modules_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
