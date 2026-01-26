using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.Dtos.System.GlobalDtos.UserRoleDtos;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.API.Controllers.System
{
    [Authorize]
    [Route("System/[controller]/[action]")]
    [ApiController]
    public class GlobalController : ControllerBase
    {
        private readonly IGlobalRepository _globalRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public GlobalController(IMapper mapper, IGlobalRepository globalRepository)
        {
            _mapper = mapper;
            _globalRepository = globalRepository;
        }



        //#region Modules,Menus, Lang 

        //[HttpGet]
        //public IActionResult GetModules()
        //{
        //    var data = _globalRepository.GetModules();
        //    return data.AsObjectResult();
        //}



        //[HttpGet("{moduleId}")]
        //public IActionResult GetMenus(int moduleId)
        //{
        //    var data = _globalRepository.GetMenus(moduleId);
        //    return data.AsObjectResult();
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult GetLoginLanguageContent()
        //{
        //    var data = _globalRepository.GetLanguageContent("auth/login");
        //    return data.AsObjectResult();
        //}



        //[HttpGet]
        //public IActionResult GetLanguageContent(string pageName)
        //{
        //    var data = _globalRepository.GetLanguageContent(pageName);
        //    return data.AsObjectResult();
        //}


        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult GetSystemLanguages()
        //{
        //    var data = _globalRepository.GetSystemLanguages();
        //    return data.AsObjectResult();
        //}


        //#endregion


        //#region Users and Roles Area



        //[HttpGet]
        //public IActionResult GetAllUsers()
        //{
        //    var data = _globalRepository.GetAllUsers();
        //    return data.AsObjectResult();
        //}



        //[HttpGet]
        //public IActionResult GetUserById(int userId)
        //{
        //    var data = _globalRepository.GetUserById(userId);
        //    return data.AsObjectResult();
        //}



        //[ModelStateControl]
        //[HttpPost]
        //public IActionResult AddUser(AddUserDto user)
        //{
        //    var data = _globalRepository.AddUser(user);
        //    return data.AsObjectResult();
        //}


        //[ModelStateControl]
        //[HttpPost]
        //public IActionResult UpdateUser(UpdateUserDto user)
        //{
        //    var data = _globalRepository.UpdateUser(user);
        //    return data.AsObjectResult();
        //}


        //[ModelStateControl]
        //[HttpPost]
        //public IActionResult AddRolesToUser(AddRolesToUserDto user)
        //{
        //    var data = _globalRepository.AddRolesToUser(user);
        //    return data.AsObjectResult();
        //}


        //[HttpDelete]
        //public IActionResult DeleteUser(int userId)
        //{
        //    var data = _globalRepository.DeleteUser(userId);
        //    return data.AsObjectResult();
        //}



        ///// <summary>
        ///// Sifre sifirlama
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[ModelStateControl]
        //[HttpPost]
        //public IActionResult ResetUserPassword(UserResetDto user)
        //{
        //    var data = _globalRepository.ResetUserPassword(user);
        //    return data.AsObjectResult();
        //}




        ///// <summary>
        ///// Sifre deyish (istifadeci)
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[ModelStateControl]
        //[HttpPost]
        //public IActionResult ChangeUserPassword(UserResetDto user)
        //{
        //    var data = _globalRepository.ChangeUserPassword(user);
        //    return data.AsObjectResult();
        //}


        ///// <summary>
        ///// Verilen userId-e gore Istifadecinin rollarini getirir. AssignStatus sutunun da 1(true), 0(false) deyerleri var.  true olduqda hemin istifadeciye muvafiq role verilib demekdir.
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult GetUserRolesByUserId(int userId)
        //{
        //    var data = _globalRepository.GetUserRolesByUserId(userId);
        //    return data.AsObjectResult();
        //}


        ///// <summary>
        ///// Butun rollarin siyahisi
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult GetAllRoles()
        //{
        //    var data = _globalRepository.GetAllRoles();
        //    return data.AsObjectResult();
        //}



        //[HttpGet]
        //public IActionResult GetRoleById(int roleId)
        //{
        //    var data = _globalRepository.GetRoleById(roleId);
        //    return data.AsObjectResult();
        //}



        ///// <summary>
        ///// Verilen rolId-sine gore rollarin menulara baglantisini getirir. AssignStatus sutunun da 1(true), 0(false) deyerleri var. true olduqda hemin rola muvafiq menu icazesi verilib demekdir.
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult GetRoleMenusByRoleId(int roleId)
        //{
        //    var data = _globalRepository.GetRoleMenusByRoleId(roleId);
        //    return data.AsObjectResult();
        //}



        ///// <summary>
        /////  Yadda saxla vurulduqda statusu true olan(secilen menular) bu servise gonderilir
        ///// </summary>
        ///// <param name="saveRoleMenusDto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult SaveRoleMenus([FromBody] SaveRoleMenusDto saveRoleMenusDto)
        //{
        //    var data = _globalRepository.SaveRoleMenus(saveRoleMenusDto);
        //    return data.AsObjectResult();
        //}


        ///// <summary>
        ///// Yeni rolun elave edilmesi ve ya movcud rol uzerinde duzelish edilmesi. (id-0 Add, id>0 Update)
        ///// </summary>
        ///// <param name="role"></param>
        ///// <returns></returns>
        //[ModelStateControl]
        //[HttpPost]
        //public IActionResult AddOrUpdateRole(RoleDto role)
        //{
        //    var data = _globalRepository.AddOrUpdateRole(role);
        //    return data.AsObjectResult();
        //}


        ////[HttpPost]
        ////public IActionResult ChangeRoleStatus(int roleId,bool status)
        ////{
        ////    var data = _globalRepository.ChangeRoleStatus(roleId, status);
        ////    return data.AsObjectResult();
        ////}


        //#endregion


    }
}
