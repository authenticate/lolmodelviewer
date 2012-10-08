

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
        public const int MAX_BONES = 128;
        public GLBone[] bindingBones;

        private GLBone[] currentFrame;
        private GLBone[] nextFrame;
        
        public GLRig()
        {
            bindingBones = new GLBone[MAX_BONES];
            currentFrame = new GLBone[MAX_BONES];
            nextFrame = new GLBone[MAX_BONES];

            for (int i = 0; i < MAX_BONES; ++i)
            {
                bindingBones[i] = new GLBone();
                currentFrame[i] = new GLBone();
                nextFrame[i] = new GLBone();
            }
        }

        public void Create(List<Quaternion> boneOrientations, List<Vector3> bonePositions,
            List<float> boneScales, List<int> boneParents )
        {

            for (int i = 0; i < boneOrientations.Count; ++i)
            {
                GLBone bone = new GLBone();

                bone.parent = boneParents[i];

                bone.worldPosition = bonePositions[i];
                bone.worldOrientation = boneOrientations[i];

                Matrix4 transform = Matrix4.Rotate(bone.worldOrientation);
                transform *= Matrix4.CreateTranslation(bone.worldPosition);
                bone.worldTransform = transform;

                bindingBones[i] = bone;
            }
        }

        /// <summary>
        /// Calculate the skinning transforms for bones.  All position and orientation data is assumed to be relative to the parent.
        /// </summary>
        /// <param name="boneID">The bone ID the data belongs to.</param>
        /// <param name="orientation">Rotation of the bone.</param>
        /// <param name="position">Translation of the bone.</param>
        public void CalculateCurrentFramePose(int boneID, Quaternion orientation, Vector3 position)
        {
            GLBone poseBone = currentFrame[boneID];

            poseBone.parent = bindingBones[boneID].parent;

            // Is this a root bone?
            if (poseBone.parent == -1)
            {
                // No parent bone for root bones.
                // So, just calculate directly.
                poseBone.worldPosition = position;                
                poseBone.worldOrientation = orientation;
            }
            else
            {
                // Determine the parent bone.
                GLBone parentBone = currentFrame[poseBone.parent];

                // Append quaternions for rotation transform B * A
                poseBone.worldOrientation = parentBone.worldOrientation * orientation;
                poseBone.worldPosition = parentBone.worldPosition +
                    Vector3.Transform(position, parentBone.worldOrientation);
            }
        }

        /// <summary>
        /// Calculate the skinning transforms for bones.  All position and orientation data is assumed to be relative to the parent.
        /// </summary>
        /// <param name="boneID">The bone ID the data belongs to.</param>
        /// <param name="orientation">Rotation of the bone.</param>
        /// <param name="position">Translation of the bone.</param>
        public void CalculateNextFramePose(int boneID, Quaternion orientation, Vector3 position)
        {
            GLBone poseBone = nextFrame[boneID];

            poseBone.parent = bindingBones[boneID].parent;

            // Is this a root bone?
            if (poseBone.parent == -1)
            {
                // No parent bone for root bones.
                // So, just calculate directly.
                poseBone.worldPosition = position;
                poseBone.worldOrientation = orientation;
            }
            else
            {
                // Determine the parent bone.
                GLBone parentBone = nextFrame[poseBone.parent];

                // Append quaternions for rotation transform B * A
                poseBone.worldOrientation = parentBone.worldOrientation * orientation;
                poseBone.worldPosition = parentBone.worldPosition +
                    Vector3.Transform(position, parentBone.worldOrientation);
            }
        }

        /// <summary>
        /// Interpolates the animation data between the current frame
        /// and the next frame.
        /// </summary>
        /// <param name="blend">(Elasped frame time / max frame time)</param>
        /// <returns></returns>
        public Matrix4[] GetBoneTransformations(float blend)
        {
            Matrix4[] transforms = new Matrix4[MAX_BONES];

            for (int i = 0; i < MAX_BONES; ++i)
            {
                //
                // Interpolate between the current frame
                // and the next frame.
                //

                // Interpolate Orientations
                Quaternion finalOrientation = Quaternion.Slerp(currentFrame[i].worldOrientation,
                    nextFrame[i].worldOrientation, blend);

                // Interpolate Positions
                Vector3 finalPosition = Vector3.Lerp(currentFrame[i].worldPosition,
                    nextFrame[i].worldPosition, blend);

                // Store
                Matrix4 finalTransform = Matrix4.Rotate(finalOrientation);
                finalTransform.M41 = finalPosition.X;
                finalTransform.M42 = finalPosition.Y;
                finalTransform.M43 = finalPosition.Z;

                // Invert binding bone to compute the result.
                Matrix4 inverse = Matrix4.Invert(bindingBones[i].worldTransform);                
                transforms[i] = inverse * finalTransform;                               
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


