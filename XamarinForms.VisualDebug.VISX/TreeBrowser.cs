using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using mshtml;
using System.Reflection;
using EnvDTE;
using XamarinForms.VisualDebug.VsixExtension.Helpers;
using Models = VisualDebug.Models;

namespace XamarinForms.VisualDebug.VsixExtension
{
    internal class TreeBrowser
    {
        private HTMLDocument _document;
        private WebBrowser _browser;
        private const string _dataJsFileName = "web\\data.js";
        private const string _chartJsFileName = "web\\chart.js";
        private const string _stylesheetFileName = "web\\appcss.css";
        private const string _htmlTemplateFileName = "web\\index.html";
        private const string _svgBody = "<div id='body'></div>";
        private const string _defaultHtmlTemplate = @"<!DOCTYPE html>
<html lang=""en"">
    <head>
        <meta http-equiv=""X-UA-Compatible"" content=""IE=Edge"" />
        <meta charset=""utf-8"" />
        <!-- This is to make sure your relative image links show up nicely. -->
        <base href=""file:///{0}/"" />
        <title>Render Tree</title>
        
        <script src='https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
        <script src='//d3js.org/d3.v3.min.js' charset='utf-8'></script>
        <!-- Here is where the custom style sheet is inserted as well as js setup code. -->
        {1}
    </head>
    <body>
        {2}
    </body>
</html>";
        private double _cachedPosition = 0,
                       _cachedHeight = 0,
                       _positionPercentage = 0;
         
        public static string GetStylesheet(string treeJson)
        {
            string file = GetCustomStylesheetFilePath();
            string folder = GetFolder();
            string Csspath = "web\\appcss.css";

            const string linkFormat = "<link rel=\"stylesheet\" href=\"{0}\" />";
            string link = string.Format(CultureInfo.CurrentCulture, linkFormat, Csspath);

            const string inlineScriptFormat = "<script>{0}</script>";

            var rawDataScript = GetScript(_dataJsFileName);

            var dataScript = string.Format(CultureInfo.CurrentCulture, rawDataScript, treeJson);

            link += string.Format(CultureInfo.CurrentCulture, inlineScriptFormat, dataScript);

            var chartScript = GetScript(_chartJsFileName);

            link += string.Format(CultureInfo.CurrentCulture, inlineScriptFormat, chartScript);
            
            if (File.Exists(file))
            {
                link += string.Format(CultureInfo.CurrentCulture, linkFormat, file);
                return link;
            }

            return link;
        }

        private static string GetFolder()
        {
            string assembly = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(assembly);
            return folder;
        }

        public static string GetCustomStylesheetFilePath()
        {
            return _stylesheetFileName;
        }

        private static string GetSolutionOrGlobalFile(string solutionFileName, string globalFilePath)
        {
            var solutionFile = GetSolutionFile(solutionFileName);

            if (null == solutionFile || !File.Exists(solutionFile))
            {
                if (!string.IsNullOrEmpty(globalFilePath))
                    return globalFilePath;
            }

            return solutionFile;
        }

        private static string GetSolutionFile(string fileName)
        {
            string folder = ProjectHelpers.GetSolutionFolderPath();

            if (string.IsNullOrEmpty(folder))
                return null;

            return Path.Combine(folder, fileName);
        }

        internal void UpdateTree(string treeJson)
        {
            if (_browser == null)
                return;

            var htmlFormatString = GetHtmlTemplate();

            var baseHref = GetFolder().Replace("\\", "/");

            var styleSheet = GetStylesheet(treeJson);

            string html;

            try
            {
                // The Markdown compiler cannot return errors
                html = string.Format(CultureInfo.InvariantCulture, htmlFormatString,
                    baseHref,
                    styleSheet,
                    _svgBody);
            }
            catch (Exception exp)
            {
                html = string.Format(CultureInfo.InvariantCulture, _defaultHtmlTemplate,
                    baseHref,
                    styleSheet,
                    CreateExceptionBox(exp)) + treeJson;
            }

            if (_document != null)
            {
                _cachedPosition = _document.documentElement.getAttribute("scrollTop");
                _cachedHeight = Math.Max(1.0, _document.body.offsetHeight);
                _positionPercentage = _cachedPosition * 100 / _cachedHeight;
            }

            _browser.NavigateToString(html);
        }

        private string CreateExceptionBox(Exception exp)
        {
            return $@"<h3>Html Load Error</h3>
<h4>{exp.Message}</h4><hr />";
        }

        private string GetHtmlTemplate()
        {
            var templateFile = _htmlTemplateFileName;

            if (!string.IsNullOrEmpty(templateFile))
            {
                try
                {
                    var template = File.ReadAllText(templateFile);
                    return template;
                }
                catch { }
            }


            //-- Return default
            return _defaultHtmlTemplate;
        }

        private static string GetScript(string scriptFile)
        {
            if (!string.IsNullOrEmpty(scriptFile))
            {
                try
                {
                    var script = File.ReadAllText(scriptFile);
                    return script;
                }
                catch { }
            }


            //-- Return default
            return "";
        }

        internal FrameworkElement CreatePreviewControl()
        {
            _browser = new WebBrowser();
            _browser.HorizontalAlignment = HorizontalAlignment.Stretch;
            _browser.VerticalAlignment = VerticalAlignment.Stretch;

            _browser.LoadCompleted += (s, e) =>
            {
                _document = _browser.Document as HTMLDocument;
                _cachedHeight = _document.body.offsetHeight;
                _document.documentElement.setAttribute("scrollTop", _positionPercentage * _cachedHeight / 100);
            };

            return _browser;
        }
    }
}