﻿using old_stuff_exchange_v2.Entities;
using ApartmentEntity = old_stuff_exchange_v2.Entities.Apartment;
using System;

namespace old_stuff_exchange_v2.Model.Building
{
    public class ResponseBuildingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? NumberFloor { get; set; }
        public int? NumberRoom { get; set; }
        public string Description { get; set; }
        public ApartmentEntity Apartment { get; set; }
    }
}
