using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ImageRebuild
{
    /// <summary>
    /// Class that implements all functionality.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// It returns only image files.
        /// </summary>
        /// <param name="fileInfoList">The list to be filtered.</param>
        /// <returns>Filtered images.</returns>
        public static List<FileInfo> FilterImages(List<FileInfo> fileInfoList)
        {
            List<String> extensions = new List<String> { ".BMP", ".EMF", ".EXIF", ".GIF", ".ICON", ".JPEG", ".JPG", ".MEMORYBMP", ".PNG", ".TIFF", ".WMF", ".JPE" };

            foreach (FileInfo currentFile in fileInfoList.ToList())
            {
                if (!extensions.Contains(Path.GetExtension(currentFile.ToString().ToUpperInvariant())))
                {
                    fileInfoList.Remove(currentFile);
                }
            }
            return fileInfoList;
        }

        /// <summary>
        /// Check directory existence. If it doesn't, creates a new one.
        /// </summary>
        /// <param name="directoryInfo">Directory Object</param>
        /// <param name="newDirectory">Directory Path.</param>
        public static void CheckDirectoryStatus(DirectoryInfo directoryInfo, string newDirectory)
        {
            if (!directoryInfo.Exists)
            {
                Directory.CreateDirectory(newDirectory);
            }
        }

        /// <summary>
        /// Executes main program.
        /// </summary>
        /// <param name="paths">Arguments provided.</param>
        public void ExecuteProgram(string[] paths)
        {
            PathInfo pathInfo = FormatPaths(paths);

            if (pathInfo.PathsNumber == 0)
            {
                Console.WriteLine("No parameters found. You have to provide at least one parameter.");
            }
            else if (pathInfo.PathsNumber == 1)
            {
                try
                {
                    string ext;
                    string rootPath = AddFinalBackslashIfNotExists(pathInfo.FirstPath);
                    string targetPath = rootPath;
                    if (new DirectoryInfo(rootPath).Exists)
                    {
                        DirectoryInfo sourcePath = new DirectoryInfo(rootPath);
                        List<FileInfo> images = FilterImages(sourcePath.GetFiles().ToList());
                        if (images.Count > 0)
                        {
                            int number = 1;
                            images = SecurityNameRefactoring(images);
                            foreach (FileInfo currentImage in images)
                            {
                                ext = currentImage.Extension;
                                currentImage.MoveTo(targetPath + "img" + number + ext);
                                number++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No images files found in this folder.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Directory has not been found. Make sure the path exists and it is populated.");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else if (pathInfo.PathsNumber == 2)
            {
                try
                {
                    string rootPath = AddFinalBackslashIfNotExists(pathInfo.FirstPath);
                    string targetPath = AddFinalBackslashIfNotExists(pathInfo.SecondPath);
                    if (new DirectoryInfo(rootPath).Exists)
                    {
                        CheckDirectoryStatus(new DirectoryInfo(targetPath), targetPath);
                        DirectoryInfo sourcePath = new DirectoryInfo(rootPath);
                        List<FileInfo> images = FilterImages(sourcePath.GetFiles().ToList());
                        if (images.Count > 0)
                        {
                            if (!TargetDirectoryHasImages(new DirectoryInfo(targetPath).GetFiles().ToList()))
                            {
                                MoveToEmptyDirectory(SecurityNameRefactoring(images), targetPath);
                            }
                            else
                            {
                                MoveToNonEmptyDirectory(SecurityNameRefactoring(images), targetPath);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No images files found in this folder.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Directory has not been found. Make sure the path exists and it is populated.");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Divide properly the paths entered.
        /// </summary>
        /// <param name="paths">Arguments entered.</param>
        /// <returns>PathInfo object.</returns>
        public static PathInfo FormatPaths(string[] paths)
        {
            PathInfo pathInfo = new PathInfo();
            if (paths.Length == 0)
            {
                return pathInfo;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (string path in paths)
                {
                    sb.Append(path + " ");
                }
                string fullPathInfo = sb.ToString();
                return SeparateCommands(fullPathInfo);
            }
        }

        /// <summary>
        /// Adds one final backslash if necessary.
        /// </summary>
        /// <param name="path">String with the path.</param>
        /// <returns>String refactored.</returns>
        public static string AddFinalBackslashIfNotExists(string path)
        {
            if (!path.EndsWith(@"\"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(path);
                sb.Append(@"\");
                path = sb.ToString();
            }
            return path;
        }

        /// <summary>
        /// Encrypt the images names to avoid conflicts later.
        /// </summary>
        /// <param name="images">Images list.</param>
        /// <returns>Images list.</returns>
        public static List<FileInfo> SecurityNameRefactoring(List<FileInfo> images)
        {
            StringBuilder sb = new StringBuilder();
            string encryptedName = "";
            string ext;
            string stringSource = "ABCDEFGHIJKLMNÑOPQRSTUVWXYabcdefghijklmnñopqrstuvwxyzZ0123456789";
            Random rm = new Random();

            foreach (FileInfo fileInfo in images)
            {
                ext = fileInfo.Extension;
                sb.Clear();
                while (sb.Length < 20)
                {
                    sb.Append(stringSource.ToCharArray()[rm.Next(0, stringSource.ToCharArray().Length - 1)]);
                }
                encryptedName = sb.ToString();
                fileInfo.MoveTo(AddFinalBackslashIfNotExists(fileInfo.DirectoryName) + encryptedName + ext);
            }
            return images;
        }

        /// <summary>
        /// Checks for the existence of image files on destiny path.
        /// </summary>
        /// <param name="images">Images list.</param>
        /// <returns>True if there's images.</returns>
        public static bool TargetDirectoryHasImages(List<FileInfo> images)
        {
            bool hasImages = false;

            if (FilterImages(images).Count > 0)
            {
                hasImages = true;
            }
            return hasImages;

        }

        /// <summary>
        /// Moves images to an empty directory.
        /// </summary>
        /// <param name="images">Images list.</param>
        /// <param name="targetPath">Target path.</param>
        public static void MoveToEmptyDirectory(List<FileInfo> images, string targetPath)
        {
            string ext;
            int number = 1;
            foreach (FileInfo currentImage in images)
            {
                ext = currentImage.Extension;
                currentImage.MoveTo(targetPath + "img" + number + ext);
                number++;
            }
        }

        /// <summary>
        /// Move images to a populated directory.
        /// </summary>
        /// <param name="images">Images list.</param>
        /// <param name="targetPath">Target path.</param>
        public static void MoveToNonEmptyDirectory(List<FileInfo> images, string targetPath)
        {
            string ext;
            DirectoryInfo outputPath = new DirectoryInfo(targetPath);
            List<FileInfo> imagesOnTargetDirectory = FilterImages(outputPath.GetFiles().ToList());

            int number = 1;
            imagesOnTargetDirectory = SecurityNameRefactoring(imagesOnTargetDirectory);
            foreach (FileInfo currentImage in imagesOnTargetDirectory)
            {
                ext = currentImage.Extension;
                currentImage.MoveTo(targetPath + "img" + number + ext);
                number++;
            }

            foreach (FileInfo currentImage in images)
            {
                ext = currentImage.Extension;
                currentImage.MoveTo(targetPath + "img" + number + ext);
                number++;
            }
        }

        /// <summary>
        /// Support method for FormatPaths.
        /// </summary>
        /// <param name="fullPathInfo">Complete string to divide.</param>
        /// <returns>PathInfo object.</returns>
        public static PathInfo SeparateCommands(string fullPathInfo)
        {
            StringBuilder sb = new StringBuilder();
            PathInfo pathInfo = new PathInfo();
            string[] separatedCommands = fullPathInfo.Split(' ');

            for (int i = 0; i < separatedCommands.Length; i++)
            {
                switch (pathInfo.PathsNumber)
                {
                    case 0:
                        if (separatedCommands[i].Equals(separatedCommands[separatedCommands.Length - 1]) &&
                           (separatedCommands[i].ToUpperInvariant().StartsWith(@"C:\") ||
                            separatedCommands[i].ToUpperInvariant().StartsWith("C:/")))
                        {
                            sb.Append(separatedCommands[i]);
                            pathInfo.FirstPath = sb.ToString().Trim();
                            pathInfo.PathsNumber++;
                            sb.Clear();
                        }
                        else
                        {
                            if (!separatedCommands[i].Equals(separatedCommands[separatedCommands.Length - 1]))
                            {
                                if (i + 1 < separatedCommands.Length)
                                {
                                    if (separatedCommands[i + 1].ToUpperInvariant().StartsWith("C:/") ||
                                        separatedCommands[i + 1].ToUpperInvariant().StartsWith(@"C:\"))
                                    {
                                        sb.Append(separatedCommands[i]);
                                        pathInfo.FirstPath = sb.ToString().Trim();
                                        pathInfo.PathsNumber++;
                                        sb.Clear();
                                    }
                                    else
                                    {
                                        sb.Append(separatedCommands[i] + " ");
                                    }
                                }
                            }
                            else
                            {
                                sb.Append(separatedCommands[i]);
                                pathInfo.FirstPath = sb.ToString().Trim();
                                pathInfo.PathsNumber++;
                                sb.Clear();
                            }
                        }
                        break;
                    case 1:
                        if (separatedCommands[i].Equals(separatedCommands[separatedCommands.Length - 1]) &&
                           (separatedCommands[i].ToUpperInvariant().StartsWith(@"C:\") ||
                            separatedCommands[i].ToUpperInvariant().StartsWith("C:/")))
                        {
                            sb.Append(separatedCommands[i]);
                            pathInfo.SecondPath = sb.ToString().Trim();
                            pathInfo.PathsNumber++;
                            sb.Clear();
                        }
                        else
                        {
                            if (!separatedCommands[i].Equals(separatedCommands[separatedCommands.Length - 1]))
                            {
                                sb.Append(separatedCommands[i] + " ");
                            }
                            else
                            {
                                sb.Append(separatedCommands[i]);
                                pathInfo.SecondPath = sb.ToString().Trim();
                                pathInfo.PathsNumber++;
                                sb.Clear();
                            }
                        }
                        break;
                }
            }
            return pathInfo;
        }
    }
}
