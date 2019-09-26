using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alabo.Extensions;

namespace Alabo.Files
{
    /// <summary>
    ///     A helper class for Directory operations.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        ///     Creates a new directory if it does not exists.
        /// </summary>
        /// <param name="path">Directory to create</param>
        public static void CreateIfNotExists(string path)
        {
            var arrays = PathConvertToList(path);
            var childPath = string.Empty;
            foreach (var item in arrays)
            {
                childPath += $"{item}\\";
                var directoryInfo = new DirectoryInfo(childPath);
                if (!Directory.Exists(directoryInfo.FullName)) Directory.CreateDirectory(directoryInfo.FullName);
            }
        }

        /// <summary>
        ///     ��ȡ���е����ļ�
        /// </summary>
        /// <param name="directory"></param>
        public static List<string> GetAllDirectory(string directory)
        {
            var dir = Directory.GetDirectories(directory);
            return dir.ToList();
        }

        /// <summary>
        ///     ���ļ���·��ת��Ϊpath
        /// </summary>
        /// <param name="path"></param>
        public static List<string> PathConvertToList(string path)
        {
            var dir = new DirectoryInfo(path);
            var array = dir.FullName.ToSplitList("\\");
            return array;
        }

        /// <summary>
        ///     ��ȡ�ļ��������е��ļ�
        /// </summary>
        /// <param name="path"></param>
        /// <param name="suffix">��׺</param>
        public static System.IO.FileInfo[] GetFileInfo(string path, string suffix = "*.cs")
        {
            var root = new DirectoryInfo(path);
            var files = root.GetFiles("*.cs");
            return files;
        }

        /// <summary>
        ///     һ��Ŀ¼
        /// </summary>
        /// <param name="path"></param>
        public static List<string> GetFirstPathFolder(string path)
        {
            var fatherDirectorys = GetAllDirectory(path); // һ��Ŀ¼
            var commpents = new List<string>();
            foreach (var father in fatherDirectorys)
            {
                var array = PathConvertToList(father);
                var folder = array[array.Count - 1];
                commpents.Add(folder);
            }

            return commpents;
        }

        /// <summary>
        ///     ����Ŀ¼
        /// </summary>
        /// <param name="path"></param>
        public static List<string> GetPathFolder(string path)
        {
            var fatherDirectorys = GetAllDirectory(path); // һ��Ŀ¼
            var commpents = new List<string>();
            foreach (var father in fatherDirectorys)
            {
                var childDirectorys = GetAllDirectory(father);
                commpents.AddRange(childDirectorys);
            }

            var elements = new List<string>();
            foreach (var item in commpents)
            {
                var array = PathConvertToList(item);
                var folder = array[array.Count - 2] + "/" + array[array.Count - 1];
                elements.Add(folder);
            }

            return elements;
        }
    }
}