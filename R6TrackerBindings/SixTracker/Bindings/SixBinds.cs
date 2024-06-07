using System.IO;
using System;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Diagnostics.Eventing.Reader;
using System.Web.Script.Serialization;
using System.Security.Cryptography;

public class SixBinds
{
    // constants
    private readonly string _directoryPath;

    public static JavaScriptSerializer JSS = new JavaScriptSerializer();

    // events
    public EventHandler<NetPacket> OnEvent;

    // temp
    static string prevText = null;

    public static NetPacket ParsePkt(string json) => new NetPacket(JSS.Deserialize<dynamic>(json));

    public SixBinds()
    {
        _directoryPath = "C:\\Users\\Yeemi\\AppData\\Local\\OverWolf\\Log\\Apps\\Rainbow 6 Siege Tracker";
    }

    public void StartWatching()
    {
        var watcher = new FileSystemWatcher(_directoryPath);

        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;

        watcher.Changed += OnChanged;
        //watcher.Created += OnChanged;

        watcher.EnableRaisingEvents = true;

    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        string text;

        try
        {
            text = File.ReadAllText(e.FullPath);
        }
        catch
        {
            return;
        }

        string GetChangedContent(string previous, string current)
        {
            // Split content into lines
            string[] previousLines = previous.Split('\n');
            string[] currentLines = current.Split('\n');

            // Find differences
            List<string> changedLines = new List<string>();
            int minLength = Math.Min(previousLines.Length, currentLines.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (previousLines[i] != currentLines[i])
                {
                    changedLines.Add(currentLines[i]);
                }
            }

            // Append remaining lines from current content if any
            for (int i = minLength; i < currentLines.Length; i++)
            {
                changedLines.Add(currentLines[i]);
            }

            // Construct the changed content
            StringBuilder changedContent = new StringBuilder();
            foreach (string line in changedLines)
            {
                changedContent.AppendLine(line);
            }

            return changedContent.ToString();
        }

        if (prevText != null && OnEvent != null)
        {
            string changedContent = GetChangedContent(prevText, text);

            // theres (in some rare cases) more then 1
            string[] separator = { "(INFO)" };
            var events = changedContent.Split(separator, StringSplitOptions.None);

            foreach (var evnt in events)
            {
                if (evnt.Contains("{"))
                {
                    try
                    {
                        int stringStart = evnt.IndexOf('{');

                        dynamic obj = ParsePkt(evnt.Substring(stringStart));

                        OnEvent.Invoke(this, obj);
                    }
                    catch { } // ????????
                }
            }
        }

        prevText = text;
    }
}