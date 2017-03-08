using LacesAPI.Helpers;
using LacesDataModel;
using LacesDataModel.Image;
using LacesDataModel.Product;
using LacesDataModel.User;
using LacesViewModel.Request;
using LacesViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

                    response.User = new LacesViewModel.Response.User();
                    response.User.CreatedDate = user.CreatedDate;
                    response.User.Description = user.Description;

                    if (string.IsNullOrEmpty(user.DisplayName) == false)
                    {
                        response.User.DisplayName = user.DisplayName;
                    }
                    else
                    {
                        response.User.DisplayName = user.UserName;
                    }

                    response.User.Email = user.Email;
                    response.User.UserName = user.UserName;
                    response.User.FollowedUsers = user.FollowedUsers;
                    response.User.FollowingUsers = user.FollowingUsers;
                    response.User.UserId = user.UserId;
                    response.User.ProfilePicture = new ProfilePicture();

                    Image profPic = new Image();

                    if (profPic.LoadAvatarByUserId(user.UserId))
                    {
                        response.User.ProfilePicture.DateLastChanged = profPic.UpdatedDate;
                        response.User.ProfilePicture.fileFormat = profPic.FileFormat;
                        response.User.ProfilePicture.fileName = profPic.FileName;
                        response.User.ProfilePicture.fileData = File.ReadAllBytes(profPic.FilePath);
                    }
                    else
                    {
                        response.User.ProfilePicture.DateLastChanged = user.CreatedDate;
                        response.User.ProfilePicture.fileFormat = ConfigurationManager.AppSettings[Constants.APP_SETTING_DEFAULT_PROFILE_PIC_FORMAT];
                        response.User.ProfilePicture.fileName = ConfigurationManager.AppSettings[Constants.APP_SETTING_DEFAULT_PROFILE_PIC_NAME];
                        response.User.ProfilePicture.fileData = File.ReadAllBytes(ConfigurationManager.AppSettings[Constants.APP_SETTING_DEFAULT_PROFILE_PIC_PATH]);
                    }
					
					response.User.Products = new List<int>();
					
					List<Product> products = Product.GetProductsForUser(user.UserId);
					
					foreach (Product prod in products)
					{
						response.User.Products.Add(prod.ProductId);
					}
					
					response.User.ProductCount = response.User.Products.Count();

                    response.Success = true;
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

        [HttpPost]
        public LacesResponse UpdateUserInfo(UpdateUserInfoRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);

                    user.Description = request.Description;
                    user.DisplayName = request.DisplayName;

                    user.Update();

                    response.Success = true;
                    response.Message = "User updated succesfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
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

        [HttpPost]
        public LacesResponse ChangeUserPassword(UpdatePasswordRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);

                    user.Password = request.NewPassword;

                    if (user.UpdatePassword(request.OldPassword))
                    {
                        response.Success = true;
                        response.Message = "Update complete.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "An error occurred when communicating with the database.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;

                if (ex.Message.Contains("incorrect") || ex.Message.Contains("find user"))
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

        [HttpPost]
        public LoginUserResponse ValidateLogin(LoginUserRequest request)
        {
            LoginUserResponse response = new LoginUserResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User();
                    user.UserName = request.UserName;
                    user.Password = request.Password;

                    if (user.ValidateLogin())
                    {
                        if (user.UserId > 0)
                        {
                            response.UserId = user.UserId;
                            response.Success = true;
                            response.Message = "Validation succesful.";
                        }
                        else
                        {
                            response.UserId = 0;
                            response.Success = false;
                            response.Message = "Invalid credentials";
                        }
                    }
                    else
                    {
                        response = new LoginUserResponse();
                        response.UserId = 0;
                        response.Success = false;
                        response.Message = "An unexpected error has occurred; please verify the format of your request.";
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
                response = new LoginUserResponse();
                response.UserId = 0;
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }


            return response;
        }

        [HttpPost]
        public LacesResponse FollowUser(FollowUserRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User followedUser = new LacesDataModel.User.User(request.FollowedUserId);
                    LacesDataModel.User.User followingUser = new LacesDataModel.User.User(request.FollowingUserId);

                    UserFollow follow = new UserFollow();

                    follow.FollowedUserId = request.FollowedUserId;
                    follow.FollowingUserId = request.FollowingUserId;
                    
                    if (follow.Add())
                    {
                        followedUser.FollowingUsers++;
                        followedUser.Update();

                        followingUser.FollowedUsers++;
                        followingUser.Update();

                        response.Success = true;
                        response.Message = "Operation completed successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Failed to add user follow.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
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

        [HttpPost]
        public LacesResponse UpdateProfileImage(AddImageRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.AssociatedEntityId);

                    string fileName = user.UserName + "." + request.ImageInfo.FileFormat;
                    string filePath = ConfigurationManager.AppSettings[Constants.APP_SETTING_USER_AVATAR_DIRECTORY] + fileName;

                    File.WriteAllBytes(filePath, request.ImageInfo.ImageData);

                    Image userAvatar = new Image();

                    if (userAvatar.LoadAvatarByUserId(user.UserId))
                    {
                        userAvatar.FileFormat = request.ImageInfo.FileFormat;
                        userAvatar.UpdatedDate = DateTime.Now;
                        userAvatar.Update();
                    }
                    else
                    {
                        userAvatar.AssociatedEntityId = user.UserId;
                        userAvatar.FileFormat = request.ImageInfo.FileFormat;
                        userAvatar.FileName = fileName;
                        userAvatar.FilePath = filePath;
                        userAvatar.ImageEntityTypeId = (int)ImageEntityTypeOptions.User;
                        userAvatar.CreatedDate = DateTime.Now;
                        userAvatar.UpdatedDate = DateTime.Now;
                        userAvatar.Add();
                    }

                    response.Success = true;
                    response.Message = "User profile picture succesfully updated.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
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
