﻿using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skoruba.IdentityServer4.Admin.Data.Entities;
using Skoruba.IdentityServer4.Admin.Data.Entities.Identity;

namespace Skoruba.IdentityServer4.Admin.Data.DbContexts
{
    public class AdminDbContext : IdentityDbContext<UserIdentity, UserIdentityRole, int, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>
    {
        private readonly ConfigurationStoreOptions _storeOptions;
        private readonly OperationalStoreOptions _operationalOptions;

        public AdminDbContext(DbContextOptions<AdminDbContext> options, 
            ConfigurationStoreOptions storeOptions,
                OperationalStoreOptions operationalOptions)
            : base(options)
        {
            _storeOptions = storeOptions;
            _operationalOptions = operationalOptions;
        }

        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<ApiSecret> ApiSecrets { get; set; }

        public DbSet<ApiScope> ApiScopes { get; set; }

        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

        public DbSet<IdentityClaim> IdentityClaims { get; set; }

        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

        public DbSet<ClientScope> ClientScopes { get; set; }

        public DbSet<ClientSecret> ClientSecrets { get; set; }

        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

        public DbSet<ClientClaim> ClientClaims { get; set; }

        public DbSet<ClientProperty> ClientProperties { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureIdentityContext(builder);
            builder.ConfigureClientContext(_storeOptions);
            builder.ConfigureResourcesContext(_storeOptions);
            builder.ConfigurePersistedGrantContext(_operationalOptions);   
        }

        private void ConfigureIdentityContext(ModelBuilder builder)
        {
            builder.Entity<UserIdentityRole>().ToTable("Roles");
            builder.Entity<UserIdentityRoleClaim>().ToTable("RoleClaims");
            builder.Entity<UserIdentityUserRole>().ToTable("UserRoles");

            builder.Entity<UserIdentity>().ToTable("Users");
            builder.Entity<UserIdentityUserLogin>().ToTable("UserLogins");
            builder.Entity<UserIdentityUserClaim>().ToTable("UserClaims");
            builder.Entity<UserIdentityUserToken>().ToTable("UserTokens");
        }
    }
}