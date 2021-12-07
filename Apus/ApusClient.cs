using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Apus
{
    public class ApusClient: IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _userName;
        private readonly string _password;
        private DateTime _authenticationTime;
        private const double _tokenTimeToLive = 8D;

        public ApusClient(string baseUrl, string userName, string password)
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };
            _userName = userName;
            _password = password;
            _authenticationTime = new DateTime();
        }

        public bool IsAuthenticated { get; private set; }

        public async Task<bool> AuthenticateAsync(bool forceAuthentication = false)
        {
            if (!forceAuthentication && IsAuthenticated && (DateTime.Now - _authenticationTime).TotalHours < _tokenTimeToLive)
            {
                return true;
            }

            var url = "/APUS/rest/login";
            var message = $"{{\"userName\":\"{_userName}\",\"apiPassword\":\"{_password}\"}}";
            var content = new StringContent(message, Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue(mediaType: "application/json") }
            };

            try
            {
                var response = await _httpClient.PostAsync(url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<AuthenticationResult>(responseString);
                    IsAuthenticated = !string.IsNullOrEmpty(result?.Token);
                    if (IsAuthenticated)
                    {
                        _authenticationTime = DateTime.Now;
                    }
                    return IsAuthenticated;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<InvoicePublicSimple>> GetInvoicesAsync(DateTime? shipmentDateFrom, DateTime? shipmentDateTo, uint limit = 10000)
        {
            var isAuthenticated = await AuthenticateAsync();
            if (!isAuthenticated)
            {
                throw new ArgumentException("Lietotājs nav autentificējies APUS sistēmā");
            }

            var url = "/APUS/rest/ws/invoices";
            var shipmentDateFromString = shipmentDateFrom.HasValue ? $"&shipmentDateFrom={shipmentDateFrom.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")}" : string.Empty;
            var shipmentDateToString = shipmentDateTo.HasValue ? $"&shipmentDateTo={shipmentDateTo.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")}" : string.Empty;
            var parameters = $"?limit={limit}{shipmentDateFromString}{shipmentDateToString}".Replace(":", "%3A").Replace("+", "%2B");

            var response = await _httpClient.GetAsync(url + parameters);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InvoicePublicSimple[]>(responseString);
                return result;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<InvoicePublicSimple>> GetInvoicesByCarrierInvoiceNumberAsync(string invoiceNumber, uint limit = 10)
        {
            await AuthenticateAsync();

            var url = "/APUS/rest/ws/invoices";
            var parameters = $"?limit={limit}&cargoInvoiceNumber={invoiceNumber}";

            var response = await _httpClient.GetAsync(url + parameters);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InvoicePublicSimple[]>(responseString);
                if (response == null || result.Length == 0)
                {
                    return new List<InvoicePublicSimple>();
                }
                var companyRelatedInvoices = result;
                return companyRelatedInvoices.ToList();
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<InvoicePublicSimple> GetFirstInvoiceByCarrierInvoiceNumberAsync(string invoiceNumber, ulong receiverOrganizationId, bool excludeReceiverOrganization)
        {
            await AuthenticateAsync();

            var url = "/APUS/rest/ws/invoices";
            var parameters = $"?limit={1}&cargoInvoiceNumber={invoiceNumber}&receiverId={receiverOrganizationId}";

            var response = await _httpClient.GetAsync(url + parameters);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InvoicePublicSimple[]>(responseString);
                if (!excludeReceiverOrganization)
                {
                    return result.FirstOrDefault();
                }
                var allDealNumberInvoices = await GetInvoicesByCarrierInvoiceNumberAsync(invoiceNumber);
                return allDealNumberInvoices.Where(n => result.All(m => n.InvoiceNumber != m.InvoiceNumber)).FirstOrDefault();
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<InvoicePublic> GetInvoiceByApusInvoiceNumberAsync(string invoiceNumber)
        {
            await AuthenticateAsync();

            var url = $"/APUS/rest/ws/invoices/{invoiceNumber}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InvoicePublic>(responseString);
                return result;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<InvoicePublic> PostInvoiceAsync(InvoicePublic invoice)
        {
            await AuthenticateAsync();

            var url = "/APUS/rest/ws/invoices";
            var message = JsonConvert.SerializeObject(invoice, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(message, Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue(mediaType: "application/json") }
            };
            var response = await _httpClient.PostAsync(url, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InvoicePublic>(responseString);
                return result;
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new ArgumentException(errorMessage);
        }

        public async Task<InvoicePublic> ChangeInvoiceStatusAsync(string invoiceNumber, InvoiceStatusCommands newStatus, DateTime updateTime)
        {
            await AuthenticateAsync();

            var url = $"/APUS/rest/ws/invoices/{invoiceNumber}/status/{newStatus.ToString().ToUpper()}?updateDate={updateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz").Replace(":", "%3A").Replace("+", "%2B")}";
            var response = await _httpClient.PostAsync(url, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InvoicePublic>(responseString);
                return result;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<OrganizationData> GetOrganizationByRegistrationNumberAsync(string fullRegistrationNumber)
        {
            await AuthenticateAsync();

            var url = $"/APUS/rest/ws/organizations";
            var parameters = $"?registrationNumber={fullRegistrationNumber}&limit=1&offset=0";

            var response = await _httpClient.GetAsync(url + parameters);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrganizationData[]>(responseString);
                return result != null && result.Length > 0 ? result[0] : null;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<OrganizationData> GetOrganizationByVatNumberAsync(string fullRegistrationNumber)
        {
            await AuthenticateAsync();

            var url = $"/APUS/rest/ws/organizations";
            var parameters = $"?fullRegistrationNumber={fullRegistrationNumber}&limit=1&offset=0";

            var response = await _httpClient.GetAsync(url + parameters);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrganizationData[]>(responseString);
                return result != null && result.Length > 0 ? result[0] : null;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<OrganizationData> PostOrganizationAsync(OrganizationData organizationData)
        {
            await AuthenticateAsync();

            var url = $"/APUS/rest/ws/organizations";
            var message = JsonConvert.SerializeObject(organizationData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(message, Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue(mediaType: "application/json") }
            };
            var response = await _httpClient.PostAsync(url, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrganizationData>(responseString);
                return result;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<FacilityPublic> PostFacilityAsync(FacilityPublic facilityData)
        {
            await AuthenticateAsync();

            var url = $"/APUS/rest/ws/facilities";
            var message = JsonConvert.SerializeObject(facilityData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(message, Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue(mediaType: "application/json") }
            };
            var response = await _httpClient.PostAsync(url, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<FacilityPublic>(responseString);
                return result;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<FacilityPublic>> GetFacilityAsync(FacilityGroupType facilitiesType, ulong organizationId, uint limit = 10000)
        {
            await AuthenticateAsync();

            var facilityTypeString = facilitiesType.ToString().ToUpper();
            var url = $"/APUS/rest/ws/facilities/{facilityTypeString}?limit={limit}&organizationId={organizationId}";

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<FacilityPublic[]>(responseString);
                return result;
            }
            throw new ArgumentException(await response.Content.ReadAsStringAsync());
        }

        void IDisposable.Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
