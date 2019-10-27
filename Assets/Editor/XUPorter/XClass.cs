using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace UnityEditor.XCodeEditor
{
	public partial class XClass : System.IDisposable
	{
		
		private string filePath;
		
		public XClass(string fPath)
		{
			filePath = fPath;
			if( !System.IO.File.Exists( filePath ) ) {
				Debug.LogError( filePath +"路径下文件不存在" );
				return;
			}
		}
		
        public string WriteAbove(string block, string newBlock)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            int beginIndex = text_all.IndexOf(block);
            if (beginIndex == -1)
            {
                return string.Format("[XUPorter]WriteAbove code block for {0} failed, block [{1}] not found.", filePath, block);
            }

            text_all = text_all.Substring(0, beginIndex) + "\n" + newBlock + "\n" + text_all.Substring(beginIndex);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(text_all);
            streamWriter.Close();

			return string.Empty;
        }

		public string WriteBelow(string block, string newBlock)
		{
			StreamReader streamReader = new StreamReader(filePath);
			string text_all = streamReader.ReadToEnd();
			streamReader.Close();
			
			int beginIndex = text_all.IndexOf(block);
			if(beginIndex == -1){
				return string.Format("[XUPorter]WriteBelow code block for {0} failed, block [{1}] not found.", filePath, block);
			}
			
			int endIndex = text_all.LastIndexOf("\n", beginIndex + block.Length);
			if (!text_all.Substring (endIndex, newBlock.Length + 2).Contains (newBlock)) {
				text_all = text_all.Substring (0, endIndex) + "\n" + newBlock + "\n" + text_all.Substring (endIndex);
			}

			StreamWriter streamWriter = new StreamWriter(filePath);
			streamWriter.Write(text_all);
			streamWriter.Close();

			return string.Empty;
		}
		
		public string Replace(string block, string newBlock)
		{
			StreamReader streamReader = new StreamReader(filePath);
			string text_all = streamReader.ReadToEnd();
			streamReader.Close();
			
			int beginIndex = text_all.IndexOf(block);
			if(beginIndex == -1){
				return string.Format("[XUPorter]Replace code block for {0} failed, block [{1}] not found.", filePath, block);
			}
			
			text_all =  text_all.Replace(block, newBlock);
			StreamWriter streamWriter = new StreamWriter(filePath);
			streamWriter.Write(text_all);
			streamWriter.Close();

			return string.Empty;
		}
		
		public void Dispose()
		{
			
		}
	}
}
