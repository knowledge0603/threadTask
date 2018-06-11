using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Diagnostics;

namespace exportTool
{
    public class Export
    {
        #region 导出
        public static string  exportData(string businessType)
        {
            string strCmd = "";
            strCmd = "exportFromWeb "  + businessType;
            string strOutPut = ExecuteExport(strCmd, 30000);
            return strOutPut;
        }
        #endregion

        #region 执行DOS命令，等待命令执行的时间（单位：毫秒），如果设定为0，则无限等待，返回DOS命令的输出，如果发生异常，返回空字符串
        public static string ExecuteExport(string dosCommand, int milliseconds)
        {
            string output = "";     //输出字符串
            if (dosCommand != null && dosCommand != "")
            {
                Process process = new Process();     //创建进程对象
                process.StartInfo.FileName = "cmd.exe";  //设定需要执行的命令
                process.StartInfo.UseShellExecute = false; //不使用系统外壳程序启动
                process.StartInfo.RedirectStandardInput = true; //不重定向输入
                process.StartInfo.RedirectStandardOutput = true; //重定向输出，而不是默认的显示在dos控制台
                process.StartInfo.RedirectStandardError = true;  //输出错误信息
                process.StartInfo.CreateNoWindow = true;  //不创建窗口
                process.StartInfo.Arguments = "/C  " + dosCommand;   //设定参数，其中的“/C”表示执行完命令后马上退出
                try
                {
                    if (process.Start())       //开始进程
                    {
                        if (milliseconds == 0)
                            process.WaitForExit();     //这里无限等待进程结束
                        else
                            process.WaitForExit(milliseconds);  //这里等待进程结束，等待时间为指定的毫秒
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出结果。
                        if (process.ExitCode == 0)
                        {
                            return "export success";
                        }
                        else {

                            return "export  Error";
                        }
                    }
                }
                catch (Exception ex)
                {
                    output = ex.Message;
                }
                finally
                {
                    if (process != null)
                    {
                        process.Close();
                        process.Dispose();
                    }
                }
            }
            return output;
        }
        #endregion
    }
}
