using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tmoWebAPI3.Models;

namespace tmoWebAPI3.Infrastructure
{
	public class ApplicationRoleManager : RoleManager<IdentityRole>
	{
		public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
			: base(roleStore)
		{
		}

		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
		{
			var appRoleManager = new tmoWebAPI3.Infrastructure.ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));

			return appRoleManager;
		}
	}
}