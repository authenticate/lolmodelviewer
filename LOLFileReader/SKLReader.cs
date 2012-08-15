

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
// Abrstraction to read .skl files.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using RAFlibPlus;

namespace LOLFileReader
{
    public class SKLReader
    {
        public static bool Read(RAFFileListEntry file, ref SKLFile data, EventLogger logger)
        {
            bool result = true;

            logger.LogEvent("Reading skl: " + file.FileName);

            try
            {
                // Get the data from the archive
                MemoryStream myInput = new MemoryStream( file.GetContent() );
                result = ReadBinary(myInput, ref data, logger);
                myInput.Close();
            }
            catch(Exception e)
            {
                logger.LogError("Unable to open memory stream: " + file.FileName);
                logger.LogError(e.Message);
                result = false;
            }

            return result;
        }

        //
        // Helper Functions. 
        // (Because nested Try/Catch looks nasty in one function block.)
        //

        private static bool ReadBinary(MemoryStream input, ref SKLFile data, EventLogger logger)
        {
            bool result = true;

            try
            {
                BinaryReader myFile = new BinaryReader(input);
                result = ReadData(myFile, ref data, logger);
                myFile.Close();
            }
            catch(Exception e)
            {
                logger.LogError("Unable to open binary reader.");
                logger.LogError(e.Message);
                result = false;
            }

            return result;
        }

        private static bool ReadData(BinaryReader file, ref SKLFile data, EventLogger logger)
        {
            bool result = true;

            try
            {
                // File Header Information.
                data.magicOne = file.ReadInt32();
                data.magicTwo = file.ReadInt32();

                data.version = file.ReadUInt32();

                if (data.version == 1 || data.version == 2)
                {
                    data.designerID = file.ReadUInt32();

                    // Read in the bones.
                    data.numBones = file.ReadUInt32();
                    for (int i = 0; i < data.numBones; ++i)
                    {
                        SKLBone bone = new SKLBone();

                        bone.name = new String(
                            file.ReadChars(SKLBone.BONE_NAME_SIZE));
                        bone.name = RemoveBoneNamePadding(bone.name);
                        bone.name = bone.name.ToLower();

                        bone.ID = i;
                        bone.parentID = file.ReadInt32();
                        bone.scale = file.ReadSingle();

                        // Read in transform matrix.
                        float[] matrix = new float[SKLBone.ORIENTATION_SIZE];
                        for (int j = 0; j < SKLBone.ORIENTATION_SIZE; ++j)
                        {
                            bone.orientation[j] = file.ReadSingle();
                        }

                        // Position from matrix.
                        bone.position[0] = matrix[3];
                        bone.position[1] = matrix[7];
                        bone.position[2] = matrix[11];

                        data.bones.Add(bone);
                    }

                    // Version two contains bone IDs.
                    if (data.version == 2)
                    {
                        data.numBoneIDs = file.ReadUInt32();
                        for (uint i = 0; i < data.numBoneIDs; ++i)
                        {
                            data.boneIDs.Add(file.ReadUInt32());
                        }
                    }
                }
                // Newest version so far.
                else if (data.version == 0)
                {
                    // Header
                    Int16 zero = file.ReadInt16(); // ?

                    data.numBones = (uint) file.ReadInt16();

                    data.numBoneIDs = file.ReadUInt32();
                    Int16 offsetToVertexData = file.ReadInt16(); // Should be 64.

                    int unknown = file.ReadInt16(); // ?

                    int offset1 = file.ReadInt32();
                    int offsetToAnimationIndices = file.ReadInt32();
                    int offset2 = file.ReadInt32();
                    int offset3 = file.ReadInt32();
                    int offsetToStrings = file.ReadInt32();

                    // Not sure what this data represents.
                    // I think it's padding incase more header data is required later.
                    file.BaseStream.Position += 20;

                    file.BaseStream.Position = offsetToVertexData;
                    for (int i = 0; i < data.numBones; ++i)
                    {
                        SKLBone bone = new SKLBone();
                        // The old scale was always 0.1.
                        // For now, just go with it.
                        bone.scale = 0.1f;

                        zero            = file.ReadInt16(); // ?
                        bone.ID         = file.ReadInt16();
                        bone.parentID   = file.ReadInt16();
                        unknown         = file.ReadInt16(); // ?

                        int namehash = file.ReadInt32();

                        float twoPointOne = file.ReadSingle(); // ?

                        bone.position[0] = file.ReadSingle(); // x
                        bone.position[1] = file.ReadSingle(); // y
                        bone.position[2] = file.ReadSingle(); // z

                        // Store in orientation matrix.
                        bone.orientation[3] = bone.position[0];
                        bone.orientation[7] = bone.position[1];
                        bone.orientation[11] = bone.position[2];

                        float one = file.ReadSingle(); // ? Maybe scales for X, Y, and Z
                        one = file.ReadSingle();
                        one = file.ReadSingle();

                        float[] quaternion = new float[4];
                        quaternion[0] = file.ReadSingle();
                        quaternion[1] = file.ReadSingle();
                        quaternion[2] = file.ReadSingle();
                        quaternion[3] = file.ReadSingle();

                        // Convert quaternion to rotation matrix.
                        QuaternionToMatrix(ref bone.orientation, quaternion);

                        float ctx = file.ReadSingle(); // ctx
                        float cty = file.ReadSingle(); // cty
                        float ctz = file.ReadSingle(); // ctz

                        data.bones.Add(bone);

                        // The rest of the bone data is unknown. Maybe padding?
                        file.BaseStream.Position += 32;
                    }

                    file.BaseStream.Position = offset1;
                    for (int i = 0; i < data.numBones; ++i) // ?
                    {
                        // 8 bytes
                        int valueOne = file.ReadInt32();
                        int valueTwo = file.ReadInt32();
                    }

                    file.BaseStream.Position = offsetToAnimationIndices;
                    for (int i = 0; i < data.numBoneIDs; ++i) // Inds for animation
                    {
                        // 2 bytes
                        UInt16 boneID = file.ReadUInt16();
                        data.boneIDs.Add(boneID);
                    }

                    file.BaseStream.Position = offsetToStrings;
                    for (int i = 0; i < data.numBones; ++i)
                    {
                        // bone names
                        string name = ""; 
                        while( name.Contains( '\0' ) == false )
                        {
                            name += new string(file.ReadChars(4));
                        }
                        name = RemoveBoneNamePadding(name);
                        name = name.ToLower();
                        
                        data.bones[i].name = name;
                    }
                }
                // Unknown Version
                else
                {
                    logger.LogError("Unknown skl version: " + data.version);
                    result = false;
                }
            }
            catch(Exception e)
            {
                logger.LogError("Skl reading error.");
                logger.LogError(e.Message);
                result = false;
            }

            logger.LogEvent("Magic One: " + data.magicOne);
            logger.LogEvent("Magic Two: " + data.magicTwo);
            logger.LogEvent("Version: " + data.version);
            logger.LogEvent("Designer ID: " + data.designerID);
            logger.LogEvent("Number of Bones: " + data.numBones);
            logger.LogEvent("Number of Bone IDs: " + data.numBoneIDs);

            return result;
        }

