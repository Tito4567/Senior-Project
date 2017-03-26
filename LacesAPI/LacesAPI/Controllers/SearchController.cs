using LacesAPI.Helpers;
using LacesAPI.Models.Request;
using LacesAPI.Models.Response;
using LacesRepo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    [Authorize]
    public class SearchController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public SearchResponse Search(SearchRequest request)
        {
            SearchResponse response = new SearchResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    SearchEntity search = new SearchEntity();

                    search.ColumnsToReturn = new List<string>();
                    search.ConnectionString = Constants.CONNECTION_STRING;
                    search.PageSizeLimit = 50;
                    search.SchemaName = Constants.SCHEMA_DEFAULT;

                    if (request.SearchType == 0) // User
                    {
                        search.TableName = Constants.TABLE_USERS;

                        search.ColumnsToReturn.Add("UserId");

                        foreach (string keyword in request.Keywords)
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "DisplayName";
                            searchCond.Operator = Condition.Operators.Like;
                            searchCond.Value = "%" + keyword.Trim() + "%";

                            search.Conditions.Add(searchCond);
                        }

                        List<LacesDataModel.User.User> results = new GenericRepository<LacesDataModel.User.User>().Read(search);

                        if (results.Count > 0)
                        {
                            response.Users = new List<int>();

                            foreach (LacesDataModel.User.User user in results)
                            {
                                response.Users.Add(user.UserId);
                            }
                        }
                        else
                        {
                            response.Success = true;
                            response.Message = "No records could be found matching the search parameters.";
                        }
                    }
                    else
                    {
                        search.TableName = Constants.TABLE_PRODUCTS;

                        search.ColumnsToReturn.Add("ProductId");

                        if (request.SearchType == 1) // Shoes
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "ProductTypeId";
                            searchCond.Operator = Condition.Operators.EqualTo;
                            searchCond.Value = "3";

                            search.Conditions.Add(searchCond);
                        }
                        else // Hats
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "ProductTypeId";
                            searchCond.Operator = Condition.Operators.EqualTo;
                            searchCond.Value = "2";

                            search.Conditions.Add(searchCond);
                        }

                        foreach (string keyword in request.Keywords)
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "Name";
                            searchCond.Operator = Condition.Operators.Like;
                            searchCond.Value = "%" + keyword.Trim() + "%";

                            search.Conditions.Add(searchCond);
                        }

                        if (string.IsNullOrEmpty(request.Brand) == false)
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "Brand";
                            searchCond.Operator = Condition.Operators.Like;
                            searchCond.Value = "%" + request.Brand.Trim() + "%";

                            search.Conditions.Add(searchCond);
                        }

                        if (request.MaxPrice > 0)
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "AskingPrice";
                            searchCond.Operator = Condition.Operators.LessOrEqual;
                            searchCond.Value = Convert.ToString(request.MaxPrice);

                            search.Conditions.Add(searchCond);
                        }

                        if (request.MinPrice > 0)
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "AskingPrice";
                            searchCond.Operator = Condition.Operators.GreaterOrEqual;
                            searchCond.Value = Convert.ToString(request.MinPrice);

                            search.Conditions.Add(searchCond);
                        }

                        if (string.IsNullOrEmpty(request.Size) == false)
                        {
                            Condition searchCond = new Condition();
                            searchCond.Column = "Size";
                            searchCond.Operator = Condition.Operators.Like;
                            searchCond.Value = "%" + request.Size.Trim() + "%";

                            search.Conditions.Add(searchCond);
                        }

                        Condition statusCond = new Condition();
                        statusCond.Column = "ProductStatusId";
                        statusCond.Operator = Condition.Operators.EqualTo;
                        statusCond.Invert = true;
                        statusCond.Value = Convert.ToString((int)LacesDataModel.Product.ProductStatusOptions.Removed);

                        search.Conditions.Add(statusCond);

                        List<LacesDataModel.Product.Product> results = new GenericRepository<LacesDataModel.Product.Product>().Read(search);

                        if (results.Count > 0)
                        {
                            response.Products = new List<int>();

                            foreach (LacesDataModel.Product.Product product in results)
                            {
                                response.Products.Add(product.ProductId);
                            }
                        }
                        else
                        {
                            response.Success = true;
                            response.Message = "No records could be found matching the search parameters.";
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
                response = new SearchResponse();
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }

            return response;   
        }
    }
}