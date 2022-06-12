using System.Collections.Generic;

namespace old_stuff_exchange_v2.Enum.User
{
    public static class UserGender
    {
        public const string MALE = "MALE";
        public const string FEMALE = "FEMALE";
        public const string OTHER = "OTHER";

        public static List<string> GetGenders()
        {
            List<string> genders = new List<string>();
            genders.Add(MALE);
            genders.Add(FEMALE);
            genders.Add(OTHER);
            return genders;
        }
    }
}
