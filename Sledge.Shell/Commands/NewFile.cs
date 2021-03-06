using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using LogicAndTrick.Oy;
using Sledge.Common.Logging;
using Sledge.Common.Shell.Commands;
using Sledge.Common.Shell.Context;
using Sledge.Common.Shell.Documents;
using Sledge.Common.Shell.Hotkeys;
using Sledge.Common.Shell.Menu;
using Sledge.Common.Translations;
using Sledge.Shell.Properties;

namespace Sledge.Shell.Commands
{
    [AutoTranslate]
    [Export(typeof(ICommand))]
    [CommandID("File:New")]
    [DefaultHotkey("Ctrl+N")]
    [MenuItem("File", "", "File", "B")]
    [MenuImage(typeof(Resources), nameof(Resources.Menu_New))]
    public class NewFile : ICommand
    {
        [ImportMany] private IEnumerable<Lazy<IDocumentLoader>> _loaders;

        public string Name { get; set; } = "New";
        public string Details { get; set; } = "New";

        public bool IsInContext(IContext context)
        {
            return true;
        }

        public async Task Invoke(IContext context, CommandParameters parameters)
        {
            var loaders = _loaders.Where(x => x.Value.CanLoad(null)).Select(x => x.Value).ToList();
            if (!loaders.Any()) return;

            var loader = loaders[0];
            if (loaders.Count > 1)
            {
                // prompt user to select document type
                //loader = null;
                // todo !
                Log.Info(nameof(NewFile), "Todo: Prompt for user to select document type");
            }

            if (loader != null)
            {
                var doc = await loader.CreateBlank();
                if (doc != null)
                {
                    await Oy.Publish("Document:Opened", doc);
                }
            }
        }
    }
}