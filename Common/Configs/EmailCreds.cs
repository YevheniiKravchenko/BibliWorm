using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configs;
public class EmailCreds
{
    public string EmailHost { get; set; }

    public string EmailUserName { get; set; }

    public string EmailPassword { get; set; }

    public string ResetPasswordUrl { get; set; }
}