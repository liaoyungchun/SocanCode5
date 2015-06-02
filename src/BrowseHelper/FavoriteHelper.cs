using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BrowseHelper
{
    public class FavoriteHelper
    {
        private static char[] CRLF = new char[] { '\r', '\n' };

        /// <summary>
        /// ��ȡ�����ղؼ�
        /// </summary>
        public static FavoriteFolder GetFavorites()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
            return GetFavoritesByPath(path);
        }

        /// <summary>
        /// �����ղؼ�����·����ȡ�ղؼ�
        /// </summary>
        /// <param name="path">�ղؼ�����·��</param>
        public static FavoriteFolder GetFavoritesByPath(string path)
        {
            FavoriteFolder folder = new FavoriteFolder();
            folder.Title = Path.GetFileNameWithoutExtension(path);

            List<FavoriteFolder> childFolders = new List<FavoriteFolder>();
            foreach (string dir in Directory.GetDirectories(path))
            {
                FavoriteFolder childFolder = GetFavoritesByPath(dir);
                childFolders.Add(childFolder);
            }

            List<FavoriteItem> items = new List<FavoriteItem>();
            foreach (string file in Directory.GetFiles(path))
            {
                if ((File.GetAttributes(file) & FileAttributes.System) != FileAttributes.Hidden
                    && (File.GetAttributes(file) & FileAttributes.System) != FileAttributes.System)
                {
                    FavoriteItem item = new FavoriteItem();
                    item.Title = Path.GetFileNameWithoutExtension(file);
                    item.URL = GetUrl(file);
                    items.Add(item);
                }
            }

            folder.FavoriteFolders = childFolders;
            folder.FavoritesItems = items;
            return folder;
        }

        /// <summary>
        /// ��ȡ�ղؼ����ļ���URL
        /// </summary>
        /// <param name="fileName">�ļ�����·��</param>
        private static string GetUrl(string fileName)
        {
            string content = Encoding.Default.GetString(ReadFile(fileName));
            int start = content.IndexOf("URL=");
            string url = string.Empty;
            if (start >= 0)
            {

                start += 4;
                int end = content.IndexOfAny(CRLF, start);
                if (end >= start)
                {
                    url = content.Substring(start, end - start);
                }
                else
                {
                    url = content.Substring(start);
                }
            }
            return url;
        }

        /// <summary>
        /// ��ȡ�ļ�����
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <return>�ֽ�����</return>
        private static byte[] ReadFile(string fileName)
        {
            FileStream s = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                int n = (int)s.Length; // �ļ�����
                byte[] content = new byte[n];
                s.Read(content, 0, n);
                return content;
            }
            finally
            {
                s.Close();
            }
        }

        /// <summary>
        /// �ղؼ��е��ļ���
        /// </summary>
        public class FavoriteFolder
        {
            private string _title;
            private List<FavoriteFolder> _favoriteFolders;
            private List<FavoriteItem> _favoriteItems;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            public List<FavoriteFolder> FavoriteFolders
            {
                get { return _favoriteFolders; }
                set { _favoriteFolders = value; }
            }

            public List<FavoriteItem> FavoritesItems
            {
                get { return _favoriteItems; }
                set { _favoriteItems = value; }
            }
        }

        /// <summary>
        /// �ղؼ��е�һ����ݷ�ʽ
        /// </summary>
        public class FavoriteItem
        {
            private string _title;
            private string _url;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            public string URL
            {
                get { return _url; }
                set { _url = value; }
            }
        }
    }
}
