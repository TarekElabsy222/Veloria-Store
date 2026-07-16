using AutoMapper;
using Veloria_Store.Application.Services.Interfaces;
using Veloria_Store.Application.ViewModels.Order;
using Veloria_Store.Domain.Repositories.Interfaces;

namespace Veloria_Store.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderAdminVM>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrderAsync();

            return _mapper.Map<List<OrderAdminVM>>(orders);
        }

        public async Task<OrderAdminDetailsVM?> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetOrderDetailsAsync(id);

            if (order == null)
                return null;

            return _mapper.Map<OrderAdminDetailsVM>(order);
        }

        public async Task<List<UserOrderVM>> GetUserOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetByUserAsync(userId);

            return _mapper.Map<List<UserOrderVM>>(orders);
        }

        public async Task UpdateStatusAsync(OrderStatusUpdateVM model)
        {
            var order = await _orderRepository.GetByIdAsync(model.Id);

            if (order == null)
                throw new Exception("Order not found.");

            order.Status = model.Status;

            _orderRepository.update(order);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
