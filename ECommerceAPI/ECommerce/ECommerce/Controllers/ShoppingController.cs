using ECommerce.Common;
using ECommerce.Common.Token;
using ECommerce.Model.JwtSetting;
using ECommerce.Model.ViewModels;
using ECommerce.Services.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IDataAccessServices dataAccessServices;
        private readonly JwtSetter jWTToken;

        public ShoppingController(IDataAccessServices dataAccessServices, IOptions<JwtSetter> jWTToken)
        {
               this.dataAccessServices = dataAccessServices;
            this.jWTToken = jWTToken.Value;
        }

        [HttpGet("get-product-categories")]
        public async Task<ApiResponse<ProductCategory>> GetProductCategory(int? Id)
        {
            ApiResponse<ProductCategory> response = new ApiResponse<ProductCategory>();

            var result = await dataAccessServices.GetProductCategories(Id);
            if(result != null)
            {
                response.Data = result;
                response.Success = true;
            }
            return response;
        }

        [HttpGet("get-offers")]
        public async Task<ApiResponse<Offer>> GetOffers(int? Id)
        {
            ApiResponse<Offer> response = new ApiResponse<Offer>();

            var result = await dataAccessServices.GetOffers(Id);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
            }
            return response;
        }

        [HttpGet("get-suggested-products")]
        public async Task<ApiResponse<Product>> GetSuggestedProducts(string category,string SubCategory,int count)
        {
            ApiResponse<Product> response = new ApiResponse<Product>();

            var result = await dataAccessServices.GetSuggestedProducts(category,SubCategory,count);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
            }
            return response;
        }

        [HttpGet("get-product-by-id")]
        public async Task<ApiResponse<Product>> GetProductById(int Id)
        {
            ApiResponse<Product> response=new ApiResponse<Product>();
            var result = await dataAccessServices.GetProductById(Id);
            if(result != null)
            {
                response.AnyData = result;
                response.Success = true;
            }
            return response;
        }

        [HttpPost("register-user")]
        public async Task<ApiPostResponse<bool>> InsertUser(User user)
        {
            ApiPostResponse<bool> response = new ApiPostResponse<bool>();

            var result = await dataAccessServices.InsertUser(user);
            
            if(result != false)
            {
                response.Success = true;
                response.Message = "Register Successfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong!";
            }
            return response;

        }

        [HttpPost("login-user")]

        public async Task<ApiPostResponse<User>> LoginUser(LoginModel loginModel)
        {
           ApiPostResponse<User> response = new ApiPostResponse<User>();
  
            var result = await dataAccessServices.IsUserPresent(loginModel);
            if(result != null)
            {
                var token = JWTToken.GenerateToken(result,jWTToken.JWT_Secret, jWTToken.Duration);
                response.Token = token;
                response.Data = result;
                response.Message = "Login Successfully";
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Invalid Credentials";
            }
            return response;
        }

        [HttpPost("insert-review")]
        public async Task<ApiPostResponse<int>> InsertReview(ReviewData reviewData)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();
            var result = await dataAccessServices.InsertReview(reviewData);
            if(result == 1)
            {
                response.Success = true;
                response.Message = "Review Added Successfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong!";

            }
            return response;
        }

        [HttpGet("review-list")]
        public async Task<ApiResponse<ReviewData>> GetProductReviewList(int ProductId)
        {
            ApiResponse<ReviewData> response = new ApiResponse<ReviewData>();

            var result = await dataAccessServices.GetProductReview(ProductId);
            if(result != null)
            {
                response.Success = true;
                response.Data = result;
            }
            return response;
        }

        [HttpPost("add-to-cart")]
        public async Task<ApiPostResponse<int>> InsertCartItems(int UserId,int ProductId)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();
            var result = await dataAccessServices.InsertCartItem(UserId, ProductId);

            if(result == 2 || result == 1)
            {
                response.Success = true;
                response.Message = "Item Added TO Cart";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong";
            }
            return response;
        }


        [HttpGet("get-active-cart")]
        public async Task<ApiResponse<Cart>> GetActiveCartOfUser(int userId)
        {
            ApiResponse<Cart> response = new ApiResponse<Cart>();
            var result = await dataAccessServices.GetActiveCartOfUser(userId);
            if (result != null)
            {
                response.Success = true;
                response.AnyData = result;
            }
            return response;
        }

        [HttpGet("get-privious-carts")]
        public async Task<ApiResponse<Cart>> GetAllPriviousCartsOfUser(int UserId)
        {
            ApiResponse<Cart> response = new ApiResponse<Cart>();
            var result = await dataAccessServices.GetAllPriviousCartOfUser(UserId);
            if(result != null)
            {
                response.Success = true;
                response.Data = result;
            }
            return response;
        }

        //[HttpGet("get-cart")]
        //public async Task<ApiResponse<Cart>> GetCart(int CartId)
        //{
        //    ApiResponse<Cart> response = new ApiResponse<Cart>();
        //    var result = await dataAccessServices.GetCart(CartId);
        //    if(result != null)
        //    {
        //        response.Success = true;
        //        response.AnyData = result;
        //    }
        //    return response;
        //}

        [HttpGet("get-cartitems")]
        public async Task<ApiResponse<CartItem>> GetCartItems(int CartId)
        {
            ApiResponse<CartItem> response = new ApiResponse<CartItem>();
            var result = await dataAccessServices.GetCartItems(CartId);
            if(result != null ) 
            { 
                response.Success = true;
                response.Data = result;
            }
            return response;
        }

        [HttpGet("get-payment-methods")]
        public async Task<ApiResponse<PaymentMethod>> GetPaymentMethodList()
        {
            ApiResponse<PaymentMethod> response = new ApiResponse<PaymentMethod>();
            var result = await dataAccessServices.GetPaymentMethods();

            if(result != null)
            {
                response.Success = true;
                response.Data = result;
            }
            return response;
        }

        [HttpPost("insert-payment")]
        public async Task<ApiPostResponse<int>> InsertPayment(Payment payment)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();
            var result = await dataAccessServices.InsertPayment(payment);
            if(result != 0)
            {
                response.Data = result;
                response.Success = true;
                response.Message = "Inserted Successfully!!";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }

        [HttpPost("insert-order")]
        public async Task<ApiPostResponse<int>> InsertOrders(Orders orders)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();
            var result = await dataAccessServices.InsertOrder(orders);
            if (result != 0)
            {
                response.Success = true;
                response.Message = "Inserted Successfully!!";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}
