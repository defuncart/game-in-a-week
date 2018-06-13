/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections;
using UnityEngine;

/// <summary>A touch manager used to regester stone touch events.</summary>
public class TouchManager : MonoBehaviour
{
    /// <summary>A delegate which takes two GameObject parameters.</summary>
    public delegate void OnStonesSelectedDelegate(GameObject selectedStone1, GameObject selectedStone2);
    /// <summary>An event which occurs when two stones have been selected.</summary>
    public event OnStonesSelectedDelegate OnStonesSelected;

    /// <summary>An enum representing the possible stone selection states.</summary>
	private enum SelectionState
	{
        /// <summary>Nothing selected (i.e. waiting on player interaction).</summary>
        None,
        /// <summary>Player has selected the first stone.</summary>
        Started
	}
    /// <summary>The current selection state.</summary>
    private SelectionState selectionState;

    /// <summary>A coroutine which checks for stone selection.</summary>
    private Coroutine CheckForSelectionCoroutine = null;
    /// <summary>Whether the manager is accepting input.</summary>
    public bool acceptInput
    {
        get { return CheckForSelectionCoroutine != null; }
        set
        {
            if(value != acceptInput)
            {
                if(value == true && acceptInput == false) { CheckForSelectionCoroutine = StartCoroutine(CheckForSelection()); } //start coroutine
                else if (value == false && acceptInput == true) { StopCoroutine(CheckForSelectionCoroutine); CheckForSelectionCoroutine = null; } //end coroutine
            }
        }
    }

    /// <summary>A layer mask for board cells.</summary>
    [Tooltip("A layer mask for board cells.")]
    [SerializeField] private LayerMask boardCellLayerMask = default(LayerMask);
    /// <summary>A layer mask for stones.</summary>
    [Tooltip("A layer mask for stones.")]
    [SerializeField] private LayerMask stoneLayerMask = default(LayerMask);

    /// <summary>The selected stones.</summary>
	private GameObject selectedStone1, selectedStone2;

    /// <summary>Callback before the component is destroyed.</summary>
	private void OnDestroy()
	{
		//delegates
        OnStonesSelected = null;
	}

    /// <summary>Resets the selection state (i.e. waiting on user interaction).</summary>
    public void ResetSelectionState()
    {
        selectionState = SelectionState.None;
        acceptInput = true;
    }

    /// <summary>A coroutine which checks for stone selection at each frame.</summary>
    private IEnumerator CheckForSelection()
    {
        while(true)
        {
            ProcessTouches();
            yield return null;
        }
    }

    /// <summary>Process any touch this frame.</summary>
    private void ProcessTouches()
    {
        if(selectionState == SelectionState.None)
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, boardCellLayerMask);
                if(hit.collider != null)
                {
                    selectedStone1 = hit.collider.gameObject;
                    selectionState = SelectionState.Started;
                }
            }
        }
        else if(selectionState == SelectionState.Started)
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, boardCellLayerMask);
                if(hit.collider != null)
                {
                    if(hit.collider.gameObject == selectedStone1) { return; }

                    selectedStone2 = hit.collider.gameObject;

                    if(BoardCell.AreAdjoining(selectedStone1, selectedStone2))
                    {
                        acceptInput = false;
                        OnStonesSelected(selectedStone1, selectedStone2);
                    }
                    else
                    {
                        selectionState = SelectionState.None;
                    }

                    selectedStone1 = selectedStone2 = null;
                }
            }
        }
    }
}
