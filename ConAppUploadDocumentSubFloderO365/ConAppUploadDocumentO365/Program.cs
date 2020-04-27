using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;
using System.Security;
using System.IO;

namespace ConAppUploadDocumentO365
{
    class Program
    {
        static void Main(string[] args)
        {
            //string siteUrl = "http://SharePoint/MySite";
            //changed the path below according to what you have
            string siteUrl = "https://ptp.sharepoint.com/";

            ClientContext clientContext = new ClientContext(siteUrl);

            SecureString securePassword = Login.GetPassword();

            clientContext.Credentials = new SharePointOnlineCredentials("login@ptp.com.my", securePassword);

            Web rootWeb = clientContext.Web;

            //using (FileStream fs = new FileStream(@"sperror.txt", FileMode.Open))
            //{
            //    Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, "/Documents/Topfolder/sperror.txt", fs, true);
            //}

            string filePath = "sperror.txt";

            FileCreationInformation newFile = new FileCreationInformation();

            newFile.Content = System.IO.File.ReadAllBytes(filePath);
            newFile.Url = System.IO.Path.GetFileName(filePath);

            SP.List oList = clientContext.Web.Lists.GetByTitle(@"Documents");

            //Add Folder 
            var folders = oList.RootFolder.Folders;
            clientContext.Load(folders);
            clientContext.ExecuteQuery();

            var folder = folders.Where(r => r.Name == "TopFolder");
            var folder1 = folder.FirstOrDefault();

            //Microsoft.SharePoint.Client.File uploadFile = oList.RootFolder.Files.Add(newFile);
            Microsoft.SharePoint.Client.File uploadFile = folder1.Files.Add(newFile);

            clientContext.Load(uploadFile);

            clientContext.ExecuteQuery();

            SP.ListItem item = uploadFile.ListItemAllFields;
            string docTitle = string.Empty;

            item["Title"] = docTitle;
            item.Update();

            clientContext.ExecuteQuery();

        }
    }
}
