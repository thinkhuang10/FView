using HMICompile;
using HMIEditEnvironment;
using LogHelper;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Util;

namespace Model
{
    public class ProjectCompiler
    {
        private readonly List<string> CompileError = new();

        private readonly List<string> CompileWarning = new();

        public bool Compile()
        {
            try
            {
                CEditEnvironmentGlobal.OutputMessage.Show();

                if (string.IsNullOrEmpty(CEditEnvironmentGlobal.ProjectHPFFilePath))
                {
                    CEditEnvironmentGlobal.OutputMessage.Say("工程文件为空.");
                    return false;
                }

                if (!SetFirstPage())
                    return false;

                ClassForScript.old = true;

                if (CEditEnvironmentGlobal.HMIRunProcess != null && !CEditEnvironmentGlobal.HMIRunProcess.HasExited)
                {
                    CEditEnvironmentGlobal.OutputMessage.Say("关闭当前调试工程.");
                    CEditEnvironmentGlobal.HMIRunProcess.Kill();
                }

                CEditEnvironmentGlobal.OutputMessage.Say("开始准备本地发布.");

                SizeHelper.SetProjectRunSize();
                OrganizeDeviceParameterVariables();
                ReplaceScriptVar();
                OrganizeFiles();

                if (!ExecuteHMICompile())
                    return false;

                foreach (string item in CompileWarning)
                {
                    CEditEnvironmentGlobal.OutputMessage.Say(item);
                }

                CEditEnvironmentGlobal.OutputMessage.Say("编译结果:0个错误," + CompileWarning.Count + "个警告");
                CEditEnvironmentGlobal.OutputMessage.Hide();

                return true;
            }
            catch (Exception ex)
            {
                CompileError.Add("错误:" + ex.Message);
                LogUtil.Error("编译错误：" + ex);
                if (CompileError.Count != 0)
                {
                    foreach (var item in CompileWarning)
                    {
                        CEditEnvironmentGlobal.OutputMessage.Say(item);
                    }

                    foreach (var item in CompileError)
                    {
                        CEditEnvironmentGlobal.OutputMessage.Say(item);
                    }

                    CEditEnvironmentGlobal.OutputMessage.Say("编译结果:" + CompileError.Count + "个错误," + CompileWarning.Count + "个警告");
                    CompileWarning.Clear();
                    CompileError.Clear();
                }
            }

            return false;
        }

        public void ExecuteHMIRun()
        {
            try
            {
                CEditEnvironmentGlobal.OutputMessage.Show();

                CEditEnvironmentGlobal.OutputMessage.Say("开始启动工程.");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo(PathHelper.GetOutputPath() + "DHMIRUN.exe")
                    {
                        UseShellExecute = false
                    }
                };
                process.Start();

                CEditEnvironmentGlobal.HMIRunProcess = process;

                CEditEnvironmentGlobal.OutputMessage.Say("成功启动工程.");
                CEditEnvironmentGlobal.OutputMessage.Say("完成本地发布.");

                CEditEnvironmentGlobal.OutputMessage.Hide();
            }
            catch(Exception ex)
            {
                CEditEnvironmentGlobal.OutputMessage.Say("执行HMIRun出现异常.");
                LogUtil.Error("执行HMIRun出现异常.异常为：" + ex);

            }
        }

        private bool SetFirstPage()
        {
            if (!CEditEnvironmentGlobal.dfs.Exists((DataFile df) => df.visable))
            {
                var dialogResult = MessageBox.Show("未设置任何初始显示页面,是否设置?", "提示", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == dialogResult)
                {
                    var form = new setFirstPageForm();
                    if (form.ShowDialog() != DialogResult.OK)
                        return false;
                }
            }

            return true;
        }

        private void OrganizeDeviceParameterVariables()
        {
            CEditEnvironmentGlobal.OutputMessage.Say("开始整理设备参数变量.");
            if (null == CEditEnvironmentGlobal.dhp.ParaIOs)
            {
                CEditEnvironmentGlobal.dhp.ParaIOs = new List<ParaIO>();
            }
        }

