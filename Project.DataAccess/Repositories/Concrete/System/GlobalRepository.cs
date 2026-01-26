using AutoMapper;
using Microsoft.Data.SqlClient;
using Project.Core.Entities.Models;
using Project.Core.Entities.SPModels.System;
using Project.Core.Utilities.Results;
using Project.Core.Utilities.Security;
using Project.Entities.Dtos.System.AuthDtos;
using Project.Entities.Dtos.System.GlobalDtos.UserRoleDtos;
using Project.Entities.Enums.System;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.DataAccess.Repositories.Concrete.System
{
    public class GlobalRepository : IGlobalRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public GlobalRepository(IMapper mapper, IUserRepository userRepository, IMenuRepository menuRepository, ILanguageRepository languageRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IRoleMenuRepository roleMenuRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _menuRepository = menuRepository;
            _languageRepository = languageRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
        }


        #region Modules,Menus, Lang 

        public Result GetModules()
        {
            var result = new Result();

            List<SqlParameter> parameters = new();
            parameters.AddUserIdParam();
            parameters.AddLanguageParam();


            var runProcedure = EfDbTools.ExecuteProcedure<SP_GetModules>("OBJ.SP_GetModules", parameters);
            result.Data = runProcedure;
            return result;
        }



        public Result GetMenus(int moduleId)
        {
            var result = new Result();

            List<SqlParameter> parameters = new();
            parameters.AddUserIdParam();
            parameters.AddLanguageParam();
            parameters.AddParam("ModuleId", moduleId);

            var runProcedure = EfDbTools.ExecuteProcedure<SP_GetMenus>("OBJ.SP_GetMenus", parameters);

            result.Data = runProcedure;
            return result;

        }


        public Result GetLanguageContent(string pageName)
        {
            var result = new Result();

            if (string.IsNullOrEmpty(pageName))
            {
                result.ResultInfo = ResultInfo.NotFound;
                return result;
            }

            List<SqlParameter> parameters = new();
            parameters.AddLanguageParam();
            parameters.AddParam("PageName", pageName);

            //var runProcedure = EfDbTools.ExecuteProcedure<SP_GetContents>("OBJ.SP_GetContents", parameters);
            var keyValues = EfDbTools.ExecuteProcedure<SP_GetPageContents>("OBJ.SP_GetPageContents", parameters);


            if (keyValues.Count > 0)
            {
                var jsonData = keyValues.ToDictionary(x => x.Key, x => x.Value);
                result.Data = jsonData;
            }
            else
            {
                result.ResultInfo = ResultInfo.NotFound;
            }

            return result;
        }



        public Result GetSystemLanguages()
        {
            var result = new Result();
            var data = _languageRepository.GetAll();
            result.Data = _mapper.Map<List<LanguageResponse>>(data);
            return result;

        }

        #endregion




        #region Users, Roles,Menu permisson


        public Result GetAllUsers() => new() { Data = _mapper.Map<List<UserDto>>(_userRepository.GetAll().OrderByDescending(x => x.Id).ToList()) };

        public Result GetUserById(int userId)
        {
            var result = new Result();
            var user = _userRepository.Get(x => x.Id == userId);
            if (user == null)
            {
                result.ResultInfo = ResultInfo.NotFound;
                return result;
            }


            result.Data = _mapper.Map<UserDto>(user);
            return result;
        }

        public ResultInfo AddRolesToUser(AddRolesToUserDto model)
        {

            if (model == null)
            {
                return ResultInfo.NotFound;

            }

            if (_userRepository.Get(x => x.Id == model.UserId) == null)

            {
                return ResultInfo.NotFound;
            }

            return AssignRolesToUser(model);
        }


        public ResultInfo AddUser(AddUserDto model)
        {
            if (model == null)
            {
                return ResultInfo.NotFound;
            }
            var mappedData = _mapper.Map<User>(model);

            if (UserAlreadyExists(mappedData, out var alreadyExists, UserExistsOperationType.Add)) return alreadyExists;

            mappedData.Password = SecurityHelper.CreateMD5(model.Password);
            mappedData.PasswordStatus = false;
            mappedData.CreateDate = DateTime.Now;


            var save = _userRepository.Add(mappedData);
            if (save.ResultInfo != ResultInfo.SaveSuccess)
            {
                return ResultInfo.UnexpectedError;
            }


            return ResultInfo.SaveSuccess;

        }

        private bool UserAlreadyExists(User model, out ResultInfo alreadyExists, UserExistsOperationType operationType)
        {
            alreadyExists = ResultInfo.AlreadyExists;


            if (_userRepository.Get(x =>
                    x.Username.ToLower() == model.Username.ToLower() &&
                    (
                        operationType == UserExistsOperationType.Update && x.Id != model.Id ||
                        operationType == UserExistsOperationType.Add
                    )) != null)
            {
                alreadyExists = ResultInfo.UserNameAlreadyExists;
                return true;
            }

            if (_userRepository.Get(x =>
                    x.Email.ToLower() == model.Email.ToLower() &&
                    (
                        operationType == UserExistsOperationType.Update && x.Id != model.Id ||
                        operationType == UserExistsOperationType.Add
                    )) != null)

            {
                alreadyExists = ResultInfo.EmailAlreadyExists;
                return true;
            }



            return false;
        }


        public ResultInfo UpdateUser(UpdateUserDto model)
        {

            if (model == null)
            {
                return ResultInfo.NotFound;

            }


            var userOriginalData = _userRepository.Get(x => x.Id == model.Id);
            if (userOriginalData == null)
            {
                return ResultInfo.NotFound;

            }


            var mappedData = _mapper.Map<User>(model, userOriginalData);




            if (UserAlreadyExists(mappedData, out var alreadyExists, UserExistsOperationType.Update)) return alreadyExists;



            var save = _userRepository.Update(mappedData);

            if (save.ResultInfo != ResultInfo.SaveSuccess)
            {
                return ResultInfo.UnexpectedError;
            }

            return ResultInfo.SaveSuccess;

        }



        public ResultInfo DeleteUser(int userId)
        {
            var result = new Result();

            if (_userRepository.FindById(userId) == null)
            {
                return ResultInfo.NotFound;

            }

            var data = _userRepository.Get(x => x.Id == userId);
            data.Status = false;

            var delete = _userRepository.Update(data);

            if (delete.ResultInfo != ResultInfo.SaveSuccess)
            {
                return ResultInfo.UnexpectedError;

            }
            return ResultInfo.Deleted;
        }



        public ResultInfo ResetUserPassword(UserResetDto user)
        {
            var result = new Result();

            if (_userRepository.Get(x => x.Id == user.UserId && x.Status == true) == null)
            {
                return ResultInfo.NotFound;
            }

            if (user.Password != user.ConfirmPassword)
            {
                return ResultInfo.ConfirmPasswordError;
            }


            var data = _userRepository.Get(x => x.Id == user.UserId);
            if (data != null)
            {
                data.Password = SecurityHelper.CreateMD5(user.Password);
                data.PasswordStatus = true;

                var delete = _userRepository.Update(data);

                if (delete.ResultInfo != ResultInfo.SaveSuccess)
                {
                    return ResultInfo.UnexpectedError;
                }
            }
            else
            {
                return ResultInfo.UnexpectedError;
            }


            return ResultInfo.SaveSuccess;
        }

        public ResultInfo ChangeUserPassword(UserResetDto user)
        {
            var result = new Result();

            if (_userRepository.Get(x => x.Id == user.UserId && x.Status == true) == null)
            {
                return ResultInfo.NotFound;
            }

            if (user.Password != user.ConfirmPassword)
            {
                return ResultInfo.ConfirmPasswordError;
            }


            var data = _userRepository.Get(x => x.Id == user.UserId);
            if (data != null)
            {
                data.Password = SecurityHelper.CreateMD5(user.Password);
                data.PasswordStatus = false;

                var delete = _userRepository.Update(data);

                if (delete.ResultInfo != ResultInfo.SaveSuccess)
                {
                    return ResultInfo.UnexpectedError;
                }
            }
            else
            {
                return ResultInfo.UnexpectedError;
            }


            return ResultInfo.SaveSuccess;
        }

        public Result GetUserRolesByUserId(int userId)
        {
            var result = new Result();

            List<SqlParameter> parameters = new();
            parameters.AddParam("UserId", userId);

            var runProcedure = EfDbTools.ExecuteProcedure<SP_GetUserRolesByUserId>("OBJ.SP_GetUserRolesByUserId", parameters);
            result.Data = runProcedure;
            return result;

        }

        public Result GetRoleById(int roleId)
        {
            var result = new Result();
            var role = _roleRepository.Get(x => x.Id == roleId);
            if (role == null)
            {
                result.ResultInfo = ResultInfo.NotFound;
                return result;
            }


            result.Data = _mapper.Map<Role>(role);
            return result;
        }

        public Result GetAllRoles()
        {
            return new Result { Data = _mapper.Map<List<Role>>(_roleRepository.GetAll()) };
        }



        public Result GetRoleMenusByRoleId(int roleId)
        {
            var result = new Result();

            List<SqlParameter> parameters = new();
            parameters.AddLanguageParam();
            parameters.AddParam("RoleId", roleId);

            var runProcedure = EfDbTools.ExecuteProcedure<SP_GetRoleMenusByRoleId>("OBJ.SP_GetRoleMenusByRoleId", parameters);
            result.Data = runProcedure;
            return result;

        }


        public ResultInfo SaveRoleMenus(SaveRoleMenusDto saveRoleMenusDto)
        {
            if (saveRoleMenusDto.RoleId <= 0 && _roleRepository.Get(x => x.Id == saveRoleMenusDto.RoleId) == null)
            {
                return ResultInfo.NotFound;
            }

            RemoveAllMenusByRoleId(saveRoleMenusDto.RoleId);

            foreach (var id in saveRoleMenusDto.MenuIds)
            {
                var add = _roleMenuRepository.Add(new RoleMenu { RoleId = saveRoleMenusDto.RoleId, MenuId = id, Status = true });

                if (add.ResultInfo != ResultInfo.SaveSuccess)
                {
                    return ResultInfo.UnexpectedError;
                }
            }

            return ResultInfo.SaveSuccess;
        }



        public ResultInfo AddOrUpdateRole(RoleDto role)
        {
            if (!RoleControlState(role, out var controlResult))
            {
                return controlResult;
            }

            if (role.Id > 0)//Update
            {
                var roleOriginalData = _roleRepository.Get(x => x.Id == role.Id);
                var mappedData = _mapper.Map<Role>(role, roleOriginalData);

                var save = _roleRepository.Update(mappedData);
                if (save.ResultInfo != ResultInfo.SaveSuccess)
                {
                    return ResultInfo.UnexpectedError;
                }

            }
            else//Add
            {
                var mappedData = _mapper.Map<Role>(role);
                //mappedData.Status = true;

                var save = _roleRepository.Add(mappedData);
                if (save.ResultInfo != ResultInfo.SaveSuccess)
                {
                    return ResultInfo.UnexpectedError;

                }
            }

            return ResultInfo.SaveSuccess;

        }



        private ResultInfo AssignRolesToUser(AddRolesToUserDto model)
        {
            #region Remove all roles
            var allRoles = _userRoleRepository.GetAll(x => x.Userid == model.UserId);
            foreach (var item in allRoles)
            {
                item.Status = false;
                _userRoleRepository.Update(item);
            }
            #endregion

            foreach (var item in model.RoleIds)
            {
                if (_userRoleRepository.Get(x => x.Id == item && (bool)x.Status) != null)
                {
                    _userRoleRepository.Add(new UserRole { Userid = model.UserId, RoleId = item });
                }
                //else
                //{
                //    return ResultInfo.NotFound;

                //}
            }
            return ResultInfo.SaveSuccess;
        }

        private bool RoleControlState(RoleDto role, out ResultInfo resultInfo)
        {
            resultInfo = default;
            if (role == null || string.IsNullOrEmpty(role.Name))
            {
                resultInfo = ResultInfo.InvalidRequestParameters;
                return false;
            }

            if (_roleRepository.Get(x => x.Name.ToLower() == role.Name.ToLower() && x.Id != role.Id) != null)
            {
                resultInfo = ResultInfo.AlreadyExists;
                return false;
            }



            var roleOriginalData = _roleRepository.Get(x => x.Id == role.Id);
            if (roleOriginalData == null && role.Id > 0)
            {
                resultInfo = ResultInfo.NotFound;
                return false;
            }



            return true;
        }

        private void RemoveAllMenusByRoleId(int roleId)
        {
            //Rolaa bagli olan menulari sil
            var roleMenus = _roleMenuRepository.GetAll(x => x.RoleId == roleId);

            if (roleMenus.Count > 0)
            {
                foreach (var item in roleMenus)
                {
                    item.Status = false;
                    _roleMenuRepository.Update(item);

                }
            }
        }

        //public ResultInfo ChangeRoleStatus(int roleId, bool status)
        //{

        //    var role = _roleRepository.Get(x => x.Id == roleId);
        //    if (role == null)
        //    {
        //        return ResultInfo.NotFound;

        //    }

        //    role.Status = status;
        //    var save = _roleRepository.Update(role);
        //    if (save.ResultInfo == ResultInfo.SaveSuccess)
        //    {
        //        return ResultInfo.SaveSuccess;

        //    }
        //    else
        //    {
        //        return ResultInfo.UnexpectedError;
        //    }


        //}

        #endregion

    }
}
