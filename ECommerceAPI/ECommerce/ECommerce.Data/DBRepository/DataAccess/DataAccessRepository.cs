using ECommerce.Model.Config;
using ECommerce.Model.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;

namespace ECommerce.Data.DBRepository.DataAccess
{
    public class DataAccessRepository : BaseRepository, IDataAccessRepository
    {
        private readonly IConfiguration _configuration;

        public DataAccessRepository(IConfiguration configuration,IOptions<DataConfig> dataconfig) : base(configuration,dataconfig)
        {
            this._configuration = configuration;
        }


        public async Task<Cart> GetActiveCartOfUser(int UserId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", UserId);

            string connString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                var query = "sp_GetActiveCart"; // Replace with your actual stored procedure name
                using (var multi = await connection.QueryMultipleAsync("sp_GetActiveCart", parameter, commandType: CommandType.StoredProcedure))
                {
                    var cart = multi.Read<Cart>().FirstOrDefault();

                    if (cart.CartId == 0)
                    {
                        return new Cart
                        {
                            CartItems = new List<CartItem>(),
                            Ordered = false,
                            OrderedOn = ""
                        };
                    }

                    var cartItems = multi.Read<CartItem>().ToList();
                    cart.CartItems = cartItems;


                    // Retrieve the user (you'll need to implement this method)
                    cart.User = await this.GetUserByID(UserId);

                    return cart;
                }
            }
        }

        public async Task<List<Cart>> GetAllPriviousCartOfUser(int UserId)
        {
            string connString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");
            using (IDbConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();

                var query = "sp_GetAllPreviousCArtOfUser";
                var parameters = new { UserId = UserId };

                var result = dbConnection.Query<Cart>(query, parameters, commandType: CommandType.StoredProcedure);

                foreach (var cart in result)
                {     
                    cart.CartItems = await GetCartItems(cart.CartId);
                    cart.User = await this.GetUserByID(UserId);
                   
                }
                
                return result.ToList();
            }
        }


        public async Task<List<CartItem>> GetCartItems(int cartId)
        {
            var query = "sp_GetCartItems";
            var parameters = new { CartId = cartId };

            var cartItems = await QueryAsync<CartItem>(query, parameters,commandType:CommandType.StoredProcedure);

            return cartItems.ToList();
        }

      


        public async Task<List<Offer>> GetOffers(int? Id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", Id);
            var data = await QueryAsync<Offer>("sp_GetOffers", parameter, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<PaymentMethod>> GetPaymentMethods()
        {
            var data = await QueryAsync<PaymentMethod>("sp_SelectPaymentMethods",commandType:CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<Product> GetProductById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var data = await QueryFirstOrDefaultAsync<Product>("sp_GetProductById", parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<List<ProductCategory>> GetProductCategories(int? Id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", Id);
            var data = await QueryAsync<ProductCategory>("sp_GetProductCategories", parameter,commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<ReviewData>> GetProductReview(int productId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ProductId", productId);
            var data = await QueryAsync<ReviewData>("sp_GetReviews", parameter, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<Product>> GetSuggestedProducts(string category, string SubCategory, int count)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Category", category);
            parameter.Add("@Subcategory", SubCategory);
            parameter.Add("@Count", count);
            var data = await QueryAsync<Product>("sp_GetSuggestedProducts", parameter, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<User> GetUserByID(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", id);
            var data = await QueryFirstOrDefaultAsync<User>("sp_GetUserById", parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> InsertCartItem(int UserId, int ProductId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", UserId);
            parameter.Add("@ProductId", ProductId);
            var data = await ExecuteAsync<int>("sp_InsertCartItem",parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> InsertOrder(Orders orders)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", orders.UserId);
            parameter.Add("@CartId", orders.CartId);
            parameter.Add("@PaymentId", orders.PaymentId);
            var data = await ExecuteAsync<int>("sp_InsOrder", parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> InsertPayment(Payment payment)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@PaymentMethodId", payment.PaymentMethodId);
            parameter.Add("@UserId", payment.UserId);
            parameter.Add("@TotalAmt",payment.TotalAmount);
            parameter.Add("@ShippingCharges", payment.ShippingCharges);
            parameter.Add("@AmountReduced", payment.AmountReduced);
            parameter.Add("@AmountPaid", payment.AmountPaid);
            var data = await QueryFirstOrDefaultAsync<int>("sp_InsPayment", parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> InsertReview(ReviewData reviewData)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", reviewData.UserId);
            parameter.Add("@ProductId", reviewData.ProductId);
            parameter.Add("@Review", reviewData.Review);
            var data = await ExecuteAsync<int>("sp_InsertReview",parameter,commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<bool> InsertUser(User user)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@FirstName", user.FirstName);
            parameter.Add("@LastName", user.LastName);
            parameter.Add("@Email",user.Email);
            parameter.Add("@Address", user.Address);
            parameter.Add("@Mobile", user.Mobile);
            parameter.Add("@Password", user.Password);
            parameter.Add("@CreatedAt", user.CreatedAt);
            parameter.Add("@ModifiedAt", user.ModifiedAt);
            var data = await QueryFirstOrDefaultAsync<bool>("sp_InsertUser", parameter,commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<User> IsUserPresent(LoginModel loginModel)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Email", loginModel.Email);
            parameter.Add("@Password", loginModel.Password);
            var data = await QueryFirstOrDefaultAsync<User>("sp_UserLogin",parameter,commandType: CommandType.StoredProcedure);
            return data;
        }


    }
}
