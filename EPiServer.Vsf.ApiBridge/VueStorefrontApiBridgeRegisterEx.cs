﻿using System.IdentityModel.Tokens;
using System.Web.Http;
using System.Web.Http.Cors;
using EPiServer.ServiceLocation;
using EPiServer.Vsf.ApiBridge.Authorization.Claims;
using EPiServer.Vsf.ApiBridge.Authorization.Token;
using EPiServer.Vsf.ApiBridge.Endpoints;
using EPiServer.Vsf.ApiBridge.Utils;
using EPiServer.Vsf.Core.ApiBridge.Endpoint;
using EPiServer.Vsf.Core.ApiBridge.Model.User;
using EPiServer.Vsf.DataAccess;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace EPiServer.Vsf.ApiBridge
{
    public static class VueStorefrontApiBridgeRegisterEx
    {
        public static void RegisterVueStorefrontBridgeDefaultService(this IServiceConfigurationProvider services, VsfApiBridgeConfiguration bridgeConfiguration)
        {
            services.AddSingleton(bridgeConfiguration);
            services.AddTransient<IUserEndpoint, UserEndpoint<VsfUser>>();
            services.AddTransient<ICartEndpoint, CartEndpoint>();
            services.AddTransient<IStockEndpoint, StockEndpoint>();
            services.AddTransient<IOrderEndpoint, OrderEndpoint>();
            services.AddScoped<QuicksilverDbContext>();
            services.AddScoped<IRefreshTokenRepository, DatabaseRefreshTokenRepository>();
            services.AddScoped<IUserTokenProvider, JwtUserTokenProvider>();
            services.AddTransient<IUserClaimsProvider<VsfUser>, UserClaimsProvider<VsfUser>>();
        }

        public static void RegisterVueStorefrontBridge(this HttpConfiguration configuration)
        {
            configuration.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            configuration.Routes.MapHttpRoute("VueStorefron API Bridge",
                "vsbridge/{controller}/{action}");
        }

        public static IAppBuilder RegisterVueStorefrontAuth(this IAppBuilder bulder, VsfApiBridgeConfiguration configuration)
        {
            bulder.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {   
                AuthenticationType = "VueStorefronToken",
                TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = configuration.IssuerSigningKey.ToSymmetricSecurityKey(),
                    ValidIssuer = configuration.ValidIssuer,
                    ValidAudience = configuration.ValidAudience,
                    ValidateLifetime = true,
                },
                Provider = new VsfJwtBearerTokenProvider()
            });
            return bulder;
        }
    }
}
