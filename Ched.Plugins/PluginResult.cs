using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ched.Plugins
{
    /// <summary>
    /// プラグインの処理結果を表します。
    /// </summary>
    public enum PluginResult
    {
        None = 0,

        /// <summary>
        /// 処理が正常に終了したことを表します。
        /// </summary>
        Succeeded = 1,

        /// <summary>
        /// ユーザにより処理がキャンセルされたことを表します。
        /// </summary>
        Cancelled = 2,

        /// <summary>
        /// 処理が正常に終了せず、中断したことを表します。
        /// </summary>
        Aborted = 3
    }
}