        private void ReplaceScriptVar()
        {
            Dictionary<string, string> dictionary = new();
            List<string> list = new();
            CEditEnvironmentGlobal.OutputMessage.Say("开始替换脚本变量.");
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                Operation.ReplaceIOName(df.ListAllShowCShape, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                df._pagedzqdLogic = Operation.ReplaceIO(df.pagedzqdLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                df._pagedzyxLogic = Operation.ReplaceIO(df.pagedzyxLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                df._pagedzgbLogic = Operation.ReplaceIO(df.pagedzgbLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                foreach (CShape item in df.ListAllShowCShape)
                {
                    item._sbsjglLogic = Operation.ReplaceIO(item.sbsjglLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                    item._sbsjldcLogic = Operation.ReplaceIO(item.sbsjldcLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                    item._sbsjrdcLogic = Operation.ReplaceIO(item.sbsjrdcLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                    item._sbsjlcLogic = Operation.ReplaceIO(item.sbsjlcLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                    item._sbsjrcLogic = Operation.ReplaceIO(item.sbsjrcLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
                }
                Operation.BinarySaveFile(CEditEnvironmentGlobal.HMIPath + "\\" + df.name + ".hpg", df);
                dictionary.Add(df.name, df.name + ".hpg");
                if (df.visable)
                {
                    list.Add(df.name);
                }
            }
            foreach (CIOAlarm iOAlarm in CEditEnvironmentGlobal.dhp.IOAlarms)
            {
                Operation.ReplaceIO(iOAlarm, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
            }
            CEditEnvironmentGlobal.dhp._cxdzgbLogic = Operation.ReplaceIO(CEditEnvironmentGlobal.dhp.cxdzgbLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
            CEditEnvironmentGlobal.dhp._cxdzyxLogic = Operation.ReplaceIO(CEditEnvironmentGlobal.dhp.cxdzyxLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
            CEditEnvironmentGlobal.dhp._cxdzqdLogic = Operation.ReplaceIO(CEditEnvironmentGlobal.dhp.cxdzqdLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
            CEditEnvironmentGlobal.dhp.devLogic = Operation.ReplaceIO(CEditEnvironmentGlobal.dhp.devjiaoben, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
            CEditEnvironmentGlobal.dhp.DstGlobalLogic = Operation.ReplaceIO(CEditEnvironmentGlobal.dhp.SrcGlobalLogic, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs, CEditEnvironmentGlobal.dhp.ParaIOs);
            CEditEnvironmentGlobal.OutputMessage.Say("完成替换脚本变量.");
            string projectfile = CEditEnvironmentGlobal.ProjectHPFFilePath;
            //CEditEnvironmentGlobal.dhp.PageGroup = treeView_工程导航.Nodes[0].Nodes[0].Tag as HmiPageGroup;
            CEditEnvironmentGlobal.dhp.Pages = dictionary;
            CEditEnvironmentGlobal.dhp.startVisiblePages = list;
            CEditEnvironmentGlobal.dirtyPageTemp = new List<string>(CEditEnvironmentGlobal.dhp.dirtyPage);
            CEditEnvironmentGlobal.dhp.dirtyPage.Clear();
            Operation.BinarySaveProject(projectfile, CEditEnvironmentGlobal.dhp);
            CEditEnvironmentGlobal.OutputMessage.Say("成功保存" + projectfile);
        }

        private void OrganizeFiles()
        {
            CEditEnvironmentGlobal.OutputMessage.Say("开始整理文件.");

            var outputDirectory = PathHelper.GetOutputPath();
            if (Directory.Exists(outputDirectory))
                Directory.Delete(outputDirectory, true);

            Directory.CreateDirectory(outputDirectory);

            var logicCodeDirectory = PathHelper.GetLogicCodePath();
            if (Directory.Exists(logicCodeDirectory))
                Directory.Delete(logicCodeDirectory, true);

            Directory.CreateDirectory(logicCodeDirectory);

            CopyProjectHMIFiles();
            CopyResources();
            CopyHMIRunFiles();
            CopyUserControl();

            CEditEnvironmentGlobal.OutputMessage.Say("整理文件成功.");
        }

        private void CopyProjectHMIFiles()
        {
            foreach (var fileInfo in DirectoryHelper.GetProjectHMIDirectory().GetFiles())
            {
                if (fileInfo.Extension.ToLower() == ".ocx" || fileInfo.Extension.ToLower() == ".dll"
                    || fileInfo.Extension.ToLower() == ".exe" || fileInfo.Extension.ToLower() == ".uim"
                    || fileInfo.Extension.ToLower() == ".data")
                {
                    File.Copy(fileInfo.FullName, PathHelper.GetOutputPath() + fileInfo.Name, overwrite: true);
                }
            }
        }

        private void CopyResources()
        {
            var projectHMIResourcsPath = DirectoryHelper.GetProjectHMIDirectory().FullName + "\\Resources\\";
            var outputResourcsPath = PathHelper.GetOutputPath() + "Resources\\";

            if (Directory.Exists(outputResourcsPath))
                Directory.Delete(outputResourcsPath, true);

            Directory.CreateDirectory(outputResourcsPath);

            if (!Directory.Exists(projectHMIResourcsPath))
                return;

            foreach (var filePath in Directory.GetFiles(projectHMIResourcsPath))
            {
                var fileName = filePath.Substring(projectHMIResourcsPath.Length);
                try
                {
                    File.Copy(filePath, Path.Combine(outputResourcsPath, fileName), true);
                }
                catch (Exception ex)
                {
                    LogUtil.Error("工程编译：复制资源文件出现异常.异常为：" + ex);
                }
            }
        }

        private void CopyHMIRunFiles()
        {
            foreach (FileInfo fileInfo in DirectoryHelper.GetHMIRunDirectory().GetFiles())
            {
                try
                {
                    File.Copy(fileInfo.FullName, PathHelper.GetOutputPath() + fileInfo.Name, overwrite: true);
                }
                catch (Exception ex)
                {
                    LogUtil.Error("工程编译：复制HMIRun文件出现异常.异常为：" + ex);
                }
            }
        }

        private void CopyUserControl()
        {
            foreach (var directoryInfo in DirectoryHelper.GetUserControlDirectory().GetDirectories())
            {
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    if (!(fileInfo.Extension.ToLower() == ".dll") || fileInfo.Name.StartsWith("ActiveX"))
                        continue;

                    try
                    {
                        File.Copy(fileInfo.FullName, PathHelper.GetOutputPath() + fileInfo.Name, overwrite: true);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Error("工程编译：复制UserControl出现异常.异常为：" + ex);
                    } 
                }
            }
        }

        private bool ExecuteHMICompile()
        {
            CEditEnvironmentGlobal.OutputMessage.Say("进入编译过程.");

            var compiler = new Compiler(CEditEnvironmentGlobal.ProjectHPFFilePath, PathHelper.GetLogicCodePath(), 
                PathHelper.GetOutputPath(), CEditEnvironmentGlobal.dhp, CEditEnvironmentGlobal.dfs);
            
            CEditEnvironmentGlobal.OutputMessage.Say("生成用户逻辑代码.");
            compiler.CreateScript();
            CEditEnvironmentGlobal.OutputMessage.Say("生成用户逻辑代码成功.");

            CEditEnvironmentGlobal.OutputMessage.Say("开始压缩资源数据.");
            compiler.ZipResource(CEditEnvironmentGlobal.dhp.Compress, CEditEnvironmentGlobal.dhp.dirtyCompile, CEditEnvironmentGlobal.dirtyPageTemp, CEditEnvironmentGlobal.OutputMessage.Say);
            CEditEnvironmentGlobal.OutputMessage.Say("压缩资源数据成功.");

            CEditEnvironmentGlobal.OutputMessage.Say("开始执行编译.");
            if (!compiler.DynamicCompile())
            {
                CEditEnvironmentGlobal.OutputMessage.Hide();
                return false;
            }
            CEditEnvironmentGlobal.OutputMessage.Say("编译工程成功.");

            CEditEnvironmentGlobal.OutputMessage.Say("开始创建ActiveX控件安装包.");
            compiler.CreateCab();
            CEditEnvironmentGlobal.OutputMessage.Say("创建ActiveX控件安装包成功.");

            CEditEnvironmentGlobal.OutputMessage.Say("编译过程完成.");

            return true;
        }

    }
}
