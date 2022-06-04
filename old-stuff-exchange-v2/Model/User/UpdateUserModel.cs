using System;

namespace Old_stuff_exchange.Model.User
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public Guid BuildingId { get; set; }
        public string Status { get; set; }
        public Guid RoleId { get; set; }
        public string Phone { get; set; }
    }
}
