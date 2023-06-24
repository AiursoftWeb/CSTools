using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.WebSockets;
using System.Text;
using Aiursoft.CSTools.Tools;

namespace Aiursoft.CSTools.Tests.Tools
{
    [TestClass]
    public class WebSocketExtendsTests
    {
        [TestMethod]
        public async Task SendMessage_ShouldSendTextMessage()
        {
            // Arrange
            var ws = new TestWebSocket();
            var message = "Hello, world!";

            // Act
            await ws.SendMessage(message);

            // Assert
            Assert.AreEqual(WebSocketMessageType.Text, ws.MessageType);
            Assert.IsTrue(ws.MessageSent);
            Assert.AreEqual(message, Encoding.UTF8.GetString(ws.Buffer!));
        }
    }

    public class TestWebSocket : WebSocket
    {
        public bool MessageSent { get; private set; }
        public WebSocketMessageType MessageType { get; private set; }
        public byte[]? Buffer { get; private set; }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
        {
            MessageSent = true;
            MessageType = messageType;
            Buffer = buffer.ToArray();
            return Task.CompletedTask;
        }

        public override WebSocketCloseStatus? CloseStatus => throw new NotImplementedException();
        public override string CloseStatusDescription => throw new NotImplementedException();
        public override WebSocketState State => throw new NotImplementedException();
        public override string SubProtocol => throw new NotImplementedException();
    }
}