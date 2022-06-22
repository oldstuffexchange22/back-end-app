using System;
using RoleEntity = old_stuff_exchange_v2.Entities.Role;
using BuildingEntity = old_stuff_exchange_v2.Entities.Building;

namespace Old_stuff_exchange.Model.User
{
    public class ResponseUserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoleName { get; set; }
        public string BuildingName { get; set; }
    }
}
