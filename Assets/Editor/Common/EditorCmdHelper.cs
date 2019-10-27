#if UNITY_EDITOR
using UnityEngine;

public static class EditorCmdHelper {

    //命令行调用
    public static void ProcessCommand(string command, string argument)
    {
        Debug.Log("---->CMD----->" + command + ":" + argument);
        System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo(command);
        start.Arguments = argument;
        start.CreateNoWindow = false;
        start.ErrorDialog = true;
        start.UseShellExecute = true;

        if (start.UseShellExecute)
        {
            start.RedirectStandardOutput = false;
            start.RedirectStandardError = false;
            start.RedirectStandardInput = false;
        }
        else
        {
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.RedirectStandardInput = true;
            start.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;
            start.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
        }

        System.Diagnostics.Process p = System.Diagnostics.Process.Start(start);

        if (!start.UseShellExecute)
        {
            Debug.LogError(p.StandardOutput);
            Debug.LogError(p.StandardError);
        }
        p.WaitForExit();
        p.Close();
    }
}
#endif