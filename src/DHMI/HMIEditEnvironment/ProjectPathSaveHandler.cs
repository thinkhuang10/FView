using DevExpress.XtraBars;
using Properties;
using System.Collections.Specialized;

namespace HMIEditEnvironment
{
    public class ProjectPathSaveHandler
    {
        /// <summary>
        /// 最近文件菜单项
        /// </summary>
        public BarListItem RecentProjectItems { get; set; }

        public StringCollection FileList;

        private int FileNumbers;

        public ProjectPathSaveHandler()
        {
            FileNumbers = Settings.Default.FileNumber;
            FileList = Settings.Default.FilePaths;
            
            if (FileList == null)
            {
                FileList = new StringCollection();
            }
        }

        /// <summary>
        /// 更新最近菜单单项
        /// </summary>
        public void UpdateMenu()
        {
            if (RecentProjectItems == null) 
                return;

            RecentProjectItems.Strings.Clear();
            for (var i = 0; i < FileList.Count; i++)
            {
                RecentProjectItems.Strings.Add(FileList[i]);
            }
        }

        /// <summary>
        /// 添加最近文件路径(每次打开文件时，调用该方法)
        /// </summary>
        /// <param name="filePath"></param>
        public void AddRecentFile(string filePath)
        {
            FileList.Insert(0, filePath);

            //从最后位置开始倒着找，如果找到一致名称，则移除旧记录
            for (int i = FileList.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (FileList[i] == FileList[j])
                    {
                        FileList.RemoveAt(i);
                        break;
                    }
                }
            }

            //最后，仅保留指定的文件列表数量
            for (int bynd = FileList.Count - 1; bynd > FileNumbers - 1; bynd--)
            {
                FileList.RemoveAt(bynd);
            }

            Settings.Default.FilePaths = FileList;
            Settings.Default.Save();

            UpdateMenu();
        }

    }
}
