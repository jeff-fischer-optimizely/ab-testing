﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Marketing.Multivariate
{
    public interface ICurrentUser
    {
        string GetDisplayName();
    }
}
