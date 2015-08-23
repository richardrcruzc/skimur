﻿using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using Skimur.Web.Avatar;
using Skimur.Web.Identity;
using Subs.Services;

namespace Skimur.Web
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.RegisterSingle<IUserStore<ApplicationUser, Guid>, ApplicationUserStore>();
            container.RegisterSingle<IIdentityMessageService, ApplicationIdentityMessageService>();
            container.RegisterSingle<ApplicationUserManager>();
            container.RegisterSingle<IAvatarService, AvatarService>();

            container.RegisterPerWebRequest<ApplicationSignInManager>();
            container.RegisterPerWebRequest<IUserContext, UserContext>();
            container.RegisterPerWebRequest<IContextService, ContextService>(); 

            container.RegisterMvcIntegratedFilterProvider();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        public int Order { get { return 0; } }
    }
}
