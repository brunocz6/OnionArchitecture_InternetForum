﻿using InternetForum.Application.Common.Interfaces;
using System;

namespace InternetForum.Infrastructure.Services
{
    /// <summary>
    /// Služba poskytující aktuální čas.
    /// </summary>
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
