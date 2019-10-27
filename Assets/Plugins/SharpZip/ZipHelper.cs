/********************************************************************
	created:	17:10:2017   10:40
	author:		jordenwu
	purpose:	解压 压缩帮助类 这里使用ICSharpZIP 的FastZip
*********************************************************************/
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace ICSharpCode.SharpZipLib
{
    public static class ZipHelper
    {
        private static string Pd = "99887766";
        public static string CreateZip(string zipPath, string dirPath,string pp="")
        {
            FastZip zip = new FastZip();
            zip.Password = pp;
            zip.CreateEmptyDirectories = true;
            zip.UseZip64 = UseZip64.On;
            zip.CreateZip(zipPath, dirPath, true, "");
            return zipPath;
        }

        public static string ExtractZip(string zipPath, string dirPath)
        {
            FastZip zip = new FastZip();
            zip.Password = Pd;
            zip.UseZip64 = UseZip64.On;
            zip.CreateEmptyDirectories = true;
            zip.ExtractZip(zipPath, dirPath, "");
            return dirPath;
        }

        public static string ExtractZip(Stream zipStream, string dirPath)
        {
            FastZip zip = new FastZip();
            zip.Password = Pd;
            zip.ExtractZip(zipStream, dirPath, FastZip.Overwrite.Always, null, "", "", false, true);
            return dirPath;
        }
    }
}

