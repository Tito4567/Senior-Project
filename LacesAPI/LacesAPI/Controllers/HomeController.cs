using LacesAPI.Helpers;
using LacesRepo;
using LacesViewModel.Request;
using LacesViewModel.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    public class HomeController : ApiController
    {
        /*
         *  GetLocalFeed
         */

        [HttpPost]
        public GetFollowingFeedResponse GetFollowingFeed(LacesRequest request)
        {
            GetFollowingFeedResponse response = new GetFollowingFeedResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    StoredProcedure proc = new StoredProcedure(Constants.CONNECTION_STRING, Constants.STORED_PROC_GET_FOLLOWING_FEED);

                    proc.AddInput("@userId", request.UserId, System.Data.SqlDbType.Int);

                    DataSet resultSet = proc.ExecuteDataSet();

                    if (resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows != null && resultSet.Tables[0].Rows.Count > 0)
                    {
                        response.Products = new List<FeedItem>();

                        foreach (DataRow row in resultSet.Tables[0].Rows)
                        {
                            FeedItem item = new FeedItem();

                            int feedResultType = Convert.ToInt32(row["FeedResultType"]);

                            item.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);

                            switch (feedResultType)
                            {
                                case 0: item.FeedResultTypeMessage = string.Empty; break;
                                case 1: item.FeedResultTypeMessage = Convert.ToString(row["UserName"]) + " liked this."; break;
                                case 2: item.FeedResultTypeMessage = Convert.ToString(row["UserName"]) + " commented on this."; break;
                            }

                            item.ProductId = Convert.ToInt32(row["ProductId"]);

                            response.Products.Add(item);
                        }
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = "Could not find any products to display.";
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
                response = new GetFollowingFeedResponse();
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }

            return response;
        }

        [HttpPost]
        public GetInterestFeedResponse GetInterestFeed(LacesRequest request)
        {
            GetInterestFeedResponse response = new GetInterestFeedResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    StoredProcedure proc = new StoredProcedure(Constants.CONNECTION_STRING, Constants.STORED_PROC_GET_INTEREST_FEED);

                    proc.AddInput("@userId", request.UserId, System.Data.SqlDbType.Int);

                    DataSet resultSet = proc.ExecuteDataSet();

                    if (resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows != null && resultSet.Tables[0].Rows.Count > 0)
                    {
                        response.Products = new List<int>();

                        foreach (DataRow row in resultSet.Tables[0].Rows)
                        {
                            response.Products.Add(Convert.ToInt32(row["ProductId"]));
                        }

                        response.Success = true;
                        response.Message = "Operation complete.";
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = "Could not find any products to display.";
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
                response = new GetInterestFeedResponse();
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }

            return response;
        }
    }
}