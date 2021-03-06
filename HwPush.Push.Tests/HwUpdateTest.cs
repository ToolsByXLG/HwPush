// <copyright file="HwUpdateTest.cs">小烈哥</copyright>
using System;
using HwUpdate.Push;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HwUpdate.Push.Tests
{
    /// <summary>此类包含 HwUpdate 的参数化单元测试</summary>
    [PexClass(typeof(global::HwUpdate.Push.HwUpdate))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class HwUpdateTest
    {
    }
}
