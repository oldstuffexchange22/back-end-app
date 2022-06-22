using Old_stuff_exchange.Model.User;
using old_stuff_exchange_v2.Model.Building;
using old_stuff_exchange_v2.Model.Role;

namespace old_stuff_exchange_v2.Entities.Extentions
{
    public static class EntityExtensions
    {
        public static ResponseUserModel ToResponseModel(this User user) { 
            ResponseUserModel model = new ResponseUserModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FullName = user.FullName;
            model.Status = user.Status;
            model.Phone = user.Phone;
            model.Email = user.Email;
            model.ImageUrl = user.ImagesUrl;
            model.Gender = user.Gender;
            model.CreatedAt = user.CreatedAt;
            model.Role = user.Role?.ToResponseModel();
            model.Building = user.Building?.ToResponseModel();
            return model;
        }

        public static ResponseRoleModel ToResponseModel(this Role role) { 
            ResponseRoleModel model = new ResponseRoleModel();
            model.Id = role.Id;
            model.Name = role.Name;
            model.Description = role.Description;
            return model;
        }

        public static ResponseBuildingModel ToResponseModel(this Building building) { 
            ResponseBuildingModel model = new ResponseBuildingModel();
            model.Id = building.Id;
            model.Name = building.Name;
            model.NumberFloor = building.NumberFloor;
            model.NumberRoom = building.NumberRoom;
            model.Description = building.Description;
            model.Apartment = building.Apartment;
            return model;
        }
    }
}
