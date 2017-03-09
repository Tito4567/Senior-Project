using LacesAPI.Helpers;
using LacesDataModel.Image;
using LacesDataModel.Product;
using LacesDataModel.User;
using LacesViewModel.Request;
using LacesViewModel.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    public class ProductController : ApiController
    {
        [HttpPost]
        public AddProductResponse AddProduct(AddProductRequest request)
        {
            AddProductResponse response = new AddProductResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.SellerId); // Ensure user exists.

                    Product product = new Product();
                    product.AskingPrice = request.AskingPrice;
                    product.Brand = request.Brand;
                    product.ConditionId = request.ConditionId;
                    product.CreatedDate = DateTime.Now;
                    product.Description = request.Description;
                    product.Name = request.ProductName;
                    product.ProductStatudId = request.ProductStatudId;
                    product.ProductTypeId = request.ProductTypeId;
                    product.SellerId = user.UserId;
                    product.Size = request.Size;
                    product.UpdatedDate = DateTime.Now;

                    if (product.Add())
                    {
                        if (request.Images != null && request.Images.Count > 0)
                        {
                            int count = 0;

                            foreach (LacesViewModel.Request.ImageInfo image in request.Images)
                            {
                                string fileName = product.ProductId + "_" + count + "." + image.FileFormat;
                                string filePath = ConfigurationManager.AppSettings[Constants.APP_SETTING_PRODUCT_IMAGE_DIRECTORY] + fileName;

                                File.WriteAllBytes(filePath, image.ImageData);

                                Image productImage = new Image();

                                productImage.AssociatedEntityId = product.ProductId;
                                productImage.FileFormat = image.FileFormat;
                                productImage.FileName = fileName;
                                productImage.FilePath = filePath;
                                productImage.ImageEntityTypeId = (int)ImageEntityTypeOptions.Product;
                                productImage.CreatedDate = DateTime.Now;
                                productImage.UpdatedDate = DateTime.Now;
                                productImage.Add();
                            }
                        }

                        response.Success = true;
                        response.Message = "Product created with Id " + product.ProductId;
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
                response = new AddProductResponse();
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
        public LacesResponse UpdateProduct(UpdateProductRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Product product = new Product(request.ProductId);

                    if (product.SellerId == request.UserId)
                    {
                        product.AskingPrice = request.AskingPrice;
                        product.Brand = request.Brand;
                        product.ConditionId = request.ConditionId;
                        product.Description = request.Description;
                        product.Name = request.ProductName;
                        product.ProductStatudId = request.ProductStatudId;
                        product.ProductTypeId = request.ProductTypeId;
                        product.Size = request.Size;
                        product.UpdatedDate = DateTime.Now;

                        if (product.Update())
                        {
                            response.Success = true;
                            response.Message = "Product successfully updated.";
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
                        response.Message = "User cannot update this product.";
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

                if (ex.Message.Contains("find product"))
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
        public GetProductResponse GetProduct(GetProductRequest request)
        {
            GetProductResponse response = new GetProductResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Product product = new Product(request.ProductId);

                    if (product.ProductStatudId != (int)ProductStatusOptions.Removed)
                    {
                        LacesDataModel.User.User user = new LacesDataModel.User.User(product.SellerId);

                        response.AskingPrice = product.AskingPrice;
                        response.Brand = product.Brand;
                        response.Comments = new List<int>();

                        List<Comment> comments = Comment.GetCommentsForProduct(product.ProductId);

                        foreach (Comment comment in comments)
                        {
                            response.Comments.Add(comment.CommentId);
                        }

                        response.CommentCount = response.Comments.Count;
                        response.ConditionId = product.ConditionId;
                        response.CreatedDate = product.CreatedDate;
                        response.Description = product.Description;

                        List<UserLike> likes = UserLike.GetCommentsForProduct(product.ProductId); // Consider adding aggregate functions to repo classes

                        response.LikeCount = likes.Count;
                        response.Name = product.Name;
                        response.ProductImages = new List<LacesViewModel.Response.ImageInfo>();

                        List<Image> images = Image.GetImagesForProduct(product.ProductId);

                        foreach (Image image in images)
                        {
                            LacesViewModel.Response.ImageInfo imageInfo = new LacesViewModel.Response.ImageInfo();

                            imageInfo.DateLastChanged = image.UpdatedDate;
                            imageInfo.fileData = File.ReadAllBytes(image.FilePath);
                            imageInfo.fileFormat = image.FileFormat;
                            imageInfo.fileFormat = image.FileName;

                            response.ProductImages.Add(imageInfo);
                        }

                        response.Size = product.Size;
                        response.UserName = user.UserName;

                        Image userImage = new Image();

                        userImage.LoadAvatarByUserId(user.UserId);

                        response.UserProfilePic = new LacesViewModel.Response.ImageInfo();
                        response.UserProfilePic.DateLastChanged = userImage.UpdatedDate;
                        response.UserProfilePic.fileData = File.ReadAllBytes(userImage.FilePath);
                        response.UserProfilePic.fileFormat = userImage.FileFormat;
                        response.UserProfilePic.fileName = userImage.FileName;

                        response.Success = true;
                        response.Message = "Product details retrieved succesfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "That product has been removed and cannot be updated.";
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
                response = new GetProductResponse();
                response.Success = false;

                if (ex.Message.Contains("find user") || ex.Message.Contains("find product"))
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
        public LacesResponse RemoveProduct(RemoveProductRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Product product = new Product(request.ProductId);

                    product.ProductStatudId = (int)ProductStatusOptions.Removed;

                    if (product.Update())
                    {
                        response.Success = true;
                        response.Message = "Product succesfully removed.";
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
                response = new GetProductResponse();
                response.Success = false;

                if (ex.Message.Contains("find product"))
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