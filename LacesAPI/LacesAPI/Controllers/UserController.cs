using LacesAPI.Helpers;
using LacesDataModel;
using LacesViewModel.Request;
using LacesViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    /// <summary>
    ///     Author:     Zachary Munson
    ///     Date:       03/02/2017
    ///     Purpose:    Controller to handle web requests for the Laces system. Depending on the number of request types we implement, this class may need to be broken up into multiple
    ///                 controllers based on category of request.
    /// </summary>
    public class UserController : ApiController
    {
        /* TODO:
         *  1. Using a static string as a security token is not very secure, and is a temporary measure. A better authentication method should be implemented before a Production release.
         *  2. When an error occurs, a generic error response is returned. It would be a good idea to implement a logging service so that errors can be tracked in detail on the server side.
         */

        [HttpPost]
        public AddUserResponse AddUser(AddUserRequest request)
        {
            AddUserResponse response = new AddUserResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User();

                    user.CreatedDate = DateTime.Now;
                    user.Description = request.Description;
                    user.DisplayName = request.DisplayName;
                    user.Email = request.Email;
                    user.Password = request.Password;
                    user.UserName = request.UserName;

                    bool userNameValid = true;
                    bool emailValid = true;

                    if (user.UserNameInUse())
                    {
                        userNameValid = false;
                    }

                    if (user.EmailInUse())
                    {
                        emailValid = false;
                    }

                    if (userNameValid && emailValid)
                    {
                        if (user.Add())
                        {
                            response.Success = true;
                            response.Message = "User succesfully created with Id: " + user.UserId;
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "An error occurred while processing your request.";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Username or email address is already in use.";

                        if (userNameValid == false)
                        {
                            response.UserNameTaken = true;
                        }

                        if (emailValid == false)
                        {
                            response.EmailTaken = true;
                        }
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch 
            {
                response = new AddUserResponse();
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }

            return response;   
        }

        // If authentication is changed to be based on a request header rather than a security token passed in a request object, this could feasibly be changed to a GET request.
        [HttpPost]
        public GetUserResponse GetUser(GetUserRequest request)
        {
            GetUserResponse response = new GetUserResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);

                    response.User = new User();
                    response.User.CreatedDate = user.CreatedDate;
                    response.User.Description = user.Description;
                    response.User.DisplayName = user.DisplayName;
                    response.User.Email = user.Email;
                    response.User.UserName = user.UserName;
                    response.User.UserId = user.UserId;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
                response = new GetUserResponse();
                response.User = null;
                response.Success = false;

                if (ex.Message.Contains("find user"))
                {
                    response.Message = ex.Message;
                }
                else
                {
                    response.Message = "An unexpected error has occurred; please verify the format of your request.";
                }
            }

            return response;
        }
    }
}
