using System;

namespace Old_stuff_exchange.Model.User
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public Guid BuildingId { get; set; }
        public Guid RoleId { get; set; }
    }
}
