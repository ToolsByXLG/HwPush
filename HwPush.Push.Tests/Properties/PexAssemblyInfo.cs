// <copyright file="PexAssemblyInfo.cs">小烈哥</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("HwPush")]
[assembly: PexInstrumentAssembly("Newtonsoft.Json")]
[assembly: PexInstrumentAssembly("System.Drawing")]
[assembly: PexInstrumentAssembly("HwBase")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("System.Windows.Forms")]
[assembly: PexInstrumentAssembly("HwModel")]
[assembly: PexInstrumentAssembly("FiddlerCore")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Newtonsoft.Json")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Drawing")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "HwBase")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Windows.Forms")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "HwModel")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "FiddlerCore")]

