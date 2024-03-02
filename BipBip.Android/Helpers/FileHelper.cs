using Android.OS;
using System;
using System.IO;
using Xamarin.Forms;
using BipBip.Droid.Helpers;
using BipBip.Services;

[assembly: Dependency(typeof(FileHelper))]
namespace BipBip.Droid.Helpers
{
    internal class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}