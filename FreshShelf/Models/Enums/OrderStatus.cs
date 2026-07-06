using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshShelf.Models.Enums;

public enum OrderStatus
{
    Pending = 0,
    Approved = 1,
    InTransit = 2,
    Delivered = 3,
    Canceled = 4
}
