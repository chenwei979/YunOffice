﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ
{
    public interface IMqConfig
    {
        string HostName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
