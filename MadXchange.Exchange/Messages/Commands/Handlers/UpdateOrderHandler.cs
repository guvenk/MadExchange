﻿using MadXchange.Common.Handlers;
using MadXchange.Common.Types;
using MadXchange.Exchange.Domain.Models;
using MadXchange.Exchange.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MadXchange.Exchange.Messages.Commands.Handlers
{
    public class UpdateOrderHandler : ICommandHandler<UpdateOrder>
    {

        private readonly IAsyncRepository<IOrder> _orderRepository;
        private readonly IExchangeOrderServiceClient _orderClient;
        private readonly ILogger _logger;
        public UpdateOrderHandler(IExchangeOrderServiceClient orderClient, IAsyncRepository<IOrder> orderRepository, ILogger<UpdateOrderHandler> logger) 
        {
            _orderClient = orderClient;
            _orderRepository = orderRepository;
            _logger = logger;
        
        }
        public async Task HandleAsync(UpdateOrder order) 
        {
            var update = await _orderClient.UpdateOrderAsync(order);
            if (update is null)
            {
                _logger.LogError($"");
            }
            else 
            {
                if(update.OrdStatus != OrderStatus.REJECTED) 
                {
                
                }
            }
        }
    }
}
