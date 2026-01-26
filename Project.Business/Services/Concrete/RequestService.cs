using AutoMapper;
using Azure;
using Azure.Core;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Newtonsoft.Json;
using Project.Business.Services.Abstract;
using Project.Core.Entities.Dtos.CommonDtos;
using Project.Core.Entities.Models;
using Project.Core.Settings;
using Project.Core.Utilities.Results;
using Project.Core.Utilities.Tools;
using Project.DataAccess.Repositories.Abstract.RequestOperations;
using Project.Entities.Dtos.RequestOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Business.Services.Concrete
{
    public class RequestService : IRequestService
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository _requestRepository;

        public RequestService(IMapper mapper, IRequestRepository requestRepository)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
        }

        public async Task<Result> GetFirmRequest(FirmRequestData model)
        {
            var result = new Result();
            OperationResult operationResult;
            RequestDetail requestModel = new RequestDetail();

            if (model == null)
            {
                return new(new { model }, ResultInfo.NotFound);
            }
            Normalize(model);

            var stringPayload = JsonConvert.SerializeObject(model);

            requestModel.UserId = model.UserId;
            requestModel.RequestData = stringPayload;
            requestModel.CreatedDate = DateTime.Now;
            //operationResult = _requestRepository.Add(requestModel);
            //var mapData = _mapper.Map<RequestDetail>(model);

            //if (mapData == null)
            //{
            //operationResult = _requestRepository.Add(mapData);
            //}
            //else
            //{
            //    operationResult = _requestRepository.Update(data);
            //}

            string responseData = await OctosIntegration(model);

            

            requestModel.ResponseData = responseData;
            result.Data = responseData; //_requestRepository.FindById(1).ResponseData;// responseData;
            operationResult = _requestRepository.Add(requestModel);

            return result;
        }

        private async Task<string>  OctosIntegration(FirmRequestData model)
        {
            Result result = new Result();
            HttpMethods http = new HttpMethods();
            BasicAuthDto basicAuth = new()
            {
                Username = AppSettings.Settings.OctosApiCredentials.Username,
                Password = AppSettings.Settings.OctosApiCredentials.Password,
            };

            try
            {
                string url = $"https://expressdata.info/api/ClientOrder/CreateOrder";
                string apiResponse = await http.PostAsync(url, content: model.OrderInfo, basicAuth: basicAuth);

                return apiResponse;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        void Normalize(FirmRequestData model)
        {
            var o = model.OrderInfo;

            o.SenderVoen ??= "";
            o.ReceiverVoen ??= "";
            o.KasparCode ??= "";

            o.TransportNumber ??= new();
            o.TransportDetail ??= new();
            o.OrderTypes ??= new();
            o.Notes ??= new();
        }

        public async Task<Result> GetOrderResponse(RequestTokenData model)
        {
            Result result = new Result();
            RequestDetail requestModel = new RequestDetail();

            HttpMethods http = new HttpMethods();
            List<ResponseData> responseList = new List<ResponseData>();
            OrderResponseData orderResponseData = new OrderResponseData(); 

            BasicAuthDto basicAuth = new()
            {
                Username = AppSettings.Settings.OctosApiCredentials.Username,
                Password = AppSettings.Settings.OctosApiCredentials.Password,
            };

            if (model == null)
            {
                return new(new { model }, ResultInfo.NotFound);
            }

            var stringPayload = JsonConvert.SerializeObject(model);
            requestModel.UserId = model.UserId;
            requestModel.RequestData = stringPayload;
            requestModel.CreatedDate = DateTime.Now;

            foreach (var token in model.TokenList)
            {
                ResponseData responseData = new ResponseData();
                string url = $"https://expressdata.info/api/ClientOrder/GetClientOrderInfo/" + token.Token;
                string apiResponse = await http.GetAsync(url, basicAuth: basicAuth);

                OrderResponseData orderResponse = JsonConvert.DeserializeObject<OrderResponseData>(apiResponse);

                responseData.Token = token.Token;
                responseData.OrderData = orderResponse;

                responseList.Add(responseData);
            }

            requestModel.ResponseData = JsonConvert.SerializeObject(responseList);
            _requestRepository.Add(requestModel);

            result.Data = requestModel.ResponseData;

            return result;
        }

    }
}
