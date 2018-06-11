using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Win32;

namespace exportTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] businessTypeArray = { "03", "04", "18", "01", "02", "05", "06", "11", "12", "13", "14", "15", "16", "17", "21", "22", "23", "62", "31", "32", "33", "41", "51", "52", "88", "601", "602", "603", "604", "605", "606", "607", "608", "609", "610", "701", "702" };
            for (int i = 0; i < businessTypeArray.Length; i++)
            {
                exportProcess objExportProcess = new exportProcess(businessTypeArray[i]);
                ThreadStart startExport = new ThreadStart(objExportProcess.excuteExport);
                Thread thredExport = new Thread(startExport);
                thredExport.Start();
            }
        }

        #region 导出数据 处理
        public class exportProcess
        {
            private string bussinessType;

            public exportProcess(string bussinessType)
            {
                this.bussinessType = bussinessType;
            }
            public void excuteExport()
            {
                var objExportTask = new Task<outPutExport>(exportTask, new inPutExport() { businessTypeInput = bussinessType });
                objExportTask.Start();
            }

        }
        #endregion

        #region  task 调用
        public class inPutExport
        {
            private string businessType;
            public string businessTypeInput { get => businessType; set => businessType = value; }
        }

        public class outPutExport
        {
            private string outPut;
            public string OutPut { get => outPut; set => outPut = value; }
        }

        public static outPutExport exportTask(object para)
        {
            return new outPutExport()
            {
                OutPut = Export.exportData(((inPutExport)para).businessTypeInput)
            };
        }
        #endregion


    }
}
