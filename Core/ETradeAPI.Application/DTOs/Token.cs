﻿using System.Globalization;

namespace ETradeAPI.Application.DTOs
{
    public class Token
    {
        public string AccessToken{ get; set; }
        public DateTime Expiration { get; set; }
    }
}
