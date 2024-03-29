﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Desk : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }      
        public DateTime? RentingStart { get; set; }
        public DateTime? RentingEnd { get; set; }

        public int? OwnerId { get; set; }
        public int RoomId { get; set; }
        public virtual Owner? Owner { get; set; }
        public virtual Room Room { get; set; }


    }
}
