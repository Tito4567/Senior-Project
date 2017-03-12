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
         *  3. Throwing exceptions when something can't be found and setting response message based on custom exception content is lazy and could lead to problems down the line. This should
         *      be changed as soon as we have bandwidth to focus on polish.
         *  4. Too much logic is being implemented in controller classes. These should really only be used to validate the requests, the actual data manipulation should be done elsewhere.
         *      The best solution would probably be to add a LacesBusiness project to the solution, and move the assignment logic there.
         *  5. Update methods should be designed so that, if a value is not included in the request, it is not included in the update. This change will have to be made carefully, as some
         *      values may be deliberately getting updated to a blank.
         *  6. Request validation
         *  7. Some of the request types should probably be consolidated, as several contain the same or similar data.
         *  8. General refactoring
         *  9. Add error codes to make it easier for the app to process failure responses.
         *  10. Add "in" to LacesRepo.Condition operator types
         *  11. Replace stored procedures with code implementations ASAP (remove reference to LacesRepo).
         *  12. Add aggregate functions to repository class.
         *  13. For more secure transactions, the Transaction table should have a status field, and should be written to both before and after a sale.
         *  14. Connection string should probably be a config setting.
         *  15. Add tag logic to searching.
         *  16. Add() should return the new record's pkey.
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
                    LacesDataModel.User.User userResult = new LacesDataModel.User.User(request.UserIdToGet);

                    response.User = new LacesViewModel.Response.User();
                    
                    response.User.CreatedDate = userResult.CreatedDate;
                    response.User.Description = userResult.Description;

                    if (string.IsNullOrEmpty(userResult.DisplayName) == false)
                    {
                        response.User.DisplayName = userResult.DisplayName;
                    }
                    else
                    {
                        response.User.DisplayName = userResult.UserName;
                    }

                    response.User.Email = userResult.Email;
                    response.User.UserName = userResult.UserName;
                    response.User.FollowedUsers = userResult.UsersFollowed;
                    response.User.FollowingUsers = userResult.UsersFollowing;
                    response.User.ProfilePicture = new LacesViewModel.Response.ImageInfo();

                    Image profPic = new Image();

                    if (profPic.LoadAvatarByUserId(userResult.UserId))
                    {
                        response.User.ProfilePicture.DateLastChanged = profPic.UpdatedDate;
                        response.User.ProfilePicture.fileFormat = profPic.FileFormat;
                        response.User.ProfilePicture.fileName = profPic.FileName;
                        response.User.ProfilePicture.fileData = File.ReadAllBytes(profPic.FilePath);
                    }
                    else
                    {
                        response.User.ProfilePicture.DateLastChanged = userResult.CreatedDate;
                        response.User.ProfilePicture.fileFormat = ConfigurationManager.AppSettings[Constants.APP_SETTING_DEFAULT_PROFILE_PIC_FORMAT];
                        response.User.ProfilePicture.fileName = ConfigurationManager.AppSettings[Constants.APP_SETTING_DEFAULT_PROFILE_PIC_NAME];
                        response.User.ProfilePicture.fileData = File.ReadAllBytes(ConfigurationManager.AppSettings[Constants.APP_SETTING_DEFAULT_PROFILE_PIC_PATH]);
                    }
					
					response.User.Products = new List<int>();

                    List<LacesDataModel.Product.Product> products = LacesDataModel.Product.Product.GetProductsForUser(userResult.UserId);

                    foreach (LacesDataModel.Product.Product prod in products)
					{
						response.User.Products.Add(prod.ProductId);
					}
					
					response.User.ProductCount = response.User.Products.Count();

                    if (userResult.UserId != request.UserId)
                    {
                        UserFollow isBeingFollowed = new UserFollow();
                        isBeingFollowed.LoadByUserids(request.UserId, userResult.UserId);

                        if (isBeingFollowed.UserFollowId > 0)
                        {
                            response.IsBeingFollowed = true;
                        }
                        else
                        {
                            response.IsBeingFollowed = false;
                        }


                        UserFollow isFollowing = new UserFollow();
                        isFollowing.LoadByUserids(userResult.UserId, request.UserId);

                        if (isFollowing.UserFollowId > 0)
                        {
                            response.IsFollowing = true;
                        }
                        else
                        {
                            response.IsFollowing = false;
                        }
                    }
                    else
                    {
                        response.IsBeingFollowed = false;
                        response.IsFollowing = false;
                    }

                    response.Success = true;
                    response.Message = "User details retrieved succesfully.";
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

                    bool changed = false;

                    if (request.Description != null && request.Description != user.Description)
                    {
                        user.Description = request.Description;
                        changed = true;
                    }

                    if (request.DisplayName != null && request.DisplayName != user.DisplayName)
                    {
                        user.DisplayName = request.DisplayName;
                        changed = true;
                    }

                    if (changed)
                    {
                        if (user.Update())
                        {
                            response.Success = true;
                            response.Message = "User updated succesfully.";
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "An error occurred when communicating with the database.";
                        }
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = "No changes made.";
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
                response = new LacesResponse();
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
                        response.Message = "An error occurred when communicating with the database.";
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
                    LacesDataModel.User.User followingUser = new LacesDataModel.User.User(request.UserId);

                    UserFollow follow = new UserFollow();

                    follow.LoadByUserids(followingUser.UserId, followedUser.UserId); // Make sure follow does not already exist.

                    if (follow.UserFollowId == 0)
                    {
                        follow.FollowedUserId = followedUser.UserId;
                        follow.FollowingUserId = followingUser.UserId;
                        follow.CreatedDate = DateTime.Now;

                        if (follow.Add())
                        {
                            followedUser.UsersFollowing++;
                            followedUser.Update();

                            followingUser.UsersFollowed++;
                            followingUser.Update();

                            response.Success = true;
                            response.Message = "Operation completed successfully.";
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "Failed to add user follow.";
                        }
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = "User is already being followed.";
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
        public LacesResponse UnfollowUser(RemoveFollowRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User followedUser = new LacesDataModel.User.User(request.FollowedUserId);
                    LacesDataModel.User.User followingUser = new LacesDataModel.User.User(request.UserId);

                    UserFollow follow = new UserFollow();
                    follow.LoadByUserids(followingUser.UserId, followedUser.UserId);

                    if (follow.UserFollowId > 0 && follow.Delete())
                    {
                        followedUser.UsersFollowing--;
                        followedUser.Update();

                        followingUser.UsersFollowed--;
                        followingUser.Update();

                        response.Success = true;
                        response.Message = "Operation completed successfully";
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

                    bool success;

                    if (userAvatar.LoadAvatarByUserId(user.UserId))
                    {
                        userAvatar.FileFormat = request.ImageInfo.FileFormat;
                        userAvatar.UpdatedDate = DateTime.Now;
                        success = userAvatar.Update();
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
                        success = userAvatar.Add();
                    }

                    if (success)
                    {
                        response.Success = true;
                        response.Message = "User profile picture succesfully updated.";
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
        public LacesResponse LikeProduct(LikeProductRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    // Confirm user and product exist
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    UserLike like = new UserLike();
                    like.UserId = user.UserId;
                    like.ProductId = product.ProductId;
                    like.CreatedDate = DateTime.Now;

                    if (like.Add())
                    {
                        response.Success = true;
                        response.Message = "Operation completed.";
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
                response = new LacesResponse();
                response.Success = false;

                if (ex.Message.Contains("find user") || ex.Message.Contains("find product") || ex.Message.Contains("find like"))
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
        public LacesResponse RemoveLike(RemoveLikeRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    UserLike like = new UserLike(request.UserLikeId);

                    if (like.Delete())
                    {
                        response.Success = true;
                        response.Message = "Operation completed.";
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

                if (ex.Message.Contains("find like"))
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
        public LacesResponse AddToInterestQueue(UpdateInterestQueueRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    // Confirm user and product exist
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    UserInterestQueue userInterest = new UserInterestQueue();

                    userInterest.LoadByUserAndProductIds(user.UserId, product.ProductId);

                    if (userInterest.UserInterestQueueId == 0)
                    {
                        userInterest.UserId = user.UserId;
                        userInterest.ProductId = product.ProductId;
                        userInterest.Interested = true;
                        userInterest.CreatedDate = DateTime.Now;
                        userInterest.UpdatedDate = DateTime.Now;

                        if (userInterest.Add())
                        {
                            response.Success = true;
                            response.Message = "Operation completed.";
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "An error occurred when communicating with the database.";
                        }
                    }
                    else if (userInterest.Interested == false)
                    {
                        userInterest.Interested = true;
                        userInterest.UpdatedDate = DateTime.Now;

                        if (userInterest.Update())
                        {
                            response.Success = true;
                            response.Message = "Operation completed.";
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "An error occurred when communicating with the database.";
                        }
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = "Operation completed.";
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
                response = new LacesResponse();
                response.Success = false;

                if (ex.Message.Contains("find user") || ex.Message.Contains("find product") || ex.Message.Contains("find like"))
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
        public LacesResponse RemoveFromInterestQueue(UpdateInterestQueueRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    // Confirm user and product exist
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    UserInterestQueue userInterest = new UserInterestQueue();

                    userInterest.LoadByUserAndProductIds(user.UserId, product.ProductId);

                    if (userInterest.UserInterestQueueId == 0)
                    {
                        userInterest.UserId = user.UserId;
                        userInterest.ProductId = product.ProductId;
                        userInterest.Interested = false;

                        if (userInterest.Add())
                        {
                            response.Success = true;
                            response.Message = "Operation completed.";
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "An error occurred when communicating with the database.";
                        }
                    }
                    else if (userInterest.Interested == true)
                    {
                        userInterest.Interested = false;

                        if (userInterest.Update())
                        {
                            response.Success = true;
                            response.Message = "Operation completed.";
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "An error occurred when communicating with the database.";
                        }
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = "Operation completed.";
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
                response = new LacesResponse();
                response.Success = false;

                if (ex.Message.Contains("find user") || ex.Message.Contains("find product") || ex.Message.Contains("find like"))
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
