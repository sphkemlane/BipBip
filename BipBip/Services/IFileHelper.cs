using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Services
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
