﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configs;
public class ConnectionModel
{
    public string Host { get; set; }

    public string Database { get; set; }

    public string Password { get; set; }

    public string ConnectionString { get; set; }
}