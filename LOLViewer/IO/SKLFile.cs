

/*
LOLViewer
Copyright 2011-2012 James Lammlein 

 

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
// Stores the contents of an .skl file.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

using LOLFileReader;

namespace LOLViewer
{
    class SKLFile
    {
        // Not sure what the first eight bytes represent
        public int              magicOne;
        public int              magicTwo;

        public uint             version;
        public uint             designerID;

        public uint             numBones;
        public List<SKLBone>    bones;

        public uint             numBoneIDs;
        public List<uint>       boneIDs;

        public SKLFile()
        {
            magicOne = magicTwo = 0;

            version = designerID = numBones = 0;
            bones = new List<SKLBone>();
            
            numBoneIDs = 0;
            boneIDs = new List<uint>();
        }

        /// <summary>
        /// Loads data from SKN and SKL files into
        /// an OpenGL rigged model.
        /// </summary>
        /// <param name="model">Where to store the data.</param>
        /// <param name="skn">The .skn data.</param>
        /// <param name="usingDDSTexture">The V coordinate in OpenGL
        /// is different from directX.  If you're using textures on
        /// this model which were intended to be used with directX, you 
        /// need to invert the V coordinate.</param>
        /// <returns></returns>
        public bool ToGLRiggedModel(ref GLRiggedModel model, SKNFile skn, bool usingDDSTexture, EventLogger logger)
        {
            bool result = true;

            // Vertex Data
            List<float> vData = new List<float>();
            List<float> nData = new List<float>();
            List<float> tData = new List<float>();
            List<float> bData = new List<float>();
            List<float> wData = new List<float>();

            // Other data.
            List<OpenTK.Quaternion> boData = new List<OpenTK.Quaternion>();
            List<OpenTK.Vector3> bpData = new List<OpenTK.Vector3>();
            List<String> bnData = new List<String>();
            List<float> bsData = new List<float>();
            List<int> bParentData = new List<int>();

            for (int i = 0; i < skn.numVertices; ++i)
            {
                // Position Information
                vData.Add(skn.vertices[i].position[0]);
                vData.Add(skn.vertices[i].position[1]);
                vData.Add(skn.vertices[i].position[2]);

                // Normal Information
                nData.Add(skn.vertices[i].normal[0]);
                nData.Add(skn.vertices[i].normal[1]);
                nData.Add(skn.vertices[i].normal[2]);

                // Tex Coords Information
                tData.Add(skn.vertices[i].texCoords[0]);
                    
                // DDS Texture.
                tData.Add(1.0f - skn.vertices[i].texCoords[1]);

                // Bone Index Information
                for (int j = 0; j < SKNVertex.BONE_INDEX_SIZE; ++j)
                {
                    bData.Add(skn.vertices[i].boneIndex[j]);
                }

                // Bone Weight Information
                wData.Add(skn.vertices[i].weights[0]);
                wData.Add(skn.vertices[i].weights[1]);
                wData.Add(skn.vertices[i].weights[2]);
                wData.Add(skn.vertices[i].weights[3]);
            }

            // Other data
            for (int i = 0; i < numBones; ++i)
            {
                Quaternion orientation = Quaternion.Identity;
                if (version == 0)
                {
                    // Version 0 SKLs contain a quaternion.
                    orientation.X = bones[i].orientation[0];
                    orientation.Y = bones[i].orientation[1];
                    orientation.Z = bones[i].orientation[2];
                    orientation.W = bones[i].orientation[3];
                }
                else
                {
                    // Other SKLs contain a rotation matrix.

                    // Create a matrix from the orientation values.
                    Matrix4 transform = Matrix4.Identity;

                    transform.M11 = bones[i].orientation[0];
                    transform.M21 = bones[i].orientation[1];
                    transform.M31 = bones[i].orientation[2];

                    transform.M12 = bones[i].orientation[4];
                    transform.M22 = bones[i].orientation[5];
                    transform.M32 = bones[i].orientation[6];

                    transform.M13 = bones[i].orientation[8];
                    transform.M23 = bones[i].orientation[9];
                    transform.M33 = bones[i].orientation[10];

                    // Convert the matrix to a quaternion.
                    orientation = OpenTKExtras.Matrix4.CreateQuatFromMatrix(transform);
                }
 
                boData.Add(orientation);

                // Create a vector from the position values.
                Vector3 position = Vector3.Zero;
                position.X = bones[i].position[0];
                position.Y = bones[i].position[1];
                position.Z = bones[i].position[2];
                bpData.Add(position);
                                
                bnData.Add(bones[i].name);
                bsData.Add(bones[i].scale);
                bParentData.Add(bones[i].parentID);
            }

            //
            // Version 0 SKL files are similar to the animation files.
            // The bone positions and orientations are relative to their parent.
            // So, we need to compute their absolute location by hand.
            //
            if (version == 0)
            {
                //
                // This algorithm is a little confusing since it's indexing identical data from
                // the SKL file and the local variable List<>s. The indexing scheme works because
                // the List<>s are created in the same order as the data in the SKL files.
                //
                for (int i = 0; i < numBones; ++i)
                {
                    // Only update non root bones.
                    if (bones[i].parentID != -1)
                    {
                        // Determine the parent bone.
                        int parentBoneID = bones[i].parentID;

                        // Update orientation.
                        // Append quaternions for rotation transform B * A.
                        boData[i] = boData[parentBoneID] * boData[i];

                        Vector3 localPosition = Vector3.Zero;
                        localPosition.X = bones[i].position[0];
                        localPosition.Y = bones[i].position[1];
                        localPosition.Z = bones[i].position[2];

                        // Update position.
                        bpData[i] = bpData[parentBoneID] + Vector3.Transform(localPosition, boData[parentBoneID]);
                    }
                }
            } 

            // Index Information
            List<uint> iData = new List<uint>();
            for (int i = 0; i < skn.numIndices; ++i)
            {
                iData.Add((uint)skn.indices[i]);
            }

            // Create
            if (version == 1)
            {
                result = model.Create((int)version, vData, nData, tData,
                    bData, wData, iData, boData, bpData,
                    bsData, bnData, bParentData, logger);
            }
            else
            {
                result = model.Create((int)version, vData, nData, tData,
                    bData, wData, iData, boData, bpData,
                    bsData, bnData, bParentData, boneIDs, logger);
            }

            return result;
        }
    }
}
