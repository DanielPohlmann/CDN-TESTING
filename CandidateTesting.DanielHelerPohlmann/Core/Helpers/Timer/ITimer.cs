namespace CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer
{
    public interface ITimer : IDisposable
    {
        void StartTimer();

        void StopTimer();

        void ResetTimer();

        long GetTotalTime();
    }
}
