


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
// Extracts model and texture information
// from the League of Legends directory
// structure.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

using LOLFileReader;

using RAFlibPlus;

using CSharpLogger;

namespace LOLViewer.IO
{
    public class LOLDirectoryReader
    {
        public const String DEFAULT_ROOT = "C:/Riot Games/League of Legends";
        public const String DEFAULT_MODEL_ROOT = "/DATA/Characters";
        public const String DEFAULT_RAF_DIRECTORY_ONE = "DATA";
        public const String DEFAULT_RAF_DIRECTORY_TWO = "Characters";

        public const String DEFAULT_EXTRACTED_TEXTURES_ROOT = "content/textures/";
        public String root;

        public Dictionary<String, RAFFileListEntry> skls;
        public Dictionary<String, RAFFileListEntry> skns;
        public Dictionary<String, RAFFileListEntry> textures;

        public List<RAFFileListEntry> inibins;
        public Dictionary<String, RAFFileListEntry> animationLists;
        public Dictionary<String, RAFFileListEntry> animations;

        public Dictionary<String, LOLModel> models;

        public LOLDirectoryReader()
        {
            root = DEFAULT_ROOT;

            inibins = new List<RAFFileListEntry>();

            animationLists = new Dictionary<String, RAFFileListEntry>();
            animations = new Dictionary<String, RAFFileListEntry>();
            
            skls = new Dictionary<String, RAFFileListEntry>();
            skns = new Dictionary<String, RAFFileListEntry>();
            textures = new Dictionary<String, RAFFileListEntry>();

            models = new Dictionary<String,LOLModel>();
        }

        /// <summary>
        /// Call this if LOL was installed in a non-default location.
        /// </summary>
        /// <param name="s">Full path to and including the "Riot Games" folder.</param>
        public void SetRoot(String s)
        {
            root = s;
        }

        public bool Read(Logger logger)
        {
            bool result = true;

            // Clear old data.

            models.Clear();

            skls.Clear();
            skns.Clear();
            textures.Clear();

            inibins.Clear();
            animationLists.Clear();
            animations.Clear();           

            DirectoryInfo rootDir = null;
            try
            {
                logger.Event("Reading models from: " + root);
                rootDir = new DirectoryInfo(root);
            }
            catch 
            {
                logger.Error("Unable to get the directory information: " + root);
                result = false;
            }

            //
            // Try to find the raf files and read them.
	        //

            if (result == true)
            {
                try
                {
                    result = GetRAFFiles(rootDir, logger);

                    // If the finding or reading fails, bail.
                    if (!result)
                    {
                        logger.Error("Unable to find the 'filearchives' directory: " + root);
                    }
                }
                catch (Exception e)
                {
                    logger.Error("Unable to open directory: " + root);
                    logger.Error(e.Message);
                    result = false;
                }
            }

            // Sanity
            if (result == true)
            {
                GenerateModelDefinitions(logger);
            }

            return result;
        }

        public void SortModelNames()
        {
            IEnumerable<KeyValuePair<String, LOLModel>> alphabetical = models.OrderBy(model => model.Key);

            Dictionary<String, LOLModel> temp = new Dictionary<String, LOLModel>();
            foreach (var m in alphabetical)
            {
                temp.Add(m.Key, m.Value);
            }

            models.Clear();
            models = temp;
        }

        public List<String> GetModelNames()
        {
            List<String> names = new List<String>();

            foreach (var model in models)
            {
                names.Add(model.Key);
            }

            return names;
        }

        public LOLModel GetModel(String name)
        {
            LOLModel result = null;

            if (models.ContainsKey(name) == true)
            {
                result = models[name];
            }

            return result;
        }

        #region Helper Functions

