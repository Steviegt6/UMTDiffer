using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DiffPatch;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;

namespace UndertaleModLib.Diff
{
    public static class CodeDiffer
    {
        public enum CodeDiffType
        {
            Added,
            Removed,
            Modified
        }

        public const string RemovedCodeList = "removed_code.list";
        public static readonly Regex HunkOffsetRegex = new Regex(@"@@ -(\d+),(\d+) \+([_\d]+),(\d+) @@", RegexOptions.Compiled);

        private static readonly ConcurrentBag<FilePatcher> Results = new ConcurrentBag<FilePatcher>();

        public static readonly string[] AllowedExtensions =
        {
            ".gml"
        };

        public static void WriteDecompiled(UndertaleData data, string directory)
        {
            Directory.CreateDirectory(directory);
            
            foreach (UndertaleCode code in data.Code)
            {
                string decompiled = Decompiler.Decompiler.Decompile(code, new GlobalDecompileContext(data, true));
                File.WriteAllText(Path.Combine(directory, code.Name.Content + ".gml"), decompiled);
            }
        }

        public static string[] Diff(string dir, string baseDir, string srcDir, string patchDir)
        {
            DirectoryInfo baseDirectory = new DirectoryInfo(baseDir);
            DirectoryInfo patchedDirectory = new DirectoryInfo(srcDir);
            DirectoryInfo patchDirectory = new DirectoryInfo(patchDir);

            foreach (FileInfo file in patchedDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                string relativePath = GetRelativePath(file.ToString(), patchedDirectory.ToString());
                
                if (!File.Exists(Path.Combine(baseDirectory.ToString(), relativePath)))
                    File.Copy(file.ToString(), Path.Combine(patchDirectory.ToString(), relativePath));
                else if (IsDiffable(relativePath))
                    DiffFile(dir, relativePath, baseDirectory.ToString(), patchedDirectory.ToString(), patchDirectory.ToString());
            }

            foreach (FileInfo file in patchDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                string relativePath = GetRelativePath(file.ToString(), patchDirectory.ToString());
                string targetPath = relativePath.EndsWith(".patch") ? relativePath.Substring(0, relativePath.Length - 6) : relativePath;
                
                if (!File.Exists(Path.Combine(patchedDirectory.ToString(), targetPath)))
                    file.Delete();
            }

            return baseDirectory.EnumerateFiles("*", SearchOption.AllDirectories)
                .Where(x =>
                {
                    string relativePath = GetRelativePath(x.ToString(), baseDirectory.ToString());
                    return !File.Exists(Path.Combine(patchedDirectory.ToString(), relativePath));
                }).Select(x => GetRelativePath(x.ToString(), baseDirectory.ToString()))
                .ToArray();
        }

        private static void DiffFile(string dir, string relativePath, string baseDirectory, string patchedDirectory, string patchDirectory)
        {
            PatchFile patchFile = Differ.DiffFiles(new LineMatchedDiffer(),
                Path.Combine(GetRelativePath(baseDirectory, dir), relativePath).Replace('\\', '/'),
                Path.Combine(GetRelativePath(patchedDirectory, dir), relativePath).Replace('\\', '/'),
                dir
            );

            string patchPath = Path.Combine(patchDirectory, relativePath + ".patch");
            
            if (!patchFile.IsEmpty)
            {
                Directory.CreateDirectory(new FileInfo(patchPath).DirectoryName ?? "");
                File.WriteAllText(patchPath, patchFile.ToString(true));
            }
            else if (File.Exists(patchPath))
                File.Delete(patchPath);
        }

        public static void Patch(string dir, string baseDir, string srcDir, string patchDir)
        {
            DirectoryInfo baseDirectory = new DirectoryInfo(baseDir);
            DirectoryInfo patchedDirectory = new DirectoryInfo(srcDir);
            DirectoryInfo patchDirectory = new DirectoryInfo(patchDir);

            string removedFileList = Path.Combine(dir, RemovedCodeList);
            HashSet<string> noCopy = new HashSet<string>(File.ReadAllLines(removedFileList));
            HashSet<string> newFiles = new HashSet<string>();

            foreach (FileInfo file in patchDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                string relativePath = GetRelativePath(file.ToString(), patchDirectory.ToString());

                if (relativePath.EndsWith(".patch"))
                {
                    newFiles.Add(PatchDiff(file.ToString(), dir).PatchedPath);
                    noCopy.Add(relativePath.Substring(0, relativePath.Length - 6));
                }
                else if (relativePath != RemovedCodeList)
                {
                    string dest = Path.Combine(patchedDirectory.ToString(), relativePath);
                    
                    File.Copy(file.ToString(), dest);
                    newFiles.Add(dest);
                }
            }

            foreach (FileInfo file in baseDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                string relativePath = GetRelativePath(file.ToString(), baseDirectory.ToString());

                if (!noCopy.Contains(relativePath))
                {
                    string dest = Path.Combine(patchedDirectory.ToString(), relativePath);
                    
                    File.Copy(file.ToString(), dest);
                    newFiles.Add(dest);
                }
            }
            
            foreach (FileInfo file in patchedDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
                if (!newFiles.Contains(file.ToString()))
                    file.Delete();
        }

        private static FilePatcher PatchDiff(string patchPath, string dir)
        {
            FilePatcher patcher = FilePatcher.FromPatchFile(patchPath, dir);
            patcher.Patch(Patcher.Mode.EXACT);

            Directory.CreateDirectory(Directory.GetParent(patcher.PatchedPath)?.ToString() ?? "");
            
            patcher.Save();

            return patcher;
        }

        private static bool IsDiffable(string path) => AllowedExtensions.Contains(Path.GetExtension(path));
        
        private static string GetRelativePath(string path, string toRemove)
        {
            path = path.Replace(toRemove, "");

            if (path.StartsWith('\\') || path.StartsWith('/'))
                path = path.Remove(0, 1);

            return path;
        }
    }
}