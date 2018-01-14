/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;

// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
	/// <summary>A LocalizationManager which supports the loading of text strings for multiple languages.</summary>
	public class LocalizationManager : MonoSingleton<LocalizationManager>
	{
		/// <summary>A struct representing a SupportedLanguage object.</summary>
		[System.Serializable]
		public class SupportedLanguage
		{
			/// <summary>The language.</summary>
			public SystemLanguage language;
			/// <summary>The json database.</summary>
			public TextAsset jsonDatabase;
		}
		/// <summary>An array of supported languages.</summary>
		[Tooltip("An array of supported languages.")]
		[SerializeField] private SupportedLanguage[] supportedLanguages;
		/// <summary>A private text database for the loaded localization.</summary>
		private Dictionary<string, string> textDatabase;
		/// <summary>If the user's system language should be matched.</summary>
		[Tooltip("If the user's system language should be matched.")]
		[SerializeField] private bool matchSystemLanguage = true;
		/// <summary>A default language to fall back on.</summary>
		[Tooltip("A default language to fall back on.")]
		[SerializeField] private SystemLanguage defaultLanguage = SystemLanguage.English;
		/// <summary>The localized language.</summary>
		private SystemLanguage localizedLanguage
		{
			get { return supportedLanguages[SettingsManager.instance.localizedLanguage].language; }
		}
		/// <summary>Whether the database is loaded.</summary>
		public bool isLoaded { get; private set; } //defaults to false

		#if UNITY_EDITOR
		/// <summary>EDITOR ONLY: An array of the database keys.</summary>
		public string[] keys
		{
			get
			{
				Assert.IsNotNull(supportedLanguages); Assert.IsTrue(supportedLanguages.Length > 0);

				Dictionary<string, string> dict = JSONSerializer.FromJson<Dictionary<string, string>>(supportedLanguages[0].jsonDatabase.text);
				return dict.Keys.ToArray();
			}
		}
		#endif

		/// <summary>Callback on Awake to initialize the object.</summary>
		protected override void Init()
		{
			#if UNITY_EDITOR
			Assert.IsTrue(SupportsLanguage(defaultLanguage));
			#endif
		}

		/// <summary>Callback when the instance should start. Load Database.</summary>
		private IEnumerator Start()
		{
			//wait until SettingsManager is created (first launch)
			while(SettingsManager.instance == null) { yield return null; }
			//set the localized language if needed, otherwise load the database
			if(SettingsManager.instance.localizedLanguage == -1)
			{
				SetLanguage( matchSystemLanguage ? Application.systemLanguage : defaultLanguage );
			}
			else
			{
				LoadDatabase();
			}
		}

		/// <summary>Sets the language using a string. Enums aren't possible as Button Onclick inspector events so
		/// this is the simplest hack to define an inspector method to set the localized language.</summary>
		/// <param name="languageString">The Language as a string.</param>
		public void SetLanguage(string languageString)
		{
			try
			{
				SystemLanguage language = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage), languageString);
				if(language != localizedLanguage) {  SetLanguage(language); }
			} 
			catch(System.Exception) { Debug.LogErrorFormat("{0} is not a valid SystemLanguage.", languageString); }
		}

		/// <summary>Sets the language using an int.</summary>
		/// <param name="languageIndex">The Language index.</param>
		public void SetLanguage(int languageIndex)
		{
			Assert.IsTrue(languageIndex >= 0 && languageIndex < supportedLanguages.Length);

			SystemLanguage language = supportedLanguages[languageIndex].language;
			if(language != localizedLanguage) {  SetLanguage(language); }
		}

		/// <summary>Sets the localization language.</summary>
		/// <param name="language">The language to set.</param>
		public void SetLanguage(SystemLanguage language)
		{
			isLoaded = false;

			//if the language isn't supported, fall back on the user's system language or a given default
			if(!SupportsLanguage(language))
			{
				Debug.LogError(string.Format("{0} is not a supported language!", language.ToString()));
				language = matchSystemLanguage ? Application.systemLanguage : defaultLanguage;
			}

			//determine the localized language index
			SupportedLanguage l = supportedLanguages.Where(x => x.language == language).ToList()[0];
			SettingsManager.instance.SetLocalizedLanguage( supportedLanguages.IndexOf<SupportedLanguage>(l) );

			//finally load the database and refresh any LocalizedText objects in the current scene
			LoadDatabase();
			RefreshCurrentSceneLocalizedText();
		}

		/// <summary>Loads the localization database.</summary>
		private void LoadDatabase()
		{
			Debug.Log("LoadDatabase");
			textDatabase = JSONSerializer.FromJson<Dictionary<string, string>>(supportedLanguages[SettingsManager.instance.localizedLanguage].jsonDatabase.text);
			isLoaded = true;
		}

		/// <summary>Determines whether a given language is supported.</summary>
		/// <returns><c>true</c>, if the language is supported, <c>false</c> otherwise.</returns>
		/// <param name="language">The language to test.</param>
		public bool SupportsLanguage(SystemLanguage language)
		{
			foreach(SupportedLanguage supportedLanguage in supportedLanguages)
			{
				if(language == supportedLanguage.language) { return true; }
			}
			return false;
		}

		/// <summary>Query the database for a given key.</summary>
		/// <param name="key">The key to query.</param>
		/// <returns>The resulting value if the key exists, otherwise the key itself.</returns>
		public string StringForKey(string key)
		{
			string result = key;
			if(textDatabase.ContainsKey(key))
			{
				result = textDatabase[key];
			} else { Debug.LogError("key " + key + " not found"); }
			return result;
		}

		/// <summary>Refreshes the localized text for the current scene.</summary>
		public static void RefreshCurrentSceneLocalizedText()
		{
			LocalizedText[] texts = FindObjectsOfType<LocalizedText>();
			foreach(LocalizedText text in texts) { text.Refresh(); }
		}
	}
}
