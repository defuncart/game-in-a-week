/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.CustomProperties;
using DeFuncArt.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName="ManagerData", menuName="Game Data/Manager Data", order=1)]
public class ManagerData : ScriptableObject
{
	/// <summary>The upgrade's image.</summary>
	[Tooltip("The upgrade's image.")]
	public Sprite image;
	/// <summary>A locailization key for the upgrade's name.</summary>
	[Tooltip("A locailization key for the upgrade's name.")]
	[SerializeField] private string nameKey;
	/// <summary>An enum denoting the type of the manager (whether they are standard or efficient).</summary>
	public enum ManagerType
	{
		/// <summary>A standard manager who runs the business.</summary>
		Standard, 
		/// <summary>An efficient manager who lowers costs.</summary>
		Efficient
	}
	/// <summary>The upgrade's type (whether it is applicable to a single business or all businesses).</summary>
	[Tooltip("The manager's type (whether they are standard or efficient).")]
	public ManagerType type;
	/// <summary>The business the manager can manage.</summary>
	[Tooltip("The business the manager can manage.")]
	public Business.Type business;
	/// <summary>If the manager can show the cash per second for the business.</summary>
	[Tooltip("If the manager can show the cash per second for the business.")]
	public bool showCashPerSecond;
	/// <summary>The amount the manager can reduce production costs for the business.</summary>
	[Tooltip("The amount the manager can reduce production costs for the business.")]
	[Range(0, 1)] public float costReductionMultiplier;
	/// <summary>The manager's one-time buy cost.</summary>
	[Tooltip("The manager's one-time buy cost.")]
	public float cost;

	#if UNITY_EDITOR
	/// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
	private void OnValidate()
	{
		Assert.IsNotNull(image, "Need to supply an image for the upgrade.");
		Assert.IsTrue(nameKey != "", "Need to supply a string key for the upgrade's name");
//		Assert.IsTrue(cost > 0, "Cost should be greater than 0.");
	}
	#endif

	/// <summary>The managers's localized name.</summary>
	new public string name
	{
		get { return LocalizationManager.instance.StringForKey(nameKey); }
	}

	/// <summary>A localized description of the manager.</summary>
	public string description
	{
		get
		{
			if(type == ManagerType.Standard) //"Runs {0}"
			{
				return string.Format(LocalizationManager.instance.StringForKey(LocalizationManagerKeys.ManagerRunsBusiness), LocalizationManager.instance.StringForKey(business.ToString())); 
			}
			else //Reduces cost by {0}%. Shows Cash Per Sec.
			{
				return string.Format(LocalizationManager.instance.StringForKey(LocalizationManagerKeys.ManagerReducesCosts), Mathf.Floor(costReductionMultiplier*100)); //as percentage
			}
		}
	}
}
