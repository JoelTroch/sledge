﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Threading.Tasks;
using LogicAndTrick.Gimme;
using Sledge.Common.Shell.Documents;

namespace Sledge.MinimalEditor.TextEditor
{
    [Export(typeof(IDocumentLoader))]
    public class TextDocumentLoader : IDocumentLoader
    {
        public IEnumerable<FileExtensionInfo> SupportedFileExtensions { get; } = new[]
        {
            new FileExtensionInfo("Text Files (*.txt)", "*.txt")
        };

        public bool CanLoad(string location)
        {
            return location == null || location.EndsWith(".txt");
        }

        public async Task<IDocument> CreateBlank()
        {
            return new TextDocument();
        }

        public async Task<IDocument> Load(string location)
        {
            return new TextDocument(location);
        }
    }
}