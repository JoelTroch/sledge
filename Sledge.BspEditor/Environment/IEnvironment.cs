﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Sledge.FileSystem;

namespace Sledge.BspEditor.Environment
{
    /// <summary>
    /// Represents an environment for a map document.
    /// </summary>
    public interface IEnvironment
    {
        string ID { get; }

        string Name { get; }

        /// <summary>
        /// The virtual file system root for the environment
        /// </summary>
        IFile Root { get; }

        /// <summary>
        /// The on-disk directories of this environment.
        /// These directories do not have to physically exist.
        /// </summary>
        IEnumerable<string> Directories { get; }

        /// <summary>
        /// Get the texture collection for this environment
        /// </summary>
        /// <returns>Async task that will return when the collection has been created</returns>
        Task<TextureCollection> GetTextureCollection();

        /// <summary>
        /// Environment extension point for custom data
        /// </summary>
        /// <param name="data">The data to add</param>
        void AddData(IEnvironmentData data);

        /// <summary>
        /// Get extension data from the environment
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <returns>The list of stored data</returns>
        IEnumerable<T> GetData<T>() where T : IEnvironmentData;
    }
}