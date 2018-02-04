using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ched.Components;

namespace Ched.Components.Exporter
{
    /// <summary>
    /// エクスポート可能な形式を表すインターフェースです。
    /// </summary>
    public interface IExporter
    {
        /// <summary>
        /// フォーマット名を取得します。
        /// </summary>
        string FormatName { get; }

        /// <summary>
        /// ファイル選択時に利用するフィルタを取得します。
        /// </summary>
        string Filter { get; }

        /// <summary>
        /// 指定のファイルへデータをエクスポートします。
        /// </summary>
        /// <param name="path">エクスポート先のパス</param>
        /// <param name="book">譜面データ</param>
        void Export(string path, ScoreBook book);

        /// <summary>
        /// 入力された譜面データに対して検証を行い結果を返します。
        /// </summary>
        /// <param name="book">検証を行う譜面データ</param>
        /// <returns>検証結果を格納した<see cref="IEnumerable{DiagnosticData}"/></returns>
        IEnumerable<DiagnosticData> GetDiagnostics(ScoreBook book);
    }

    /// <summary>
    /// 独自の拡張情報を持つエクスポート可能な形式を表すインターフェースです。
    /// </summary>
    /// <typeparam name="TArgs">拡張情報用クラス</typeparam>
    public interface IExtendedExpoerter<TArgs> : IExporter
    {
        TArgs CustomArgs { get; set; }
    }

    /// <summary>
    /// 譜面データに対する検証結果を表します。
    /// </summary>
    public class DiagnosticData
    {
        /// <summary>
        /// この<see cref="DiagnosticData"/>に対する重大度を取得します。
        /// </summary>
        public DiagnosticSeverity Severity { get; }

        /// <summary>
        /// この<see cref="DiagnosticData"/>に対する説明を取得します。
        /// </summary>
        public string Description { get; }

        public DiagnosticData(DiagnosticSeverity severity, string description)
        {
            Severity = severity;
            Description = description;
        }
    }

    /// <summary>
    /// <see cref="DiagnosticData"/>に対する重大度を表します。
    /// </summary>
    public enum DiagnosticSeverity
    {
        /// <summary>
        /// エクスポート上問題にならない情報を表します。
        /// </summary>
        Information,

        /// <summary>
        /// エクスポート上問題がある状態を表します。
        /// </summary>
        Warning,

        /// <summary>
        /// エクスポートできない状態を表します。
        /// </summary>
        Error,
    }
}
