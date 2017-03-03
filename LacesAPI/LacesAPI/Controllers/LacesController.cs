using LacesAPI.Helpers;
using LacesDataModel;
using LacesViewModel.Request;
using LacesViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    public class LacesController : ApiController
    {
        public HttpResponseMessage AddUser(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            AddUserResponse userResponse = new AddUserResponse();

            try
            {
                string requestData = request.Content.ReadAsStringAsync().Result;

                AddUserRequest userRequest = JsonConvert.DeserializeObject<AddUserRequest>(requestData);

                if (userRequest.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User();

                    user.CreatedDate = DateTime.Now;
                    user.Description = userRequest.Description;
                    user.DisplayName = userRequest.DisplayName;
                    user.Email = userRequest.Email;
                    user.Password = userRequest.Password;
                    user.UserName = userRequest.UserName;

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
                            userResponse.Success = true;
                            userResponse.Message = "User succesfully created with Id: " + user.UserId;
                        }
                        else
                        {
                            userResponse.Success = false;
                            userResponse.Message = "An error occurred while processing your request.";
                        }
                    }
                    else
                    {
                        userResponse.Success = false;
                        userResponse.Message = "Username or email address is already in use.";

                        if (userNameValid == false)
                        {
                            userResponse.UserNameTaken = true;
                        }

                        if (emailValid == false)
                        {
                            userResponse.EmailTaken = true;
                        }
                    }
                }
                else
                {
                    userResponse.Success = false;
                    userResponse.Message = "Invalid security token.";
                }
            }
            catch 
            {
                userResponse = new AddUserResponse();
                userResponse.Success = false;
                userResponse.Message = "An unexpected error has occurred.";
            }

            string responseData = JsonConvert.SerializeObject(userResponse);

            response.Content = new StringContent(responseData, Encoding.UTF8, "application/json");

            return response;   
        }
    }
}
