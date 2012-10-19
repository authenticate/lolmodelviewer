

/*
LOLViewer
Copyright 2011-2012 James Lammlein, Adrian Astley 

 

This file is part of LOLViewer.

LOLViewer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

LOLViewer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with LOLViewer.  If not, see <http://www.gnu.org/licenses/>.

*/

//
// Encapsulates the process from going from binding joint transformations
// and animation bone transformations to a skinning transformation.
//

// I finally got all this stuff nailed down for most models.  This deserves an asci pokemon.

/*
____________¶¶
___________¶¶¶¶
__________¶¶¶¶¶¶
_________¶¶¥¥¥¶¶¶
________¶¶¥¥¥¥¥¶¶¶__________________________________________¶¶¶¶¶¶¶¶
________¶¶¥¥¥¥¥¥¶¶¶_____________________________________¶¶¶¶¶¥¥¥¥¥¶¶
________¶¶¥¥¥¥¥¥ƒƒ¶¶________________________________¶¶¶¶¥¥¥¥¥¥¥¥¶¶¶¶
________¶¶¥¥¥¥ƒƒƒƒƒ¶¶___________________________¶¶¶¶ƒƒ¥¥¥¥¥¥¥¥¶¶¶¶
________¶¶¶ƒƒƒƒƒƒƒƒ§¶¶________________________¶¶ƒƒƒƒƒƒƒ¥¥¥¥¥¶¶¶¶
_________¶¶¶ƒƒƒƒƒƒ§§¶¶____________________¶¶¶¶ƒƒƒƒƒƒƒƒƒƒ¥¥¶¶¶¶
___________¶¶ƒƒƒƒƒ§§¶¶__________________¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒ¶¶¶¶
____________¶¶ƒƒ§§§§¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§¶¶
_____________¶¶§§§§§§§ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§¶¶
______________¶¶§§§ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§¶¶¶¶___________________¶¶¶¶¶¶
____________¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§¶¶¶____________________¶¶¶ƒƒƒƒƒ¶¶
__________¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ¶¶¶¶¶¶ƒƒƒƒ§§§¶¶¶___________________¶¶ƒƒƒƒƒƒƒƒ¶¶
_________¶¶ƒƒ¶¶¶¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒ¶¶__¶¶¶¶ƒƒƒ§§§§§¶¶________________¶¶ƒƒƒƒƒƒƒƒƒƒ¶¶
________¶¶ƒƒ¶¶__¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒ¶¶¶¶¶¶¶¶ƒƒƒ§§§§§§¶¶___________¶¶¶¶ƒƒƒƒƒƒƒƒ§§§§§§¶¶
_______¶¶ƒƒƒ¶¶¶¶¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ¶¶¶¶¶¶ƒƒƒƒƒ§§§§§§¶¶________¶¶ƒƒƒƒƒƒƒƒ§§§§§§§§§§¶¶
_______¶¶ƒƒƒƒ¶¶¶¶ƒƒƒƒƒ¥¥¥ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ####§§§§§¶¶____¶¶¶¶ƒƒƒƒƒƒƒƒ§§§§§§§§§§§§¶¶
_______¶¶###ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ¥¥ƒƒƒƒƒƒ########§§§§¶¶¶¶¶¶ƒƒƒƒƒƒƒƒ§§§§§§§§§§§§§§§§¶¶
_______¶¶####ƒƒƒƒƒƒƒƒ¥¥¥¥¥¥¥¥¥¥¥ƒƒƒƒƒƒ########§§¶¶¶¶ƒƒ¶¶¶¶ƒƒƒƒ§§§§§§§§§§§§§§§§§§¶¶
____¶¶¶¶¶¶###ƒƒƒƒƒƒƒƒƒ¥¥¥#####¥ƒƒƒƒƒƒƒ########¶¶ƒƒ¶¶ƒƒƒƒƒƒ¶¶§§§§§§§§§§§§§§§§§§§§¶¶
__¶¶¶ƒƒ¶¶¶¶#ƒƒƒƒƒƒƒƒƒƒ¥¥####¥¥ƒƒƒƒƒƒƒƒƒƒ####§§¶¶ƒƒƒƒƒƒƒƒ¶¶¶¶§§§§§§§§§§§§§§§§¶¶¶¶
_¶¶ƒƒ¶ƒƒƒƒ¶¶ƒƒƒƒƒƒƒƒƒƒƒƒ¥¥¥¥ƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§¶¶ƒƒƒƒƒƒƒƒƒƒƒƒ¶¶§§§§§§§§§§§§¶¶¶¶
¶¶ƒƒƒƒƒƒ§§§§¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ¶¶ƒƒƒƒƒƒƒƒ§§§§¶¶§§§§§§§§§§¶¶¶¶
__¶¶ƒƒ§§§§§§¶¶¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§¶¶¶§§§§§§§§¶¶
____¶¶§§§§§§§¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§¶__¶§§§§§§¶¶
______¶¶§§§§§§ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§¶¶____¶¶§§§§§§¶¶
________¶¶¶§ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§¶¶_______¶¶§§§§§§¶¶
_________¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§¶¶¶¶____¶¶¶¶§§§§§§§§§§¶¶
_________¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§¶¶§§¶¶¶¶¶¶ƒƒ§§§§§§§§¶¶¶¶
________¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§§¶¶ƒƒƒƒ§§§§§§¶¶¶¶
________¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§§§¶¶§§§§§§§¶¶¶¶
__¶¶¶¶¶¶¶¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§§§§¶¶§§§§§§¶¶
_¶¶ƒƒ¶¶ƒƒƒ¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§§§§¶¶¶¶§§§§§§¶¶
_¶¶ƒƒƒ¶¶ƒƒƒ¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§§§§§¶¶__¶¶¶###§§§¶¶
__¶¶§§§§§§§§¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§§§§§§§§¶¶¶¶¶#######§§§¶¶
___¶¶§§§§§§§§¶¶ƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒƒ§§§§§§§§§§§§§§§§§########¶¶¶¶¶¶
____¶¶§§§§§§§§¶¶§§§§ƒƒƒƒƒƒƒ§§§§§§§§§§§§§§§§§§§####¶¶¶¶¶¶
_____¶¶§§§§§§§¶¶§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§¶¶¶¶
_______¶¶¶¶¶¶¶§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§¶¶
______________¶¶¶¶¶¶¶¶¶¶§§§§§§§§§§§§§§§§§§§§¶¶
________________________¶¶¶¶¶¶§§§§§§§§§§¶¶¶¶
____________________________¶¶¶¶§§§§¶¶¶¶¶
____________________________¶¶§§§§§§§§¶¶
____________________________¶¶§§¶¶§§§¶¶
_____________________________¶¶§¶¶§§¶¶
______________________________¶¶¶¶¶¶
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace LOLViewer.Graphics
{
    class GLRig
    {
        private Dictionary<String, GLAnimation> animations;

        public GLRig(int maximumNumberOfBones)
        {
            animations = new Dictionary<String, GLAnimation>();
        }   

        public void Create(List<Quaternion> boneOrientations, List<Vector3> bonePositions,
            List<float> boneScales, List<int> boneParents, List<String> boneNames,
            Dictionary<String, GLAnimation> animations)
        {
            // Bones are not always in order between the ANM and SKL files.
            Dictionary<String, int> boneNameToID = new Dictionary<String, int>();
            Dictionary<int, String> boneIDToName = new Dictionary<int, String>();

            // Create the binding transform.  (The SKL initial transform.)
            GLAnimation bindingBones = new GLAnimation();
            for (int i = 0; i < boneOrientations.Count; ++i)
            {
                GLBone bone = new GLBone();

                bone.name = boneNames[i];

                boneNameToID[bone.name] = i;
                boneIDToName[i] = bone.name;

                bone.parent = boneParents[i];

                bone.transform = Matrix4.Rotate(boneOrientations[i]);
                bone.transform.M41 = bonePositions[i].X;
                bone.transform.M42 = bonePositions[i].Y;
                bone.transform.M43 = bonePositions[i].Z;

                bone.transform = Matrix4.Invert(bone.transform);

                bindingBones.bones.Add(bone);
            }

            // Convert animations into absolute space.
            foreach (var animation in animations)
            {
                // This is sort of a mess. 
                // We need to make sure "parent" bones are always updated before their "children".  The SKL file contains
                // bones ordered in this manner.  However, ANM files do not always do this.  So, we sort the bones in the ANM to match the ordering in
                // the SKL file.
                animation.Value.bones.Sort((a, b) => boneNameToID[a.name].CompareTo(boneNameToID[b.name]));

                foreach (var bone in animation.Value.bones)
                {
                    int id = boneNameToID[bone.name];
                    bone.parent = bindingBones.bones[id].parent;

                    // Sanity.
                    if (boneNameToID.ContainsKey(bone.name))
                    {
                        // For each frame...
                        for (int i = 0; i < bone.frames.Count; ++i)
                        {
                            Matrix4 parentTransform = Matrix4.Identity;
                            if (bone.parent >= 0)
                            {
                                GLBone parent = animation.Value.bones[bone.parent];
                                parentTransform = parent.frames[i];
                            }
                            bone.frames[i] = bone.frames[i] * parentTransform;
                        }
                    }
                }
            }

            // Multiply the animation transforms by the binding transform.
            foreach (var animation in animations)
            {
                foreach (var bone in animation.Value.bones)
                {
                    int id = boneNameToID[bone.name];
                    GLBone bindingBone = bindingBones.bones[id];

                    // Sanity.
                    if (boneNameToID.ContainsKey(bone.name))
                    {
                        for (int i = 0; i < bone.frames.Count; ++i)
                        {
                            bone.frames[i] = bindingBone.transform * bone.frames[i];
                        }
                    }
                }
            }

            this.animations = animations;
        }
        
        /// <summary>
        /// Interpolates the animation data between the current frame
        /// and the next frame.
        /// </summary>
        /// <param name="blend">Elasped frame time / max frame time)</param>
        /// <param name="transforms">A preallocated array of transformation matrices.</param>
        /// <returns>The resultant bone transformation.  This value is identical to the reference parameter result.</returns>
        public Matrix4[] GetBoneTransformations(String animation, int frame, float blend, ref Matrix4[] transforms)
        {
            GLAnimation anm = animations[animation];

            int nextFrame = (frame + 1) % (int)anm.numberOfFrames;
            for (int i = 0; i < anm.bones.Count; ++i)
            {
                // Get the current frame's transform.
                Matrix4 current = anm.bones[i].frames[frame];

                // Break it down into a vector and quaternion.
                Vector3 currentPosition = Vector3.Zero;
                currentPosition.X = current.M41;
                currentPosition.Y = current.M42;
                currentPosition.Z = current.M43;

                Quaternion currentOrientation = OpenTKExtras.Matrix4.CreateQuatFromMatrix(current);

                // Get the next frame's transform.
                Matrix4 next = anm.bones[i].frames[nextFrame];

                // Break it down into a vector and quaternion.
                Vector3 nextPosition = Vector3.Zero;
                nextPosition.X = next.M41;
                nextPosition.Y = next.M42;
                nextPosition.Z = next.M43;

                Quaternion nextOrientation = OpenTKExtras.Matrix4.CreateQuatFromMatrix(next);

                // Interpolate the frame data.
                Vector3 finalPosition = Vector3.Lerp(currentPosition, nextPosition, blend);
                Quaternion finalOrientation = Quaternion.Slerp(currentOrientation, nextOrientation, blend);

                // Rebuild a transform.
                Matrix4 finalTransform = Matrix4.Rotate(finalOrientation);
                finalTransform.M41 = finalPosition.X;
                finalTransform.M42 = finalPosition.Y;
                finalTransform.M43 = finalPosition.Z;

                transforms[i] = finalTransform;
            }

            return transforms;
        }
    }
}

/*
______________________________________________________¶¶¶¶¶¶
__________¶¶¶¶____________________________________¶¶¶¶11333¶¶
________¶¶1111¶¶¶¶__________¶¶¶¶¶¶¶¶¶¶________¶¶¶¶111133¶¶33¶¶
________¶¶¶¶111133¶¶¶¶__¶¶¶¶111111111¶¶¶¶¶__¶¶11111133¶¶¶¶§§¶¶
_________¶¶¶¶¶3333§§§§¶¶1111111133333333¶¶¶¶¶1111133¶¶¶¶¶¶§§¶¶
__________¶¶¶¶¶¶§§§§¶¶1111111133333333333333111133¶¶¶¶¶¶¶¶§§¶¶
___________¶¶¶¶¶¶¶¶¶¶1111111333333333333333333333333¶¶¶¶¶¶§§¶¶
____________¶¶¶¶11¶¶11111133333333333¶¶333333333333333¶¶¶¶§§¶¶
___________¶¶¶1111¶¶1111113333¶¶¶¶333¶¶33333333333333333¶¶§§¶¶
__________¶¶11¶¶¶¶¶¶11111133¶¶3333¶¶¶¶33¶¶¶¶¶¶¶¶33333333333¶¶
________¶¶11¶¶¯¯¯¯¶¶11111133¶¶3333¶¶33¶¶¯¯¯¯¯¯¯¯¶¶3333333333¶¶
_______¶¶1¶¶¯ððððððð¶¶111113333333¶¶¶¶ðððððððððð¯¯¶¶33333333¶¶
______¶¶11¶¶ðð¯¯¯¯ðððð¶¶11111133¶¶¶¶ðð¯¯¯¯ðððððððð¯¯¶¶3333333¶¶
______¶¶11¶¶ðð___ððððððð¶¶¶¶¶¶¶¶11¶¶ððð___ðððððððð¯¯¶¶3333333§¶¶
______¶¶11¶¶ðððððððððððð¶¶11111111¶¶ðððððððððððððð¯¯¶¶333333§§¶¶
______¶¶11¶¶ðððððððððððð¶¶11111111¶¶ðððððððððððððð¯¯¶¶333333§§¶¶
__¶¶¶¶¶¶1111¶¶ðððððððð¶¶111111111111¶¶ðððððððððððð¶¶33333333§§¶¶
¶¶1111¶¶111111¶¶¶¶¶¶¶¶1111111111111111¶¶¶¶¶¶¶¶¶¶¶¶3333333333§§¶¶
¶¶11111¶¶11111111111111111¶¶¶¶¶¶111111113333333333333333333§§§¶¶
_¶¶33333331111111111111111¶¶¶¶¶¶11111111333333333333333333§§§§¶¶
___¶¶333¶¶331111111111111111¶¶¶¶11111133333333¶¶33333333§§§§§¶¶
_____¶¶¶¶¶3333111111111111111111111133333333¶¶333333§§§§§§§§§¶¶
________¶¶¶333333311111111111111333333333333¶¶33§§§§¶¶§§§§§§¶¶
__________¶¶333333333333333333333333333333333¶¶¶¶¶¶¶§§§§§§§¶¶
____________¶¶3333333333333333333333333¶¶¶¶¶¶3§§§§§§§§§§§§¶¶
______________¶¶¶¶33333333333333333333¶¶3333¶¶33§§§§§§§§§¶¶
________________¶¶¶¶§§§33333333333333¶¶33§§§§§¶¶§§§§§§¶¶¶
____________________¶¶§§§§33333333333¶¶3§§§§§§¶¶§§¶¶¶¶
______________________¶¶¶¶¶¶§§§§§§§§§¶¶§§§§§§§¶¶¶¶
____________________¶¶¶§§§§§¶¶¶¶¶¶¶¶¶¶¶§§§§§§§¶¶
__________________¶¶3333§§§§§§§§¶¶_____¶¶¶¶¶¶¶
__________________¶¶333333§§¶¶¶¶
____________________¶¶¶¶¶¶¶¶

*/


