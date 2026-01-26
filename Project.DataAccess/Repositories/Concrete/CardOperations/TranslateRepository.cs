using AutoMapper;
using Microsoft.Data.SqlClient;
using Project.CardOperations.API.Infrastructure.Entities.Dtos.TranslateDtos;
using Project.DataAccess.Repositories.Abstract.CardOperations;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;
using Project.Core.Utilities.Results;
using Project.Core.Entities.SPModels.CardOperations;

namespace Project.DataAccess.Repositories.Concrete.CardOperations
{
    public class TranslateRepository : EntityRepositoryBase<MenuLang, ProjectAppDbContext>, ITranslateRepository
    {
        private readonly IObjectContentsRepository _objectContentRepository;
        private readonly IMapper _mapper;
        private readonly ICardLangRepository _cardLangRepository;
        private readonly IModuleLangRepository _moduleRepository;

        public TranslateRepository(IMapper mapper, ICardLangRepository cardLangRepository, IModuleLangRepository moduleRepository, IObjectContentsRepository objectContentRepository) : base(mapper)
        {
            _mapper = mapper;
            _cardLangRepository = cardLangRepository;
            _moduleRepository = moduleRepository;
            _objectContentRepository = objectContentRepository;
        }
        public Result GetMenuNames()
        {
            var result = new Result();
            List<SqlParameter> parameters = new();
            var runProcedure = EfDbTools.ExecuteProcedure<SP_GetMenuWithModule>("CRD.SP_GetMenuWithModule", parameters);
            result.Data = runProcedure;
            return result;
        }
        public Result GetMenuById(int id)
        {
            var result = new Result();
            var getMenus = GetAll(x => x.MenuId == id && x.Status == true).Select(s => new
            {
                s.LangId,
                s.Value
            });
            result.Data = getMenus;
            return result;
        }
        public Result Save(EditMenuNameRequest edit)
        {
            var result = new Result();
            var map = _mapper.Map<List<MenuLang>>(edit.Language);
            foreach (var item in edit.Language)
            {
                var checkLang = Get(x => x.MenuId == edit.MenuId && x.LangId == item.LangId);
                if (checkLang == null)
                {
                    var mapRow = _mapper.Map<MenuLang>(item);
                    mapRow.MenuId = edit.MenuId;
                    var add = Add(mapRow);
                    if (add.ResultInfo == ResultInfo.SaveFailure)
                    {
                        result.ResultInfo = ResultInfo.SaveFailure;
                    }
                    result.ResultInfo = ResultInfo.SaveSuccess;
                }
                else
                {
                    var mapRow = _mapper.Map<MenuLang>(item);
                    mapRow.Id = checkLang.Id;
                    mapRow.Status = true;
                    mapRow.MenuId = edit.MenuId;
                    var add = Update(mapRow);
                    if (add.ResultInfo == ResultInfo.SaveFailure)
                    {
                        result.ResultInfo = ResultInfo.SaveFailure;
                    }
                    result.ResultInfo = ResultInfo.SaveSuccess;
                }
            }

            var getDetails = GetAll(x => x.MenuId == edit.MenuId && x.Status == true);
            var mapDetail = _mapper.Map<List<MenuLang>>(edit.Language);
            foreach (var service in getDetails.Where(s => s.Status == true && s.MenuId == edit.MenuId))
            {
                service.Status = false;
                foreach (var item in mapDetail.Where(x => x.LangId == service.LangId))
                {
                    service.Status = true;
                }
                Update(service);
            }
            return result;
        }

