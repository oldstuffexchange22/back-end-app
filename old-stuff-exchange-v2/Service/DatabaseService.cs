using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum;
using System.Collections.Generic;
using System.Linq;

namespace Old_stuff_exchange.Service
{
    public class DatabaseService
    {
        public AppDbContext _context;

        public DatabaseService(AppDbContext context)
        {
            _context = context;
        }

        public void GererateData() {
            int count = _context.Apartments.Count();
            if (count <= 0) {
                Apartment apartment = new Apartment
                {
                    Name = "Man Thien"
                };
                _context.Apartments.Add(apartment);
                _context.SaveChanges();
                List<Building> buildings = new List<Building>();
                for (int i = 0; i < 10; i++)
                {
                    Building building = new Building
                    {
                        Name = "C" + (i + 1),
                        ApartmentId = apartment.Id,
                        NumberFloor = 5,
                        NumberRoom = 50
                    };
                    buildings.Add(building);
                }
                _context.Buildings.AddRange(buildings);
                _context.SaveChanges();


                Role role = new Role
                {
                    Name = RoleNames.ADMIN,
                };
                Role role2 = new Role
                {
                    Name = RoleNames.RESIDENT,
                };
                _context.Roles.Add(role);
                _context.Roles.Add(role2);
                _context.SaveChanges();

                Role residentRole = _context.Roles.FirstOrDefault(role => role.Name == RoleNames.RESIDENT);
                Role adminRole = _context.Roles.FirstOrDefault(role => role.Name == RoleNames.RESIDENT);
                User newUser = new User
                {
                    UserName = "nvtan.a5@gmail.com",
                    Email = "nvtan.a5@gmail.com",
                    Status = UserStatus.ACTIVE,
                    Role = residentRole,
                    FullName = "nvtan.a5@gmail.com",
                    BuildingId = buildings[0].Id
                };
                User newUser1 = new User
                {
                    UserName = "nvtan.a6@gmail.com",
                    Email = "nvtan.a6@gmail.com",
                    Status = UserStatus.ACTIVE,
                    Role = residentRole,
                    FullName = "nvtan.a6@gmail.com",
                    BuildingId = buildings[0].Id
                };
                User newUser2 = new User
                {
                    UserName = "nvtan.a7@gmail.com",
                    Email = "nvtan.a7@gmail.com",
                    Status = UserStatus.ACTIVE,
                    Role = adminRole,
                    FullName = "nvtan.a7@gmail.com",
                    BuildingId = buildings[0].Id
                };
                User newUser3 = new User
                {
                    UserName = "nvtan.a8@gmail.com",
                    Email = "nvtan.a8@gmail.com",
                    Status = UserStatus.ACTIVE,
                    Role = adminRole,
                    FullName = "nvtan.a8@gmail.com",
                    BuildingId = buildings[0].Id
                };
                List<User> users = new List<User>();
                users.Add(newUser1);
                users.Add(newUser);
                users.Add(newUser2);
                users.Add(newUser3);
                _context.Users.AddRange(users);
                _context.SaveChanges();
            }

            Category category = new Category
            {
                Name = "Electronic"
            };
            Category category1 = new Category
            {
                Name = "Chicken"
            };
            List<Category> categories = new List<Category>();
            categories.Add(category1);
            categories.Add(category);
            _context.Categories.AddRange(categories);
            _context.SaveChanges();
            Category childC = new Category
            {
                Name = "Laptop",
                ParentId = category.Id
            };
            Category childC1 = new Category
            {
                Name = "Screen",
                ParentId = category.Id
            };
            Category childC2 = new Category
            {
                Name = "Keyboard",
                ParentId = category.Id
            };
            List<Category> child1 = new List<Category>();
            child1.Add(childC);
            child1.Add(childC1);
            child1.Add(childC2);
            _context.Categories.AddRange(child1);
            _context.SaveChanges();
            Category childCC1 = new Category
            {
                Name = "Acer",
                ParentId = childC.Id
            };
            Category childCC2 = new Category
            {
                Name = "Asus",
                ParentId = childC.Id
            };
            List <Category> childCC3 = new List<Category>();
            childCC3.Add(childCC1);
            childCC3.Add(childCC2);
            _context.AddRange(childCC3);
            _context.SaveChanges();
        }
            

    }
}
