﻿using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using MadXchange.Connector.Messages.Commands;
using MadXchange.Exchange.Domain.Models;
using MadXchange.Exchange.Dto;
using System;

namespace MadXchange.Connector.Messages.Events
{
    public class OrderRejectedEvent : IRejectedEvent
    {
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
        public ICommand Command { get; }
        public Order Order { get; }
        public Guid Id { get; } = Guid.NewGuid();
        public string Reason { get; }
        public string Code { get; }

        public OrderRejectedEvent(CreateOrder command, Order order)
        {
            Command = command;
            Order = order;
        }

        

    }
}