        //
        // Helper Functions
        //

        private static String RemoveBoneNamePadding(String s)
        {
            int position = s.IndexOf('\0');
            if (position >= 0)
            {
                s = s.Remove(position);
            }

            return s;
        }

        //
        // Added this function when the reference to OpenTK was removed from the IO classes.
        //

        /// <summary>
        /// Converts a quaternion to a matrix.
        /// </summary>
        /// <param name="result">The resultant matrix.  Expects a float[16].</param>
        /// <param name="quaternion">The source quaternion.  Expects a float[4].</param>
        private static void QuaternionToMatrix(ref float[] result, float[] quaternion)
        {
			float X = quaternion[0];
			float Y = quaternion[1];
			float Z = quaternion[2];
			float W = quaternion[3];
			
			float xx = X * X;
			float xy = X * Y;
			float xz = X * Z;
			float xw = X * W;
			float yy = Y * Y;
			float yz = Y * Z;
			float yw = Y * W;
			float zz = Z * Z;
			float zw = Z * W;
            
            result[0] = 1 - 2 * (yy + zz);
            result[1] = 2 * (xy - zw);
            result[2] = 2 * (xz + yw);

            result[4] = 2 * (xy + zw);
            result[5] = 1 - 2 * (xx + zz);
            result[6] = 2 * (yz - xw);

            result[8] = 2 * (xz - yw);
            result[9] = 2 * (yz + xw);
            result[10] = 1 - 2 * (xx + yy);
        }
    }
}
