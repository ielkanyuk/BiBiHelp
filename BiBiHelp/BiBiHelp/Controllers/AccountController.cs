using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using BiBiHelp.Helpers;
using BiBiHelp.Helpers.Enum;
using BiBiHelp.Model;
using BiBiHelp.Model.Interfaces.Authorization;
using BiBiHelp.Model.Services;
using BiBiHelp.Models;

namespace BiBiHelp.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string email, string password, double clientTimeOffset, bool remember = true)
        {
            ELoginResult result = ServiceManager.UserService.CanLogin(email, password);
            
            switch (result)
            {
                    case ELoginResult.Success:
                        ClientAuthService.Login(email, remember, clientTimeOffset);
                        return new AjaxResult(ReturnViewType.OperationSuccess);

                    case ELoginResult.UserBlocked:
                        return new AjaxResult(new { Data = new { message = "Пользователь заблокирован" } }, ReturnViewType.OperationFailed | ReturnViewType.Json);

                    case ELoginResult.UserRemoved:
                    case ELoginResult.UserNotFound:
                    case ELoginResult.InvalidPassword:
                    return new AjaxResult(new { Data = new { message = "Адрес почты или пароль введены неверно" } }, ReturnViewType.OperationFailed | ReturnViewType.Json);
            }

            return new AjaxResult(new { Data = new { message = "Пользователь заблокирован" } }, ReturnViewType.OperationFailed | ReturnViewType.Json);
            //if (ModelState.IsValid)
            //{
            //    if (Membership.ValidateUser(/*model.UserName, model.Password*/email, password))
            //    {
            //        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            //        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
            //            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            //        {
            //            return Redirect(returnUrl);
            //        }
            //        else
            //        {
            //            return RedirectToAction("Index", "App");
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "The user name or password provided is incorrect.");
            //    }
            //}
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "App");
        }

        /// <summary>
        /// Форма входа и регистрации для не залогиненых
        /// </summary>
        public ActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SignUp(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "App");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            return View(model);
        }
        
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
