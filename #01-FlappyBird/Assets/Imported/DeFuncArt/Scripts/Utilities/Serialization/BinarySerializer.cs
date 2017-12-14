﻿/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>Part of the DeFuncArt.Serialization namespace.</summary>
namespace DeFuncArt.Serialization
{
	/// <summary>A Binary Serializer which serializes and deserializes classes into binary format.</summary>
	public class BinarySerializer
	{
		/// <summary>Load an instance of the class T from file.</summary>
		/// <param name="filename">Filename of the file to load.</param>
		/// <typeparam name="T">The object type to be loaded.</typeparam>
		/// <returns>A loaded instance of the class T.</returns>
		public static T Load<T>(string filename) where T: class
		{
			string path = PathForFilename(filename);
			if(BinarySerializer.PathExists(path))
			{
				try
				{
					using(Stream stream = File.OpenRead(path))
					{
						BinaryFormatter formatter = new BinaryFormatter();
						return formatter.Deserialize(stream) as T;
					}
				}
				catch(Exception e) { Debug.LogWarning(e.Message); }
			}
			return default(T);
		}

		/// <summary>Save an instance of the class T to file.</summary>
		/// <param name="filename">Filename of file to save.</param>
		/// <param name="data">The class object to save.</param>
		/// <typeparam name="T">The object type to be saved.</typeparam>
		public static void Save<T>(string filename, T data) where T: class
		{
			string path = PathForFilename(filename);
			using(Stream stream = File.OpenWrite(path))
			{    
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, data);
			}
		}

		/// <summary>Determine whether a file exists at a given filepath.</summary>
		/// <param name="filepath">Filepath of the file.</param>
		/// <returns>True if the file exists, otherwise file.</returns>
		private static bool PathExists(string filepath)
		{
			return File.Exists(filepath);
		}

		/// <summary>Determine if a File with a given filename exists.</summary>
		/// <param name="filename">Filename of the file.</param>
		/// <returns>Bool if the file exists.</returns>
		public static bool FileExists(string filename)
		{
			return PathExists(PathForFilename(filename));
		}

		/// <summary>Delete a File with a given filename.</summary>
		/// <param name="filename">Filename of the file.</param>
		public static void DeleteFile(string filename)
		{
			string filepath = PathForFilename(filename);
			if(PathExists(filepath))
			{
				File.Delete(filepath);
			}
		}

		/// <summary>Determine the correct filepath for a filename. In UNITY_EDITOR this is in the project's root
		/// folder, on mobile it is in the persistent data folder while standalone is the data folder.</summary>
		/// <param name="filename">Filename of the file.</param>
		/// <returns>The filepath for a given file on the current device.</returns>
		private static string PathForFilename(string filename)
		{
			string path = filename; //for editor
			#if UNITY_STANDALONE
			path = Path.Combine(Application.dataPath, filename);
			#elif UNITY_IOS || UNITY_ANDROID
			path = Path.Combine(Application.persistentDataPath, filename);
			#endif
			return path;
		}
	}
}
