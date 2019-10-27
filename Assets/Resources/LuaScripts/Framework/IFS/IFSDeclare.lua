 --[[********************************************************************
 *		作者： jordenwu
 *		时间： 2018-08-08
 *		描述： IFS 状态申明
 *********************************************************************--]]
 
 IFSState={
        --开始
        Start=0,
        --首次zip包移动状态
        FirstMoveInit=1,
        FirstMoving=2,
        FirstMoveSuccess=3,
        FirstMoveFailed=4,
        --首次zip包网络下载状态
        FirstDownloadInit=5,
        FirstDownloading=6,
        FirstDownloadSuccess=7,
        FirstDownloadFailed=8,
        --首次解压
        FirstUnZip=9,
        --本地文件列表初始化
        LocalFileListInit=10,
        --本地文件完整性检查
        LocalFileListCheck=11,
        --网络文件列表下载
        NetFileListDownload=12,
        --Diff文件列表
        LocalDiffNetFileList=13,
        --下载差异开始
        DownloadDiffFileListBegin=14,
        --Diff下载中
        DownloadingDiff=15,
        --Diff下载完成
        DownloadDiffSuccess=16,
        --生成最新文件列表
        GenerateLastFileList=17,
        --结束
        Over=18,
}