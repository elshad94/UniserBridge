using Project.Core.Utilities.Results;

public static class ResultDataGenerator
{
    private static IResultData Generate(object data, ResultInfo resultInfo) => new ResultData
    {
        Data = data,
        Message = resultInfo is ResultInfo.Success or ResultInfo.NotImplemented ? string.Empty : CommonRepository.GetResultMessageValue(resultInfo),
        StatusCode = (int)resultInfo,
        Status = (int)resultInfo < 2000 && resultInfo != ResultInfo.NotImplemented
    };

    internal static IResultData Generate(Result result) => Generate(result.Data, result.ResultInfo);

    internal static IResultData Generate(ResultInfo resultInfo) => Generate(null, resultInfo);
}