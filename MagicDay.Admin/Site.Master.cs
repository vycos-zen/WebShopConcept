//using MagicDay.BusinessLogic.Classes;
using MagicDay.BusinessLogic.DataAccess;
using MagicDay.BusinessLogic.Authentication;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MagicDay.DataModel;
using MagicDay.BusinessLogic.General;

namespace MagicDay.Admin
{

    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pageHeaderText.Text = "MagicDay Management";
            RegisterClientScripts();
        }
        protected void RegisterClientScripts()
        {
            string clientScripts = "clientScripts";
            Type scriptType = this.GetType();
            ClientScriptManager masterScriptManager = Page.ClientScript;

            if (!masterScriptManager.IsStartupScriptRegistered(scriptType, clientScripts))
            {
                StringBuilder clientScriptsBuilder = new StringBuilder();
                clientScriptsBuilder.Append("<script src=\"Scripts/jquery-3.1.1.js\" type=\"text/javascript\"></script>");
                clientScriptsBuilder.Append("<script src=\"Scripts/PageControl.js\" type=\"text/javascript\"></script>");
                masterScriptManager.RegisterStartupScript(scriptType, clientScripts, clientScriptsBuilder.ToString(), false);
            }
        }
        protected void navHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", true);
        }
        protected void navCategories_Click(object sender, EventArgs e)
        {
            Response.Redirect(NavigationHelper.CategoriesView(), true);
        }
        protected void navProducts_Click(object sender, EventArgs e)
        {
            Response.Redirect(NavigationHelper.ProductsView(), true);
        }

        //registration parts retained during development phase
        protected void registerUser_Click(object sender, EventArgs e)
        {
            // not fully implemented

            //var userStore = new UserStore<IdentityUser>();
            //var manager = new UserManager<IdentityUser>(userStore);
            //var user = new IdentityUser() { UserName = userEmail.Text };
            // IdentityResult result = manager.Create(user, userPwd.Text);
            // if (result.Succeeded)
            //  {
            //using (MagicDayEntities dataModel = new MagicDayEntities())
            //{

            //    if (!IsUserExists(userEmail.Text) && Page.IsValid)
            //    {
            //        User mdUser = new User();
            //        mdUser.UserID = Guid.NewGuid();
            //        mdUser.UserFullName = userFullName.Text;
            //        mdUser.UserEmail = userEmail.Text;
            //        mdUser.UserPasswordHash = mdOperations.HashPassword(userPwd.Text);
            //        dataModel.Users.Add(mdUser);
            //        dataModel.SaveChanges();
            //        registrationToggle();
            //    }
            //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            //authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
            //Response.Redirect("~/Default.aspx");
            // }
            //  }
            //   else
            // {
            // }
        }
        public bool IsUserExists(string userEmailToCheck)
        {
            return LoginManager.IsUserExists(userEmailToCheck);
        }
        protected void loginPanelButton_Click(object sender, EventArgs e)
        {
            loginToggle();
        }
        protected void registerPanelButton_Click(object sender, EventArgs e)
        {
            registrationToggle();
        }
        protected void loginToggle()
        {
            //registration parts retained during development phase
            loginPanel.Visible = !loginPanel.Visible;
            loginPanelButton.Text = (loginPanelButton.Text == "Login") ? "Cancel" : "Login";
            // registerPanelButton.Text = "Register";
            //  registerPanel.Visible = false;
            // headerBody.CssClass = (registerPanel.Visible || loginPanel.Visible) ? "headerBodyExpanded" : "headerBodyNormal";
            headerBody.CssClass = (loginPanel.Visible) ? "headerBodyExpanded" : "headerBodyNormal";
        }
        protected void registrationToggle()
        {
            //registration parts retained during development phase

            // registerPanel.Visible = !registerPanel.Visible;
            // registerPanelButton.Text = (registerPanelButton.Text == "Register") ? "Cancel" : "Register";
            loginPanelButton.Text = "Login";
            loginPanel.Visible = false;
            // headerBody.CssClass = (registerPanel.Visible || loginPanel.Visible) ? "headerBodyExpanded" : "headerBodyNormal";
            headerBody.CssClass = (loginPanel.Visible) ? "headerBodyExpanded" : "headerBodyNormal";
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            User activeUser = new User();
            string enteredPassword = ((TextBox)loginControl.FindControl("Password")).Text;
            string enteredUsername = ((TextBox)loginControl.FindControl("userName")).Text;
            User userIsValid = LoginManager.UserLoginCheck(enteredUsername, enteredPassword);

            if (userIsValid != null)
            {
                activeUser = userIsValid;
                greetingsLabel.Text = "Welcome, " + activeUser.FullName;
                loginToggle();
            }
            else
            {
                greetingsLabel.Text = "-";
            }
        }
    }
}