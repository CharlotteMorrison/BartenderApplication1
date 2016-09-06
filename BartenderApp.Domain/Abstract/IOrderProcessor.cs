using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BartenderApp.Domain.Entities;

namespace BartenderApp.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, OrderDetails orderDetails);
    }
}
