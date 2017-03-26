using LacesAPI.Helpers;
using LacesAPI.Models.Request;
using LacesAPI.Models.Response;
using LacesRepo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    [Authorize]
    public class NotificationController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public GetNotificationsResponse GetNotifications(LacesRequest request)
        {
            GetNotificationsResponse response = new GetNotificationsResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    StoredProcedure proc = new StoredProcedure(Constants.CONNECTION_STRING, Constants.STORED_PROC_GET_NOTIFICATIONS);

                    proc.AddInput("@userId", request.UserId, System.Data.SqlDbType.Int);

                    DataSet resultSet = proc.ExecuteDataSet();

                    if (resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows != null && resultSet.Tables[0].Rows.Count > 0)
                    {
                        response.Notifications = new List<Notification>();

                        foreach (DataRow row in resultSet.Tables[0].Rows)
                        {
                            Notification alert = new Notification();

                            alert.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            alert.NotificationType = Convert.ToInt32(row["NotificationTypeId"]);
                            alert.ProductId = Convert.ToInt32(row["ProductId"]);
                            alert.UserName = Convert.ToString(row["UserName"]);

                            response.Notifications.Add(alert);
                        }

                        response.Success = true;
                        response.Message = "Notifications retrieved succesfully.";
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = "Could not find any notifications to display.";
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
                response = new GetNotificationsResponse();
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public LacesResponse UpdateAlertCheckTime(LacesRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);

                    user.LastAlertCheck = DateTime.Now;

                    if (user.Update())
                    {
                        response.Success = true;
                        response.Message = "Operation complete.";
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