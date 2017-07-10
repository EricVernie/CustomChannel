using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using OnPremise.DirectLine.Connector.Client;
namespace OnPremise.DirectLine.Connector.Test
{
    [TestClass]
    public class UnitTest1
    {
        ClientHelper _client;
        [TestInitialize]
        public void Initialize()
        {
            _client = new Client.ClientHelper("http://localhost:28321");
        }
        [TestMethod]
        public async Task Connector_Start_New_Conversation()
        {
            var conversation = await _client.StartNewConversationAsync();
            Console.Out.WriteLine($"ConversationId:{conversation.ConversationId}");
            
        }
        [TestMethod]
        public async Task Connector_Get_Token()
        {
            var JWToken = await _client.GetTokenAsync();
            Console.Out.WriteLine($"Token:{JWToken.AccessToken}");
            Console.Out.WriteLine($"Expire in :{JWToken.ExpiresIn}");
        }
        [TestMethod]
        public async Task Connector_Get_Author()
        {
            var me = await _client.GetAuthor();
            Console.Out.WriteLine($"author:{me.Author}");
            Console.Out.WriteLine($"date:{me.Date}");

        }
    }
}