        // Single recursive function to replace the double
        // It still assumes a certain LoL directory, but it has a little more flexibility than a straight path
        // It should be only a bit slower than a stright path since it's a selective recurse, not a complete
        private bool GetRAFFiles(DirectoryInfo dir, Logger logger)
        {
            if (dir.Name.ToLower() == "league of legends")
            {
                foreach (DirectoryInfo dInfo in dir.GetDirectories())
                {
                    // If we have found the filearchives directory, just pass true all the way up the stack
                    if (GetRAFFiles(dInfo, logger))
                        return true;
                }
            }
            else if (dir.Name.ToLower() == "rads")
            {
                foreach (DirectoryInfo dInfo in dir.GetDirectories())
                {
                    if (GetRAFFiles(dInfo, logger))
                        return true;
                }
            }
            else if (dir.Name.ToLower() == "projects")
            {
                foreach (DirectoryInfo dInfo in dir.GetDirectories())
                {
                    if (GetRAFFiles(dInfo, logger))
                        return true;
                }
            }
            else if (dir.Name.ToLower() == "lol_game_client")
            {
                foreach (DirectoryInfo dInfo in dir.GetDirectories())
                {
                    if (GetRAFFiles(dInfo, logger))
                        return true;
                }
            }
            else if (dir.Name.ToLower() == "filearchives")
            {
                return ReadRAFs(dir, logger);
            }
            
            // Directory we don't care about.
            return false;
        }

