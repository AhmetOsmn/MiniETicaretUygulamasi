﻿namespace MiniETicaretAPI.Application.Dtos.Order
{
    public class ListOrder
    {
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; } = null!;
    }
}
