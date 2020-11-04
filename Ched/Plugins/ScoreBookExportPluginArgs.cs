using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ched.Core;
using Ched.Plugins;

namespace Ched.Plugins
{
    public class ScoreBookExportPluginArgs : IScoreBookExportPluginArgs
    {
        private ScoreBook ScoreBook { get; }
        private Func<string> getCustomDataFunc { get; }
        private Action<string> setCustomDataFunc { get; }

        public Stream Stream { get; }
        public bool IsQuick { get; }

        public ScoreBookExportPluginArgs(ScoreBook scoreBook, Stream stream, bool isQuick, Func<string> getCustomDataFunc, Action<string> setCustomDataFunc)
        {
            ScoreBook = scoreBook;
            Stream = stream;
            IsQuick = isQuick;
            this.getCustomDataFunc = getCustomDataFunc;
            this.setCustomDataFunc = setCustomDataFunc;
        }

        public ScoreBook GetScoreBook() => ScoreBook.Clone();

        public string GetCustomData() => getCustomDataFunc();

        public void SetCustomData(string data) => setCustomDataFunc(data);
    }
}
