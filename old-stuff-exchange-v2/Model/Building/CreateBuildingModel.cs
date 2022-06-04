using System;

namespace Old_stuff_exchange.Model.Building
{
    public class CreateBuildingModel
    {
        public string Name { get; set; }
        public int? NumberFloor { get; set; }
        public int? NumberRoom { get; set; }

        public string Description { get; set; }

        public Guid ApartmentId { get; set; }
    }
}
