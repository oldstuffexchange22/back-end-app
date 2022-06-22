using System;
using RoleEntity = old_stuff_exchange_v2.Entities.Role;
using BuildingEntity = old_stuff_exchange_v2.Entities.Building;
using old_stuff_exchange_v2.Model.Role;
using old_stuff_exchange_v2.Model.Building;

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
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public ResponseRoleModel Role { get; set; }
        public ResponseBuildingModel Building { get; set; }
    }
}
