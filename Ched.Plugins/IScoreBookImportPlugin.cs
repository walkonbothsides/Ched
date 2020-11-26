using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Ched.Core;

namespace Ched.Plugins
{
    /// <summary>
    /// 譜面データのインポートを行うプラグインを表します。
    /// </summary>
    public interface IScoreBookImportPlugin : IPlugin
    {
        /// <summary>
        /// ファイル選択時に利用するフィルタ文字列を取得します。
        /// </summary>
        string FileFilter { get; }

        /// <summary>
        /// 譜面データのインポート処理を行います。
        /// </summary>
        /// <param name="args">インポート時に渡される情報を表す<see cref="IScoreBookImportPluginArgs"/></param>
        /// <param name="book">正常にインポート処理が終了した場合、インポートされたデータを格納します。インポート処理に失敗した場合はnullを格納します。</param>
        /// <returns>インポート処理が正常に終了した場合、<see cref="PluginResult.Succeeded"/>を返します。</returns>
        PluginResult Import(IScoreBookImportPluginArgs args, out ScoreBook book);
    }

    /// <summary>
    /// 譜面データのインポート時に渡される情報を表します。
    /// </summary>
    public interface IScoreBookImportPluginArgs : IDiagnosable
    {
        /// <summary>
        /// データを読み取るストリームを取得します。
        /// </summary>
        Stream Stream { get; }
    }
}
