using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowAl.Application.Shared;

public class Permissions
{
    public static class Category
    {
        public const string Create = "Category.Create";
        public const string Update = "Category.Update";
        public const string Delete = "Category.Delete";


        public static List<string> All = new()
        {
           Create,
            Update,
            Delete,
        };
    }
    public static class Product
    {
        public const string Create = "Product.Create";
        public const string Update = "Product.Update";
        public const string Delete = "Product.Delete";
       

        public static List<string> All = new()
        {
            Create,
            Update,
            Delete,
            


        };
    }
    public static class Account
    {
        public const string AddRole = "Account.AddRole";
        public const string Create = "Account.Create";
        public const string GetAllUsers = "Account.GetAllUsers";

        public static List<string> All = new()
        {
              AddRole,
              Create,
            GetAllUsers
        };
    }

    public static class Role
    {
        public const string Create = "Role.Create";
        public const string Update = "Role.Update";
        public const string Delete = "Role.Delete";
        public const string GetAllPermission = "Role.GetAllPermission";

        public static List<string> All = new()
        {
           Create,
            Update,
            Delete,
        };

    }
}
