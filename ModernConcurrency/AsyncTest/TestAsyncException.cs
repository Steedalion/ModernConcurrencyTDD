using System;

namespace ModernConcurrency
{
    internal class TestAsyncException : Exception
    {
        public TestAsyncException(string shouldFail):base(shouldFail)
        {
        }
    }
}