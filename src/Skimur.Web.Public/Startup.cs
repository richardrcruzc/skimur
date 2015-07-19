﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BundleTransformer.Core.Transformers;
using Infrastructure.Messaging;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SimpleInjector;
using Skimur.Web.Public;

// ReSharper disable once RedundantNameQualifier

[assembly: OwinStartup(typeof(Startup))]
namespace Skimur.Web.Public
{
    public class Startup : IRegistrar
    {
        IAppBuilder _app;
        // todo: factor out the command/event bus handling to separate exe via a build script
        IBusLifetime _busLifetime;

        public void Configuration(IAppBuilder app)
        {
            _app = app;
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
            ConfigureAuth(app);
            ConfigureContainer();

            // todo: factor out the command/event bus handling to separate exe via a build script
            _busLifetime = SkimurContext.Resolve<IBusLifetime>();
        }

        public void ConfigureContainer()
        {
            SkimurContext.Initialize(new Infrastructure.Registrar(),
                new Infrastructure.Membership.Registrar(),
                new Infrastructure.Email.Registrar(),
                new Infrastructure.Messaging.Registrar(),
                new Infrastructure.Messaging.RabbitMQ.Registrar(),
                new Registrar(),
                new Subs.Registrar(),
                new Subs.Worker.Registrar(), // TODO: split this into separate exe via a build script
                this);
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Frontpage",
                url: "",
                defaults: new { controller = "Subs", action = "Frontpage" });

            routes.MapRoute(
              name: "FrontpageHot",
              url: "hot",
              defaults: new { controller = "Subs", action = "Frontpage", sort = "hot" });

            routes.MapRoute(
               name: "FrontpageNew",
               url: "new",
               defaults: new { controller = "Subs", action = "Frontpage", sort = "new" });

            routes.MapRoute(
               name: "FrontpageControversial",
               url: "controversial",
               defaults: new { controller = "Subs", action = "Frontpage", sort = "controversial" });

            routes.MapRoute(
               name: "FrontpageTop",
               url: "top",
               defaults: new { controller = "Subs", action = "Frontpage", sort = "top" });

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Subs", action = "SearchSite" });

            routes.MapRoute(
                name: "Vote",
                url: "vote",
                defaults: new { controller = "Subs", action = "Vote" });

            routes.MapRoute(
                name: "UnVote",
                url: "unvote",
                defaults: new { controller = "Subs", action = "UnVote" });

            routes.MapRoute(
                name: "SubRandom",
                url: "s/random",
                defaults: new { controller = "Subs", action = "Random" });

            routes.MapRoute(
               name: "Sub",
               url: "s/{name}",
               defaults: new { controller = "Subs", action = "Posts" });

            routes.MapRoute(
               name: "SubHot",
               url: "s/{name}/hot",
               defaults: new { controller = "Subs", action = "Posts", sort = "hot" });

            routes.MapRoute(
               name: "SubNew",
               url: "s/{name}/new",
               defaults: new { controller = "Subs", action = "Posts", sort = "new" });

            routes.MapRoute(
               name: "SubControversial",
               url: "s/{name}/controversial",
               defaults: new { controller = "Subs", action = "Posts", sort = "controversial" });

            routes.MapRoute(
               name: "SubTop",
               url: "s/{name}/top",
               defaults: new { controller = "Subs", action = "Posts", sort = "top" });

            routes.MapRoute(
               name: "SubSearch",
               url: "s/{name}/search",
               defaults: new { controller = "Subs", action = "SearchSub" });

            routes.MapRoute(
                name: "Post",
                url: "s/{subName}/post/{slug}/{title}",
                defaults: new { controller = "Subs", action = "Post", title = UrlParameter.Optional });

            routes.MapRoute(
                name: "User",
                url: "user/{userName}",
                defaults: new { controller = "Users", action = "User" });

            routes.MapRoute(
                name: "Domain",
                url: "domain/{domain}",
                defaults: new { controller = "Domains", action = "Domain" });

            routes.MapRoute(
                name: "Subscribe",
                url: "subscribe/{subName}",
                defaults: new { controller = "Subs", action = "Subscribe", subName = UrlParameter.Optional /*not really optionally, but they could provide subName via ajax if they wish*/});

            routes.MapRoute(
                name: "UnSubscribe",
                url: "unsubscribe/{subName}",
                defaults: new { controller = "Subs", action = "UnSubscribe", subName = UrlParameter.Optional /*not really optionally, but they could provide subName via ajax if they wish*/ });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }

        public void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            var scriptsBundle = new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/modernizr.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/app.js",
                "~/Scripts/app.ui.js");
            var stylesBundle = new StyleBundle("~/bundles/styles").Include(
                "~/Content/site.less");

            scriptsBundle.Transforms.Add(new ScriptTransformer());
            stylesBundle.Transforms.Add(new StyleTransformer());

            bundles.Add(scriptsBundle);
            bundles.Add(stylesBundle);
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.UseGoogleAuthentication(clientId: "...",
            //     clientSecret: "...");
        }

        public void Register(Container container)
        {
            container.RegisterPerWebRequest(() => HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterSingle(_app.GetDataProtectionProvider());
            container.RegisterMvcControllers(typeof(global::Skimur.Web.Registrar).Assembly);
        }

        public int Order { get { return 0; } }
    }
}
