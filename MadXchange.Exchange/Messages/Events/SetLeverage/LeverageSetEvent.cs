﻿using MadXchange.Common.Messages;
using MadXchange.Exchange.Domain.Models;
using System;

namespace MadXchange.Exchange.Messages.Events
{
    public class LeverageSetEvent : IEvent
    {
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
        public ICommand Command { get; }
        public IPosition Position { get; }
        public Guid Id { get; } = Guid.NewGuid();
        public LeverageSetEvent(Commands.SetLeverage command, IPosition position)
        {
            Command = command;
            Position = position;
        }
    }
}
