using ECommerce.Data.DBRepository.DataAccess;
using ECommerce.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.DataAccess
{
    public class DataAccessServices : IDataAccessServices
    {
        private readonly IDataAccessRepository dataAccessRepository;

        public DataAccessServices(IDataAccessRepository dataAccessRepository)
        {
            this.dataAccessRepository = dataAccessRepository;
        }

        public async Task<Cart> GetActiveCartOfUser(int UserId)
        {
            return await dataAccessRepository.GetActiveCartOfUser(UserId);
        }

        public async Task<List<Cart>> GetAllPriviousCartOfUser(int UserId)
        {
            return await dataAccessRepository.GetAllPriviousCartOfUser(UserId);
        }

        //public async Task<Cart> GetCart(int CartId)
        //{
        //    return await dataAccessRepository.GetCart(CartId);
        //}

        public async Task<List<CartItem>> GetCartItems(int cartId)
        {
            return await dataAccessRepository.GetCartItems(cartId);
        }

        public async Task<List<Offer>> GetOffers(int? Id)
        {
            return await dataAccessRepository.GetOffers(Id);
        }

        public async Task<List<PaymentMethod>> GetPaymentMethods()
        {
            return await dataAccessRepository.GetPaymentMethods();
        }

        public async Task<Product> GetProductById(int id)
        {
           return await dataAccessRepository.GetProductById(id);
        }

        public async Task<List<ProductCategory>> GetProductCategories(int? Id)
        {
            return await dataAccessRepository.GetProductCategories(Id);
        }

        public async Task<List<ReviewData>> GetProductReview(int productId)
        {
            return await dataAccessRepository.GetProductReview(productId);
        }

        public async Task<List<Product>> GetSuggestedProducts(string category, string SubCategory, int count)
        {
           return await dataAccessRepository.GetSuggestedProducts(category, SubCategory, count);
        }

        public Task<User> GetUserByID(int id)
        {
            return dataAccessRepository.GetUserByID(id);
        }

        public async Task<int> InsertCartItem(int UserId, int ProductId)
        {
            return await dataAccessRepository.InsertCartItem(UserId, ProductId);
        }

        public async Task<int> InsertOrder(Orders orders)
        {
            return await dataAccessRepository.InsertOrder(orders);
        }

        public async Task<int> InsertPayment(Payment payment)
        {
            return await dataAccessRepository.InsertPayment(payment);
        }

        public async Task<int> InsertReview(ReviewData reviewData)
        {
            return await dataAccessRepository.InsertReview(reviewData);
        }

        public async Task<bool> InsertUser(User user)
        {
            return await dataAccessRepository.InsertUser(user);
        }

        public async Task<User> IsUserPresent(LoginModel loginModel)
        {
            return await dataAccessRepository.IsUserPresent(loginModel);
        }
    }
}
