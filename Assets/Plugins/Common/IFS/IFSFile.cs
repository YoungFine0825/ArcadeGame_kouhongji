/********************************************************************
	created:	24:5:2018   
	filename: 	IFS
	author:		jordenwu
	
	purpose:	IFS 文件定义 IFS文件是一种类似zip tar的归档文件 目的是对一个目录下的所有文件 采用LZMA压缩 后合并成一个 IFS文件
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JW.Common;

namespace JW.IFS
{
    //IFS内部对文件压缩方式定义
    public enum IFSCompressType
    {
        //保持原样不压缩
        None = 0,
        //LZMA 压缩方式
        LZMA = 1,
    }

    /// <summary>
    /// 归档的单文件条目定义
    /// </summary>
    public class IFSEntry
    {
        //此名称是相对归档目录的路径
        //名称有长度限制 目前用short 类型来表示名称字节长度 
        public string Name;
        //数据长度
        public int DataSize;
        //数据开始位置
        public int DataPos;
        //数据
        public byte[] Data = null;
    }

    /// <summary>
    /// 自定义IFS 文件 支持流方式 获取内部子文件数据  同时也可以解压出来
    /// </summary>
    public class IFSFile
    {
        //
        public const uint IFSSignature = 0x9966AABB;
        //签名
        public uint Signature = IFSSignature;
        //压缩方式
        public IFSCompressType CompressType = IFSCompressType.None;
        //条目数目
        public int EntryCount = 0;
        //条目
        public IFSEntry[] Entrys;
        //条目字典
        private JW.Common.JWObjDictionary<string,IFSEntry> _entryDic;
        
        public IFSFile()
        {
            Signature = IFSSignature;
        }

        //不解压方式 流式内部获取条目数据  可用于对小文件(Lua) 合并成一个ifs文件 
        private Stream _stream;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool InitWithFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            _stream = new FileStream(path, FileMode.Open);
            return Init();
        }

        public bool InitWithData(byte[] data)
        {
            if (data == null)
            {
                return false;
            }
            _stream = new MemoryStream(data);
            return Init();
        }

        private bool Init()
        {
            //分析备用
            int totalL = (int)_stream.Length;
            if (totalL < 20)
            {
                JW.Common.Log.LogE("IFSFile InitWithData Error IFS File Length");
                return false;
            }
            //获取签名
            byte[] bbs = new byte[16];
            int offset = 0;
            _stream.Read(bbs, 0, 16);
            uint sig = (uint)IFSArchiver.ConvertBytesToInt(bbs, ref offset);
            if (sig != IFSFile.IFSSignature)
            {
                JW.Common.Log.LogE("IFSFile InitWithData Error Signature ");
                return false;
            }
            this.Signature = sig;
            //获取压缩方式
            this.CompressType = (IFSCompressType)IFSArchiver.ConvertBytesToInt(bbs, ref offset);
            //条目个数
            this.EntryCount = IFSArchiver.ConvertBytesToInt(bbs, ref offset);
            //名称长度
            int entryNameL = IFSArchiver.ConvertBytesToInt(bbs, ref offset);
            if (this.EntryCount == 0)
            {
                JW.Common.Log.LogE("IFSFile InitWithData Error  EntryCount");
                return false;
            }
            //条目名称段
            this.Entrys = new IFSEntry[this.EntryCount];
            this._entryDic = new JWObjDictionary<string, IFSEntry>(this.EntryCount);

            for (int i = 0; i < this.EntryCount; i++)
            {
                this.Entrys[i] = new IFSEntry();
            }
            //
            bbs = null;
            //
            byte[] names = new byte[entryNameL];
            _stream.Read(names, 0, entryNameL);
            offset = 0;
            //读取条目名称
            for (int i = 0; i < this.EntryCount; i++)
            {
                IFSEntry entry = this.Entrys[i];
                entry.Name = IFSArchiver.ConvertBytesToString(names, ref offset);
                if (_entryDic.ContainsKey(entry.Name))
                {
                    JW.Common.Log.LogE("IFSFile Repeat Entry:"+ entry.Name);
                }
                else
                {
                    _entryDic.Add(entry.Name, entry);
                }
            }
            //条目数据位置
            offset = 0;
            names = null;
            byte[] poss = new byte[this.EntryCount * 4];
            _stream.Read(poss, 0, this.EntryCount * 4);
            for (int i = 0; i < this.EntryCount; i++)
            {
                IFSEntry entry = this.Entrys[i];
                int vv = IFSArchiver.ConvertBytesToInt(poss, ref offset);
                entry.DataPos = vv;
            }
            offset = 0;
            poss = null;
            return true;
        }

        /// <summary>
        /// 判断是否含有条目
        /// </summary>
        /// <param name="name">条目名称</param>
        /// <returns></returns>
        public bool IsHaveEntry(string name)
        {
            if (_entryDic != null)
            {
                return _entryDic.ContainsKey(name);
            }
            return false;
        }

        /// <summary>
        /// 获取条目数据
        /// </summary>
        /// <param name="name">条目名称</param>
        /// <returns></returns>
        public byte[] GetEntryData(string name)
        {
            if (this.CompressType == IFSCompressType.LZMA)
            {
                JW.Common.Log.LogE("IFSFile GetEntryData Error No Support LZMA Entry TODO");
                return null;
            }

            if (this.EntryCount <= 0)
            {
                JW.Common.Log.LogE("IFSFile GetEntryData Error No Init");
                return null;
            }

            if (_stream == null)
            {
                JW.Common.Log.LogE("IFSFile GetEntryData Error No Init");
                return null;
            }

            IFSEntry find = null;
            if(!_entryDic.TryGetValue(name,out find))
            {
                find = null;
            }
            //for (int i = 0; i < this.EntryCount; i++)
            //{
            //    IFSEntry e = Entrys[i];
            //    if (e.Name.Equals(name))
            //    {
            //        find = e;
            //        break;
            //    }
            //}
            if (find == null)
            {
                JW.Common.Log.LogE("IFSFile GetEntryData Error No Entry :" + name);
                return null;
            }

            if (find.Data != null)
            {
                return find.Data;
            }
            //读取
            _stream.Seek(find.DataPos, SeekOrigin.Begin);
            byte[] ll = new byte[4];
            _stream.Read(ll, 0, 4);
            int offset = 0;
            int vv = IFSArchiver.ConvertBytesToInt(ll, ref offset);
            find.DataSize = vv;
            //
            if (vv == 0)
            {
                JW.Common.Log.LogE("IFSFile GetEntryData Error No Entry Data:" + name);
                return null;
            }
            byte[] buffer = new byte[vv];
            _stream.Read(buffer, 0, buffer.Length);
            //
            find.Data = buffer;
            //
            return buffer;
        }

        public void Close()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }
            if (Entrys != null)
            {
                for (int i = 0; i < Entrys.Length; i++)
                {
                    Entrys[i].Data = null;
                }
                Entrys = null;
            }
        }

        public void LogFileInfo()
        {
            if (Entrys != null)
            {
                for (int i = 0; i < this.EntryCount; i++)
                {
                    IFSEntry e = Entrys[i];
                    JW.Common.Log.LogD(e.Name);
                }
            }
        }
    }
}

