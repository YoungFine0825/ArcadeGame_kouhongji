/********************************************************************
	created:	18:5:2018   
	filename: 	BinaryPackInfo
	author:		jordenwu
	
	purpose:	二进制文件夹 信息定义
*********************************************************************/
namespace JW.Res
{
    /// <summary>
    /// 二进制文件包信息定义
    /// </summary>
    [System.Serializable]
    public class BinaryPackInfo : ResPackInfo
    {
      
        /// <summary>
        /// 二进制文件包类型
        /// </summary>
        /// <returns></returns>
        public override byte GetPackType()
        {
            return (byte)ResPackType.ResPackTypeBinary;
        }

        /// <summary>
        /// 读取二进制配置文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public override void Read(byte[] data, ref int offset)
        {
            base.Read(data, ref offset);
        }

        /// <summary>
        /// 写入二进制配置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public override void Write(byte[] data, ref int offset)
        {
            base.Write(data, ref offset);
        }

        //检查位置
        protected override bool CheckLocation()
        {
            if (!base.CheckLocation())
            {
                for (int i = 0; i < Count; i++)
                {
                    //检查资源列表位置
                    ResInfo r = base.Resources[i];
                    if (FileUtil.IsExistInIFSExtraFolder(r.Path + r.Ext))
                    {
                        r.Outside = true;
                    }
                }
            }
            return true;
        }
    }
}
