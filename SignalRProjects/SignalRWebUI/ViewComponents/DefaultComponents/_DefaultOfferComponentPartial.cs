using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Dtos.DiscountDtos;
using Newtonsoft.Json;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultOfferComponentPartial:ViewComponent
    {
		private readonly IHttpClientFactory _httpClientFactory;
		public _DefaultOfferComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult>InvokeAsync()
        {
          var client =_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5084/api/Discount");
            var jsonData= await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultDiscountDto>>(jsonData);
            return View(values);

        }
    }
}
