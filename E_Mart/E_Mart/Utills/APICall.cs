﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Utills
{
    public class APICall
    {
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        private HttpClient _httpClient;

        private HttpClientHandler GetHttpClientHandler()
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
            return httpClientHandler;
        }
        private void CreateHttpClient()
        {
            _httpClient = new HttpClient(GetHttpClientHandler());
        }



        public async Task<T> CallApiGetAsync<T>(string apiPath) where T : new()
        {
            if (_httpClient == null)
            {
                CreateHttpClient();
            }
            T data = new T();
            if (_httpClient.BaseAddress == null)
              _httpClient.BaseAddress = new Uri(App.APIBaseURL);
            
            var response = await _httpClient.GetAsync(apiPath);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(result);
            }
            return data;
        }
        public async Task<T> CallApiPostAsync<T>(string apiPath, T postData) where T : new()
        {
            if (_httpClient == null)
            {
                CreateHttpClient();
            }

            T data = new T();

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(App.APIBaseURL);
            }
            var JsonData = JsonConvert.SerializeObject(postData);
            var StringData = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiPath, StringData);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(result);
            }
            return data;
        }
        public async Task<T> CallApiPutAsync<T>(string apiPath, int id, T postData) where T : new()
        {
            apiPath = apiPath + "/" + id;
            if (_httpClient == null)
            {
                CreateHttpClient();
            }

            T data = new T();

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(App.APIBaseURL);
            }
            var JsonData = JsonConvert.SerializeObject(postData);
            var StringData = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiPath, StringData);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(result);
                return data;
            }
            return default(T);
        }
        public async Task<T> CallApiPostAsync<T, X>(string apiPath, X postData) where T : new() where X : new()
        {
            if (_httpClient == null)
            {
                CreateHttpClient();
            }

            T data = new T();
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(App.APIBaseURL);
            }
            var JsonData = JsonConvert.SerializeObject(postData);
            var StringData = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiPath, StringData);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(result);
            }
            return data;
        }
        public async Task<bool> CallApiDeleteAsync(string apiPath)
        {
            if (_httpClient == null)
            {
                CreateHttpClient();
            }

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(App.APIBaseURL);
            }
            var response = await _httpClient.DeleteAsync(apiPath);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<string> CallApiPostimageAsync(string apiPath, MultipartFormDataContent Content)
        {
            if (_httpClient == null)
            {
                CreateHttpClient();
            }

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(App.APIBaseURL);
            }
            var response = await _httpClient.PostAsync(apiPath, Content);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;

        }



    }
}
