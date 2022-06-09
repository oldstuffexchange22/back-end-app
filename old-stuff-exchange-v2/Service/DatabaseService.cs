using Bogus;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum;
using System;
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

        public void GenerateDataWithBogus() {
            // Generate apartment
            Faker<Apartment> FakerAparment = new Faker<Apartment>()
                .RuleFor(a => a.Name, faker => faker.Lorem.Sentence(4))
                .RuleFor(a => a.Address, faker => faker.Lorem.Sentence(10));
            List<Apartment> apartments = FakerAparment.Generate(5);
            _context.Apartments.AddRange(FakerAparment);

            // Generate building
            Faker<Building> FakerBuilding = new Faker<Building>()
                .RuleFor(b => b.Name, faker => faker.Lorem.Sentence(3))
                .RuleFor(b => b.Apartment, faker => faker.PickRandom(apartments))
                .RuleFor(b => b.NumberFloor, faker => faker.Random.Int(3, 10))
                .RuleFor(b => b.NumberRoom, faker => faker.Random.Int(30, 100))
                .RuleFor(b => b.Description, faker => faker.Lorem.Sentence(6));
            List<Building> buildings = FakerBuilding.Generate(100);
            _context.Buildings.AddRange(buildings);

            // Generate role
            Role roleAdmin = new Role
            {
                Name = RoleNames.ADMIN,
            };
            Role roleResident = new Role
            {
                Name = RoleNames.RESIDENT,
            };
            List<Role> roles = new List<Role>();
            roles.Add(roleAdmin);
            roles.Add(roleResident);
            _context.AddRange(roles);

            // Generate user
            User userAdmin = new User
            {
                UserName = "nvtan.a5@gmail.com",
                Email = "nvtan.a5@gmail.com",
                Status = UserStatus.ACTIVE,
                Role = roleAdmin,
                FullName = "nvtan.a5@gmail.com",
                BuildingId = buildings[0].Id
            };
            Faker<User> FakerUser = new Faker<User>()
                .RuleFor(u => u.UserName, faker => faker.Person.UserName)
                .RuleFor(u => u.Email, faker => faker.Person.Email)
                .RuleFor(u => u.Status, UserStatus.ACTIVE)
                .RuleFor(u => u.Role, roleResident)
                .RuleFor(u => u.FullName, faker => faker.Person.FullName)
                .RuleFor(u => u.Gender, faker => faker.PickRandom(UserGender.GetGenders()))
                .RuleFor(u => u.Building, faker => faker.PickRandom(buildings));
            _context.Add(userAdmin);
            List<User> users = FakerUser.Generate(40);
            users.Add(userAdmin);
            _context.AddRange(users);

            // Generate wallet
            Wallet chairityWallet = new Wallet
            {
                Type = WalletType.CHAIRITY,
                Currency = Currency.XU,
                Status = WalletStatus.ACTIVE,
                UserId = null,
                Balance = 0
            };
            Wallet systemWallet = new Wallet
            {
                Type = WalletType.SYSTEM,
                Currency = Currency.XU,
                Status = WalletStatus.ACTIVE,
                UserId = null,
                Balance = 0
            };
            Faker<Wallet> FakerWalletDefault = new Faker<Wallet>()
                .RuleFor(w => w.Type, WalletType.DEFAULT)
                .RuleFor(w => w.Currency, Currency.XU)
                .RuleFor(w => w.Status, WalletStatus.ACTIVE)
                .RuleFor(w => w.Balance, faker => faker.Random.Int(500, 1000))
                .RuleFor(w => w.Desription, faker => faker.Lorem.Sentence(6));
            Faker<Wallet> FakerWalletPromotion = new Faker<Wallet>()
                .RuleFor(w => w.Type, WalletType.PROMOTION)
                .RuleFor(w => w.Currency, Currency.XU)
                .RuleFor(w => w.Status, WalletStatus.ACTIVE)
                .RuleFor(w => w.Balance, faker => faker.Random.Int(500, 1000))
                .RuleFor(w => w.Desription, faker => faker.Lorem.Sentence(6));
            List<Wallet> wallets = new List<Wallet>();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Role != roleAdmin) {
                    Wallet walletDefault = FakerWalletDefault.Generate();
                    Wallet walletPromotion = FakerWalletDefault.Generate();
                    walletDefault.UserId = users[i].Id;
                    walletPromotion.UserId = users[i].Id;
                    List<Wallet> walletUserDefine = new List<Wallet>();
                    walletUserDefine.Add(walletDefault);
                    walletUserDefine.Add(walletPromotion);
                    wallets.AddRange(walletUserDefine);
                }
            }
            _context.Wallets.AddRange(wallets);

            // Generate category
            Faker<Category> FakerCategory = new Faker<Category>()
                .RuleFor(c => c.Name, faker => faker.Lorem.Sentence(3, 4))
                .RuleFor(c => c.Description, faker => faker.Lorem.Sentence(20));
            List<Category> categories = FakerCategory.Generate(4);
            _context.AddRange(categories);
            _context.SaveChanges();

            List<Category> parentCategories = _context.Categories.ToList();
            Faker<Category> FakerChilrenCategory = new Faker<Category>()
                .RuleFor(c => c.Name, faker => faker.Lorem.Sentence(3, 4))
                .RuleFor(c => c.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(c => c.Parent, faker => faker.PickRandom(parentCategories));
            List<Category> chilrenCategories = FakerChilrenCategory.Generate(10);
            _context.Categories.AddRange(chilrenCategories);
            _context.SaveChanges();

            Faker<Category> FakerChilrenOfChilrenCategory = new Faker<Category>()
                .RuleFor(c => c.Name, faker => faker.Lorem.Sentence(3, 4))
                .RuleFor(c => c.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(c => c.Parent, faker => faker.PickRandom(chilrenCategories));
            List<Category> chilrenOfChilrenCategories = FakerChilrenCategory.Generate(10);
            _context.Categories.AddRange(chilrenOfChilrenCategories);
            _context.SaveChanges();

            // Generate Post
            List<Guid> listUserId = new List<Guid>();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Role != roleAdmin) listUserId.Add(users[i].Id);
            }
            Faker<Post> FakerPost = new Faker<Post>()
                .RuleFor(p => p.Title, faker => faker.Lorem.Sentence(10))
                .RuleFor(p => p.Description, faker => faker.Lorem.Sentences(2))
                .RuleFor(p => p.Price, faker => faker.Random.Int(200, 500))
                .RuleFor(p => p.Status, faker => faker.PickRandom(PostStatus.ListPostSatus()))
                .RuleFor(p => p.Author, faker => faker.PickRandom(users.Take(20)))
                .RuleFor(p => p.UserBought, faker => faker.PickRandom(listUserId));
            List<Post> posts = FakerPost.Generate(400);
            _context.Posts.AddRange(posts);

            // Generate product
            List<Product> products = new List<Product>();
            Faker<Product> FakerProduct = new Faker<Product>()
                .RuleFor(p => p.Name, faker => faker.Lorem.Sentence(4))
                .RuleFor(p => p.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(p => p.Price, faker => faker.Random.Int(50, 100))
                .RuleFor(p => p.Status, faker => faker.Random.Int(20, 100) + "%")
                .RuleFor(p => p.Category, faker => faker.PickRandom(_context.Categories.ToList()));
            // .RuleFor(p => p.Post, faker => faker.PickRandom(posts));
            for (int i = 0; i < posts.Count; i++)
            {
                Product product1 = FakerProduct.Generate();
                Product product2 = FakerProduct.Generate();
                product1.PostId = posts[i].Id;
                product2.PostId = posts[i].Id;
                products.Add(product1);
                products.Add(product2);
            }
            _context.Products.AddRange(products);

            // Generate deposit
            Faker<Deposit> FakerDeposit = new Faker<Deposit>()
                .RuleFor(d => d.WalletElectricName, faker => faker.PickRandom(WalletElectricName.GetWalletNames()))
                .RuleFor(d => d.Descripion, faker => faker.Lorem.Sentence(6))
                .RuleFor(d => d.Amount, faker => faker.Random.Int(500, 3000) * 1000)
                .RuleFor(d => d.RemainingCoinInWallet, faker => faker.Random.Int(200, 10000))
                .RuleFor(d => d.User, faker => faker.PickRandom(users.Where(u => u.Role != roleAdmin)));
            List<Deposit> deposits = FakerDeposit.Generate(500);
            for (int i = 0; i < deposits.Count; i++)
            {
                deposits[i].CoinExchange = deposits[i].Amount / 1000;
            }
            _context.AddRange(deposits);

            // Generate transaction
            Faker<Transaction> FakerTransactionPost = new Faker<Transaction>()
                .RuleFor(t => t.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(t => t.Status, TransactionStatus.SUCCESS)
                .RuleFor(t => t.Type, faker => faker.PickRandom(TransactionType.GetTransactionPost()))
                .RuleFor(t => t.Amount, faker => faker.Random.Int(100, 200))
                .RuleFor(t => t.Balance, faker => faker.Random.Int(1000, 2000))
                .RuleFor(t => t.Wallet, faker => faker.PickRandom(wallets))
                .RuleFor(t => t.Post, faker => faker.PickRandom(posts));
            Faker<Transaction> FakerTransactionDeposit = new Faker<Transaction>()
                .RuleFor(t => t.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(t => t.Status, TransactionStatus.SUCCESS)
                .RuleFor(t => t.Type, TransactionType.RECHARGE)
                .RuleFor(t => t.Amount, faker => faker.Random.Int(100, 200))
                .RuleFor(t => t.Balance, faker => faker.Random.Int(1000, 2000))
                .RuleFor(t => t.Wallet, faker => faker.PickRandom(wallets))
                .RuleFor(t => t.Deposit, faker => faker.PickRandom(deposits));
            List<Transaction> transactions = new List<Transaction>();
            List<Transaction> transactionPost = FakerTransactionPost.Generate(800);
            List<Transaction> transactionDepost = FakerTransactionDeposit.Generate(200);
            transactions.AddRange(transactionPost);
            transactions.AddRange(transactionDepost);
            _context.AddRange(transactions);
            _context.SaveChanges();
        }

        public void DeleteAllData() {
            _context.Products.RemoveRange(_context.Products);
            _context.Categories.RemoveRange(_context.Categories);
            _context.Transactions.RemoveRange(_context.Transactions);
            _context.Deposits.RemoveRange(_context.Deposits);
            _context.Posts.RemoveRange(_context.Posts);
            _context.Wallets.RemoveRange(_context.Wallets);
            _context.Users.RemoveRange(_context.Users);
            _context.Roles.RemoveRange(_context.Roles);
            _context.Buildings.RemoveRange(_context.Buildings);
            _context.Apartments.RemoveRange(_context.Apartments);
            _context.SaveChanges();
        }
            

    }
}
