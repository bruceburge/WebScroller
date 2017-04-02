using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using CefSharp.WinForms.Internals;

namespace WebScrollerClient
{
    public partial class Form1 : Form
    {

        public ChromiumWebBrowser chromeBrowser;
        private bool mouseIsDown;
        private Point firstPoint;

        String page = string.Format(@"{0}\resources\html\page.html", Application.StartupPath);

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            // Note that if you get an error or a white screen, you may be doing something wrong !
            // Try to load a local file that you're sure that exists and give the complete path instead to test
            // for example, replace page with a direct path instead :
            // String page = @"C:\Users\SDkCarlos\Desktop\afolder\index.html";

            //String page = @"Z:\Users\Bruce\documents\visual studio 2015\Projects\WebScroller\WebScroller\page.html";            

            if (!File.Exists(page))
            {
                MessageBox.Show("Error The html file doesn't exists : " + page);
            }

            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(page);
            chromeBrowser.BackColor = Color.Black;
            // Add it to the form and fill it to the form window.
            //this.Controls.Add(chromeBrowser);
            //chromeBrowser.Visible = false;
            this.splitContainer1.Panel1.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            chromeBrowser.LoadingStateChanged += ChromeBrowser_LoadingStateChanged;

            //These events don't bubble up in winforms :-( 
            //chromeBrowser.MouseDown += ChromeBrowser_MouseDown; ;
            //chromeBrowser.MouseMove += Form1_MouseMove;
            //chromeBrowser.MouseUp += Form1_MouseUp;

            // Allow the use of local resources in the browser
            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;
            //chromeBrowser.Visible = false;
        }

        private void ChromeBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!e.CanReload));
        }

        private void SetIsLoading(bool v)
        {
            //chromeBrowser.Visible = v;
        }

        public Form1()
        {
            InitializeComponent();

            //resize the form to half the screen, then center it at the top
            this.Width = (int)Screen.PrimaryScreen.WorkingArea.Width / 2;
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Location = new Point(x, 0);

            //btnMove.Width = splitContainer1.Panel2.Width;

            InitializeChromium();
            // Register an object in javascript named "cefCustomObject" with function of the CefCustomObject class :3
            chromeBrowser.RegisterJsObject("cefCustomObject", new CefCustomObject(chromeBrowser, this));
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }


        #region HandleScreenMovement
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = true;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = false;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                // Get the difference between the two points
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }


        private void btnMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = true;
            }
        }

        private void btnMove_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = false;
            }
        }

        private void btnMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                // Get the difference between the two points
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }
        #endregion

        #region Right Click Menu
        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Application.Exit();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromeBrowser.Reload(true);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
