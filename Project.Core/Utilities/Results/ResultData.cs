namespace Project.Core.Utilities.Results
{
    public class ResultData : IResultData
    {
        internal ResultData()
        {

        }

        public object Data { get; internal set; }

        public string Message { get; internal set; }

        public int StatusCode { get; internal set; }

        public bool Status { get; internal set; }
    }
}