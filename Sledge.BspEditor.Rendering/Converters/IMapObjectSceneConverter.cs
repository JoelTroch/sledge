using System.Collections.Generic;
using System.Threading.Tasks;
using Sledge.BspEditor.Documents;
using Sledge.BspEditor.Primitives.MapObjects;
using Sledge.BspEditor.Rendering.Scene;

namespace Sledge.BspEditor.Rendering.Converters
{
    public interface IMapObjectSceneConverter
    {
        /// <summary>
        /// The priority of this converter.
        /// </summary>
        MapObjectSceneConverterPriority Priority { get; }

        /// <summary>
        /// Checks if further converters should be abandoned after this converter runs.
        /// </summary>
        /// <param name="smo">The current SceneMapObject</param>
        /// <param name="document">The current document</param>
        /// <param name="obj">The MapObject that's being converted</param>
        /// <returns>True to stop processing all further converters</returns>
        bool ShouldStopProcessing(SceneMapObject smo, MapDocument document, IMapObject obj);

        /// <summary>
        /// Check if the object is supported by this converter.
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>True if this converter supports the object</returns>
        bool Supports(IMapObject obj);

        /// <summary>
        /// Convert the MapObject and put the objects in the SceneMapObject.
        /// Returns false if the MapObject is considered invalid and should be ignored.
        /// </summary>
        /// <param name="smo">The SceneMapObject to add scene objects to</param>
        /// <param name="document">The current document</param>
        /// <param name="obj">The object to convert</param>
        /// <returns>False if the object is invalid</returns>
        Task<bool> Convert(SceneMapObject smo, MapDocument document, IMapObject obj);

        /// <summary>
        /// Process an existing SceneMapObject after the source properties have changed.
        /// </summary>
        /// <param name="args">An object to inject changes and deletions into</param>
        /// <param name="smo">The SceneMapObject to update scene objects in</param>
        /// <param name="document">The current document</param>
        /// <param name="obj">The object to update</param>
        /// <param name="propertyNames">The names of all properties that have changed</param>
        /// <returns>False if the update failed for some reason</returns>
        Task<bool> PropertiesChanged(SceneObjectsChangedEventArgs args, SceneMapObject smo, MapDocument document, IMapObject obj, HashSet<string> propertyNames);
    }
}