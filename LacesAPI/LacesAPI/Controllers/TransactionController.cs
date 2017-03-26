using LacesAPI.Helpers;
using LacesAPI.Models.Request;
using LacesAPI.Models.Response;
using LacesDataModel.Product;
using LacesDataModel.Transaction;
using System;
using System.Configuration;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    [Authorize]
    public class TransactionController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public LacesResponse ProcessTransaction(ProcessTransactionRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    LacesDataModel.User.User buyer = new LacesDataModel.User.User(request.BuyerId);
                    LacesDataModel.User.User seller = new LacesDataModel.User.User(request.SellerId);
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    Transaction trans = new Transaction();

                    trans.Amount = request.Amount;
                    trans.BuyerId = buyer.UserId;
                    trans.ProductId = product.ProductId;
                    trans.ReferenceNumber = request.ReferenceNumber;
                    trans.SellerId = seller.UserId;
                    trans.CreatedDate = DateTime.Now;

                    if (trans.Add())
                    {
                        product.ProductStatusId = (int)ProductStatusOptions.Sold;

                        product.Update();

                        response.Success = true;
                        response.Message = "Transaction data saved succesfully.";
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
    }
}