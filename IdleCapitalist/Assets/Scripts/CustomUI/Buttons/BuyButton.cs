/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.UI;
//using System;
//using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A simple extension of DAButton used as a buy button for upgrades, managers or leveling up.</summary>
public class BuyButton : DAButton
{
	/*
	/// <summary>The field info for m_Interactable, a private variable of the UI.Selectable class (which UI.Button extends from.</summary>
	private FieldInfo fieldInfo;

	/// <summary>Callback when the component awakes.</summary>
	override protected void Awake()
	{
		//firstly call the base class
		base.Awake();

		//move up in the inheritence hierarchy and try to find field m_Interactable
		Type type = this.GetType();
		while(type != null)
		{
			fieldInfo = type.GetField("m_Interactable", BindingFlags.Instance | BindingFlags.NonPublic);
			if(fieldInfo != null) { break; } //break out of the loop
			type = type.BaseType;
		}
		Assert.IsNotNull(fieldInfo, "Field 'm_Interactable' not found in type 'BuyButton'");
	}

	/// <summary>Gets or sets a value indicating whether this <see cref="BuyButton"/> is interactable. Also updates the button's color.</summary>
	/// <value><c>true</c> if interactable; otherwise, <c>false</c>.</value>
	new public bool interactable
	{
		get { return (bool)fieldInfo.GetValue(this); }
		set { fieldInfo.SetValue(this, value); image.color = (value ? GameColors.purple : GameColors.gray); }
	}
	*/

	/// <summary>Sets whether the button is interactable, automatically updating its color.</summary>
	public void SetInteractableAndColor(bool interactable)
	{
		this.interactable = interactable;
		image.color = (interactable ? GameColors.purple : GameColors.gray);
	}
}
