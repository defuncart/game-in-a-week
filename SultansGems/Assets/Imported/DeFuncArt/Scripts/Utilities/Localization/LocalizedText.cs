/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
#if UNITY_EDITOR
using System.Collections;
#endif
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
	/// <summary>A simple script which localizes a Text component.</summary>
	public class LocalizedText : MonoBehaviour
	{
		/// <summary>An optional key used to query the localization database.</summary>
        [Tooltip("An optional key used to query the localization database.")]
        [SerializeField] private string key;
		/// <summary>The object's Text component.</summary>
		private Text text;

		/// <summary>Callback when the instance is started.</summary>
		private void Start()
		{
			if(key == "") { key = this.name; } //if no key is supplied, use object's name
            Assert.IsFalse(key.IsNullOrEmpty(), string.Format("Expected a non-null or non-empty key on Object {0}", name));

			text = GetComponent<Text>();
            Assert.IsNotNull(text, string.Format("LocalizedText: Expected a Text Component on Object {0}", name));
			#if UNITY_EDITOR
			StartCoroutine(RefreshOnceLocalizationManagerHasLoaded());
            #else
			Refresh();
            #endif
        }

		/// <summary>Refreshes the text value.</summary>
		public void Refresh()
		{
			text.text = LocalizationManager.instance.StringForKey(key);
		}

		#if UNITY_EDITOR
		/// <summary>Refreshs the text once the LocalizationManager has loaded.</summary>
		private IEnumerator RefreshOnceLocalizationManagerHasLoaded()
		{
			while(LocalizationManager.instance == null) { yield return null; }
			while(!LocalizationManager.instance.isLoaded) { yield return null; }
			Refresh();
		}
		#endif
	}
}
