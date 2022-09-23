using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SynchronizationContextTestHelper
{
    public class SynchronizationContextTestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            Func<Task> function = async () =>
            {
                var declaringType = testMethod.MethodInfo.DeclaringType;
                var instance = Activator.CreateInstance(declaringType);
                await InvokeMethodsWithAttribute<TestInitializeAttribute>(instance, declaringType);
                await (Task)testMethod.MethodInfo.Invoke(instance, null);
                await InvokeMethodsWithAttribute<TestCleanupAttribute>(instance, declaringType);
            };
            var result = new TestResult();
            result.Outcome = UnitTestOutcome.Passed;
            var stopwatch = Stopwatch.StartNew();
            try
            {
                RunInSyncContext(function);
            }
            catch (Exception ex)
            {
                result.Outcome = UnitTestOutcome.Failed;
                result.TestFailureException = ex;
            }
            result.Duration = stopwatch.Elapsed;
            return new[] { result };
        }

        private static async Task InvokeMethodsWithAttribute<A>(object instance, Type declaringType) where A : Attribute
        {
            if (declaringType.BaseType != typeof(object))
                await InvokeMethodsWithAttribute<A>(instance, declaringType.BaseType);

            var methods = declaringType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (var methodInfo in methods)
                if (methodInfo.DeclaringType == declaringType && methodInfo.GetCustomAttribute<A>() != null)
                {
                    if (methodInfo.ReturnType == typeof(Task))
                    {
                        var task = (Task)methodInfo.Invoke(instance, null);
                        if (task != null)
                            await task;
                    }
                    else
                        methodInfo.Invoke(instance, null);
                }
        }

        public static void RunInSyncContext(Func<Task> function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
            var prevContext = SynchronizationContext.Current;
            try
            {
                var syncContext = new DispatcherSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(syncContext);
                var task = function();
                if (task == null)
                    throw new InvalidOperationException();

                var frame = new DispatcherFrame();
                var t2 = task.ContinueWith(x => { frame.Continue = false; }, TaskScheduler.Default);
                Dispatcher.PushFrame(frame);   // execute all tasks until frame.Continue == false

                task.GetAwaiter().GetResult(); // rethrow exception when task has failed
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(prevContext);
            }
        }
    }
}
