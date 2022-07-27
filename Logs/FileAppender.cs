using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
using System;
using System.IO;


public class FileAppender
{
    private readonly object _lockObject = new object();
    public string FileName { get; }
    public FileAppender (string fileName)
    {
        FileName = fileName;
    }
    public bool Append(string content)
    {
        try
        {
            lock (_lockObject)
            {
                using (FileStream fs = File.Open(FileName, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    var bytes = Encoding.UTF8.GetBytes(content);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

}
