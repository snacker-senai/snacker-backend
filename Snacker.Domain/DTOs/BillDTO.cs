﻿using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class BillDTO : BaseDTO
    {
        public BillDTO(Bill bill)
        {
            Id = bill.Id;
            Active = bill.Active;
        }
        public bool Active { get; set; }
    }
}
