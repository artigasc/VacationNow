using GoTourWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoTourWeb.Helpers {

	public class ApiClient {

		private readonly HttpClient _vHttpClient;
		private readonly string _vBaseUrl;
		private readonly Dictionary<string, string> _vParameters;

		public ApiClient(string baseUrl) {
			if (string.IsNullOrEmpty(baseUrl))
				throw new ArgumentNullException(nameof(baseUrl));
			_vBaseUrl = baseUrl;
			_vHttpClient = new HttpClient {
				MaxResponseContentBufferSize = 2147483647
			};
			_vHttpClient.DefaultRequestHeaders.Accept.Clear();
			_vHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_vParameters = new Dictionary<string, string>();
		}

		internal Task ExecuteGet<T>(object twitter) {
			throw new NotImplementedException();
		}

		internal Task ExecuteGet<T>(object place, string id) {
			throw new NotImplementedException();
		}

		//internal Task ExecutePost(object addProduct)
		//{
		//    throw new NotImplementedException();
		//}

		public void AddParameter(string key, object value) {
			if (_vParameters.ContainsKey(key)) {
				_vParameters[key] = value.ToString();
			} else {
				_vParameters.Add(key, value.ToString());
			}
		}

		public void AddObjectParameter(string key, object value) {
			var serializedValue = JsonConvert.SerializeObject(value);
			AddParameter(key, serializedValue);
		}



		public async Task<ServiceResponseViewModel> ExecuteGet<T>(string vMethod) where T : class {
			var vFullMethod = $"{_vBaseUrl}/{vMethod}";
			if (_vParameters.Any()) {
				vFullMethod += "?";
				vFullMethod = _vParameters.Aggregate(vFullMethod,
					(current, parameter) => current + $"{parameter.Key}={parameter.Value}&");
				vFullMethod = vFullMethod.Substring(0, vFullMethod.Length - 1);
			}
			var vResponse = await _vHttpClient.GetAsync(vFullMethod).ConfigureAwait(false);

			var vResponseJson = await vResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            var vResult = new ServiceResponseViewModel {
            	Content = JsonConvert.DeserializeObject<T>(vResponseJson),
            	Status = vResponse.StatusCode,
            	StatusDescription = vResponse.ReasonPhrase
            };
            return vResult;
		}

		public async Task<ServiceResponseViewModel> ExecuteGetToString(string vMethod) {
			var vFullMethod = $"{_vBaseUrl}/{vMethod}";
			if (_vParameters.Any()) {
				vFullMethod += "?";
				vFullMethod = _vParameters.Aggregate(vFullMethod,
					(current, parameter) => current + $"{parameter.Key}={parameter.Value}&");
				vFullMethod = vFullMethod.Substring(0, vFullMethod.Length - 1);
			}
			var vResponse = await _vHttpClient.GetAsync(vFullMethod).ConfigureAwait(false);

			var vResponseJson = await vResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = new ServiceResponseViewModel();

   //         var result = new ClientResponseViewModel {
			//	Content = vResponseJson,
			//	Status = vResponse.StatusCode,
			//	StatusDescription = vResponse.ReasonPhrase
			//};
			return result;
		}


        public async Task<ServiceResponseViewModel> ExecuteGet<T>(string method, string id) where T : class {
            var fullMethod = $"{_vBaseUrl}/{method}/{id}";
            //if (_parameters.Any()) {
            //    fullMethod += "?";
            //    fullMethod = _parameters.Aggregate(fullMethod,
            //        (current, parameter) => current + $"{parameter.Key}={parameter.Value}&");
            //    fullMethod = fullMethod.Substring(0, fullMethod.Length - 1);
            //}
            var response = await _vHttpClient.GetAsync(fullMethod).ConfigureAwait(false);

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = new ServiceResponseViewModel {
                Content = JsonConvert.DeserializeObject<T>(responseJson),
                Status = response.StatusCode,
                StatusDescription = response.ReasonPhrase
            };

            return result;
        }


        public async Task<ServiceResponseViewModel> ExecuteGetWithJson<T>(string vMethod, object _object) where T : class {
            var vFullMethod = $"{_vBaseUrl}/{vMethod}";
            var para = JsonConvert.SerializeObject(_object, Formatting.Indented);
            var content = new StringContent(para, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage {
                RequestUri = new Uri(vFullMethod, UriKind.Absolute),
                Method = HttpMethod.Get,
                Content = content
            };
            
            ////request.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(para));
            //request.Content = new StringContent(para, Encoding.UTF8, "application/json");
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var vResponse = _vHttpClient.SendAsync(request).GetAwaiter().GetResult();
            var vResponseJson = await vResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            

            var vResult = new ServiceResponseViewModel {
                Content = JsonConvert.DeserializeObject<T>(vResponseJson),
                Status = vResponse.StatusCode,
                StatusDescription = vResponse.ReasonPhrase
            };
            return vResult;
        }


        //public async Task<ClientResponse> ExecutePut(string method, string nameParameter) {
        //	//var para = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
        //	var para = _vParameters[nameParameter].ToString();
        //	var content = new StringContent(para, Encoding.UTF8, "application/json");
        //	var response = await _vHttpClient.PutAsync(new Uri($"{_vBaseUrl}/{method}", UriKind.Absolute), content).ConfigureAwait(false);
        //	var result = new ClientResponse {
        //		Status = response.StatusCode,
        //		StatusDescription = response.ReasonPhrase,
        //		Content = response.IsSuccessStatusCode
        //	};

        //	return result;
        //}

        //public async Task<ClientResponse> ExecuteDelete(string method, string id) {
        //	var fullMethod = $"{_vBaseUrl}/{method}/{id}";
        //	//if (_parameters.Any()) {
        //	//    fullMethod += "?";
        //	//    fullMethod = _parameters.Aggregate(fullMethod,
        //	//        (current, parameter) => current + $"{parameter.Key}={parameter.Value}&");
        //	//    fullMethod = fullMethod.Substring(0, fullMethod.Length - 1);
        //	//}
        //	var response = await _vHttpClient.DeleteAsync(fullMethod).ConfigureAwait(false);

        //	//var data =
        //	//    JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        //	//var result = new ClientResponse {
        //	//    Content = data.FirstOrDefault().Value as bool?,
        //	//    Status = response.StatusCode,
        //	//    StatusDescription = response.ReasonPhrase
        //	//};

        //	var result = new ClientResponse {
        //		Status = response.StatusCode,
        //		StatusDescription = response.ReasonPhrase,
        //		Content = response.IsSuccessStatusCode
        //	};

        //	return result;
        //}

        //public async Task<ClientResponse> ExecutePost(string method) {
        //	var para = JsonConvert.SerializeObject(_vParameters, Formatting.Indented);
        //	//var para = _parameters["user"].ToString();
        //	var content = new StringContent(para, Encoding.UTF8, "application/json");
        //	var response = await _vHttpClient.PostAsync(new Uri($"{_vBaseUrl}/{method}", UriKind.Absolute), content).ConfigureAwait(false);
        //	var result = new ClientResponse {
        //		Status = response.StatusCode,
        //		StatusDescription = response.ReasonPhrase,
        //		Content = response.IsSuccessStatusCode
        //	};

        //	return result;
        //}

        public async Task<ServiceResponseViewModel> ExecutePost(string vMethod, string vNameParameter) {
			//var para = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
			var para = _vParameters[vNameParameter].ToString();
			var content = new StringContent(para, Encoding.UTF8, "application/json");
			var response = await _vHttpClient.PostAsync(new Uri($"{_vBaseUrl}/{vMethod}", UriKind.Absolute), content).ConfigureAwait(false);
			var result = new ServiceResponseViewModel {
				Status = response.StatusCode,
				StatusDescription = response.ReasonPhrase,
				Content = response.IsSuccessStatusCode
			};

			return result;
		}


		public async Task<ServiceResponseViewModel> ExecutePost<T>(string vMethod) where T : class {
			var para = JsonConvert.SerializeObject(_vParameters, Formatting.Indented);
			var content = new StringContent(para, Encoding.UTF8, "application/json");
			var response = _vHttpClient.PostAsync(new Uri($"{_vBaseUrl}/{vMethod}", UriKind.Absolute), content).GetAwaiter().GetResult();

			var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			var result = new ServiceResponseViewModel {
				Content = JsonConvert.DeserializeObject<T>(responseJson),
				Status = response.StatusCode,
				StatusDescription = response.ReasonPhrase
			};

			return result;
		}

		public async Task<ServiceResponseViewModel> ExecutePost<T>(string method, object _object) where T : class {
			//var para = JsonConvert.SerializeObject(_object);
			var para = JsonConvert.SerializeObject(_object, Formatting.Indented);
			var content = new StringContent(para, Encoding.UTF8, "application/json");
			var response = _vHttpClient.PostAsync(new Uri($"{_vBaseUrl}/{method}", UriKind.Absolute), content).GetAwaiter().GetResult();
			var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			var result = new ServiceResponseViewModel {
				Content = JsonConvert.DeserializeObject<T>(responseJson),
				Status = response.StatusCode,
				StatusDescription = response.ReasonPhrase
			};
			return result;
		}

		public async Task<ServiceResponseViewModel> ExecutePost(string method, object _object) {
			//var para = JsonConvert.SerializeObject(_object);
			var para = JsonConvert.SerializeObject(_object, Formatting.Indented);
			var content = new StringContent(para, Encoding.UTF8, "application/json");
			var response = _vHttpClient.PostAsync(new Uri($"{_vBaseUrl}/{method}", UriKind.Absolute), content).GetAwaiter().GetResult();
			var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			var result = new ServiceResponseViewModel {
				Content = JsonConvert.DeserializeObject(responseJson),
				Status = response.StatusCode,
				StatusDescription = response.ReasonPhrase
			};
			return result;
		}

		public void CleanParameters() {
			_vParameters.Clear();
		}

	}

}
