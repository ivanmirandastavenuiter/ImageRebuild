# ImageRebuild-BETA-version

## What it is and what it does?

ImageRebuild is just a simple program that eases image files management. Its functioning is actually pretty easy and simple to handle. 
It works mainly through two ways: renaming only or renaming and moving, depending on the circumstances. It is thought to be used on Windows 
console and it can accept one or two parameters (the location where the images are stored).

It is simple: if you introduce one path, it filters, rename and order the images on that folder. On the other hand, if you introduce both, 
then you extract all image files from origin path to target path and get them renamed. The name refactoring is set following the pattern 
"img" + number. So then, it is useful if you need all images with a common name for whatever your purpose is. 

**Program is written to only accept full paths.** Template: C:\Users\Iván\Desktop\Photos or C:/Users/Iván/Desktop/Photos.

## How do I use it?

This program is written in C#, which means you need this language installed on your computer. From this point on, it's pretty easy. 

### Obtaining .exe file

* First, go inside the folder which stores the cs file to compile it.
* Second, you have to locate the **csc** (C# compiler). Normally it is stored in this path: c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc. Note that you have to compile three files (all at once): ImageRebuild.cs, Controller.cs and PathInfo.cs To do so, try something like this:

```
C:\Users\Iván\Desktop\SECUREREPOS\ImageRebuild\ImageRebuild>c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc ImageRebuild.cs C:\Users\Iván\Desktop\SECUREREPOS\ImageRebuild\ImageRebuild\Controller\Controller.cs C:\Users\Iván\Desktop\SECUREREPOS\ImageRebuild\ImageRebuild\Entities\PathInfo.cs
```

### Using .exe file

Now it is almost ready. Tip filename.exe + path1 + path2 and run it. 

**New featurings will be soon available**
