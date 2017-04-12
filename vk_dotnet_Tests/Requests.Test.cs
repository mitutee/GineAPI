using System;
using Xunit;
using vk_dotnet;
using vk_dotnet.Methods;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace vk_dotnet_Tests
{
    public class Requests_Tes
    {

        private ITestOutputHelper output;
        public Requests_Tes(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async void CallAPI_ReturnsResponseString()
        {
            // arrange
            string req = "https://api.vk.com/method/users.get?user_id=210700286&v=5.52";

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

        [Fact]
        public async void SendMessage_ReturnsMessageId()
        {
            // arrange
            string my_id = "80314023";
            string text = $"Hello, this is the message from unit test at {DateTime.UtcNow}";
            string fresh_token = "326fbb2d47e44d7df0e2568770cbb0137ba0db36637028be553965d7608b83e1880913726e165219b3a7a";

            Messages_Methods m = new Messages_Methods(fresh_token);

            // act
            string resp = await m.Send(my_id, text);

            //assert
            Assert.Matches("\\d+", resp);
            output.WriteLine(resp);
        }

        [Fact]
        public async void UseExpiredToken_ThrowsAutorizationException()
        {
            // arrange
            string my_id = "80314023";
            string text = $"Hello, this is the message from unit test at {DateTime.UtcNow}";
            string expired_token = "100fa52eb3d7982fe304603f63f22c76fc8076fb3221a2dce92f580feb611dfbd72df7360791b13bf1a31";

            Messages_Methods m = new Messages_Methods(expired_token);

            // act


            //assert
            await Assert.ThrowsAsync<AutorizationException>(async () => await m.Send(my_id, text));
        }

        [Fact]
        public async void Call_CanMessage_Returns_True()
        {
            // arrange
            string fresh_token = "326fbb2d47e44d7df0e2568770cbb0137ba0db36637028be553965d7608b83e1880913726e165219b3a7a";



            // act
            bool res = BotClient.TokenIsValid(fresh_token);

            //assert
            Assert.True(res);
        }

        private async Task _sendInvalidReq()
        {
            string invalid_req = "https://api.vk.com/method/uAAAAAAAAsrs.gasdfet?user_id=210700286&v=5.52";
            await vk_dotnet.Methods.Method.CallApiAsync(invalid_req);

        }



    }
}
