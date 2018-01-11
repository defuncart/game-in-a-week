/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>Abstract Custom Editor which Manager components who have keys can extend.</summary>
	public abstract class ManagerWithKeysEditor : Editor
	{
		/// <summary>The filepath to save $MANAGERNAME$Keys.cs.</summary>
		protected abstract string filepath { get; }
		/// <summary>An array of keys which should be saved in $MANAGERNAME$Keys.cs.</summary>
		protected abstract string[] keys { get; }

		/// <summary>Callback to draw the inspector.</summary>
		public override void OnInspectorGUI()
		{
			//draw the default inspector
			DrawDefaultInspector();
			//if in edit mode, draw a button construct a C# file containing the Manager's keys
			if(!Application.isPlaying) 
			{
				if(GUILayout.Button(new GUIContent(text: "Construct Keys Enum", 
					tooltip: "Constructs an enum of the various keys for the manager.")))
				{
					Export(keys, target.GetType().Name, filepath);
				}
			}
		}

		/// <summary>Exports an array of keys for the manager to a given filepath.</summary>
		/// <param name="keys">The array of keys.</param>
		/// <param name="managerName">The manager's name.</param>
		/// <param name="filepath">The filepath to export to.</param>
		protected void Export(string[] keys, string managerName, string filepath)
		{
			//create a cs file, overwritting if necessary
			using(TextWriter outfile = new StreamWriter(filepath, false))
			{
				outfile.WriteLine("/*\n *\tWritten by James Leahy. (c) 2018 DeFunc Art.\n *\thttps://github.com/defuncart/\n */");
				outfile.WriteLine("\n/// <summary>An enum of the various keys for " + managerName + ".</summary>");
				outfile.WriteLine("public enum " + managerName + "Keys\n{");
				for(int i=0; i < keys.Length; i++)
				{
					outfile.WriteLine("\t" + keys[i] + (i < keys.Length - 1 ? "," : ""));
				}
				outfile.WriteLine("}");
			}
			//update asset database
			AssetDatabase.Refresh();
		}
	}
}
#endif