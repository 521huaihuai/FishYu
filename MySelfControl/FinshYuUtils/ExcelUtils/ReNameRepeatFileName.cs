using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FinshYuUtils.ExcelUtils
{
    public class ReNameRepeatFileName
    {
        /// <summary>
        /// 改变重名
        /// </summary>
        /// <param name="fileName">输入文件名</param>
        /// <returns>输出文件名</returns>
        public static string ReFileName(string fileName)
        {
            try
            {
                //string fileNameWithoutExtension = Path.GetFullPath(fileName);
                string fileNameWithoutExtension = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName));
                string fileNameExtension = Path.GetExtension(fileName);
                string reNameFile = string.Empty;

                int index = 1;
                while (true)
                {
                    reNameFile = fileNameWithoutExtension + "(" + index + ")" + fileNameExtension;
                    if (!File.Exists(reNameFile))
                    {
                        break;
                    }
                    else
                    {
                        index ++;
                    }
                }

                return  reNameFile;
            }
            catch (Exception err)
            {
                //Log.Write.Error(err.Message);
                return fileName;
            }
        }
    }
}
