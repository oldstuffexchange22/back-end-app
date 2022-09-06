using Old_stuff_exchange.Model.User;
using old_stuff_exchange_v2.Model.Apartment;
using old_stuff_exchange_v2.Model.Building;
using old_stuff_exchange_v2.Model.Category;
using old_stuff_exchange_v2.Model.Deposit;
using old_stuff_exchange_v2.Model.Post;
using old_stuff_exchange_v2.Model.Product;
using old_stuff_exchange_v2.Model.Role;
using old_stuff_exchange_v2.Model.Transaction;
using old_stuff_exchange_v2.Model.Wallet;
using System.Linq;

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
            model.ApartmentId = building.ApartmentId;
            model.Apartment = building.Apartment?.ToResponseModel();
            return model;
        }
        public static ResponseApartmentModel ToResponseModel(this Apartment apartment) { 
            ResponseApartmentModel model = new ResponseApartmentModel();
            model.Id = apartment.Id;
            model.Address = apartment.Address;
            model.Description = apartment.Description;
            model.Name = apartment.Name;
            model.ImageUrl = apartment.ImageUrl;
            return model;
        }
        public static ResponseDepositModel ToResponseModel(this Deposit deposit) { 
            ResponseDepositModel model = new ResponseDepositModel();
            model.Id = deposit.Id;
            model.WalletElectricName = deposit.WalletElectricName;
            model.Description = deposit.Description;
            model.Amount = deposit.Amount;
            model.CreatedAt = deposit.CreatedAt;
            model.User = deposit.User?.ToResponseModel();
            return model;
        }
        public static ResponsePostModel ToResponseModel(this Post post) {
            ResponsePostModel model = new ResponsePostModel();
            model.Id = post.Id;
            model.Title = post.Title;
            model.Description = post.Description;
            model.Price = post.Price;
            model.ImageUrl = post.ImageUrl;
            model.CreatedAt = post.CreatedAt;
            model.Expired = post.Expired;
            model.PublishedAt = post.PublishedAt;
            model.LastUpdatedAt = post.LastUpdatedAt;
            model.Status = post.Status;
            model.UserBought = post.UserBought;
            model.Author = post.Author?.ToResponseModel();
            model.Products = post.Products.ToList();
            return model;
        }
        public static ResponseProductModel ToResponseModel(this Product product) { 
            ResponseProductModel model = new ResponseProductModel();
            model.Id = product.Id;
            model.Name = product.Name;
            model.Price = product.Price;
            model.Description = product.Description;
            model.Status = product.Status;
            model.PostId = product.PostId;
            model.Category = product.Category.ToResponseModel();
            return model;
        }
        public static ResponseCategoryModel ToResponseModel(this Category category) { 
            ResponseCategoryModel model = new ResponseCategoryModel();
            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ParentId = category.ParentId;
            return model;
        }
        public static ResponseWalletModel ToResponseModel(this Wallet wallet) { 
            ResponseWalletModel model = new ResponseWalletModel();
            model.Id = wallet.Id;
            model.Balance = wallet.Balance;
            model.Type = wallet.Type;
            model.Currency = wallet.Currency;
            model.Status = wallet.Status;
            model.CreatedAt = wallet.CreatedAt;
            model.LastUpdatedAt = wallet.LastUpdatedAt;
            model.Desription = wallet.Desription;
            model.User = wallet.User?.ToResponseModel();
            return model;
        }
        public static ResponseTransactionModel ToResponseModel(this Transaction transaction) { 
            ResponseTransactionModel model = new ResponseTransactionModel();
            model.Id = transaction.Id;
            model.Description = transaction.Description;
            model.Status = transaction.Status;
            model.Type = transaction.Type;
            model.CoinExchange = transaction.CoinExchange;
            model.Balance = transaction.Balance;
            model.CreatedAt = transaction.CreatedAt;
            model.WalletId = transaction.WalletId;
            model.PostId = transaction.PostId;
            model.DepositId = transaction.DepositId;
            return model;
        }
    }
}
