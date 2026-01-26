using Project.Core.Utilities.Results;
using Project.Entities.Dtos.System.GlobalDtos.UserRoleDtos;

namespace Project.DataAccess.Repositories.Abstract.System
{
    public interface IGlobalRepository
    {
        Result GetModules();
        Result GetMenus(int moduleId);

        Result GetLanguageContent(string pageName);
        Result GetSystemLanguages();


        Result GetAllUsers();
        Result GetUserById(int userId);

        ResultInfo AddUser(AddUserDto model);
        ResultInfo UpdateUser(UpdateUserDto model);


        ResultInfo DeleteUser(int userId);
        ResultInfo ResetUserPassword(UserResetDto user);
        ResultInfo ChangeUserPassword(UserResetDto user);
        ResultInfo AddRolesToUser(AddRolesToUserDto model);

        Result GetUserRolesByUserId(int userId);
        Result GetAllRoles();
        Result GetRoleById(int roleId);
        Result GetRoleMenusByRoleId(int roleId);
        ResultInfo SaveRoleMenus(SaveRoleMenusDto saveRoleMenusDto);
        ResultInfo AddOrUpdateRole(RoleDto role);

        //ResultInfo ChangeRoleStatus(int roleId, bool status);


    }
}
