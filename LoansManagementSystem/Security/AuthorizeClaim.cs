﻿using LoansManagementSystem.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace LoansManagementSystem.Security;

public class AuthorizeClaim : ActionFilterAttribute
{
    private readonly string[] claims;

    public AuthorizeClaim(string[] claims)
    {
        this.claims = claims;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string? UserRole = string.Empty;

        if (context.HttpContext.User.Claims.Any())
            UserRole = context.HttpContext.User.Claims.Where(c => c.Type == "UserRole").Select(c => c.Value)
                .FirstOrDefault();

        var cache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;

        var jwt = context.HttpContext.RequestServices.GetService(typeof(IJwt)) as IJwt;

        if (!string.IsNullOrEmpty(UserRole))
        {
            if (claims.Any(claim => claim != UserRole))
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            jwt.UserRole = context.HttpContext.User.Claims.Where(c => c.Type == "UserRole").Select(c => c.Value)
                .First();

            jwt.UserId = context.HttpContext.User.Claims.Where(c => c.Type == "UserId").Select(c => c.Value)
                .First();

            List<Claim> refreshedTokenClaims = new()
            {
                new Claim("UserId", jwt.UserId),
                new Claim("UserRole", jwt.UserRole),
            };

            var refreshedToken = JwtHelper.GetJwt(cache.Get<string>(Configurations.CacheKeyUsersTsk),
                DateTime.Now.AddMinutes(cache.Get<int>(Configurations.CacheKeyUsersJwtExpiration)),
                refreshedTokenClaims);

            context.HttpContext.Response.Headers.Add("refreshedtoken", refreshedToken);
        }
        else
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}