        public Result GetModules()
        {
            var result = new Result();
            var getModules = _moduleRepository.GetAll(x => x.Status == true).Select(s => new
            {
                s.ModulId,
                s.Value
            });
            result.Data = getModules;
            return result;
        }
        public Result GetModuleById(int id)
        {
            var result = new Result();
            var getModules = _moduleRepository.GetAll(x => x.ModulId == id && x.Status == true).Select(s => new
            {
                s.LangId,
                s.Value
            });
            result.Data = getModules;
            return result;
        }
        public Result SaveModul(SaveModuleRequest edit)
        {
            var result = new Result();
            var map = _mapper.Map<List<ModuleLang>>(edit.Language);
            foreach (var item in edit.Language)
            {
                var checkLang = _moduleRepository.Get(x => x.ModulId == edit.ModulId && x.LangId == item.LangId);
                if (checkLang == null)
                {
                    var mapRow = _mapper.Map<ModuleLang>(item);
                    mapRow.ModulId = edit.ModulId;
                    var add = _moduleRepository.Add(mapRow);
                    if (add.ResultInfo == ResultInfo.SaveFailure)
                    {
                        result.ResultInfo = ResultInfo.SaveFailure;
                    }
                    result.ResultInfo = ResultInfo.SaveSuccess;
                }
                else
                {
                    var mapRow = _mapper.Map<ModuleLang>(item);
                    mapRow.Id = checkLang.Id;
                    mapRow.Status = true;
                    mapRow.ModulId = edit.ModulId;
                    var add = _moduleRepository.Update(mapRow);
                    if (add.ResultInfo == ResultInfo.SaveFailure)
                    {
                        result.ResultInfo = ResultInfo.SaveFailure;
                    }
                    result.ResultInfo = ResultInfo.SaveSuccess;
                }
            }

            var getDetails = _moduleRepository.GetAll(x => x.ModulId == edit.ModulId && x.Status == true);
            var mapDetail = _mapper.Map<List<ModuleLang>>(edit.Language);
            foreach (var service in getDetails.Where(s => s.Status == true && s.ModulId == edit.ModulId))
            {
                service.Status = false;
                foreach (var item in mapDetail.Where(x => x.LangId == service.LangId))
                {
                    service.Status = true;
                }
                _moduleRepository.Update(service);
            }
            return result;
        }



        public Result GetObjectNames()
        {
            var result = new Result();
            List<SqlParameter> parameters = new();
            var runProcedure = EfDbTools.ExecuteProcedure<SP_GetObjectContent>("CRD.SP_GetObjectContent", parameters);
            result.Data = runProcedure;
            return result;
        }
        public Result GetObjectById(int id)
        {
            var result = new Result();
            var getMenus = _objectContentRepository.GetAll(x => x.ObjectId == id && x.Status == true).Select(s => new
            {
                s.LangId,
                s.Value
            });
            result.Data = getMenus;
            return result;
        }
        public Result SaveObject(ObjectRequest edit)
        {
            var result = new Result();
            var map = _mapper.Map<List<ObjectContentsLang>>(edit.Language);
            foreach (var item in edit.Language)
            {
                var checkLang = _objectContentRepository.Get(x => x.ObjectId == edit.ObjectId && x.LangId == item.LangId);
                if (checkLang == null)
                {
                    var mapRow = _mapper.Map<ObjectContentsLang>(item);
                    mapRow.ObjectId = edit.ObjectId;
                    var add = _objectContentRepository.Add(mapRow);
                    if (add.ResultInfo == ResultInfo.SaveFailure)
                    {
                        result.ResultInfo = ResultInfo.SaveFailure;
                    }
                    result.ResultInfo = ResultInfo.SaveSuccess;
                }
                else
                {
                    var mapRow = _mapper.Map<ObjectContentsLang>(item);
                    mapRow.Id = checkLang.Id;
                    mapRow.Status = true;
                    mapRow.ObjectId = edit.ObjectId;
                    var add = _objectContentRepository.Update(mapRow);
                    if (add.ResultInfo == ResultInfo.SaveFailure)
                    {
                        result.ResultInfo = ResultInfo.SaveFailure;
                    }
                    result.ResultInfo = ResultInfo.SaveSuccess;
                }
            }
            var getDetails = _objectContentRepository.GetAll(x => x.ObjectId == edit.ObjectId && x.Status == true);
            var mapDetail = _mapper.Map<List<ObjectContentsLang>>(edit.Language);
            foreach (var service in getDetails.Where(s => s.Status == true && s.ObjectId == edit.ObjectId))
            {
                service.Status = false;
                foreach (var item in mapDetail.Where(x => x.LangId == service.LangId))
                {
                    service.Status = true;
                }
                _objectContentRepository.Update(service);
            }
            return result;
        }
    }
}