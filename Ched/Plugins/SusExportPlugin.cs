using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using Ched.Components.Exporter;
using Ched.UI;

namespace Ched.Plugins
{
    public class SusExportPlugin : IScoreBookExportPlugin
    {
        public string DisplayName => "Sliding Universal Score";

        public string FileFilter => "Sliding Universal Score(*.sus)|*.sus";

        public PluginResult Export(IScoreBookExportPluginArgs args)
        {
            SusArgs susArgs = JsonConvert.DeserializeObject<SusArgs>(args.GetCustomData() ?? "") ?? new SusArgs();
            if (!args.IsQuick)
            {
                var form = new SusExportForm(susArgs);
                if (form.ShowDialog() != DialogResult.OK) return PluginResult.Cancelled;
                args.SetCustomData(JsonConvert.SerializeObject(form.ExportArgs));
            }

            var exporter = new SusExporter() { CustomArgs = susArgs };
            exporter.Export(args.GetScoreBook(), args.Stream);

            return PluginResult.Succeeded;
        }
    }
}
