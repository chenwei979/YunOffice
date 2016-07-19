using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunOffice.Common.RabbitMq
{
    public class MqConfig : IMqConfig
    {
        public MqConfig()
        {
            HostName = "192.168.232.128";
            UserName = "test_user";
            Password = "123";
        }

        public string HostName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}