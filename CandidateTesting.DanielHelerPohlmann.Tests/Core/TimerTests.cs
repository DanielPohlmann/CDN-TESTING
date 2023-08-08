using Timer = CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer.Timer;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Core
{
    public class TimerTests
    {
        [Fact]
        public async Task Timer_StartStop_GetTotalTime()
        {
            // Arrange
            var timer = new Timer();
            timer.StartTimer();

            // Act
            await Task.Delay(1000);
            timer.StopTimer();

            // Assert
            var totalTime = timer.GetTotalTime();
            Assert.True(totalTime >= 1000 && totalTime < 1100);
        }
    }
}
