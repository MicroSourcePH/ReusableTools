﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MSI_MailManager.Models;

namespace MSI_Runner
{
    public class Program
    {
        static void Main(string[] args)
        {
            MSI_MailManager.MailManager mailManager = new MSI_MailManager.MailManager();
            Program p = new Program();
            string result = mailManager.SendEmail(new Email()
            {
                MessageInformation = new MessageInformation()
                {
                    Body = "TEST BODY",
                    IsHTMLBody = false,
                    Subject = "TEST SUBJECT",
                    Attachments = p.GetTestAttachments(5),
                    CompressAttachments = true,
                    CompressedAttachmentFileName = "TESTARCHIVE"
                },
                //RecipientInformation = new List<RecipientInformation> { new RecipientInformation() { ToEmail = "hil.jacla@gmail.com"} },
                SenderInformation = new SenderInformation()
                {
                    FromEmail = "offshoreconfie@gmail.com",
                    FromName = "Hilario Jacla III",
                    FromPassword = "7tfRPX-=",
                },
                SMTPInformation = new SMTPInformation()
                {
                    EnableSSL = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = true
                }
            });
            Console.WriteLine(result);
        }

        /// <summary>
        /// Generates a text file in the current assembly directory and sends the path to the caller
        /// </summary>
        /// <returns></returns>
        public List<string> GetTestAttachments(int fileCount)
        {
            List<string> filePaths = new List<string>();
            for (int x = 0; x < fileCount; x++)
            {
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/SamleTextFile_" + x + ".txt";
                try
                {
                    if (!File.Exists(path))
                    {
                        File.Create(path).Close();
                        TextWriter tw = new StreamWriter(path);
                        tw.WriteLine("This is an auto-generated text.");
                        tw.Close();
                    }
                    else if (File.Exists(path))
                    {
                        TextWriter tw = new StreamWriter(path);
                        tw.WriteLine("This is an auto-generated text.");
                        tw.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
                filePaths.Add(path);
            }
            return filePaths;
        }
    }
}
