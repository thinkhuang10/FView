using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CommonSnappableTypes;

public class DHMIImageManage
{
    public static Dictionary<string, Image> DicImages;

    private static readonly string tempdllPath = null;

    public static string projectname = "";

    public static string ipaddress = "";

    public static string projectpath = "";

    public static string SaveImage(Image newImage)
    {
        try
        {
            if (newImage == null)
            {
                return null;
            }
            if (DicImages == null)
            {
                DicImages = new Dictionary<string, Image>();
            }
            string baseDirectory = tempdllPath;
            if (baseDirectory == null)
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            bool flag = false;
            if (baseDirectory.ToLower().Contains("http:"))
            {
                flag = true;
            }
            string text = Guid.NewGuid().ToString() + ".jpg";
            string text2 = "";
            if (flag)
            {
                string text3 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + projectname + "\\" + ipaddress + "\\Resources\\";
                if (!Directory.Exists(text3))
                {
                    Directory.CreateDirectory(text3);
                }
                text2 = text3 + text;
                try
                {
                    newImage.Save(text2);
                    DicImages.Add(text, newImage);
                    return text;
                }
                catch
                {
                    return null;
                }
            }
            if (!Directory.Exists(projectpath + "\\Resources"))
            {
                Directory.CreateDirectory(projectpath + "\\Resources");
            }
            newImage.Save(projectpath + "\\Resources\\" + text);
            DicImages.Add(text, newImage);
            return text;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null;
        }
    }

    public static Image LoadImage(string ImageName)
    {
        try
        {
            if (ImageName == null)
            {
                return null;
            }
            if (DicImages == null)
            {
                DicImages = new Dictionary<string, Image>();
            }
            if (DicImages.ContainsKey(ImageName))
            {
                return DicImages[ImageName];
            }
            string baseDirectory = tempdllPath;
            if (baseDirectory == null)
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            bool flag = false;
            if (baseDirectory.ToLower().Contains("http:"))
            {
                flag = true;
            }
            string text = "";
            if (flag)
            {
                string text2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + projectname + "\\" + ipaddress + "\\Resources\\";
                if (!Directory.Exists(text2))
                {
                    Directory.CreateDirectory(text2);
                }
                text = text2 + ImageName;
                try
                {
                    DownloadFile(baseDirectory + "\\Resources\\" + ImageName, text);
                    MemoryStream stream = new(File.ReadAllBytes(text));
                    Image image = Image.FromStream(stream);
                    image.Tag = ImageName;
                    DicImages.Add(ImageName, image);
                    return DicImages[ImageName];
                }
                catch
                {
                    return null;
                }
            }
            if (!Directory.Exists(projectpath + "\\Resources"))
            {
                Directory.CreateDirectory(projectpath + "\\Resources");
            }
            if (File.Exists(projectpath + "\\Resources\\" + ImageName))
            {
                MemoryStream stream2 = new(File.ReadAllBytes(projectpath + "\\Resources\\" + ImageName));
                Image image2 = Image.FromStream(stream2);
                image2.Tag = ImageName;
                DicImages.Add(ImageName, image2);
                return DicImages[ImageName];
            }
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null;
        }
    }

    private static void DownloadFile(string srcFile, string dstFile)
    {
        using WebClient webClient = new();
        if (File.Exists(dstFile))
        {
            return;
        }
        using Stream stream = webClient.OpenRead(srcFile);
        using FileStream fileStream = new(dstFile, FileMode.Create);
        byte[] buffer = new byte[32768];
        int num = 0;
        while ((num = stream.Read(buffer, 0, 32768)) != 0)
        {
            fileStream.Write(buffer, 0, num);
        }
        fileStream.Close();
        fileStream.Dispose();
    }
}
