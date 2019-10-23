using Autofac;
using Common.Models;
using Infra.AspnetcoreIdentity;
using Infra.Efcore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace CompositionRoot
{
    public static class Composition
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<ProjectUserStore>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProjectRoleStore>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(r => ProjectDbContextFactory.CreateDbContext())
                .As<ProjectDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EfCoreUserRepo>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserClaimsPrincipalFactory<User>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClaimFactory>()
                .As<IUserClaimsPrincipalFactory<User>>()
                .InstancePerLifetimeScope();
        }
    }
}
