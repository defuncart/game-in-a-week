/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A group of DARadioButtons supporting single selection.</summary>
	public class DARadioButtonGroup : MonoBehaviour
	{
		/// <summary>An event which is triggered when an index is selected.</summary>
		public EventHandlerInt OnIndexWasSelected;
		/// <summary>The group's radio buttons.</summary>
		[SerializeField] private DARadioButton[] radioButtons;
		/// <summary>The group's selected index.</summary>
		public int selectedIndex { get; private set; }

		/// <summary>Callback when the instance starts.</summary>
		private void Start()
		{
			Assert.IsTrue(radioButtons.Length > 1);

			for(int i=0; i < radioButtons.Length; i++)
			{
				int temp = i; //a temporary local variable to pass to the closure
				radioButtons[i].onClick.AddListener(() => {
					RadioButtonIsSelected(temp);
				});
			}
			selectedIndex = -1; //ResetForIndex must be called to initialize the buttons
		}

		/// <summary>Callback when the instance is being destroyed.</summary>
		private void OnDestroy()
		{
			for(int i=0; i < radioButtons.Length; i++)
			{
				radioButtons[i].onClick.RemoveAllListeners();
			}
		}

		/// <summary>Reset the selected radio button to a given index.</summary>
		public void ResetForIndex(int index)
		{
			RadioButtonIsSelected(index);
		}

		/// <summary>Callback when a radio button is selected.</summary>
		public void RadioButtonIsSelected(int index)
		{
			Assert.IsTrue(index >= 0 && index < radioButtons.Length);

			if(index != selectedIndex)
			{
				//update the selected index and trigger an event
				selectedIndex = index;
				if(OnIndexWasSelected != null) { OnIndexWasSelected(selectedIndex); }
				//update the group's buttons
				for(int i=0; i < radioButtons.Length; i++)
				{
					radioButtons[i].SetSelected(selectedIndex == i); 
				}
			}
		}
	}
}
