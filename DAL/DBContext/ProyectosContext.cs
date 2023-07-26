using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public partial class ProyectosContext : DbContext
{
    public ProyectosContext()
    {
    }

    public ProyectosContext(DbContextOptions<ProyectosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atmosphere> Atmospheres { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<PermissionsProfilesWebModule> PermissionsProfilesWebModules { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<TypesModule> TypesModules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserState> UserStates { get; set; }

    public virtual DbSet<Usersa> Usersas { get; set; }

    public virtual DbSet<WebModule> WebModules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Proyectos;User Id=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atmosphere>(entity =>
        {
            entity.HasKey(e => e.AtmosphereId).HasName("atmosphere_pkey");

            entity.ToTable("atmosphere", "proyectos");

            entity.Property(e => e.AtmosphereId).HasColumnName("atmosphere_id");
            entity.Property(e => e.AtmosphereName)
                .HasMaxLength(250)
                .HasColumnName("atmosphere_name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customers_pkey");

            entity.ToTable("customers", "proyectos");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(255)
                .HasColumnName("customer_email");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name");
        });

        modelBuilder.Entity<PermissionsProfilesWebModule>(entity =>
        {
            entity.HasKey(e => e.PermissionProfileWebModuleId).HasName("permissions_profiles_web_modules_pkey");

            entity.ToTable("permissions_profiles_web_modules", "proyectos");

            entity.Property(e => e.PermissionProfileWebModuleId)
                .HasDefaultValueSql("nextval('proyectos.permissions_profiles_web_modu_permission_profile_web_module_seq'::regclass)")
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
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("profiles_pkey");

            entity.ToTable("profiles", "proyectos");

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.ProfileName)
                .HasMaxLength(100)
                .HasColumnName("profile_name");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.ToTable("projects", "proyectos");

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

            entity.HasMany(d => d.ProjectUsers).WithMany(p => p.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectsUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("ProjectUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_fk"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("project_fk"),
                    j =>
                    {
                        j.HasKey("ProjectId", "ProjectUserId").HasName("projects_users_pkey");
                        j.ToTable("projects_users", "proyectos");
                        j.IndexerProperty<int>("ProjectId").HasColumnName("project_id");
                        j.IndexerProperty<int>("ProjectUserId").HasColumnName("project_user_id");
                    });
        });

        modelBuilder.Entity<ProjectStatus>(entity =>
        {
            entity.HasKey(e => e.ProjectStatusId).HasName("project_status_pkey");

            entity.ToTable("project_status", "proyectos");

            entity.Property(e => e.ProjectStatusId).HasColumnName("project_status_id");
            entity.Property(e => e.ProjectStatusDescripcion)
                .HasMaxLength(200)
                .HasColumnName("project_status_descripcion");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.ProjectTasksId).HasName("project_tasks_pkey");

            entity.ToTable("project_tasks", "proyectos");

            entity.Property(e => e.ProjectTasksId).HasColumnName("project_tasks_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectTasksAtmosphere).HasColumnName("project_tasks_atmosphere");
            entity.Property(e => e.ProjectTasksDelivered)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("project_tasks_delivered");
            entity.Property(e => e.ProjectTasksEndDate).HasColumnName("project_tasks_end_date");
            entity.Property(e => e.ProjectTasksObservations)
                .HasMaxLength(300)
                .HasColumnName("project_tasks_observations");
            entity.Property(e => e.ProjectTasksStartDate).HasColumnName("project_tasks_start_date");
            entity.Property(e => e.RpojectTasks)
                .HasMaxLength(300)
                .HasColumnName("rpoject_tasks");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("project_tasks_projects_fk");

            entity.HasOne(d => d.ProjectTasksAtmosphereNavigation).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.ProjectTasksAtmosphere)
                .HasConstraintName("project_tasks_atmosphere_fk");

            entity.HasMany(d => d.TasksUsers).WithMany(p => p.Tasks)
                .UsingEntity<Dictionary<string, object>>(
                    "TasksUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("TasksUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("tasks_users_fk"),
                    l => l.HasOne<ProjectTask>().WithMany()
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("tasks_fk"),
                    j =>
                    {
                        j.HasKey("TasksId", "TasksUserId").HasName("tasks_users_pkey");
                        j.ToTable("tasks_users", "proyectos");
                        j.IndexerProperty<int>("TasksId").HasColumnName("tasks_id");
                        j.IndexerProperty<int>("TasksUserId").HasColumnName("tasks_user_id");
                    });
        });

        modelBuilder.Entity<TypesModule>(entity =>
        {
            entity.HasKey(e => e.TypeModuleId).HasName("types_modules_pkey");

            entity.ToTable("types_modules", "proyectos");

            entity.Property(e => e.TypeModuleId).HasColumnName("type_module_id");
            entity.Property(e => e.TypeModuleName)
                .HasMaxLength(100)
                .HasColumnName("type_module_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users", "proyectos");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(200)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasColumnType("character varying")
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("user_profiles_pkey");

            entity.ToTable("user_profiles", "proyectos");

            entity.Property(e => e.UserProfileId).HasColumnName("user_profile_id");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.ProfileId)
                .HasConstraintName("user_profiles_profile_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_profiles_user_id_fkey");
        });

        modelBuilder.Entity<UserState>(entity =>
        {
            entity.HasKey(e => e.UserStateId).HasName("user_states_pkey");

            entity.ToTable("user_states", "proyectos");

            entity.Property(e => e.UserStateId).HasColumnName("user_state_id");
            entity.Property(e => e.UserStateName)
                .HasMaxLength(100)
                .HasColumnName("user_state_name");
        });

        modelBuilder.Entity<Usersa>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("usersa_pkey");

            entity.ToTable("usersa", "proyectos");

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

            entity.HasOne(d => d.UserState).WithMany(p => p.Usersas)
                .HasForeignKey(d => d.UserStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usersa_user_state_id_fkey");
        });

        modelBuilder.Entity<WebModule>(entity =>
        {
            entity.HasKey(e => e.WebModuleId).HasName("web_modules_pkey");

            entity.ToTable("web_modules", "proyectos");

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
                .HasConstraintName("web_modules_type_module_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
