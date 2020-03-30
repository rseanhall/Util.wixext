// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolsetTest.Util
{
    using System.IO;
    using System.Linq;
    using WixBuildTools.TestSupport;
    using WixToolset.Core.TestPackage;
    using WixToolset.Data;
    using WixToolset.Data.Tuples;
    using WixToolset.Util;
    using Xunit;

    public class UtilExtensionFixture
    {
        [Fact]
        public void CanBuildUsingFileShare()
        {
            var folder = TestData.Get(@"TestData\UsingFileShare");
            var build = new Builder(folder, typeof(UtilExtensionFactory), new[] { folder });

            var results = build.BuildAndQuery(Build, "Binary", "CustomAction", "Wix4FileShare", "Wix4FileSharePermissions");
            Assert.Equal(new[]
            {
                "Binary:Wix4UtilCA_X86\t[Binary data]",
                "CustomAction:Wix4ConfigureSmbInstall_X86\t1\tWix4UtilCA_X86\tConfigureSmbInstall\t",
                "CustomAction:Wix4ConfigureSmbUninstall_X86\t1\tWix4UtilCA_X86\tConfigureSmbUninstall\t",
                "CustomAction:Wix4CreateSmb_X86\t11265\tWix4UtilCA_X86\tCreateSmb\t",
                "CustomAction:Wix4CreateSmbRollback_X86\t11585\tWix4UtilCA_X86\tDropSmb\t",
                "CustomAction:Wix4DropSmb_X86\t11265\tWix4UtilCA_X86\tDropSmb\t",
                "CustomAction:Wix4DropSmbRollback_X86\t11585\tWix4UtilCA_X86\tCreateSmb\t",
                "Wix4FileShare:ExampleFileShare\texample\tfilF5_pLhBuF5b4N9XEo52g_hUM5Lo\tAn example file share\tINSTALLFOLDER",
                "Wix4FileSharePermissions:ExampleFileShare\tEveryone\t1",
            }, results.OrderBy(s => s).ToArray());
        }

        [Fact]
        public void CanBuildUsingFileShareX64()
        {
            var folder = TestData.Get(@"TestData\UsingFileShare");
            var build = new Builder(folder, typeof(UtilExtensionFactory), new[] { folder });

            var results = build.BuildAndQuery(BuildX64, "Binary", "CustomAction", "Wix4FileShare", "Wix4FileSharePermissions");
            Assert.Equal(new[]
            {
                "Binary:Wix4UtilCA_X86\t[Binary data]",
                "CustomAction:Wix4ConfigureSmbInstall_X86\t1\tWix4UtilCA_X86\tConfigureSmbInstall\t",
                "CustomAction:Wix4ConfigureSmbUninstall_X86\t1\tWix4UtilCA_X86\tConfigureSmbUninstall\t",
                "CustomAction:Wix4CreateSmb_X86\t11265\tWix4UtilCA_X86\tCreateSmb\t",
                "CustomAction:Wix4CreateSmbRollback_X86\t11585\tWix4UtilCA_X86\tDropSmb\t",
                "CustomAction:Wix4DropSmb_X86\t11265\tWix4UtilCA_X86\tDropSmb\t",
                "CustomAction:Wix4DropSmbRollback_X86\t11585\tWix4UtilCA_X86\tCreateSmb\t",
                "Wix4FileShare:ExampleFileShare\texample\tfilF5_pLhBuF5b4N9XEo52g_hUM5Lo\tAn example file share\tINSTALLFOLDER",
                "Wix4FileSharePermissions:ExampleFileShare\tEveryone\t1",
            }, results.OrderBy(s => s).ToArray());
        }

        [Fact]
        public void CanBuildCloseApplication()
        {
            var folder = TestData.Get(@"TestData\CloseApplication");
            var build = new Builder(folder, typeof(UtilExtensionFactory), new[] { folder });

            var results = build.BuildAndQuery(BuildX64, "Binary", "CustomAction", "Wix4CloseApplication");
            Assert.Equal(new[]
            {
                "Binary:Wix4UtilCA_X86\t[Binary data]",
                "CustomAction:Wix4CheckRebootRequired_X86\t65\tWix4UtilCA_X86\tWixCheckRebootRequired\t",
                "CustomAction:Wix4CloseApplications_X86\t1\tWix4UtilCA_X86\tWixCloseApplications\t",
                "CustomAction:Wix4CloseApplicationsDeferred_X86\t3073\tWix4UtilCA_X86\tWixCloseApplicationsDeferred\t",
                "Wix4CloseApplication:CloseMyApp\texplorer.exe\t\t\t3\t\tMYAPPISRUNNING\t\t",
            }, results.OrderBy(s => s).ToArray());
        }

        [Fact]
        public void CanBuildInternetShortcut()
        {
            var folder = TestData.Get(@"TestData\InternetShortcut");
            var build = new Builder(folder, typeof(UtilExtensionFactory), new[] { folder });

            var results = build.BuildAndQuery(BuildX64, "Binary", "CustomAction", "RemoveFile", "Wix4InternetShortcut");
            Assert.Equal(new[]
            {
                "Binary:Wix4UtilCA_X86\t[Binary data]",
                "CustomAction:Wix4CreateInternetShortcuts_X86\t3073\tWix4UtilCA_X86\tWixCreateInternetShortcuts\t",
                "CustomAction:Wix4RollbackInternetShortcuts_X86\t3329\tWix4UtilCA_X86\tWixRollbackInternetShortcuts\t",
                "CustomAction:Wix4SchedInternetShortcuts_X86\t1\tWix4UtilCA_X86\tWixSchedInternetShortcuts\t",
                "RemoveFile:wixshortcut\tfilF5_pLhBuF5b4N9XEo52g_hUM5Lo\tvtpzs3bw.lnk|WiX Toolset.lnk\tINSTALLFOLDER\t2",
                "Wix4InternetShortcut:wixshortcut\tfilF5_pLhBuF5b4N9XEo52g_hUM5Lo\tINSTALLFOLDER\tWiX Toolset.lnk\thttps://wixtoolset.org\t0\t\t0",
            }, results.OrderBy(s => s).ToArray());
        }

        [Fact]
        public void CanBuildWithPermissionEx()
        {
            var folder = TestData.Get(@"TestData\PermissionEx");
            var build = new Builder(folder, typeof(UtilExtensionFactory), new[] { folder });

            var results = build.BuildAndQuery(BuildX64, "Binary", "CreateFolder", "CustomAction", "Wix4SecureObject");
            Assert.Equal(new[]
            {
                "Binary:Wix4UtilCA_X64\t[Binary data]",
                "CreateFolder:INSTALLFOLDER\tfilF5_pLhBuF5b4N9XEo52g_hUM5Lo",
                "CustomAction:Wix4ExecSecureObjects_X64\t11265\tWix4UtilCA_X64\tExecSecureObjects\t",
                "CustomAction:Wix4ExecSecureObjectsRollback_X64\t11521\tWix4UtilCA_X64\tExecSecureObjectsRollback\t",
                "CustomAction:Wix4SchedSecureObjects_X64\t1\tWix4UtilCA_X64\tSchedSecureObjects\t",
                "CustomAction:Wix4SchedSecureObjectsRollback_X64\t1\tWix4UtilCA_X64\tSchedSecureObjectsRollback\t",
                "Wix4SecureObject:secJTI3ywlGOTsHWSdR4bEtGDc.veU\tCreateFolder\t\tEveryone\t268435456\tfilF5_pLhBuF5b4N9XEo52g_hUM5Lo",
            }, results.OrderBy(s => s).ToArray());
        }

        [Fact]
        public void CanBuildWithEventManifest()
        {
            var folder = TestData.Get(@"TestData\EventManifest");
            var build = new Builder(folder, typeof(UtilExtensionFactory), new[] { folder });

            var results = build.BuildAndQuery(BuildX64, "Binary", "CustomAction", "Wix4EventManifest", "Wix4XmlFile");
            Assert.Equal(new[]
            {
                "Binary:Wix4UtilCA_X86\t[Binary data]",
                "CustomAction:Wix4ConfigureEventManifestRegister_X86\t1\tWix4UtilCA_X86\tConfigureEventManifestRegister\t",
                "CustomAction:Wix4ConfigureEventManifestUnregister_X86\t1\tWix4UtilCA_X86\tConfigureEventManifestUnregister\t",
                "CustomAction:Wix4ExecXmlFile_X86\t11265\tWix4UtilCA_X86\tExecXmlFile\t",
                "CustomAction:Wix4ExecXmlFileRollback_X86\t11521\tWix4UtilCA_X86\tExecXmlFileRollback\t",
                "CustomAction:Wix4RegisterEventManifest_X86\t3073\tWix4UtilCA_X86\tCAQuietExec\t",
                "CustomAction:Wix4RollbackRegisterEventManifest_X86\t3393\tWix4UtilCA_X86\tCAQuietExec\t",
                "CustomAction:Wix4RollbackUnregisterEventManifest_X86\t3329\tWix4UtilCA_X86\tCAQuietExec\t",
                "CustomAction:Wix4SchedXmlFile_X86\t1\tWix4UtilCA_X86\tSchedXmlFile\t",
                "CustomAction:Wix4UnregisterEventManifest_X86\t3137\tWix4UtilCA_X86\tCAQuietExec\t",
                "Wix4EventManifest:Manifest.dll\t[#Manifest.dll]",
                "Wix4XmlFile:Config_Manifest.dllMessageFile\t[#Manifest.dll]\t/*/*/*/*[\\[]@messageFileName[\\]]\tmessageFileName\t[Manifest.dll]\t4100\tManifest.dll\t",
                "Wix4XmlFile:Config_Manifest.dllResourceFile\t[#Manifest.dll]\t/*/*/*/*[\\[]@resourceFileName[\\]]\tresourceFileName\t[Manifest.dll]\t4100\tManifest.dll\t",
            }, results.OrderBy(s => s).ToArray());
        }

        [Fact]
        public void CanBuildBundleWithSearches()
        {
            var burnStubPath = TestData.Get(@"TestData\.Data\burn.exe");
            var folder = TestData.Get(@"TestData\BundleWithSearches");
            var rootFolder = TestData.Get();
            var wixext = Path.Combine(rootFolder, "WixToolset.Util.wixext.dll");

            using (var fs = new DisposableFileSystem())
            {
                var baseFolder = fs.GetFolder();
                var intermediateFolder = Path.Combine(baseFolder, "obj");
                var bundlePath = Path.Combine(baseFolder, @"bin\test.exe");
                var baFolderPath = Path.Combine(baseFolder, "ba");
                var extractFolderPath = Path.Combine(baseFolder, "extract");

                var result = WixRunner.Execute(new[]
                {
                    "build",
                    Path.Combine(folder, "Bundle.wxs"),
                    "-ext", wixext,
                    "-loc", Path.Combine(folder, "Bundle.en-us.wxl"),
                    "-bindpath", Path.Combine(folder, "data"),
                    "-intermediateFolder", intermediateFolder,
                    "-burnStub", burnStubPath,
                    "-o", bundlePath
                });

                result.AssertSuccess();

                Assert.True(File.Exists(bundlePath));
#if TODO
                Assert.True(File.Exists(Path.Combine(baseFolder, @"bin\test.wixpdb")));
#endif

                var extractResult = BundleExtractor.ExtractBAContainer(null, bundlePath, baFolderPath, extractFolderPath);
                extractResult.AssertSuccess();

                var bundleExtensionDatas = extractResult.SelectBundleExtensionDataNodes("/be:BundleExtensionData/be:BundleExtension[@Id='WixUtilBundleExtension']");
                Assert.Equal(1, bundleExtensionDatas.Count);
                Assert.Equal("<BundleExtension Id='WixUtilBundleExtension'>" +
                    "<WixDetectSHA2Support Id='DetectSHA2SupportId' />" +
                    "</BundleExtension>", bundleExtensionDatas[0].GetTestXml());

                var utilSearches = extractResult.SelectManifestNodes("/burn:BurnManifest/*[self::burn:ExtensionSearch or self::burn:FileSearch or self::burn:MsiProductSearch or self::burn:RegistrySearch]");
                Assert.Equal(4, utilSearches.Count);
                Assert.Equal("<ExtensionSearch Id='DetectSHA2SupportId' Variable='IsSHA2Supported' " +
                    "ExtensionId='WixUtilBundleExtension' />", utilSearches[0].GetTestXml());
                Assert.Equal("<FileSearch Id='FileSearchId' Variable='FileSearchVariable' " +
                    $@"Path='%windir%\System32\mscoree.dll' Type='exists' />", utilSearches[1].GetTestXml());
                Assert.Equal("<MsiProductSearch Id='ProductSearchId' Variable='ProductSearchVariable' Condition='1 &amp; 2 &lt; 3' " +
                    "UpgradeCode='{738D02BF-E231-4370-8209-E9FD4E1BE2A1}' Type='version' />", utilSearches[2].GetTestXml());
                Assert.Equal("<RegistrySearch Id='RegistrySearchId' Variable='RegistrySearchVariable' " +
                    @"Root='HKLM' Key='SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' Value='Release' Type='value' VariableType='string' />", utilSearches[3].GetTestXml());
            }
        }

        private static void Build(string[] args)
        {
            var result = WixRunner.Execute(args);
            result.AssertSuccess();
        }

        private static void BuildX64(string[] args)
        {
            var newArgs = args.ToList();
            newArgs.Add("-platform");
            newArgs.Add("x64");

            var result = WixRunner.Execute(newArgs.ToArray());
            result.AssertSuccess();
        }
    }
}
