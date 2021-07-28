using System.Collections.Generic;
using System.Linq;

using TheGarageAPI.Entities;

namespace TheGarageAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<DataUser> WithoutPasswords(this IEnumerable<DataUser> users)
        {
            if(users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static DataUser WithoutPassword(this DataUser user)
        {
            if (user == null) return null;

            user.PasswordHash = null;
            user.PasswordSalt = null;
            return user;
        }
    }
}