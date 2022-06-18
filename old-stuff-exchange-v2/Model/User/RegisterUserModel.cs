using System;

namespace Old_stuff_exchange.Model.User
{
    public class RegisterUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public Guid? BuildingId { get; set; }
    }
}
