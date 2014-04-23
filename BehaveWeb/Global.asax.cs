using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Behave.BehaveCore.DataClasses;
using Behave.BehaveCore.DBUtils;

namespace Behave.BehaveWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class BehaveMvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public const string AUTH_TOKEN_COOKIE_NAME = "AuthToken"; // Doesn't belong here

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            string token = String.Empty;

            if (application.Context.Request.Cookies[AUTH_TOKEN_COOKIE_NAME] != null)
            {
                token = application.Context.Request.Cookies[AUTH_TOKEN_COOKIE_NAME].Value;
            }

            application.Context.User = new UserImplementation(token);
        }

        public enum UserLevel
        {
            Anonymous,
            User,
            Admin
        }
    }

    public class UserImplementation : IPrincipal
    {
        public UserImplementation(string token)
        {
            _identity = new IdentityImplementation(token);
        }

        private IIdentity _identity;
        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }

    public class IdentityImplementation : IIdentity
    {


        public IdentityImplementation(string token)
        {
            _authToken = token;
        }
        private string _authToken;
        private SqlConnection _dbConn;


        public string AuthenticationType
        {
            get { return "random-auth-type"; }
        }

        public bool IsAuthenticated
        {
            get { return UserAccessLevel != BehaveMvcApplication.UserLevel.Anonymous; }
        }

        public string Name
        {
            get { return "UserBob"; }
        }

        public BehaveMvcApplication.UserLevel UserAccessLevel
        {
            get
            {
                BehaveMvcApplication.UserLevel accessLevel = BehaveMvcApplication.UserLevel.Anonymous;
                using (SqlConnection dbConn = Connection.Create())
                {
                    dbConn.Open();
                    if (new BehaveUser(_authToken).IsAuthenticated(dbConn))
                    {
                        accessLevel = BehaveMvcApplication.UserLevel.User;
                    }
                    dbConn.Close();
                }

                return accessLevel;
            }
        }
    }
}