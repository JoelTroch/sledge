using System.Collections.Generic;
using System.Linq;
using Sledge.DataStructures.MapObjects;
using Sledge.Editor.Actions;
using Sledge.Editor.Actions.MapObjects.Operations;
using Sledge.Extensions;

namespace Sledge.Editor.Problems
{
    public class TextureAxisPerpendicularToFace : IProblemCheck
    {
        public IEnumerable<Problem> Check(Map map)
        {
            var faces = map.WorldSpawn
                .Find(x => x is Solid).OfType<Solid>()
                .SelectMany(x => x.Faces)
                .ToList();
            foreach (var face in faces)
            {
                var normal = face.Texture.GetNormal();
                if (DMath.Abs(face.Plane.Normal.Dot(normal)) <= 0.0001m) yield return new Problem(GetType(), new [] { face }, Fix, "Texture axis perpendicular to face", "The texture axis of this face is perpendicular to the face plane. This occurs when manipulating objects with texture lock off, as well as various other operations. Re-align the texture to the face to repair. Fixing the problem will reset the textures to the face plane.");
            }
        }

        public IAction Fix(Problem problem)
        {
            return new EditFace(problem.Faces, x => x.AlignTextureToFace(), false);
        }
    }
}