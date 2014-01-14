using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abot.Crawler;
using Abot.Poco;

namespace WebCrawlerWordsCount
{
    public partial class WebCrawlerWordCount : Form
    {
        delegate void StringParameterDelegate(string value);
        delegate void CoutnersDelegate(int linksFound, int linksVisited, int linksSkipped);
        delegate void UpdateListViewDelegate(string url, int wordsCount, string filepath);

        int m_linksFound;
        int m_linksVisited;
        int m_linksSkipped;

        string m_strSiteFolder;
        string m_strSiteStatsFilename;

        Uri m_baseURL;
        BackgroundWorker m_backgroundWorker;

        StreamWriter m_logFile;

        public WebCrawlerWordCount()
        {
            InitializeComponent();
            //siteURL.Text = @"http://www.ynet.co.il/";
            //siteURL.Text = @"http://startupgrind.com/";
            siteURL.Text = @"http://www.exent.com/";
            progressBar.Minimum = 0;

            m_linksFound = 0;
            m_linksVisited = 0;
            m_linksSkipped = 0;
            UpdateCounters(m_linksFound, m_linksVisited, m_linksSkipped);

            m_strSiteStatsFilename = "_stats.csv";

            m_backgroundWorker = new BackgroundWorker();
            m_backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorkerDoWork);
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (Uri.IsWellFormedUriString(siteURL.Text, UriKind.Absolute))
            {
                CreateLogFile("log.txt");
                m_baseURL = new Uri(siteURL.Text);
                Log(@"Start checking site: " + m_baseURL.ToString());
                Log("host is: " + m_baseURL.Host);
                CreateFolderForSite(m_baseURL.Host);
                
                m_backgroundWorker.RunWorkerAsync(m_baseURL.ToString());
            }
            else
            {
                LogErr("site URL is malformed");
            }

        }

