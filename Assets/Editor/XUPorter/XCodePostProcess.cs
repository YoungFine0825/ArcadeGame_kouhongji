using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
#endif
using System.IO;
using System;

public static class XCodePostProcess
{
#if UNITY_EDITOR

    enum WriteOpt
    {
        Below = 1,
        Above = 2,
        Replace = 3,
    }

    [PostProcessBuild(999)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
		//IOS
		if (target != BuildTarget.iOS)
        {
            Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        Debug.Log("pathToBuiltProject:" + pathToBuiltProject);
        string path = Path.GetFullPath(pathToBuiltProject);
        Debug.Log("pathToBuiltProject full path:" + path);
        // Create a new project object from build target
        XCProject project = new XCProject(path);
		//添加类库搜索路径
		project.AddFrameworkSearchPaths("$(SRCROOT)/Frameworks/Plugins/iOS");

		//根据projmods配置文件扩展Xcode工程
		string[] files = null;
		files = Directory.GetFiles(Application.dataPath, "*.projmods", SearchOption.AllDirectories);
		//
        foreach (string file in files)
        {
            UnityEngine.Debug.Log("ProjMod File: " + file);
            project.ApplyMod(file);
        }
        ////设置签名的证书， 第二个参数 你可以设置成你的证书
	    string bitcode = "NO"; // YES or NO
		Debug.Log ("Bitcode: " + bitcode);
		project.BitCodeEnableExceptions(bitcode);

        //project.overwriteBuildSetting("CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Distribution", "Release");
        project.overwriteBuildSetting("DEPLOYMENT_POSTPROCESSING", "YES", "Release");
        project.overwriteBuildSetting("DEPLOYMENT_POSTPROCESSING", "YES", "RELEASE");
        //project.overwriteBuildSetting("Deployment Postprocessing", "Yes", "");

		//编辑InfoPlist
        EditorPlist(path);
		//扩展代码
        //EditorCode(path);
        // Finally save the xcode project
        project.Save();

    }


    private static void EditorPlist(string filePath)
    {

        XCPlist list = new XCPlist(filePath);
        string PlistAdd = @"
            <key>CFBundleIdentifier</key>
            <string>com.scbczx.luckystar</string>
			<key>XUPORTER</key>
			<string>XUPorter XCodePostProcess Add</string>
			<key>CFBundleDisplayName</key>
			<string>掌上游乐场</string>
			<key>CFBundleURLTypes</key>
			<array>
				<dict>
					<key>CFBundleTypeRole</key>
					<string>Editor</string>
					<key>CFBundleURLName</key>
					<string>wechat</string>
					<key>CFBundleURLSchemes</key>
					<array>
						<string>wx801def83edbb80a3</string>
					</array>
				</dict>
				<dict>
					<key>CFBundleTypeRole</key>
					<string>Editor</string>
					<key>CFBundleURLName</key>
					<string></string>
					<key>CFBundleURLSchemes</key>
					<array>
						<string>com.scbczx.luckystar</string>
					</array>
				</dict>
			</array>
			<key>LSApplicationQueriesSchemes</key>
			<array>
				<string>weixin</string>
				<string>wechat</string>
			</array>
			<key>NSAppTransportSecurity</key>
			<dict>
				<key>NSAllowsArbitraryLoads</key>
				<true/>
			</dict>	
			<key>NSLocationAlwaysUsageDescription</key>
			<string>若允许，“个人信息”面板中将显示精准的位置信息；否则，“个人信息”面板中的位置信息可能有误</string>
			<key>NSLocationUsageDescription</key>
			<string>若允许，“个人信息”面板中将显示精准的位置信息；否则，“个人信息”面板中的位置信息可能有误</string>
			<key>NSLocationWhenInUseUsageDescription</key>
			<string>若允许，“个人信息”面板中将显示精准的位置信息；否则，“个人信息”面板中的位置信息可能有误</string>
			<key>NSMicrophoneUsageDescription</key>
			<string>microphoneDesciption</string>
			";

        list.AddKey(PlistAdd);
        list.ReplaceKey("<string>en</string>", "<string>zh_CN</string>");
        list.ReplaceKey("<string>Hydra</string>", "<string>????</string>");
        list.ReplaceKey("<string>0.0.0</string>", "<string>0.0.0</string>");
        list.ReplaceKey("<string>1.0</string>", "<string>0</string>");
        list.Save();
    }

	/// <summary>
	/// 编辑代码
	/// </summary>
	/// <param name="filePath">File path.</param>
    private static void EditorCode(string filePath)
    {
        XClass UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");
        WriteBasic(UnityAppController);
        WriteTerminate(UnityAppController);

    }

    

    private static void WriteBasic(XClass UnityAppController)
    {
        // hide status bar
		Write(UnityAppController, WriteOpt.Below, "::printf(\"-> applicationDidFinishLaunching()\\n\");", "\t[application setStatusBarHidden: YES];");
    }

    
    /// <summary>
    /// 退出时屏蔽异常
    /// </summary>
    /// <param name="UnityAppController"></param>
    private static void WriteTerminate(XClass UnityAppController)
    {
        // 屏蔽异常信号
        Write(UnityAppController, WriteOpt.Above, "- (void)applicationWillTerminate:(UIApplication*)application",
            "- (void)OnApplicationQuit\n{\n\tNSSetUncaughtExceptionHandler(NULL);\n\n\tsignal(SIGABRT, SIG_DFL);\n\tsignal(SIGILL, SIG_DFL);\n\tsignal(SIGSEGV, SIG_DFL);\n\tsignal(SIGFPE, SIG_DFL);\n\tsignal(SIGBUS, SIG_DFL);\n\tsignal(SEGV_ACCERR, SIG_DFL);\n}\n");

        // 移除所有监听
		//Write(UnityAppController, WriteOpt.Below, "::printf(\"-> applicationWillTerminate()\\n\");", "\n\n[[NSNotificationCenter defaultCenter] removeObserver:self];\n");

        // 去掉原调用
        Write(UnityAppController, WriteOpt.Replace, "Profiler_UninitProfiler();", "");
        Write(UnityAppController, WriteOpt.Replace, "UnityCleanup();", "[self OnApplicationQuit];");
    }
		
    // opt: 1-below, 2-above, 3-replace
    private static void Write(XClass cls, WriteOpt opt, string oldBlock, string newBlock)
    {
        string error = string.Empty;
        switch (opt)
        {
            case WriteOpt.Below:
                error = cls.WriteBelow(oldBlock, newBlock);
                break;
            case WriteOpt.Above:
                error = cls.WriteAbove(oldBlock, newBlock);
                break;
            case WriteOpt.Replace:
                error = cls.Replace(oldBlock, newBlock);
                break;
        }

        if (!string.IsNullOrEmpty(error))
        {
            throw new System.Exception(error);
        }
    }

#endif

    public static void Log(string message)
    {
        UnityEngine.Debug.Log("PostProcess: " + message);
    }
}
