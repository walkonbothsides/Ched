using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ched.Core;
using Ched.Plugins;

namespace Ched.Components
{
    internal class ExportManager
    {
        private IScoreBookExportPlugin LastUsedPlugin { get; set; }
        private string LastOutputPath { get; set; }
        private Dictionary<string, string> CustomDataCache { get; set; }

        public bool CanReExport => LastUsedPlugin != null;

        protected void Initialize()
        {
            LastUsedPlugin = null;
        }

        public void Load(ScoreBook book)
        {
            Initialize();
            CustomDataCache = new Dictionary<string, string>(book.ExportArgs);
        }

        public void Save(ScoreBook book)
        {
            book.ExportArgs = new Dictionary<string, string>(CustomDataCache);
        }

        public (PluginResult Result, ScoreBookExportPluginArgs Args) Export(ScoreBook book, IScoreBookExportPlugin plugin, string dest)
        {
            return Export(book, plugin, dest, false);
        }

        public (PluginResult Result, ScoreBookExportPluginArgs Args) ReExport(ScoreBook book)
        {
            if (!CanReExport) throw new InvalidOperationException();
            return Export(book, LastUsedPlugin, LastOutputPath, true);
        }

        protected (PluginResult Result, ScoreBookExportPluginArgs Args) Export(ScoreBook book, IScoreBookExportPlugin plugin, string dest, bool isQuick)
        {
            string name = ResolvePluginName(plugin);
            LastOutputPath = dest;

            using (var stream = new FileStream(dest, FileMode.Create, FileAccess.Write))
            {
                var args = new ScoreBookExportPluginArgs(book, stream, isQuick, () => CustomDataCache[name], customData => CustomDataCache[name] = customData);
                var result = plugin.Export(args);
                LastUsedPlugin = plugin;
                return (result, args);
            }
        }

        protected string ResolvePluginName(IScoreBookExportPlugin plugin) => plugin.GetType().FullName;
    }
}
