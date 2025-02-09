using Domain.Database;
using Domain.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Models.DatabaseModel;
using ServerSide.Repository;

namespace ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        public OrderController(OrderRepository _order)
        {
            _orderRepository = _order;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _orderRepository.GetOrdersAsync()));
        }
        [HttpGet("SortBySelected/{Selected}")]
        public async Task<IActionResult> SortBySelected(string Selected)
        {
            var russlt = await _orderRepository.GetOrdersBySelected(Selected);
            return  (russlt).Count() !=0 ? Ok(russlt) : BadRequest(russlt);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderData data)
        {

            OrderDbModel model = new()
            {
                Id = 0,
                Items = data.Items.Select(item => new OrderDbItem
                {
                    id = item.id,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                }).ToList(),
                CustomerName = data.CustomerName,
                DeliveryDate = data.DeliveryDate,
                OrderDate = data.OrderDate,
                PhoneNumber = data.PhoneNumber,
                Status = data.Status,
                Note = data.Note,
              
            };
            return _orderRepository.Insert(model).IsSucceeded ? Ok("Order Inserted") : BadRequest();
        }
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {          
            return _orderRepository.Delete(id).IsSucceeded ? Ok("Order Deleted") : BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> EditOrder(OrderData data)
        {
            var NewModel = new OrderDbModel()
            {
                Id = data.Id,
                DeliveryDate = data.DeliveryDate,
                OrderDate = data.OrderDate,
                PhoneNumber = data.PhoneNumber,
                Status = data.Status,
                Items = data.Items.Select(x=> new OrderDbItem()
                {
                    id = x.id,
                    OrderDbId = data.Id,
                    Name = x.Name,
                    Price= x.Price,
                    Quantity = x.Quantity,
                    
                }).ToList(),
                Note= data.Note,
                CustomerName = data.CustomerName,
            };
            var ret = _orderRepository.Update(NewModel);
            return ret.IsSucceeded ? Ok(ret.Comment) : BadRequest(ret.Comment);

        }
    }
}
