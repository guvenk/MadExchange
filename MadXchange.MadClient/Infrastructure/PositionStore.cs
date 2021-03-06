﻿using MadXchange.Exchange.Domain.Models;
using System.Collections.Generic;

namespace MadXchange.MadClient.Infrastructure
{
    public class PositionStore
    {
        private readonly Dictionary<string, IPosition> _PositionStore;

        public PositionStore()
        {
            _PositionStore = new Dictionary<string, IPosition>();
        }

        public void Store(IPosition pos)
        {
            _PositionStore[pos.Symbol] = pos;
        }

        public IPosition GetPosition(string symbol)
        {
            if (_PositionStore.ContainsKey(symbol))
            {
                return _PositionStore[symbol];
            }
            return null;
        }
    }
    
}