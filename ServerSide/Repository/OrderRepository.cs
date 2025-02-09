using Domain.Database;
using Domain.DataModels;
using Domain.DataModels.ServerSide.Models.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;
using ServerSide.Models.DatabaseModel;
using ServerSide.Repository.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServerSide.Repository
{
    public class OrderRepository : Repository<OrderDbModel>
    {
        private readonly ApplicatonDbContext _context;

        public OrderRepository(ApplicatonDbContext context) : base(context)
        {
            _context = context;
        }
        public ReturnModel UpdateOrder(OrderDbModel model)
        {
            var existingOrder = _context.Orders
                .Include(o => o.Items) // Include existing items
                .FirstOrDefault(o => o.Id == model.Id);

            if (existingOrder == null)
                return new ReturnModel() { Comment = "order not found " , IsSucceeded = false };

            // Update scalar properties
            _context.Entry(existingOrder).CurrentValues.SetValues(model);

            // Handle Items
            foreach (var existingItem in existingOrder.Items.ToList())
            {
                // If the incoming items don't contain the existing item, remove it
                if (!model.Items.Any(i => i.id == existingItem.id))
                    _context.Orders.SingleOrDefault(x=>x.Id == existingOrder.Id).Items.Remove(existingItem);
            }

            foreach (var item in model.Items)
            {
                var existingItem = existingOrder.Items
                    .FirstOrDefault(i => i.id == item.id);

                if (existingItem != null)
                    // Update existing item
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
                else
                {
                    // Add new item
                    existingOrder.Items.Add(item);
                }
            }
        
            _context.SaveChanges();
            return new ReturnModel() { Comment = "Updated successfully ", IsSucceeded = true };
        }
        public async Task<List<OrderData>> GetOrdersBySelected (string Selected)
        {
            return await _context.Orders.Include(o => o.Items)
             .Where(x => x.Status == Selected)
             .Select(c => new OrderData
             {
                 Id = c.Id,
                 DeliveryDate = c.DeliveryDate,
                 OrderDate = c.OrderDate,
                 PhoneNumber = c.PhoneNumber,
                 Note = c.Note,
                 Status = c.Status,
                 CustomerName = c.CustomerName,
                 Items = c.Items.Select(item => new OrderItem
                     {
                         id = item.id,
                         Name = item.Name,
                         Price = item.Price,
                         Quantity = item.Quantity,
                     }).ToList()
             }).ToListAsync();

        }
        public async Task<List<OrderData>> GetOrdersAsync()
        {
            return await _context.Orders.Select(c => new OrderData
                {
                    Id = c.Id,
                    DeliveryDate = c.DeliveryDate,
                    OrderDate = c.OrderDate,
                    PhoneNumber = c.PhoneNumber,
                Note = c.Note,
                Status = c.Status,
                    CustomerName = c.CustomerName,
                    Items = c.Items.Select(item => new  OrderItem
                    {
                        id = item.id,
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                    }).ToList() 
                }).ToListAsync();
        }
    }

    }

