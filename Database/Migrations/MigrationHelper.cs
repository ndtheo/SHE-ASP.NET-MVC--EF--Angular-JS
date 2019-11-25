#region Using Directives

using System.Data.Entity.Migrations;

#endregion

namespace Database.Migrations
{
    internal static class MigrationHelper
    {
        public static string InsertMenuItemCommand(this DbMigration mg, string title, string controller)
        {
            return $"INSERT INTO [dbo].[MenuItems] ([Name], [ControllerName],[Disabled], [Hidden], [EditableRights]) VALUES ('{title}','{controller}',0,0,0);" +
                   $"INSERT INTO [dbo].[Rights] ([RoleId], [MenuItemId], [View], [Create], [Edit], [Delete], [Export]) VALUES('admin', (select id from MenuItems where ControllerName = '{controller}'), 1, 1, 1, 1, 1);";
        }
    }
}