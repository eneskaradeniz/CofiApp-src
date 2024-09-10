namespace CofiApp.Domain.Enums
{
    public enum Permission
    {
        All = 1,

        GetUsers = 2,
        GetUserById = 3,
        CreateUser = 4,
        UpdateUser = 5,
        RemoveUser = 6,

        GetRoles = 7,
        CreateRole = 8,
        UpdateRole = 9,
        RemoveRole = 10,
        GetRolesPermissions = 11,
        AssignPermission = 12,
        AssignUser = 13,

        CreateProduct = 14,
    }
}
