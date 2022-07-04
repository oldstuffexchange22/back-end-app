using System;

namespace old_stuff_exchange_v2.Model.Apartment
{
    public class UpdateApartmentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
