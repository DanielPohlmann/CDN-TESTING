using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Core;
using System.Linq.Expressions;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Helper
{
    public static class MockHelper
    {
        private static Expression<Action<ILogger<T>>> Verify<T>(LogLevel level)
        {
            return x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
            );
        }

        public static void Verify<T>(this Mock<ILogger<T>> mock, LogLevel level, Times times)
        {
            mock.Verify(Verify<T>(level), times);
        }
    }
}
