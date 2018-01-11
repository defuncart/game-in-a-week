﻿/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Serialization;
using UnityEngine;

/// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
	/// <summary>A base abstract class which can be extented to make a serializable, singleton class.
	/// The class is saved to/loaded from disk using the class's name.</summary>
	[System.Serializable]
	public abstract class SerializableSingleton<T> where T : class
	{
		/// <summary>The class's name.</summary>
		protected static string className
		{
			get { return typeof(T).Name; }
		}

		/// <summary>A backing variable for instance.</summary>
		protected static T _instance;
		/// <summary>A computed property that returns a static instance of the class.
		/// If the instance hasn't already been loaded, then it is loaded from File.</summary>
		public static T instance
		{
			get { return _instance ?? (_instance = BinarySerializer.Load<T>(className)); }
		}

		/// <summary>As the object's constructor is private, this method allows the creation of 
		/// the object. Only creates the object if one isn't already saved to disk.</summary>
		public static void Create()
		{
			if(!BinarySerializer.FileExists(className))
			{
				_instance = (T)System.Activator.CreateInstance(type: typeof(T), nonPublic: true);
			}
		}

		/// <summary>Saves the current instance to file.</summary>
		protected void Save()
		{
			BinarySerializer.Save(className, this);
		}
	}
}
