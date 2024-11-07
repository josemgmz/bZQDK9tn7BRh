

using System;
using NUnit.Framework;
using UnityEngine;

namespace Framework.Test
{
    [TestFixture]
    public class ExecutionPerformanceTest
    {
    
        [Test]
        public void Performance()
        {
            var testClass = new TestClass();
            
            // Measure the time taken to execute the method directly
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var elapsedMs = 0L;
            
            for (var i = 0; i < 1000; i++)
            {
                testClass.TestMethod();
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Debug.Log("Direct Execution Time: " + elapsedMs);
            
            // Measure the time taken to execute the method using reflection
            watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 1000; i++)
            {
                var method = testClass.GetType().GetMethod("TestMethod");
                method.Invoke(testClass, null);
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Debug.Log("Reflection Execution Time: " + elapsedMs);
            
            // Now test the cost of using delegates
            watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 1000; i++)
            {
                Action action = testClass.TestMethod;
                action();
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Debug.Log("Delegate Execution Time: " + elapsedMs);
        }
    }

    #region Test Class

    public class TestClass
    {
        public void TestMethod()
        {
            double result = 0;
            for (var i = 0; i < 1000000; i++)
            {
                result += Math.Sqrt(i) * Math.Sin(i) * Math.Cos(i);
            }
        }
    }

    #endregion
}
