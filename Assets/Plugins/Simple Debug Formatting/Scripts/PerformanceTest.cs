using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace PerformanceTest
{
    public static class PerformanceTest
    {
        public static void Start(Action _Action)
        {
            var _stopwatch = new Stopwatch();
        
            _stopwatch.Start();
            _Action.Invoke();
            _stopwatch.Stop();
        
            Debug.Log(string.Concat("<b>Elapsed ticks: </b>", _stopwatch.ElapsedTicks.ToString()));
        }
    }   
}