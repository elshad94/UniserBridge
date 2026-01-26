using AutoMapper;
using Microsoft.Data.SqlClient;
using Project.DataAccess.Repositories.Abstract.CardOperations;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;
using Project.Core.Entities.SPModels;   
using Project.Core.Utilities.Results;

namespace Project.DataAccess.Repositories.Concrete.CardOperations
{
    public class CardLangRepository : EntityRepositoryBase<CardLang, ProjectAppDbContext>, ICardLangRepository
    {
        private readonly IMapper _mapper;
        public CardLangRepository(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }

        public Result GetCardLangByType(string type, int cardId)
        {
            var result = new Result();
            var parameters = new List<SqlParameter> { new("Type", type), new("CardId", cardId) };
            var runProcedure = EfDbTools.ExecuteProcedure<SP_GetCardLangByType>("CRD.SP_GetCardLangByType", parameters);
            result.Data = runProcedure;
            return result;
        }
    }
}
