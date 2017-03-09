using LacesAPI.Helpers;
using LacesDataModel.Image;
using LacesDataModel.Product;
using LacesDataModel.User;
using LacesViewModel.Request;
using LacesViewModel.Response;
using System;
using System.Configuration;
using System.IO;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    public class CommentController : ApiController
    {
        [HttpPost]
        public LacesResponse AddComment(AddCommentRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment();

                    // Confirm user and product exist
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);
                    Product product = new Product(request.ProductId);

                    comment.CreatedDate = DateTime.Now;
                    comment.ProductId = product.ProductId;
                    comment.Text = request.Text;
                    comment.UpdatedDate = DateTime.Now;
                    comment.UserId = user.UserId;

                    if (comment.Add())
                    {
                        response.Success = true;
                        response.Message = "Comment added succesfully.";
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
            catch
            {
                response = new LacesResponse();
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }

            return response;
        }

        [HttpPost]
        public LacesResponse UpdateComment(UpdateCommentRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment(request.CommentId);

                    comment.Text = request.Text;
                    comment.UpdatedDate = DateTime.Now;

                    if (comment.Update())
                    {
                        response.Success = true;
                        response.Message = "Comment updated succesfully.";
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

                if (ex.Message.Contains("find comment"))
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
        public GetCommentResponse GetComment(GetCommentRequest request)
        {
            GetCommentResponse response = new GetCommentResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment(request.CommentId);
                    LacesDataModel.User.User user = new LacesDataModel.User.User(comment.UserId);
                    Image userImage = new Image();

                    userImage.LoadAvatarByUserId(comment.UserId);

                    response.CreatedDate = comment.CreatedDate;
                    response.Text = comment.Text;
                    response.UpdatedDate = comment.UpdatedDate;

                    response.UserImage = new LacesViewModel.Response.ImageInfo();
                    response.UserImage.DateLastChanged = userImage.UpdatedDate;
                    response.UserImage.fileData = File.ReadAllBytes(userImage.FilePath);
                    response.UserImage.fileFormat = userImage.FileFormat;
                    response.UserImage.fileName = userImage.FileName;

                    response.UserName = user.UserName;

                    response.Success = true;
                    response.Message = "Comment details retrieved succesfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
                response = new GetCommentResponse();
                response.Success = false;

                if (ex.Message.Contains("find comment") || ex.Message.Contains("find user"))
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
        public LacesResponse RemoveComment(RemoveCommentRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment(request.CommentId);

                    if (comment.Delete())
                    {
                        response.Success = true;
                        response.Message = "Comment removed succesfully.";
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

                if (ex.Message.Contains("find comment"))
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