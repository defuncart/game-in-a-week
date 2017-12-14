/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.UI;
using DeFuncArt.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>A store item for the shop.</summary>
public class StoreItem : MonoBehaviour
{
	#region Properties

	/// <summary>An enum representing the type of item.</summary>
	public enum Type
	{
		Plane, Level
	}
	/// <summary>The item's type.</summary>
	[Tooltip("The store item's type.")]
	[SerializeField] private Type type;
	/// <summary>Whether the item is a level.</summary>
	private bool isLevel
	{
		get { return type == Type.Level; }
	}
	/// <summary>The item's index.</summary>
	[Tooltip("The item's index.")] [Range(0, 3)]
	[SerializeField] private int index;
	/// <summary>A reference to the insufficient coins panel.</summary>
	[Tooltip("A reference to the insufficient coins panel.")]
	[SerializeField] private GameObject insufficientCoinsPanel;
	/// <summary>The item's cost panel.</summary>
	[Tooltip("The item's cost panel.")]
	[SerializeField] private GameObject costPanel;
	/// <summary>The item's cost text.</summary>
	[Tooltip("The item's cost text.")]
	[SerializeField] private Text costText;
	/// <summary>The item's buy button.</summary>
	[Tooltip("The item's buy button.")]
	[SerializeField] private DAButton buyButton;
	/// <summary>The item's select button.</summary>
	[Tooltip("The item's select button.")]
	[SerializeField] private DAButton selectButton;
	/// <summary>Whether the item is unlocked.</summary>
	private bool isUnlocked
	{
		get
		{ 
			if(isLevel) { return PlayerManager.instance.LevelIsUnlocked(index); }
			else { return PlayerManager.instance.PlaneIsUnlocked(index); }
		}
	}
	/// <summary>Whether the item is selected.</summary>
	private bool isSelected
	{
		get
		{ 
			if(isLevel) { return PlayerManager.instance.currentLevel == index; }
			else { return PlayerManager.instance.currentPlane == index; }
		}
	}
	/// <summary>The item's cost.</summary>
	private int cost
	{
		get
		{ 
			if(isLevel) { return GameStore.GetPriceForLevel(index); }
			else { return GameStore.GetPriceForPlane(index); }
		}
	}

	#endregion

	#region Methods

	/// <summary>Callback when the instance is started.</summary>
	private void Start()
	{		
		Refresh(); //refresh the item
	}

	/// <summary>Refreshes the item.</summary>
	public void Refresh()
	{
		//update cost
		if(isUnlocked) { costPanel.SetActive(false); }
		else { costText.text = cost.ToString(); }
		//display buy or select button
		buyButton.gameObject.SetActive(!isUnlocked);
		selectButton.gameObject.SetActive(isUnlocked && !isSelected);
		//update onlick
		if(!isUnlocked)
		{
			buyButton.onClick.RemoveAllListeners();
			buyButton.onClick.AddListener(() => {
				
				//if the play has enough coins, buy the item and refresh shop
				if(PlayerManager.instance.coins >= cost)
				{
					AudioManager.instance.PlaySFX(AudioManagerKeys.itemBought); //play bought sfx
					if(isLevel) { PlayerManager.instance.SetLevelUnlocked(index); }
					else { PlayerManager.instance.SetPlaneUnlocked(index); }
					PlayerManager.instance.DecrementCoins(cost);
					Refresh();
					FindObjectOfType<GameScene>().Shop_UpdateCoinsText();
				}
				else //otherwise display the insufficient coins panel and play unavailable sfx
				{
					insufficientCoinsPanel.SetActive(true);
					AudioManager.instance.PlaySFX(AudioManagerKeys.insufficientCoins);
					this.Invoke( ()=>{ insufficientCoinsPanel.SetActive(false); }, AnimationDuration.ONE_SECOND );
				}
			});
		}
		else if(!isSelected)
		{
			selectButton.onClick.RemoveAllListeners();
			selectButton.onClick.AddListener(() => {
				//play sfx
				AudioManager.instance.PlaySFX(AudioManagerKeys.buttonGeneric);
				//update PlayerManager settings
				if(isLevel){ PlayerManager.instance.currentLevel = index; }
				else { PlayerManager.instance.currentPlane = index; }
				//reset the player
				FindObjectOfType<Player>().Reset();
				//get all sprites with AutoLoadSprite scripts and refresh them
				AutoLoadSprite[] sprites = FindObjectsOfType<AutoLoadSprite>();
				foreach(AutoLoadSprite sprite in sprites) { sprite.Refresh(); }
				//refresh all store items
				StoreItem[] items = FindObjectsOfType<StoreItem>();
				foreach(StoreItem item in items) { item.Refresh(); }
			});
		}
	}

	#endregion
}
