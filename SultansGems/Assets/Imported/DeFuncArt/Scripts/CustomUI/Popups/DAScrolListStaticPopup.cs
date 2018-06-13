/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
    /// <summary>A static popup which presents a list of T panels.</summary>
    public abstract class DAScrollListStaticPopup<T> : DAPopupStatic where T : MonoBehaviour
    {
        /// <summary>The Transform of the ScrollView's content (parent of all instantiated panels).</summary>
        [Tooltip("The Transform of the ScrollView's content (parent of all instantiated panels).")]
        [SerializeField] private RectTransform scrollViewContent = null;
        /// <summary>The panel prefab to instantiate.</summary>
        [Tooltip("The panel prefab to instantiate.")]
        [SerializeField] private T panelPrefab = null;
        /// <summary>The combined height of a panel and any vertical spacing between panels.</summary>
        [Tooltip("The combined height of a panel and any vertical spacing between panels")]
        [Range(25, 400)] [SerializeField] private float contentHeight = 300;

        /// <summary>The number of panels to create.</summary>
        protected abstract int numberPanelsToCreate { get; }
        /// <summary>An array of instantiated panels.</summary>
        protected List<T> panels;

        /// <summary>Displays the popup.</summary>
        public override void Display()
        {
            //adjust height and reset position
            scrollViewContent.sizeDelta = new Vector2(0, contentHeight * numberPanelsToCreate);
            scrollViewContent.anchoredPosition = Vector2.zero;
            //instantiate a list of word panels
            CreatePanels();
            //call base display
            base.Display();
        }

        /// <summary>Hides the popup.</summary>
        public override void Hide()
        {
            Assert.IsNotNull(panels);

            //call base hide
            base.Hide();

            //destroy all instantiated panels and clear the list
            DestroyPanels();
        }

        /// <summary>Creates the panels.</summary>
        protected abstract void CreatePanels();

        /// <summary>Destroys the panels.</summary>
        protected virtual void DestroyPanels()
        {
            for(int i=0; i < panels.Count; i++)
            {
                if(panels[i] != null) { Destroy(panels[i].gameObject); }
            }
            panels.Clear();
        }

        /// <summary>Initializes the panels list, if necessary.</summary>
        protected void InitializePanelsList()
        {
            if(panels == null) { panels = new List<T>(); }
        }

        /// <summary>Instantiates and returns a panel.</summary>
        protected T InstantiatePanel()
        {
            T panel = Instantiate(panelPrefab, scrollViewContent);
            panels.Add(panel);
            return panel;
        }
    }
}
