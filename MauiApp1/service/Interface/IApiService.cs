

using Domain.Data_Transfare_Object;
using Domain.DataModels;
using Domain.DataModels.ServerSide.Models.DatabaseModel;

namespace MauiApp1.service.Interface
{
    public interface IApiService
    {
        public  Task<ServiceReturnModel<List<CategoryData>>> GetCategorys();
        public Task<ServiceReturnModel<List<CandyData>>> GetCandies();
        public Task<ServiceReturnModel<List<CandyData>>> GetCandiesByCategory(int id);
        public Task<ServiceReturnModel<bool>> AddCandy(CandyData data);
        public Task<ServiceReturnModel<bool>> AddCategory(AddCategoryDto data);
        public Task<ServiceReturnModel<bool>> AddOrder(OrderData data);
        public Task<ServiceReturnModel<bool>> EditOrder(OrderData data);

        public Task<ServiceReturnModel<bool>> EditCandy(CandyData data);
        public Task<ServiceReturnModel<bool>> EditCategory(CategoryData data);
        public Task<ServiceReturnModel<bool>> RemoveOrder(int id);
        public Task<ServiceReturnModel<bool>> RemoveCandy(int id);
        public Task<ServiceReturnModel<bool>> RemoveCategory(int id);
        public Task<ServiceReturnModel<List<OrderData>>> GetOrdersAsync();
        public Task<ServiceReturnModel<List<OrderData>>> GetOrdersByFilterAsync(string Filter);

        public Task<ServiceReturnModel<bool>> EditOrderStatus(OrderData order,string newstatus);






    }
}
