using System;
using Xunit;
using vk_dotnet;
using vk_dotnet.Methods;
using System.Threading.Tasks;

namespace vk_dotnet_Tests
{
    public class Requests_Tes
    {
        [Fact]
        public async void CallAPI_ReturnsResponseString()
        {
            // arrange
            string req  = "https://api.vk.com/method/users.get?user_id=210700286&v=5.52";

            // act
            string r = await vk_dotnet.Methods.Method.CallApiAsync(req);

            // assert
            Assert.IsType<string>(r);

        }

        [Fact]
        public async void CallAPI_ThrowsApiException()
        {
            // arrange

            // act
  
            // assert
            await Assert.ThrowsAnyAsync<ApiException>(_sendInvalidReq);

        }

        private async Task _sendInvalidReq()
        {
            string invalid_req = "https://api.vk.com/method/uAAAAAAAAsrs.gasdfet?user_id=210700286&v=5.52";
            await vk_dotnet.Methods.Method.CallApiAsync(invalid_req);
        }



    }
}