        // Replacement for individual raf reading and individual filetype searching
        // Provide the directory of RADS\projects\lol_game_client\filearchives or the equivalent
        private bool ReadRAFs(DirectoryInfo dir, Logger logger)
        {
            try
            {
                RAFMasterFileList rafFiles = new RAFMasterFileList(dir.FullName);
                logger.Event("Opening the 'filearchives' directory: " + dir.FullName);
                foreach (RAFMasterFileList.RAFSearchResult result in rafFiles.SearchFileEntries(new string[] { ".dds", ".skn", ".skl", ".inibin", "animations.list", ".anm" }, RAFMasterFileList.RAFSearchType.All))
                {
                    RAFFileListEntry e = result.value;

                    // Split off the actual file name from the full path
                    String name = e.FileName.Substring(e.FileName.LastIndexOf('/') + 1).ToLower();

                    switch (result.searchPhrase)
                    {
                        case ".dds":
                            // Try to parse out unwanted textures.
                            if (!e.FileName.ToLower().Contains("loadscreen") &&
                                !e.FileName.ToLower().Contains("circle") &&
                                !e.FileName.ToLower().Contains("square") &&
                                e.FileName.ToLower().Contains("data") &&
                                e.FileName.ToLower().Contains("characters"))
                            {
                                // Check that the file isn't already in the dictionary
                                if (!textures.ContainsKey(name))
                                {
                                    textures.Add(name, e);
                                }
                                else
                                {
                                    logger.Warning("Duplicate texture " + name + ": " + e.FileName);
                                }
                            }
                            break;

                        case ".skn":
                            if (!skns.ContainsKey(name))
                            {
                                skns.Add(name, e);
                            }
                            else
                            {
                                logger.Warning("Duplicate skn " + name + ": " + e.FileName);
                            }
                            break;

                        case ".skl":
                            if (!skls.ContainsKey(name))
                            {
                                skls.Add(name, e);
                            }
                            else
                            {
                                logger.Warning("Duplicate skn " + name + ": " + e.FileName);
                            }
                            break;

                        case ".inibin":
                            // Try to only read champion inibins
                            if (e.FileName.ToLower().Contains("data") &&
                                e.FileName.ToLower().Contains("characters"))
                            {
                                inibins.Add(e);
                            }
                            else
                            {
                                logger.Warning("Excluding inibin " + name + ": " + e.FileName);
                            }
                            break;

                        case "animations.list":
                            // Remove the file name.
                            name = e.FileName.Remove(e.FileName.LastIndexOf('/'));

                            // Remove proceeding directories to get the parent directory
                            name = name.Substring(name.LastIndexOf('/') + 1).ToLower();

                            // Name is the parent directory.
                            if (!animationLists.ContainsKey(name))
                            {
                                animationLists.Add(name, e);
                            }
                            else
                            {
                                logger.Warning("Duplicate animation list " + name + ": " + e.FileName);
                            }
                            break;

                        case ".anm":
                            // Remove the .anm extension.
                            name = name.Remove(name.Length - 4);

                            if (!animations.ContainsKey(name))
                            {
                                animations.Add(name, e);
                            }
                            else
                            {
                                logger.Warning("Duplicate anm " + name + ": " + e.FileName);
                            }
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                // Something went wrong. Most likely the RAF read failed due to a bad directory.
                logger.Error("Failed to open RAFs");
                logger.Error(e.Message);
                return false;
            }

            return true;
        }

        private void GenerateModelDefinitions(Logger logger)
        {
            foreach (RAFFileListEntry f in inibins)
            {
                InibinFile iniFile = new InibinFile();
                bool readResult = InibinReader.Read(f, ref iniFile, logger);

                if (readResult == true)
                {
                    // Add the models from this .inibin file
                    List<ModelDefinition> modelDefs = iniFile.GetModelStrings();
                    for (int j = 0; j < modelDefs.Count; ++j)
                    {
                        // Name the model after the parent directory
                        // of the .inibin plus the name from the .inibin.
                        // Some things overlap without both.
                        String name = modelDefs[j].name;

                        String directoryName = f.FileName;
                        int pos = directoryName.LastIndexOf("/");
                        directoryName = directoryName.Remove(pos);
                        pos = directoryName.LastIndexOf("/");
                        directoryName = directoryName.Substring(pos + 1);

                        // Sometimes the name from the .inibin file is "".
                        // So, just name it after the directory
                        if (name == "")
                        {
                            name = directoryName + "/" + directoryName;
                        }
                        else
                        {
                            name = directoryName + "/" + name;
                        }

                        try
                        {
                            LOLModel model;
                            bool storeResult = StoreModel(modelDefs[j], out model, logger);

                            if (storeResult == true)
                            {
                                // Try to store animations for model as well
                                storeResult = StoreAnimations(ref model, logger);
                            }

                            if (storeResult == true)
                            {
                                if (models.ContainsKey(name) == false)
                                {
                                    logger.Event("Adding model definition: " + name);
                                    models.Add(name, model);
                                }
                                else
                                {
                                    logger.Warning("Duplicate model definition: " + name);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            logger.Error("Unable to store model definition: " + name);
                            logger.Error(e.Message);
                        }
                    }
                }
            }
        }

        private bool StoreModel(ModelDefinition def, out LOLModel model, Logger logger)
        {
            model = new LOLModel();
            model.skinNumber = def.skin;
            model.animationList = def.anmListKey.ToLower();

            // Find the skn.
            if (skns.ContainsKey(def.skn))
            {
                model.skn = skns[def.skn];
            }
            else
            {
                logger.Error("Unable to find skn file: " + def.skn);
                return false;
            }

            // Find the skl.
            if (skls.ContainsKey(def.skl))
            {
                model.skl = skls[def.skl];
            }
            else
            {
                logger.Error("Unable to find skl file: " + def.skl);
                return false;
            }

            // Find the texture.
            if (textures.ContainsKey(def.tex))
            {
                model.texture = textures[def.tex];
            }
            else
            {
                logger.Error("Unable to find texture file: " + def.tex);
                return false;
            }

            return true;
        }

        private bool StoreAnimations(ref LOLModel model, Logger logger)
        {
            bool result = true;

            Dictionary<String, String> animationStrings =
                new Dictionary<String, String>();

            // Sanity
            if (animationLists.ContainsKey(model.animationList) == true)
            {
                result = ANMListReader.Read(model.skinNumber - 1, // indexing in animations.list assumes the original skin to be -1
                    animationLists[model.animationList], ref animationStrings, logger);
            }
            else
            {
                logger.Error("Unable to find animation list: " + model.animationList);
            }

            if (result == true)
            {
                // Store the animations in the model.
                foreach (var a in animationStrings)
                {
                    if (animations.ContainsKey(a.Value) == true)
                    {
                        if (model.animations.ContainsKey(a.Key) == false)
                        {
                            model.animations.Add(a.Key, animations[a.Value]);
                        }
                        else
                        {
                            logger.Error("Duplicate animation: " + a.Key);
                        }
                    }
                    else
                    {
                        logger.Error("Unable to find animation: " + a.Value);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}