        private void updateStartWorking()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(updateStartWorking));
                return;
            }
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Enabled = true;
            listViewResults.Items.Clear();
            siteURL.Enabled = false;
            goButton.Enabled = false;
            settingsButton.Enabled = false;
        }

        private void updateFinishedWorking()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(updateFinishedWorking));
                return;
            }
            progressBar.Value = progressBar.Maximum;
            progressBar.Enabled = false;
            siteURL.Enabled = true;
            goButton.Enabled = true;
            settingsButton.Enabled = true;
        }
        
        private void backgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            string url = (string)e.Argument;

            updateStartWorking();
            LogMsg(@"starting worker thread on url: " + url);

            PoliteWebCrawler crawler = new PoliteWebCrawler();
            crawler.PageCrawlStartingAsync += crawler_PageCrawlStartingAsync;
            crawler.PageCrawlCompletedAsync += crawler_PageCrawlCompletedAsync;
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowedAsync;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowedAsync;

            Uri uri = new Uri(url);
            CrawlResult result = crawler.Crawl(uri);

            if (m_backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                m_backgroundWorker.ReportProgress(0);
            }

            LogMsg("**** worker completed");
            updateFinishedWorking();
            listViewToCsvAsync();
        }

        void listViewToCsvAsync()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(listViewToCsvAsync));
                return;
            }
            listViewToCsv(listViewResults, m_strSiteStatsFilename);
        }

        void listViewToCsv(ListView listView, string filename)
        {
            string filepath = Path.Combine(m_strSiteFolder, filename);
            StreamWriter file = new StreamWriter(filepath);
            string delimiter = ",";
            file.WriteLine("statistics for site: " + siteURL.Text);
            file.WriteLine("total links: " + listView.Items.Count.ToString());

            // the data itself
            foreach (ListViewItem item in listView.Items)
            {
                string line = "";
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    line += subItem.Text + delimiter;
                }
                // write line to file.
                file.WriteLine(line);
            }
            file.Flush();
            file.Close();
        }

        void crawler_PageCrawlStartingAsync(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            string msg;

            m_linksFound++;
            UpdateCounters(m_linksFound, -1, -1);

            msg = "checking: " + pageToCrawl.Uri.AbsoluteUri.ToString() + " (parent: " + pageToCrawl.ParentUri.AbsoluteUri.ToString() + ")";
            LogMsg(msg);
        }

        void crawler_PageCrawlCompletedAsync(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            string msg;

            m_linksVisited++;
            UpdateCounters(-1, m_linksVisited, -1);

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                msg = "Failed to crawl to page: " + crawledPage.Uri.AbsoluteUri.ToString();
                LogErr(msg);
            } 
            else
            {
                msg = "visited: " + crawledPage.Uri.AbsoluteUri.ToString();
                LogMsg(msg);
                CountWordsOnPage(crawledPage);
            }
        }

        void crawler_PageCrawlDisallowedAsync(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            string msg;

            m_linksSkipped++;
            UpdateCounters(-1, -1, m_linksSkipped);

            msg = "Did not crawl the links on page " + pageToCrawl.Uri.AbsoluteUri.ToString() + " due to " + e.DisallowedReason.ToString();
            LogErr(msg);
        }

        void crawler_PageLinksCrawlDisallowedAsync(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            string msg;

            m_linksSkipped++;
            UpdateCounters(-1, -1, m_linksSkipped);

            msg = "Did not crawl the links on page " + crawledPage.Uri.AbsoluteUri.ToString() + " due to " + e.DisallowedReason.ToString();
            LogErr(msg);
        }

        private void CountWordsOnPage(CrawledPage crawledPage)
        {
            string url = crawledPage.Uri.AbsoluteUri.ToString();
            HtmlAgilityPack.HtmlDocument htmlDoc = crawledPage.HtmlDocument;
            HtmlAgilityPack.HtmlNode root = htmlDoc.DocumentNode;

            string page_str = "";
            foreach (HtmlAgilityPack.HtmlNode node in root.SelectNodes("//text()[not(parent::script)]"))
            {
                // scrap out unwanted nodes
                if (node.ParentNode.Name == "script")
                    continue;
                if (node.ParentNode.Name == "style")
                    continue;
                if (node.ParentNode.Name == "#comment")
                    continue;
                string node_str = node.InnerText.Trim();
                node_str = CleanupString(node_str);
                page_str += "\n" + node_str;
            }
            string[] words = Regex.Split(page_str, @"\W+");
            LogMsg("found " + words.Length + " words in " + url);

            // create file name from url
            string filename = crawledPage.Uri.AbsolutePath.Replace('/', '.');
            // remove preceding and trailing '.'
            filename = filename.Trim(new Char[] {' ', '.'});
            filename += ".txt";
            LogMsg("filename: " + filename);
            SaveWords(filename, words);
            UpdateListView(url, words.Length, filename);
        }

        private string CleanupString(string str)
        {
            string cleaned = str;
            cleaned.Replace("&nbsp;", " ");
            cleaned.Replace('.', ' ');
            cleaned.Replace(',', ' ');
            cleaned.Replace('\'', ' ');
            cleaned.Replace('`', ' ');
            return cleaned;
        }

        private void UpdateListView(string url, int wordsCount, string filepath)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new UpdateListViewDelegate(UpdateListView), url, wordsCount, filepath);
                return;
            }

            ListViewItem item = new ListViewItem(url);
            item.SubItems.Add(wordsCount.ToString());
            item.SubItems.Add(filepath);

            listViewResults.Items.Add(item);
            listViewResults.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void UpdateCounters(int linksFound, int linksVisited, int linksSkipped)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CoutnersDelegate(UpdateCounters), linksFound, linksVisited, linksSkipped);
                return;
            }

            if (linksFound > -1)
            {
                textBoxLinksFound.Text = linksFound.ToString();
                progressBar.Maximum = linksFound;
            }
            if (linksVisited > -1)
            {
                textBoxLinksVisited.Text = linksVisited.ToString();
                progressBar.Value = linksVisited;
            }
            if (linksSkipped > -1)
            {
                textBoxLinksSkipped.Text = linksSkipped.ToString();
            }
        }

        private void LogErr(string msg)
        {
            LogMsg("ERROR: " + msg);
        }

        private void LogMsg(string msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StringParameterDelegate(LogMsg), msg);
                return;
            }
            Log(msg);
        }

        private void Log(string msg)
        {
            string timestamp = DateTime.Now.ToString("[HH:mm:ss] ");
            LogToFile(timestamp + msg);
            listBoxLogger.Items.Add(timestamp + msg);
            listBoxLogger.TopIndex = listBoxLogger.Items.Count - 1;
        }

        private void CreateLogFile(string filePath)
        {
            m_logFile = new StreamWriter(filePath,false, Encoding.UTF8);
        }

        private void LogToFile(string msg)
        {
            m_logFile.WriteLine(msg);
        }

        private void CloseLogFile()
        {
            m_logFile.Close();
        }

        private void CreateFolderForSite(string folderpath)
        {
            DirectoryInfo dirInfo = Directory.CreateDirectory(folderpath);
            if (dirInfo.Exists)
            {
                m_strSiteFolder = folderpath;
            }
        }

        private void SaveWords(string filename, string[] words)
        {
            string filepath = Path.Combine(m_strSiteFolder, filename);
            LogMsg("saving words to file: " + filepath);
            if (!File.Exists(filepath))
            {
                File.WriteAllLines(filepath, words, Encoding.UTF8);
            }
        }

        private void listViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            string row_str = "";
            if (listViewResults.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewResults.SelectedItems[0];
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    row_str += item.SubItems[i].Text + "\t";
                }
                Clipboard.SetText(row_str);
            }
        }

    }
}
