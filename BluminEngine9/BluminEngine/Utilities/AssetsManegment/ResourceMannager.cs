using BluminEngine9.Rendering.Shading;
using BluminEngine9.Utilities.AssetsManegment.Types;
using BluminEngine9.Utilities.EventSystem;
using System;
using System.IO;

namespace BluminEngine9.Utilities.AssetsManegment
{
    public class ResourceMannager
    {
        static Dictionary<string, Guid> fileIds = new Dictionary<string, Guid>();
        static Dictionary<Guid, FileData> LoadedFiles = new Dictionary<Guid, FileData>();
        static Dictionary<Guid, Image> LoadedImages = new Dictionary<Guid, Image>();
        static Dictionary<Guid, Shader> LoadedShaders = new Dictionary<Guid, Shader>();

        public static void BatchLoadedFiles()
        {
            foreach (var item in LoadedFiles)
            {
                BatchFile(item.Value);
            }
        }
        public static void DeleteDuplicates()
        {
            List<Guid> delete = new List<Guid>();
            foreach (var v1 in LoadedFiles)
            {
                foreach (var v2 in LoadedFiles)
                {
                    if (v1.Value.Bytes.SequenceEqual(v2.Value.Bytes))
                    {
                        if (!delete.Contains(v1.Key) || !delete.Contains(v2.Key))
                        {
                            if (!v1.Value.Name.Equals(v2.Value.Name))
                            {
                                if (!delete.Contains(v2.Key))
                                {
                                    delete.Add(v2.Key);
                                    delete.Remove(v1.Key);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var item in delete)
            {
                LoadedFiles.Remove(item);
            }
        }
        public static void LoadAllDataFromAssetsPath()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                foreach (var item in Directory.GetDirectories(AppInfo.AssetsPath))
                {
                    foreach (var items in Directory.GetFiles(item))
                    {
                        Loadfile(items);
                    }
                }

                foreach (var item in Directory.GetFiles(AppInfo.AssetsPath))
                {
                    Loadfile(item);
                }
            }));
            t.Start();
        }

        public static FileData GetFile(Guid instanceId)
        {
            return LoadedFiles[instanceId];
        }


        public static Guid LoadFile(string file)
        {
            FileData fs = Loadfile(file);
            BatchFile(fs);
            return fs.InstanceId;
        }
        private static FileData Loadfile(string file)
        {
            FileInfo fi = new FileInfo(file);
            if (!fileIds.ContainsKey(fi.FullName))
            {
                Guid instanceId = Guid.NewGuid();
                byte[] bytes = File.ReadAllBytes(fi.FullName);
                FileData fd = new FileData(fi.Extension.Remove(0, 1), fi.Name.Split(".")[0], fi.DirectoryName, fi.FullName, bytes, instanceId);
                LoadedFiles.Add(instanceId, fd);
                fileIds.Add(fi.FullName, instanceId);
                return fd;
            }
            return default;
        }


        private static void BatchFile(FileData item)
        {
            if (item.Extention == "png" || item.Extention == "jpg")
            {
                Image img = new Image(item);
                LoadedImages.Add(item.InstanceId, img);
            }

            if (item.Extention == "obj")
            {

            }


            if (item.Extention == "shad")
            {
                Shader shad = new Shader(item);
                LoadedShaders.Add(item.InstanceId, shad);
            }
        }

        public static FileData GetFile(string AssetName)
        {
            AssetName = AssetName.Replace('/', '\\');
            string file = AppInfo.AssetsPath + "\\" + AssetName;

            if (fileIds.ContainsKey(file))
            {
                return LoadedFiles[fileIds[file]];
            }
            else
            {
                return LoadedFiles[LoadFile(file)];
            }
        }
        public static Image getImage(string AssetName)
        {
            AssetName = AssetName.Replace('/', '\\');
            string file = AppInfo.AssetsPath + "\\" + AssetName;

            if (fileIds.ContainsKey(file))
            {
                return LoadedImages[fileIds[file]];
            }
            else
            {
                return LoadedImages[LoadFile(file)];
            }
        }
        public static Shader getShader(string AssetName)
        {
            AssetName = AssetName.Replace('/', '\\');
            string file = AppInfo.AssetsPath + "\\" + AssetName;

            if (fileIds.ContainsKey(file))
            {
                return LoadedShaders[fileIds[file]];
            }
            else
            {
                return LoadedShaders[LoadFile(file)];
            }
        }
    }

    public struct FileData
    {
        public string Extention { get; }
        public string Name { get; }
        public string DirectoryPath { get; }
        public string Fullpath { get; }
        public byte[] Bytes { get; }
        public Guid InstanceId { get; }

        public FileData(string extention, string name, string directoryPath, string fullpath, byte[] bytes, Guid instanceId)
        {
            Extention = extention;
            Name = name;
            DirectoryPath = directoryPath;
            Fullpath = fullpath;
            Bytes = bytes;
            InstanceId = instanceId;
        }

        public FileData(string extention, string name, string directoryPath, string fullpath) : this()
        {
            Extention = extention;
            Name = name;
            DirectoryPath = directoryPath;
            Fullpath = fullpath;
            InstanceId = Guid.NewGuid();
        }

        public override string? ToString()
        {
            return "Extention: " + Extention + " ------- Name: " + Name + " ----------Directory: " + DirectoryPath + " ----------------Fullpath: " + Fullpath + " ------------Instance id: " + InstanceId;
        }
    }
}
