using System.Diagnostics;

namespace CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer
{
    public class Timer : ITimer
    {
        private readonly Stopwatch _timer;

        public Timer()
        {
            _timer = new Stopwatch();
        }

        public long GetTotalTime()
        {
            return _timer.ElapsedMilliseconds;
        }

        public void ResetTimer()
        {
            _timer.Reset();
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _timer.Stop();
            _timer.Reset();
        }

        ~Timer()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
}
