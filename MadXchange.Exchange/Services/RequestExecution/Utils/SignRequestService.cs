﻿using MadXchange.Exchange.Domain.Models;
using MadXchange.Exchange.Infrastructure.Repositories;
using MadXchange.Exchange.Interfaces;
using System;
using ServiceStack;

namespace MadXchange.Exchange.Services.RequestExecution
{
    public interface ISignRequests
    {
        string SignRequest(Guid accountId, string url, string param);
    }


    public class SignRequestService : ISignRequests
    {

        private readonly IExpiresTimeProvider _expiresTimeProvider;
        private readonly ISignatureProvider _signatureProvider;
        private readonly IApiKeySetRepository _apiKeySetRepository;

        public SignRequestService(IApiKeySetRepository apikeysetRepo) 
        {
            _expiresTimeProvider = new ExpiresTimeProvider();
            _signatureProvider = new SignatureProvider();
            _apiKeySetRepository = apikeysetRepo;
        }

        public SignRequestService(IApiKeySetRepository apikeysetRepo, ISignatureProvider signatureProvider, IExpiresTimeProvider timeProvider)
        {
            _expiresTimeProvider = timeProvider;
            _signatureProvider = signatureProvider;
            _apiKeySetRepository = apikeysetRepo;
        }


        public string SignRequest(Guid accountId, string url, string param)
        {
            var account = _apiKeySetRepository.GetAccount(accountId);
            if (account is null) throw new InvalidOperationException($"account with given key was not found: {accountId}");           
            switch (account.Exchange) 
            {                
                default:
                    var timeExpires = _expiresTimeProvider.Get(account.Exchange);
                    return SignByBit(account, url, param, timeExpires);
            }
        }

        private string SignByBit(ApiKeySet account, string url, string param, long timeExpires) 
        {
            if (param.StartsWith('?')) param = param.Substring(1); //removes trailing q.m. of param string in form-format
            var signString = "".AddQueryParam("api-key", account.ApiKey).CombineWith(param).AddQueryParam("timestamp", timeExpires);
            string signature = _signatureProvider.CreateSignature(account.ApiSecret, signString);
            return url.CombineWith(signString).AddQueryParam("sign", signature);
        }
    }
}