using System.IO;
namespace EduERPApi.Infra
{
    public static class FileLoadandSave
    {
        public static byte[] ReadContent(string Folderpath,string fileGuid)
        {
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int Offset = 0;
            int bytesRead = 0;
            FileStream fs = new FileStream(Folderpath + "/" + fileGuid, FileMode.Open, FileAccess.Read);
            MemoryStream ms = new MemoryStream();
            do
            {
                bytesRead = fs.Read(buffer, Offset, bufferSize);
                if(bytesRead>0)
                {
                    ms.Write(buffer, 0, bytesRead);
                }
                //Offset += bytesRead;
            } while (bytesRead != 0);
            fs.Close();
            byte[] TotalBytes= ms.ToArray();
            ms.Close();
            return TotalBytes;
        }

        public static void WriteContent(string Folderpath, string fileGuid,Stream sw)
        {
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int Offset = 0;
            int bytesRead = 0;
            FileStream fs = new FileStream(Folderpath + "/" + fileGuid, FileMode.Create, FileAccess.Write);
            
            do
            {
                bytesRead = sw.Read(buffer, Offset, bufferSize);
                if (bytesRead > 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
                //Offset += bytesRead;
            } while (bytesRead != 0);
            fs.Close();            
        }

        public static void DeleteFile(string folderpath,string fileGuid)
        {
            if(File.Exists(folderpath+"/"+ fileGuid))
            {
                File.Delete(folderpath + "/" + fileGuid);
            }
        }

    }
}
