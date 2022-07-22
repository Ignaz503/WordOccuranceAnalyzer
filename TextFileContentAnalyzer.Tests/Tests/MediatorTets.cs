using TextFileContentAnalyzer.Testing.Core.Asserts;
using TextFileContentAnalyzer.Testing.Core;
using TextFileContentAnalyzer.Core.Mediator;

namespace TextFileContentAnalyzer.Tests.Tests;

[Test]
public class MediatorTets
{
    public struct Message { }

    public class TestObject 
    {
        readonly IMediator<Message> _mediator;

        public TestObject(IMediator<Message> mediator)
        {
            _mediator = mediator;
            _mediator.Subsrcibe(OnMessage);
        }

        void OnMessage(Message msg) 
        {
            _mediator.Unsubscribe(OnMessage);
        }

    }

    [Fact]
    public void MediatorDoesNot_RemoveHandler_IfBusy() 
    {
        var mediator = new Publisher<Message>();
        try
        {
            var obj = new TestObject(mediator);

            mediator.Publish(new());
        }
        catch (Exception ex) 
        {
            Assert.Throw(ex);
        }
        Assert.IsTrue(mediator.SubscribedHandlerCount == 1);
    }

}
