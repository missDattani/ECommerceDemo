using ECommerce.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.DataAccess
{
    public interface IDataAccessServices
    {
        Task<List<ProductCategory>> GetProductCategories(int? Id);

        Task<List<Offer>> GetOffers(int? Id);

        Task<List<Product>> GetSuggestedProducts(string category, string SubCategory, int count);
        Task<Product> GetProductById(int id);

        Task<bool> InsertUser(User user);

        Task<User> IsUserPresent(LoginModel loginModel);

        Task<int> InsertReview(ReviewData reviewData);

        Task<List<ReviewData>> GetProductReview(int productId);
        Task<User> GetUserByID(int id);
        Task<int> InsertCartItem(int UserId, int ProductId);
        Task<Cart> GetActiveCartOfUser(int UserId);
        //Task<Cart> GetCart(int CartId);
        Task<List<Cart>> GetAllPriviousCartOfUser(int UserId);
        Task<List<CartItem>> GetCartItems(int cartId);
        Task<List<PaymentMethod>> GetPaymentMethods();
        Task<int> InsertPayment(Payment payment);
        Task<int> InsertOrder(Orders orders);

    }
}
