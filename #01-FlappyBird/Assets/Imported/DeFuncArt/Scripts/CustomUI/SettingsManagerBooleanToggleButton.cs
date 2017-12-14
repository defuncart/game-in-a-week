/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	public class SettingsManagerBooleanToggleButton : DAToggleButton
	{
		/// <summary>The button's settings variable.</summary>
		[SerializeField] private SettingsManager.BooleanVariable settingsVariable;

		/// <summary>Callback when the instance is started.</summary>
		new private void Start()
		{
			//first determine if the setting is selected or not, then call base class
			selected = SettingsManager.instance.GetBooleanVariableValue(settingsVariable);
			base.Start();
		}

		/// <summary>A callback when a touch down event has been registered.</summary>
		public override void OnPointerDown(PointerEventData eventData)
		{
			//pass event data onto base class
			base.OnPointerDown(eventData);
			//if the button is interactable, send selected state onto SettingsManager
			if(interactable) { SettingsManager.instance.ToggleBooleanVariable(settingsVariable); }
		}
	}
}
