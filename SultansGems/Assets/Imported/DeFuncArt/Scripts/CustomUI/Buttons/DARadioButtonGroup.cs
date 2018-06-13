/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using System.Collections.Generic;
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

        /// <summary>Whether the player manually assigns the radio buttons.</summary>
        [Tooltip("Whether the player manually assigns the radio buttons")]
        [SerializeField] protected bool manuallyAssign = true;
		/// <summary>The group's radio buttons.</summary>
        [Tooltip("The group's radio buttons.")]
        [SerializeField] protected DARadioButton[] radioButtons;

        /// <summary>The group's selected index.</summary>
        public int selectedIndex { get; protected set; }

		/// <summary>Callback when the instance awakes.</summary>
		private void Awake()
		{
            if(manuallyAssign) { AutomaticallySetRadioButtonListners(); }
			selectedIndex = -1; //ResetForIndex must be called to initialize the buttons
		}

		/// <summary>Callback when the instance is being destroyed.</summary>
		private void OnDestroy()
		{
			for(int i=0; i < radioButtons.Length; i++)
			{
				radioButtons[i].onClick.RemoveAllListeners();
			}
            OnIndexWasSelected = null;
		}

        #if UNITY_EDITOR
        /// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
        private void OnValidate()
        {
            if(!manuallyAssign && radioButtons.Length > 0) { radioButtons = new DARadioButton[0]; }
            if(manuallyAssign)
            {
                Assert.AreNotEqual(radioButtons.Length, 0, string.Format("{0} has no assigned Radio Buttons", name));
                for(int i = 0; i < radioButtons.Length; i++)
                {
                    Assert.IsNotNull(radioButtons[i], string.Format("Expected radio button at index {0} for {1} to be not null.", i, name));
                }
            }
        }
        #endif

        /// <summary>Automatically sets the group's radio buttons.</summary>
        public void AutomaticallySetRadioButtons()
        {
            List<DARadioButton> buttonList = new List<DARadioButton>();
            for(int i=0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    DARadioButton button = transform.GetChild(i).GetComponent<DARadioButton>();
                    if(button != null) { buttonList.Add(button); }
                }
            }
            radioButtons = buttonList.ToArray();
        }

        public void RemoveAllRadioButtonListners()
        {
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i].onClick.RemoveAllListeners();
            }
        }

        /// <summary>Automatically sets the listners for each of the group's radio buttons.</summary>
        public void AutomaticallySetRadioButtonListners()
        {
            for(int i = 0; i < radioButtons.Length; i++)
            {
                int temp = i; //a temporary local variable to pass to the closure
                radioButtons[i].onClick.AddListener(() => {
                    RadioButtonIsSelected(temp);
                });
            }
        }

        /// <summary>Resets all buttons to be unselected.</summary>
        public void Reset()
        {
            selectedIndex = -1;
            for(int i=0; i < radioButtons.Length; i++)
            {
                radioButtons[i].SetSelected(false);
            }
        }

        /// <summary>Reset the selected radio button to a given index.</summary>
        public void ResetForIndex(int index, bool ensureDifferentIndex=true)
		{
            RadioButtonIsSelected(index: index, triggerCallback: false, ensureDifferentIndex: ensureDifferentIndex);
		}

		/// <summary>Callback when a radio button is selected.</summary>
		public virtual void RadioButtonIsSelected(int index, bool triggerCallback=true, bool ensureDifferentIndex=true)
		{
			Assert.IsTrue(index >= 0 && index < radioButtons.Length);

            //if indeces should be different, only update if so. otherwise update regardless.
            if(ensureDifferentIndex)
            {
                if(index != selectedIndex) { SetIndex(index: index, triggerCallback: triggerCallback); }
            }
            else
            {
                SetIndex(index: index, triggerCallback: triggerCallback);
            }
		}

        /// <summary>Sets the group's selected index.</summary>
        private void SetIndex(int index, bool triggerCallback = true)
        {
            //update the selected index and trigger an event
            selectedIndex = index;
            if(triggerCallback && OnIndexWasSelected != null) { OnIndexWasSelected(selectedIndex); }
            //update the group's buttons
            for(int i=0; i < radioButtons.Length; i++)
            {
                radioButtons[i].SetSelected(selectedIndex == i);
            }
        }
	}
}
