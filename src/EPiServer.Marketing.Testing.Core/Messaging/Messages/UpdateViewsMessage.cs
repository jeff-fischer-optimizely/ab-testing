﻿using System;

namespace EPiServer.Marketing.Testing.Messaging
{
    public class UpdateViewsMessage
    {
        public Guid TestId { get; set; }

        public int ItemVersion { get; set; }
    }
}
