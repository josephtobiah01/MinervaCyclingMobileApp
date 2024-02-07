using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Models.ApiRequest;
using MinervaCyclingMobileApp.Models.ApiResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Services
{
    public class CreateContactService : ICreateContactService
    {
        private readonly IApiManager _apiManager;

        public CreateContactService(IApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        public async Task<CreateContactResponse> CreateContact(CreateContactRequest createContactRequest)
        {

            try
            {
                var contactData = new
                {
                    userLang = "EN",
                    firstname = createContactRequest.FirstName,
                    lastname = createContactRequest.LastName,
                    email = createContactRequest.Email,
                    dateOfBirth = createContactRequest.DateOfBirth?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            };

                string jsonContent = JsonConvert.SerializeObject(contactData);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var result = await _apiManager.SendForResponseAsync<CreateContactResponse>("/api/Contact/AddContact", HttpMethod.Put, content);


                if (result != null && result.Id > 0) 
                {
                    return result;
                }
                else
                {
                    return new CreateContactResponse
                    {
                        StatusCode = 0000,
                        Message = "The contact could not be created."
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                await App.Current.MainPage.DisplayAlert("Sorry", $"There is an error: {ex.Message}", "Ok");

                return new CreateContactResponse
                {
                    StatusCode = 0000,
                    Message = $"The contact could not be created. Error Message: {ex.Message}"
                };
            }
           
        }

    }
}
