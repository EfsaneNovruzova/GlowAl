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

    public static class CareProduct
    {
        public const string Create = "CareProduct.Create";
        public const string Update = "CareProduct.Update";
        public const string Delete = "CareProduct.Delete";
        public const string GetMy = "CareProduct.GetMy";

        public static List<string> All = new()
        {
            Create,
            Update,
            Delete,
            GetMy
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
    public static class Review
    {
        public const string Create = "Review.Create";
        public const string Update = "Review.Update";
        public const string Delete = "Review.Delete";

        public static List<string> All = new()
        {
            Create,
            Update,
            Delete,
        };
    }
    public static class Favorite
    {
        public const string Create = "Favorite.Create";
        public const string Delete = "Favorite.Delete";

        public static List<string> All = new()
        {
            Create,
            Delete,
        };
    }
    public static class AIQueryHistory
    {
        public const string Create = "AIQueryHistory.Create";
        public const string Delete = "AIQueryHistory.my";

        public static List<string> All = new()
        {
            Create,
            Delete,
        };
    }
}
