

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
        private int currentFrame;

        private int numberOfBones;
        private int maximumNumberOfBones;

        private GLBone[] bindingBones;        
        private GLBone[] currentFrames;
        private GLBone[] nextFrames;

        public GLRig(int maximumNumberOfBones)
        {
            currentFrame = -1;
            numberOfBones = 0;
            this.maximumNumberOfBones = maximumNumberOfBones;

            bindingBones = new GLBone[maximumNumberOfBones];
            currentFrames = new GLBone[maximumNumberOfBones];
            nextFrames = new GLBone[maximumNumberOfBones];

            for (int i = 0; i < maximumNumberOfBones; ++i)
            {
                bindingBones[i] = new GLBone();
                currentFrames[i] = new GLBone();
                nextFrames[i] = new GLBone();
            }
        }

        public void Create(List<Quaternion> boneOrientations, List<Vector3> bonePositions,
            List<float> boneScales, List<int> boneParents )
        {
            for (int i = 0; i < boneOrientations.Count; ++i)
            {
                GLBone bone = new GLBone();

                bone.parent = boneParents[i];

                bone.position = bonePositions[i];
                bone.orientation = boneOrientations[i];

                Matrix4 transform = Matrix4.Rotate(bone.orientation);
                transform *= Matrix4.CreateTranslation(bone.position);

                // Invert the binding bone's transform here instead of every frame.
                transform = Matrix4.Invert(transform);
                
                bone.transform = transform;

                bindingBones[i] = bone;
            }
        }

        public void Reset()
        {
            currentFrame = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameID">The current frame's ID.</param>
        /// <param name="maxFrames">The number of frames in the current animation.</param>
        /// <param name="bones"></param>
        /// <param name="boneNameToID"></param>
        public void UpdateBoneTransformations(int frameID, int maxFrames, List<GLBone> bones, 
            Dictionary<String, int> boneNameToID)
        {
            // Only update if the frame changed.
            if (frameID != currentFrame)
            {
                // Update the number of bones.
                numberOfBones = bones.Count;

                int nextFrameID = (currentFrame + 1) % maxFrames;
                if (frameID == nextFrameID && currentFrame != -1)
                {
                    //
                    // Normal Case. Increment to the next frame.
                    //

                    // We can cache the "next" transformation as the "current" transformations.
                    GLBone[] temp = currentFrames;
                    currentFrames = nextFrames;
                    nextFrames = temp;

                    //
                    // For the next frame.
                    //

                    foreach (GLBone bone in bones)
                    {
                        if (boneNameToID.ContainsKey(bone.name))
                        {
                            int boneID = boneNameToID[bone.name];
                            GLBone bindingBone = bindingBones[boneID];

                            GLFrame frame = bone.frames[nextFrameID];
                            GLBone poseBone = nextFrames[boneID];
                            poseBone.parent = bindingBone.parent;

                            GLBone parentBone = null;
                            if (poseBone.parent >= 0)
                            {
                                parentBone = nextFrames[poseBone.parent];
                            }
                            poseBone = CalculateAbsoluteTransform(ref poseBone, bindingBone, parentBone, frame);
                        }
                        //else

                        // Not sure what to do if it doesn't contain the bone.
                        // Last time I checked, this does happen more frequently that I'd like to ignore.
                        // It's probably why certain models don't animate properly.
                    }
                }
                else
                {
                    //
                    // Special Case. We can not cache the transformations.
                    //

                    foreach (GLBone bone in bones)
                    {
                        if (boneNameToID.ContainsKey(bone.name))
                        {
                            int boneID = boneNameToID[bone.name];
                            GLBone bindingBone = bindingBones[boneID];

                            //
                            // For the current frame.
                            //

                            GLFrame frame = bone.frames[frameID];
                            GLBone poseBone = currentFrames[boneID];
                            poseBone.parent = bindingBone.parent;
                            
                            GLBone parentBone = null;
                            if (poseBone.parent >= 0)
                            {
                                parentBone = currentFrames[poseBone.parent];
                            }
                            poseBone = CalculateAbsoluteTransform(ref poseBone, bindingBone, parentBone, frame);

                            //
                            // For the next frame.
                            //

                            frame = bone.frames[nextFrameID];
                            poseBone = nextFrames[boneID];
                            poseBone.parent = bindingBone.parent;

                            parentBone = null;
                            if (poseBone.parent >= 0)
                            {
                                parentBone = nextFrames[poseBone.parent];
                            }
                            poseBone = CalculateAbsoluteTransform(ref poseBone, bindingBone, parentBone, frame);
                        }
                        //else

                        // Not sure what to do if it doesn't contain the bone.
                        // Last time I checked, this does happen more frequently that I'd like to ignore.
                        // It's probably why certain models don't animate properly.
                    }
                }

                // Update the frame ID.
                currentFrame = frameID;
            }            
        }
        
        /// <summary>
        /// Interpolates the animation data between the current frame
        /// and the next frame.
        /// </summary>
        /// <param name="blend">Elasped frame time / max frame time)</param>
        /// <param name="transforms">A preallocated array of transformation matrices.</param>
        /// <returns>The resultant bone transformation.  This value is identical to the reference parameter result.</returns>
        public Matrix4[] GetBoneTransformations(float blend, ref Matrix4[] transforms)
        {
            // Only update a transform for which we have a bone.
            for (int i = 0; i < numberOfBones; ++i)
            {
                //
                // Interpolate between the current frame
                // and the next frame.
                //

                // Interpolate Orientations
                Quaternion finalOrientation = Quaternion.Slerp(currentFrames[i].orientation,
                    nextFrames[i].orientation, blend);

                // Interpolate Positions
                Vector3 finalPosition = Vector3.Lerp(currentFrames[i].position,
                    nextFrames[i].position, blend);

                // Store
                Matrix4 finalTransform = Matrix4.Rotate(finalOrientation);
                finalTransform.M41 = finalPosition.X;
                finalTransform.M42 = finalPosition.Y;
                finalTransform.M43 = finalPosition.Z;

                // Compute the result.
                transforms[i] = bindingBones[i].transform * finalTransform;                               
            }
            
            return transforms;
        }

        //
        // Helper Functions.
        //

        /// <summary>
        /// Calculate the transform in absolute coordinates for the pose bone.
        /// </summary>
        /// <param name="poseBone">The preallocated pose bone.</param>
        /// <param name="bindingBone">The binding bone in relative coordinates.</param>
        /// <param name="parentBone">The parent bone in relative coordinates.</param>
        /// <param name="frame">The animation frame in relative coordinates.</param>
        /// <returns>The result in absolute coordinates.</returns>
        private GLBone CalculateAbsoluteTransform(ref GLBone poseBone, GLBone bindingBone, GLBone parentBone, GLFrame frame)
        {
            // Normal Case.
            if (parentBone != null)
            {
                // Append quaternions for rotation transform B * A
                poseBone.orientation = parentBone.orientation * frame.orientation;
                poseBone.position = parentBone.position + Vector3.Transform(frame.position, parentBone.orientation);
            }
            // Root bone case.
            else
            {
                poseBone.position = frame.position;
                poseBone.orientation = frame.orientation;
            }

            return poseBone;
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


