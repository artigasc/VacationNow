using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.Helper {

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
    
        
		public async Task<ServiceResponse> ExecutePost<T>(object valObject) where T : class {
			var vPara = JsonConvert.SerializeObject(valObject, Formatting.Indented);
			var vContent = new StringContent(vPara, Encoding.UTF8, "application/json");
			var vResponse = _vHttpClient.PostAsync(new Uri($"{_vBaseUrl}", UriKind.Absolute), vContent).GetAwaiter().GetResult();
			var vResponseJson = await vResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var vResult = new ServiceResponse {
				Content = JsonConvert.DeserializeObject<T>(vResponseJson),
				Status = vResponse.StatusCode,
				StatusDescription = vResponse.ReasonPhrase
			};
			return vResult;
		}

		
		public void CleanParameters() {
			_vParameters.Clear();
		}

	}

}
