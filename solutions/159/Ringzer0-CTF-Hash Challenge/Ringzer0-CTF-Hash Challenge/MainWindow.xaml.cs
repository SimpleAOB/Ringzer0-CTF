using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ringzer0_CTF_Hash_Challenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            hashtb.Text = "Ready";
        }
        string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        private void CTF_Start_Click(object sender, RoutedEventArgs e)
        {
            WebRequest request = WebRequest.Create("https://ringzer0team.com/challenges/159/");
            request.Method = "GET";
            request.Headers.Add("Cookie", "PHPSESSID=8344r0af359fe2nf6tt2nsguo1");
            string response = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            int start = response.IndexOf("BEGIN HASH") + 26;
            int end = response.IndexOf("END HASH") - 16;
            string hash = response.Substring(start, end - start);
            string hashdir = @"C:\Users\William\Documents\Hacking\Ringzer0-CTF\hashes";
            string clrText = "";
            using (StreamReader f = new StreamReader(hashdir + "\\" + hash.Substring(0, 4)))
            {
                string txt = f.ReadToEnd();
                string[] parsed = txt.Split(';');
                foreach (string s in parsed)
                {
                    if (s.Contains(hash))
                    {
                        clrText = (s.Split(':'))[1];
                        break;
                    }
                }
            }
            WebRequest request2 = WebRequest.Create("https://ringzer0team.com/challenges/159/" + clrText);
            request2.Method = "GET";
            request2.Headers.Add("Cookie", "PHPSESSID=8344r0af359fe2nf6tt2nsguo1");
            string response2 = new StreamReader(request2.GetResponse().GetResponseStream()).ReadToEnd();
            if (response2.IndexOf("too slow!") > 0)
            {
                Console.WriteLine("Too slow, try again");
                resp1.Text = response;
                hashtb.Text = clrText;
            }
            else if (response2.IndexOf("FLAG-") > 0)
            {
                Console.WriteLine("Found flag: " + response2.Substring(response2.IndexOf("FLAG-"), 31));
                Flag_Box.Text = response2.Substring(response2.IndexOf("FLAG-"), 31);
            }
        }
    }
}
