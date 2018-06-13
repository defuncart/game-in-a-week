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
	public abstract class CustomKeysEditor : Editor
	{
		/// <summary>The asset's name.</summary>
		protected abstract string assetName { get; }
		/// <summary>The filepath to save $ASSETNAME$Keys.cs.</summary>
		protected abstract string filepath { get; }
		/// <summary>An array of keys which should be saved in $ASSETNAME$Keys.cs.</summary>
		protected abstract string[] keys { get; }

		/// <summary>Draws the export button. Should be used within OnInspectorGUI.</summary>
		protected void DrawExportButton()
		{
			//if in edit mode, draw a button construct a C# file containing the Manager's keys
			if(!Application.isPlaying) 
			{
				if(GUILayout.Button(new GUIContent(text: "Construct Keys Enum", 
					tooltip: "Constructs an enum of the various keys for the manager.")))
				{
					Export(keys, filepath);
				}
			}
		}

		/// <summary>Exports an array of keys for the manager to a given filepath.</summary>
		/// <param name="keys">The array of keys.</param>
		/// <param name="filepath">The filepath to export to.</param>
		protected void Export(string[] keys, string filepath)
		{
			//create a cs file, overwritting if necessary
			using(TextWriter outfile = new StreamWriter(filepath, false))
			{
				outfile.WriteLine("/*\n *\tWritten by James Leahy. (c) 2018 DeFunc Art.\n *\thttps://github.com/defuncart/\n */");
				outfile.WriteLine("\n/// <summary>An enum of the various keys for " + assetName + ".</summary>");
				outfile.WriteLine("public enum " + assetName + "Keys\n{");
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