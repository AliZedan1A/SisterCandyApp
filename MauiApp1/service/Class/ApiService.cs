
using Domain.Data_Transfare_Object;
using Domain.DataModels;
using Domain.DataModels.ServerSide.Models.DatabaseModel;
using MauiApp1.service.Interface;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MauiApp1.service.Class
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiService(IHttpClientFactory client)
        {
            _httpClientFactory = client;
        }
        public async Task<ServiceReturnModel<bool>> AddCandy(CandyData data)
        {
            data.Category = string.Empty;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonData = JsonSerializer.Serialize(data, options);

            var client = _httpClientFactory.CreateClient("Server");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Candy/AddCandy", content);
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }
        

        public async Task<ServiceReturnModel<bool>> AddCategory(AddCategoryDto data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
             string jsonData = JsonSerializer.Serialize(data, options);

            var client = _httpClientFactory.CreateClient("Server");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Candy/AddCategory", content);
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> AddOrder(OrderData data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonData = JsonSerializer.Serialize(data, options);
            var client = _httpClientFactory.CreateClient("Server");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Order", content);
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> EditCandy(CandyData data)
        {
            var x = (await GetCandies()).Value.SingleOrDefault(x => x.Id == data.Id).CategoryId;
            if (data.CategoryId == 0)
            {
            }

            data.Category = string.Empty;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonData = JsonSerializer.Serialize(data, options); 
            var client = _httpClientFactory.CreateClient("Server");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Candy/UpdateCandy", content);
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> EditCategory(CategoryData data)
        {

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonData = JsonSerializer.Serialize(data, options);
            var client = _httpClientFactory.CreateClient("Server");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Candy/UpdateCategory", content);
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> EditOrder(OrderData data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonData = JsonSerializer.Serialize(data, options);
            var client = _httpClientFactory.CreateClient("Server");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Order", content);
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> EditOrderStatus(OrderData order, string newstatus)
        {
            var client = _httpClientFactory.CreateClient("Server");
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            order.Status = newstatus;
            string jsonData = JsonSerializer.Serialize(order, options);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Order", content);

            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true, Value = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<List<CandyData>>> GetCandies()
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.GetAsync("Candy/GetCandies");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseobject = await response.Content.ReadFromJsonAsync<List<CandyData>>();

                    return new() { IsSucceeded = true, Value = responseobject };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<List<CandyData>>> GetCandiesByCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.GetAsync($"Candy/GetCandisByCategory/{id}");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseobject = await response.Content.ReadFromJsonAsync<List<CandyData>>();

                    return new() { IsSucceeded = true, Value = responseobject };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<List<CategoryData>>> GetCategorys()
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.GetAsync("Candy/GetCategorys");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseobject = await response.Content.ReadFromJsonAsync<List<CategoryData>>();

                    return new() { IsSucceeded = true, Value = responseobject };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false,Comment = ex.Message };

            }

        }

        public async Task<ServiceReturnModel<List<OrderData>>> GetOrdersAsync()
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.GetAsync("Order");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseobject = await response.Content.ReadFromJsonAsync<List<OrderData>>();

                    return new() { IsSucceeded = true, Value = responseobject };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<List<OrderData>>> GetOrdersByFilterAsync(string Filter)
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.GetAsync($"Order/SortBySelected/{Filter}");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseobject = await response.Content.ReadFromJsonAsync<List<OrderData>>();

                    return new() { IsSucceeded = true, Value = responseobject };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> RemoveCandy(int id)
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.DeleteAsync($"Candy/DeleteCandy/{id}");
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> RemoveCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.DeleteAsync($"Candy/DeleteCategory/{id}");
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }

        public async Task<ServiceReturnModel<bool>> RemoveOrder(int id)
        {
            var client = _httpClientFactory.CreateClient("Server");
            var response = await client.DeleteAsync($"Order/Remove/{id}");
            try
            {
                if (response.IsSuccessStatusCode)
                {

                    return new() { IsSucceeded = true };
                }
                else
                {
                    return new() { IsSucceeded = false, Comment = response.StatusCode.ToString() };

                }
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };

            }
        }
    }
}
