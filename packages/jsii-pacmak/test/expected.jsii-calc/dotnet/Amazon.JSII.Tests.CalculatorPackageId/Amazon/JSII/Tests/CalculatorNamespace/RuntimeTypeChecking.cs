using Amazon.JSII.Runtime.Deputy;
using System;

namespace Amazon.JSII.Tests.CalculatorNamespace
{
    [JsiiClass(typeof(RuntimeTypeChecking), "jsii-calc.RuntimeTypeChecking", "[]")]
    public class RuntimeTypeChecking : DeputyBase
    {
        public RuntimeTypeChecking(): base(new DeputyProps(new object[]{}))
        {
        }

        protected RuntimeTypeChecking(ByRefValue reference): base(reference)
        {
        }

        protected RuntimeTypeChecking(DeputyProps props): base(props)
        {
        }

        [JsiiMethod("methodWithDefaultedArguments", null, "[{\"name\":\"arg1\",\"type\":{\"primitive\":\"number\",\"optional\":true}},{\"name\":\"arg2\",\"type\":{\"primitive\":\"string\",\"optional\":true}},{\"name\":\"arg3\",\"type\":{\"primitive\":\"date\",\"optional\":true}}]")]
        public virtual void MethodWithDefaultedArguments(double? arg1, string arg2, DateTime? arg3)
        {
            InvokeInstanceVoidMethod(new object[]{arg1, arg2, arg3});
        }

        [JsiiMethod("methodWithOptionalAnyArgument", null, "[{\"name\":\"arg\",\"type\":{\"primitive\":\"any\",\"optional\":true}}]")]
        public virtual void MethodWithOptionalAnyArgument(object arg)
        {
            InvokeInstanceVoidMethod(new object[]{arg});
        }

        /// <summary>Used to verify verification of number of method arguments.</summary>
        [JsiiMethod("methodWithOptionalArguments", null, "[{\"name\":\"arg1\",\"type\":{\"primitive\":\"number\"}},{\"name\":\"arg2\",\"type\":{\"primitive\":\"string\"}},{\"name\":\"arg3\",\"type\":{\"primitive\":\"date\",\"optional\":true}}]")]
        public virtual void MethodWithOptionalArguments(double arg1, string arg2, DateTime? arg3)
        {
            InvokeInstanceVoidMethod(new object[]{arg1, arg2, arg3});
        }
    }
}