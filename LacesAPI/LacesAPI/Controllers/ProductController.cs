using LacesAPI.Helpers;
using LacesDataModel.Image;
using LacesDataModel.Product;
using LacesDataModel.User;
using LacesRepo;
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
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId); // Ensure user exists.

                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product();
                    product.AskingPrice = request.AskingPrice;
                    product.Brand = request.Brand;
                    product.ConditionId = request.ConditionId;
                    product.CreatedDate = DateTime.Now;
                    product.Description = request.Description;
                    product.Name = request.ProductName;
                    product.ProductStatusId = (int)ProductStatusOptions.Open;
                    product.ProductTypeId = request.ProductTypeId;
                    product.SellerId = user.UserId;
                    product.Size = request.Size;
                    product.UpdatedDate = DateTime.Now;

                    if (product.Add())
                    {
                        if (request.Images != null && request.Images.Count > 0)
                        {
                            int count = 0;

                            /* This is needed until we can get the Id back from Add(). Remove ASAP */

                            List<LacesDataModel.Product.Product> queryResult = new List<LacesDataModel.Product.Product>();

                            SearchEntity search = new SearchEntity();

                            search.ColumnsToReturn = new List<string>();
                            search.ColumnsToReturn.Add("ProductId");

                            search.ConnectionString = Constants.CONNECTION_STRING;

                            search.OrderBy = new OrderBy();
                            search.OrderBy.Column = "ProductId";
                            search.OrderBy.Direction = OrderByDirection.DESC;

                            search.PageSizeLimit = 1;

                            search.SchemaName = Constants.SCHEMA_DEFAULT;
                            search.TableName = Constants.TABLE_PRODUCTS;

                            queryResult = new GenericRepository<LacesDataModel.Product.Product>().Read(search);

                            if (queryResult.Count > 0)
                            {
                                product.ProductId = queryResult[0].ProductId;
                            }

                            /* End remove section */    

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

                        if (request.Tags != null && request.Tags.Count > 0)
                        {
                            foreach (string tag in request.Tags)
                            {
                                LacesDataModel.Product.Tag newTag = new LacesDataModel.Product.Tag();

                                newTag.ProductId = product.ProductId;
                                newTag.Description = tag;

                                newTag.Add();
                            }
                        }

                        response.Success = true;
                        response.Message = "Product succesfully created.";
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
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    if (product.SellerId == request.UserId)
                    {
                        bool changed = false;

                        if (request.AskingPrice > 0)
                        {
                            product.AskingPrice = request.AskingPrice;
                            changed = true;
                        }

                        if (request.Brand != null)
                        {
                            product.Brand = request.Brand;
                            changed = true;
                        }

                        if (request.ConditionId > 0)
                        {
                            product.ConditionId = request.ConditionId;
                            changed = true;
                        }

                        if (request.Description != null)
                        {
                            product.Description = request.Description;
                            changed = true;
                        }

                        if (request.ProductName != null)
                        {
                            product.Name = request.ProductName;
                            changed = true;
                        }

                        if (request.ProductStatusId > 0)
                        {
                            product.ProductStatusId = request.ProductStatusId;
                            changed = true;
                        }

                        if (request.ProductTypeId > 0)
                        {
                            product.ProductTypeId = request.ProductTypeId;
                            changed = true;
                        }

                        if (request.Size != null)
                        {
                            product.Size = request.Size;
                            changed = true;
                        }

                        if (changed)
                        {
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
                            response.Success = true;
                            response.Message = "No changes were made.";
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
        public GetShortProductResponse GetShortProduct(GetProductRequest request)
        {
            GetShortProductResponse response = new GetShortProductResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    if (product.ProductStatusId != (int)ProductStatusOptions.Removed)
                    {
                        LacesDataModel.User.User user = new LacesDataModel.User.User(product.SellerId);

                        List<Comment> comments = Comment.GetCommentsForProduct(product.ProductId);

                        response.CommentCount = comments.Count;

                        List<UserLike> likes = UserLike.GetLikesForProduct(product.ProductId); // Consider adding aggregate functions to repo classes

                        response.LikeCount = likes.Count;
                        response.ProductImage = new LacesViewModel.Response.ImageInfo();

                        List<Image> images = Image.GetImagesForProduct(product.ProductId);

                        if (images.Count > 0)
                        {
                            response.ProductImage.DateLastChanged = images[0].UpdatedDate;
                            response.ProductImage.fileData = File.ReadAllBytes(images[0].FilePath);
                            response.ProductImage.fileFormat = images[0].FileFormat;
                            response.ProductImage.fileFormat = images[0].FileName;
                        }

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
                response = new GetShortProductResponse();
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
        public GetDetailedProductResponse GetDetailedProduct(GetProductRequest request)
        {
            GetDetailedProductResponse response = new GetDetailedProductResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    if (product.ProductStatusId != (int)ProductStatusOptions.Removed)
                    {
                        LacesDataModel.User.User user = new LacesDataModel.User.User(product.SellerId);

                        response.Product = new LacesViewModel.Response.Product();

                        response.Product.AskingPrice = product.AskingPrice;
                        response.Product.Brand = product.Brand;
                        response.Product.Comments = new List<int>();

                        List<Comment> comments = Comment.GetCommentsForProduct(product.ProductId);

                        foreach (Comment comment in comments)
                        {
                            response.Product.Comments.Add(comment.CommentId);
                        }

                        response.Product.CommentCount = response.Product.Comments.Count;
                        response.Product.ConditionId = product.ConditionId;
                        response.Product.CreatedDate = product.CreatedDate;
                        response.Product.Description = product.Description;

                        List<UserLike> likes = UserLike.GetLikesForProduct(product.ProductId); // Consider adding aggregate functions to repo classes

                        response.Product.LikeCount = likes.Count;
                        response.Product.Name = product.Name;
                        response.Product.ProductImages = new List<LacesViewModel.Response.ImageInfo>();

                        List<Image> images = Image.GetImagesForProduct(product.ProductId);

                        foreach (Image image in images)
                        {
                            LacesViewModel.Response.ImageInfo imageInfo = new LacesViewModel.Response.ImageInfo();

                            imageInfo.DateLastChanged = image.UpdatedDate;
                            imageInfo.fileData = File.ReadAllBytes(image.FilePath);
                            imageInfo.fileFormat = image.FileFormat;
                            imageInfo.fileFormat = image.FileName;

                            response.Product.ProductImages.Add(imageInfo);
                        }

                        response.Product.Size = product.Size;
                        response.UserName = user.UserName;

                        Image userImage = new Image();

                        userImage.LoadAvatarByUserId(user.UserId);

                        response.UserProfilePic = new LacesViewModel.Response.ImageInfo();
                        response.UserProfilePic.DateLastChanged = userImage.UpdatedDate;
                        response.UserProfilePic.fileData = File.ReadAllBytes(userImage.FilePath);
                        response.UserProfilePic.fileFormat = userImage.FileFormat;
                        response.UserProfilePic.fileName = userImage.FileName;
                        response.Product.Tags = new List<LacesViewModel.Response.Tag>();

                        List<LacesDataModel.Product.Tag> tags = LacesDataModel.Product.Tag.GetTagsForProduct(product.ProductId);

                        foreach (LacesDataModel.Product.Tag tag in tags)
                        {
                            LacesViewModel.Response.Tag respTag = new LacesViewModel.Response.Tag();

                            respTag.TagId = tag.TagId;
                            respTag.Description = tag.Description;

                            response.Product.Tags.Add(respTag);
                        }

                        UserInterestQueue interest = new UserInterestQueue();

                        interest.LoadByUserAndProductIds(user.UserId, product.ProductId);

                        if (interest.UserInterestQueueId > 0)
                        {
                            if (interest.Interested)
                            {
                                response.UserInterestStatus = (int)UserInterestStatusOption.Interested;
                            }
                            else
                            {
                                response.UserInterestStatus = (int)UserInterestStatusOption.Uninterested;
                            }
                        }
                        else
                        {
                            response.UserInterestStatus = (int)UserInterestStatusOption.Unknown;
                        }

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
                response = new GetDetailedProductResponse();
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
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    product.ProductStatusId = (int)ProductStatusOptions.Removed;

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
        public LacesResponse AddTag(AddTagRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId); // Verify product exists

                    LacesDataModel.Product.Tag tag = new LacesDataModel.Product.Tag();

                    tag.ProductId = product.ProductId;
                    tag.Description = request.Description;

                    if (tag.Add())
                    {
                        response.Success = true;
                        response.Message = "Tag succesfully added.";
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
        public LacesResponse RemoveTag(RemoveTagRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.Product.Tag tag = new LacesDataModel.Product.Tag(request.TagId);

                    if (tag.Delete())
                    {
                        response.Success = true;
                        response.Message = "Tag succesfully removed.";
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

                if (ex.Message.Contains("find tag"))
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