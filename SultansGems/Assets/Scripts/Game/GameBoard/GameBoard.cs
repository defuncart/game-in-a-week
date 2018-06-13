/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>The gameboard.</summary>
public class GameBoard : MonoBehaviour
{
	#region Properties

    /// <summary>The gameboard's width.</summary>
	public const int COLUMNS = 9;
    /// <summary>The gameboard's height.</summary>
	public const int ROWS = 9;

    /// <summary>An event which occures when the player has scored points.</summary>
    public EventHandlerInt OnPointsScored;
    /// <summary>An event which occures when the player has made a successful move.</summary>
    public EventHandler OnSuccessfulMove;
    /// <summary>An event which occurs when the gameboard has finished animating (signaling that stones can again be selected).</summary>
    public EventHandler AnimatingFinished;

	/// <summary>The board cell prefab.</summary>
    [Tooltip("The board cell prefab.")]
	[SerializeField] private BoardCell boardCellPrefab = null;
	/// <summary>A 2D array of board cells.</summary>
	private BoardCell[,] cells;
    /// <summary>A 2D array of board pieces.</summary>
    private BoardPiece[,] pieces;
	/// <summary>An array of stone prefabs.</summary>
    [Tooltip("An array of stone prefabs.")]
    [SerializeField] private Stone[] stonePrefabs = null;
    /// <summary>A random stone prefab (for the current level).</summary>
	private Stone randomStonePrefab
	{
        get { return stonePrefabs.RandomObjectWithProbabilityDistribution(LevelsManager.instance.currentLevel.stonesDistribution); }
	}
    /// <summary>The transform of the gameboard's mask.</summary>
    [Tooltip("The transform of the gameboard's mask.")]
    [SerializeField] private Transform mask = null;
    /// <summary>A boardcell spritemask prefab.</summary>
    [Tooltip("A boardcell spritemask prefab.")]
    [SerializeField] private Transform boardCellSpriteMaskPrefab = null;

    /// <summary>A backing variable for visible.</summary>
    private bool _visible = false;
    /// <summary>The gameboard's visibility.</summary>
    public bool visible
    {
        get { return _visible; }
        set
        {
            _visible = value;
            //enable or disable the sprite renderer of all the gameboard's children (i.e. cells and pieces)
            SpriteRenderer[] spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in spriteRenderers) { spriteRenderer.enabled = value; }
        }
    }

	#endregion

	#region Initialization

	/// <summary>Callback when the component awakes.</summary>
	private void Awake()
	{
		cells = new BoardCell[COLUMNS, ROWS];
		pieces = new BoardPiece[COLUMNS, ROWS];
	}

    /// <summary>Callback before the component is destroyed.</summary>
    private void OnDestroy()
    {
        //delegates
        OnPointsScored = null; OnSuccessfulMove = null; AnimatingFinished = null;
    }

    /// <summary>Creates the gameboard.</summary>
    public void Create()
    {
        CreateBoardCells();
    }

	/// <summary>Resets the board.</summary>
	public void Reset()
	{
		//remove any pieces on the board
        RemoveAllPieces();
		//and then fill the board
		FillBoardWithStones();
	}

    /// <summary>Creates the board cells.</summary>
	private void CreateBoardCells()
	{
		for(int x=0; x < COLUMNS; x++)
		{
			for(int y=0; y < ROWS; y++)
			{
				if(LevelsManager.instance.currentLevel.IsValidCell(x, y))
				{
					BoardCell cell = Instantiate(boardCellPrefab, this.transform, false) as BoardCell;
					cell.SetXY(x, y);
					cells[x, y] = cell;
				}
				else
				{
					if(y != 0) //don't need a mask at the bottom of the board
					{
						Transform maskCell = Instantiate(boardCellSpriteMaskPrefab, mask, false) as Transform;
						maskCell.localPosition = new Vector3(x, y);
					}
				}
			}
		}
	}

	#endregion

	#region Fill & Remove

    /// <summary>Fills the board with stones.</summary>
	private void FillBoardWithStones()
	{
		//place any initial pieces
		for(int x=0; x < COLUMNS; x++)
		{
			for(int y=0; y < ROWS; y++)
			{
				if(IsValidCell(x, y))
				{
					DATuple<bool, int> isStone = LevelsManager.instance.currentLevel.IsStone(x, y);
					if(isStone.first)
					{
						FillPieceAt(x, y, stonePrefabs[isStone.second], true);
					}
				}
			}
		}

		//fill rest of the pieces
		for(int x=0; x < COLUMNS; x++)
		{
			for(int y=0; y < ROWS; y++)
			{
				if(pieces[x, y] == null && IsValidCell(x, y))
				{
					FillPieceAt(x, y, randomStonePrefab, true);
					while(CreatesMatchOnFill(x, y))
					{
						RemoveAt(x, y);
						FillPieceAt(x, y, randomStonePrefab, true);
					}
				}
			}
		}
	}

    /// <summary>Fills a given piece at a given position.</summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="pieceToFill">The piece to fill.</param>
    /// <param name="automaticallySetLocation">If the transform should automaticaly be set.</param>
    private void FillPieceAt(int x, int y, BoardPiece pieceToFill, bool automaticallySetLocation = false)
	{
        Assert.IsTrue(PointIsOnBoard(x, y), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x, y, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(x, y), string.Format("Expected point ({0}, {1}) to be valid.", x, y));

		BoardPiece piece = Instantiate(pieceToFill, this.transform, false) as BoardPiece;
		piece.SetXY(x, y, automaticallySetLocation);
		pieces[x, y] = piece;
        if(!visible) { piece.GetComponent<SpriteRenderer>().enabled = false; }
	}

    /// <summary>Determine if a given position results in matches.</summary>
    /// <returns><c>true</c>, if a match exists, <c>false</c> otherwise.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="minLength">The minimum length for a match.</param>
	private bool CreatesMatchOnFill(int x, int y, int minLength = Constants.MINIMUM_STONES_FOR_MATCH)
	{
        Assert.IsTrue(PointIsOnBoard(x, y), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x, y, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(x, y), string.Format("Expected point ({0}, {1}) to be valid.", x, y));

        List<BoardPiece> horizontalMatches = FindHorizontalMatches(x, y, minLength);
        List<BoardPiece> verticalMatches = FindVerticalMatches(x, y, minLength);
        return horizontalMatches.Count > 0 || verticalMatches.Count > 0;
	}

    /// <summary>Removes all pieces from the board.</summary>
    private void RemoveAllPieces()
	{
        for(int x=0; x < COLUMNS; x++)
        {
            for(int y=0; y < ROWS; y++)
            {
                if(pieces[x, y] != null) { RemoveAt(x, y); }
            }
        }
	}

    /// <summary>Removes a board piece at a given position.</summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
	private void RemoveAt(int x, int y)
	{
        Assert.IsTrue(PointIsOnBoard(x, y), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x, y, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(x, y), string.Format("Expected point ({0}, {1}) to be valid.", x, y));
        
		if(pieces[x, y] != null)
		{
			Destroy(pieces[x, y].gameObject);
			pieces[x, y] = null;
		}
	}

    /// <summary>Removes a list of board pieces.</summary>
    /// <param name="piecesToRemove">A list of board pieces to remove.</param>
	private void RemovePieces(List<BoardPiece> piecesToRemove)
	{
		foreach(BoardPiece piece in piecesToRemove)
		{
			if(piece != null) { RemoveAt(piece.x, piece.y); }
		}
	}

	#endregion

	#region Moving & Swapping

    /// <summary>Tries to swap pieces at two specified cells.</summary>
    /// <param name="go1">The first board cell.</param>
    /// <param name="go2">The second board cell.</param>
    public IEnumerator Swap(GameObject go1, GameObject go2)
	{
		BoardCell cell1 = go1.GetComponent<BoardCell>();
		BoardCell cell2 = go2.GetComponent<BoardCell>();

		int x1 = cell1.x; int y1 = cell1.y; int x2 = cell2.x; int y2 = cell2.y;
		SwapPieces(x1, y1, x2, y2);

        AudioManager.instance.PlaySFX(SFXDatabaseKeys.swap);

		yield return WaitFor.Seconds(AnimationDuration.SWAP);

        List<BoardPiece> totalMatches = new List<BoardPiece>();
        totalMatches.AddRange(FindMatchesAt(x2, y2));
        totalMatches.AddRange(FindMatchesAt(x1, y1));
	
        if(totalMatches.Count == 0)
        {
            Debug.Log("No match");
            SwapPieces(x1, y1, x2, y2);

            AudioManager.instance.PlaySFX(SFXDatabaseKeys.unswap);

            yield return WaitFor.Seconds(AnimationDuration.SWAP);

            AnimatingFinished();
        }
        else
        {
            StartCoroutine(ClearCollapseRefillBoard(totalMatches.ToList()));
        }
	}

    /// <summary>Swaps two pieces at two sets of coordinates.</summary>
    /// <param name="x1">The first x-coordinate.</param>
    /// <param name="y1">The first y-coordinate.</param>
    /// <param name="x2">The second x-coordinate.</param>
    /// <param name="y2">The second y-coordinate.</param>
    private void SwapPieces(int x1, int y1, int x2, int y2)
    {
        Assert.IsTrue(PointIsOnBoard(x1, y1), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x1, y1, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(x1, y1), string.Format("Expected point ({0}, {1}) to be valid.", x1, y1));
        Assert.IsTrue(PointIsOnBoard(x2, y2), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x2, y2, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(x2, y2), string.Format("Expected point ({0}, {1}) to be valid.", x2, y2));

        BoardPiece piece1 = pieces[x1, y1];
        BoardPiece piece2 = pieces[x2, y2];

        MovePieceToInTime(piece1, x2, y2, AnimationDuration.SWAP);
        MovePieceToInTime(piece2, x1, y1, AnimationDuration.SWAP);
    }

    /// <summary>Moves a given board piece from one position to another in a given time.</summary>
    /// <param name="piece">The board Piece.</param>
    /// <param name="x">The x-coorinate.</param>
    /// <param name="y">The y-coorinate.</param>
    /// <param name="time">The time (duration).</param>
    private void MovePieceToInTime(BoardPiece piece, int x, int y, float time)
    {
        Assert.IsTrue(PointIsOnBoard(x, y), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x, y, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(x, y), string.Format("Expected point ({0}, {1}) to be valid.", x, y));

        piece.SetXY(x, y);
        StartCoroutine(piece.transform.MoveLocallyToInTime(new Vector3(x, y, 0), time));
        pieces[x, y] = piece;
    }

	#endregion

	#region Utilities

    /// <summary>Determines if a point is on the board.</summary>
    /// <returns><c>true</c>, if the point is on the board, <c>false</c> otherwise.</returns>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
	private bool PointIsOnBoard(int x, int y)
	{
		return x >= 0 && x < ROWS && y >= 0 && y < COLUMNS;
	}

    /// <summary>Determines if a cell is valid..</summary>
    /// <returns><c>true</c>, if the cell is valid, <c>false</c> otherwise.</returns>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
	private bool IsValidCell(int x, int y)
	{
        Assert.IsTrue(PointIsOnBoard(x, y), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x, y, COLUMNS, ROWS));

		return cells[x, y] != null;
	}

    // DEBUG
    /// <summary>Prints the board to the console.</summary>
    private void Print()
    {
        string[,] array = new string[COLUMNS, ROWS];
        for(int x=0; x < COLUMNS; x++)
        {
            for(int y=0; y < ROWS; y++)
            {
                if(!IsValidCell(x, y)) { array[x, y] = "x"; }
                else if(pieces[x, y].isStone) { array[x, y] = (pieces[x, y] as Stone).type.ToString(); }
                else { array[x, y] = "?"; }
            }
        }

        //print to console
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        for(int i=array.GetLength(0) - 1; i >= 0; i--)
        {
            if(i != array.GetLength(0) - 1) { sb.Append(", "); }

            sb.Append("[");
            for(int j=0; j < array.GetLength(1); j++)
            {
                if(j != 0) { sb.Append(", "); }
                sb.Append(array[j, i].ToString());
            }
            sb.Append("]");
        }
        sb.Append("}");
        Debug.Log(sb.ToString());
    }
    // DEBUG

	#endregion

	#region Matches

    /// <summary>Finds all matches from a given position in a given direction.</summary>
    /// <returns>A list of matched pieces.</returns>
    /// <param name="startX">The start x-coordinate.</param>
    /// <param name="startY">The start y-coordinate.</param>
    /// <param name="searchDirection">The search direction.</param>
    /// <param name="minNumPieces">The minimum number of pieces for a valid match.</param>
    private List<BoardPiece> FindMatches(int startX, int startY, SearchDirection searchDirection, int minNumPieces = Constants.MINIMUM_STONES_FOR_MATCH)
	{
        Assert.IsTrue(PointIsOnBoard(startX, startY), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", startX, startY, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(startX, startY), string.Format("Expected point ({0}, {1}) to be valid.", startX, startY));
        Assert.IsTrue(searchDirection.isNormal);
        Assert.IsTrue(minNumPieces > 0);

		List<BoardPiece> matches = new List<BoardPiece>();
		matches.Add(pieces[startX, startY]);

		Stone currentStone = pieces[startX, startY] as Stone;

        int searchDepth = (ROWS > COLUMNS ? ROWS : COLUMNS) - 1; //Mathf.Max(ROWS, COLUMNS) - 1;
        for(int i=1; i < searchDepth; i++)
		{
			int nextX = startX + searchDirection.x * i;
			int nextY = startY + searchDirection.y * i;

			if(!PointIsOnBoard(nextX, nextY)) { break; }
			//if(!IsValidCell(nextX, nextY)) { continue; } //continue to next loop
            if(!IsValidCell(nextX, nextY)) { break; }

			Stone nextStone = pieces[nextX, nextY] as Stone;
			if(nextStone == null) { break; } //TO DO

			if(nextStone.type != currentStone.type) { break; }

			matches.Add(nextStone);
		}

		if(matches.Count < minNumPieces) { matches.Clear(); } return matches;
	}

    /// <summary>Finds all horizontal matches from a given position.</summary>
    /// <returns>A list of matched pieces.</returns>
    /// <param name="startX">The start x-coordinate.</param>
    /// <param name="startY">The start y-coordinate.</param>
    /// <param name="minNumPieces">The minimum number of pieces for a valid match.</param>
    private List<BoardPiece> FindHorizontalMatches(int startX, int startY, int minNumPieces = Constants.MINIMUM_STONES_FOR_MATCH)
	{
        Assert.IsTrue(PointIsOnBoard(startX, startY), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", startX, startY, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(startX, startY), string.Format("Expected point ({0}, {1}) to be valid.", startX, startY));
        Assert.IsTrue(minNumPieces >= Constants.MINIMUM_STONES_FOR_MATCH);

        List<BoardPiece> leftwardMatches = FindMatches(startX, startY, SearchDirection.left, minNumPieces-1);
        List<BoardPiece> rightwardMatches = FindMatches(startX, startY, SearchDirection.right, minNumPieces-1);
		List<BoardPiece> totalMatches = leftwardMatches.Union(rightwardMatches).ToList();

        if(totalMatches.Count < minNumPieces) { totalMatches.Clear(); } return totalMatches;
	}

    /// <summary>Finds all horizontal matches from a given position.</summary>
    /// <returns>A list of matched pieces.</returns>
    /// <param name="startX">The start x-coordinate.</param>
    /// <param name="startY">The start y-coordinate.</param>
    /// <param name="minNumPieces">The minimum number of pieces for a valid match.</param>
    private List<BoardPiece> FindVerticalMatches(int startX, int startY, int minNumPieces = Constants.MINIMUM_STONES_FOR_MATCH)
	{
        Assert.IsTrue(PointIsOnBoard(startX, startY), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", startX, startY, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(startX, startY), string.Format("Expected point ({0}, {1}) to be valid.", startX, startY));
        Assert.IsTrue(minNumPieces >= Constants.MINIMUM_STONES_FOR_MATCH);

		List<BoardPiece> upwardMatches = FindMatches(startX, startY, SearchDirection.up, 2);
		List<BoardPiece> downwardMatches = FindMatches(startX, startY, SearchDirection.down, 2);
		List<BoardPiece> totalMatches = upwardMatches.Union(downwardMatches).ToList();

		if(totalMatches.Count < minNumPieces) { totalMatches.Clear(); } return totalMatches;
	}

    /// <summary>Finds all matches from a given position.</summary>
    /// <returns>A list of matched pieces.</returns>
    /// <param name="startX">The start x-coordinate.</param>
    /// <param name="startY">The start y-coordinate.</param>
    /// <param name="minNumPieces">The minimum number of pieces for a valid match.</param>
    private List<BoardPiece> FindMatchesAt(int startX, int startY, int minNumPieces = Constants.MINIMUM_STONES_FOR_MATCH)
	{
        Assert.IsTrue(PointIsOnBoard(startX, startY), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", startX, startY, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(startX, startY), string.Format("Expected point ({0}, {1}) to be valid.", startX, startY));
        Assert.IsTrue(minNumPieces >= Constants.MINIMUM_STONES_FOR_MATCH);

		List<BoardPiece> horizontalMatches = FindHorizontalMatches(startX, startY);
		List<BoardPiece> verticalMatches = FindVerticalMatches(startX, startY);
		List<BoardPiece> totalMatches = horizontalMatches.Union(verticalMatches).ToList();

		if(totalMatches.Count < minNumPieces) { totalMatches.Clear(); } return totalMatches;
	}

    /// <summary>Finds all matches for a list of board pieces.</summary>
    /// <returns>A list of matched pieces.</returns>
    /// <param name="piecesToFindMatches">A list of pieces ot find matches for.</param>
    /// <param name="minNumPieces">The minimum number of pieces for a valid match.</param>
    private List<BoardPiece> FindMatchesAt(List<BoardPiece> piecesToFindMatches, int minNumPieces = Constants.MINIMUM_STONES_FOR_MATCH)
	{
        IEnumerable<BoardPiece> matches = new List<BoardPiece>();
		foreach(BoardPiece piece in piecesToFindMatches)
		{
			matches = matches.Union(FindMatchesAt(piece.x, piece.y, minNumPieces));
		}
        return matches.ToList();
	}

	#endregion

	#region Collapse & Refill

    /// <summary>Collapses pieces in a given column.</summary>
    /// <returns>A list of collapsed pieces.</returns>
    /// <param name="column">The column's index.</param>
	private List<BoardPiece> CollapseColumn(int column)
	{
        Assert.IsTrue(column >= 0 && column < COLUMNS, string.Format("Expected column index {0} to be within (0, {1}).", column, COLUMNS-1));

		List<BoardPiece> movingPieces = new List<BoardPiece>();
		for(int i=0; i < COLUMNS - 1; i++)
		{
			if(pieces[column, i] == null && IsValidCell(column, i))
			{
				for(int j=i+1; j < COLUMNS; j++)
				{
					if(pieces[column, j] != null)
					{
						if(!movingPieces.Contains(pieces[column, j])){ movingPieces.Add(pieces[column, j]); }
						MovePieceToInTime(pieces[column, j], column, i, AnimationDuration.COLUMN_COLLAPSE * (j-i));
						pieces[column, j] = null;
						break;
					}
				}
			}
		}
		return movingPieces;
	}

    /// <summary>Collapses a list of pieces.</summary>
    /// <returns>A list of collapsed pieces.</returns>
    /// <param name="piecesToCollapse">The pieces to collapse.</param>
	private List<BoardPiece> CollapseColumn(List<BoardPiece> piecesToCollapse)
	{
        IEnumerable<BoardPiece> movingPieces = new List<BoardPiece>();
		List<int> columnsToCollapse = piecesToCollapse.Select(piece => piece.x).Distinct().ToList();
		foreach(int columnToCollapse in columnsToCollapse)
		{
            movingPieces = movingPieces.Union(CollapseColumn(columnToCollapse));
		}
        return movingPieces.ToList();
	}

    /// <summary>Clears and collapses a list of board pieces and refills the board.</summary>
    /// <param name="piecesToClearAndCollapse">The pieces to clear and collapse.</param>
    private IEnumerator ClearCollapseRefillBoard(List<BoardPiece> piecesToClearAndCollapse)
	{
        //short delay
		yield return WaitFor.Seconds(Duration.QUATER_SECOND);

        //while there are matches, keep clearing, collapsing and refilling the board
        while(piecesToClearAndCollapse.Count >0)
        {
            //determine points scored and remove pieces

            yield return GetPointsScoredForPieces(piecesToClearAndCollapse, pointsScored => {
                OnPointsScored(pointsScored);
            });

            AudioManager.instance.PlaySFX(SFXDatabaseKeys.match);

            RemovePieces(piecesToClearAndCollapse);

            yield return WaitFor.Seconds(Duration.EIGHT_SECOND);

            //next collapse pieces

            List<BoardPiece> movingPieces = CollapseColumn(piecesToClearAndCollapse);

            yield return WaitFor.Seconds(AnimationDuration.COLUMN_COLLAPSE);

            //next refill the board

            movingPieces.AddRange(RefillEmptyBoardCells());

            yield return WaitFor.Seconds(AnimationDuration.SWAP);

            //finally determine if there are any new matches

            piecesToClearAndCollapse = FindMatchesAt(movingPieces);
        }

        //trigger events
        AnimatingFinished();
        OnSuccessfulMove();

		yield return null; //unnecessary??
	}

    /// <summary>Determines the points scored for a list of pieces. This is a callback to ensure pieces aren't removed before it finishes.</summary>
    /// <param name="boardPieces">The board pieces.</param>
    /// <param name="callback">A callback.</param>
    private IEnumerator GetPointsScoredForPieces(List<BoardPiece> boardPieces, System.Action<int> callback)
    {
        int returnValue = 0;
        List<Stone> stones = boardPieces.Where(x => x != null && x.isStone).Select(x => x as Stone).ToList();
        foreach(Stone stone in stones)
        {
            returnValue += LevelsManager.instance.PointsForStone(stone.type);
        }

        yield return null;
        callback(returnValue);
    }

    /// <summary>Refill all empty board cells.</summary>
    private List<BoardPiece> RefillEmptyBoardCells()
	{
        List<BoardPiece> filledPieces = new List<BoardPiece>();
        int[] countOfEmptyPiecesInColumns = GetCountOfEmptyPiecesInColumns();

		for(int x=0; x < COLUMNS; x++)
		{
			for(int y=0; y < ROWS; y++)
			{
				if(pieces[x, y] == null && IsValidCell(x, y))
				{
					FillPieceAt(x, y, randomStonePrefab);
					while(CreatesMatchOnFill(x, y))
					{
						RemoveAt(x, y);
						FillPieceAt(x, y, randomStonePrefab);
					}
                    filledPieces.Add(pieces[x, y]);

					//set position above the board
					int initialY = pieces[x, y].y + countOfEmptyPiecesInColumns[x];
					pieces[x, y].transform.localPosition = new Vector3(x, initialY, 0);
					StartCoroutine(pieces[x, y].transform.MoveLocallyToInTime(new Vector3(x, y, 0), AnimationDuration.SWAP));
				}
			}
		}

        return filledPieces;
	}

    /// <summary>Returns a count of the number of empty pieces in each column.</summary>
	private int[] GetCountOfEmptyPiecesInColumns()
	{
		int[] returnArray = new int[ROWS];
		for(int index=0; index < ROWS; index++)
		{
			returnArray[index] = CountOfEmptyPiecesInColumn(index);
		}
		return returnArray;
	}

    /// <summary>Returns a count of the number of empty pieces for a given column.</summary>
    /// <param name="column">The column's index.</param>
	private int CountOfEmptyPiecesInColumn(int column)
	{
		int returnCount = 0;
		for(int i=0; i < COLUMNS; i++)
		{
			if(pieces[column, i] == null && IsValidCell(column, i)) { returnCount++; }
		}
		return returnCount;
	}

	#endregion

	#region GetPieces

    /// <summary>Returns all the pieces in a given row.</summary>
    /// <returns>All the row's pieces.</returns>
    /// <param name="row">The row index.</param>
	private List<BoardPiece> GetPiecesInRow(int row)
	{
		List<BoardPiece> piecesToReturn = new List<BoardPiece>();
		for(int x=0; x < COLUMNS; x++)
		{
			//use isvalid cell
			if(pieces[x, row] != null) { piecesToReturn.Add(pieces[x, row]); }
		}
		return piecesToReturn;
	}

    /// <summary>Returns all the pieces in a given column.</summary>
    /// <returns>All the column's pieces.</returns>
    /// <param name="column">The column index.</param>
	private List<BoardPiece> GetPiecesInColumn(int column)
	{
		List<BoardPiece> piecesToReturn = new List<BoardPiece>();
		for(int y=0; y < ROWS; y++)
		{
			//use is valid cell
			if(pieces[column, y] != null) { piecesToReturn.Add(pieces[column, y]); }
		}
		return piecesToReturn;
	}

    /// <summary>Returns all adjacent pieces for a given cell position.</summary>
    /// <returns>The adjacent pieces.</returns>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <param name="offset">The horizontal and vertical offset. Defaults to 1.</param>
	private List<BoardPiece> GetAdjacentPieces(int x, int y, int offset=1)
	{
        Assert.IsTrue(PointIsOnBoard(x, y), string.Format("Expected point ({0}, {1}) to be within ({2}, {3}).", x, y, COLUMNS, ROWS));
        Assert.IsTrue(IsValidCell(x, y), string.Format("Expected point ({0}, {1}) to be valid.", x, y));
        Assert.IsTrue(offset >= 0);

		List<BoardPiece> piecesToReturn = new List<BoardPiece>();
		for(int i=x-offset; i <= x+offset; i++)
		{
			for(int j=y-offset; j <= y+offset; j++)
			{
				if(IsValidCell(i, j)) { piecesToReturn.Add(pieces[i, j]); }
			}
		}
		return piecesToReturn;
	}

    /// <summary>Returns all stones of the same type as the given piece.</summary>
    /// <returns>All stones of a given type.</returns>
    /// <param name="piece">The board piece.</param>
    private List<BoardPiece> GetAllStonesOfType(BoardPiece piece)
	{
        Assert.IsTrue(piece.isStone);

		return GetAllStonesOfType((piece as Stone).type);
	}

    /// <summary>Returns all stones of a given type.</summary>
    /// <returns>All stones of a given type.</returns>
    /// <param name="type">The board piece's type.</param>
	private List<BoardPiece> GetAllStonesOfType(int type)
	{
        Assert.IsTrue(type >= 0 && type < Constants.NUMBER_STONE_TYPES);

		List<BoardPiece> piecesToReturn = new List<BoardPiece>();
		for(int x=0; x < COLUMNS; x++)
		{
			for(int y=0; y < ROWS; y++)
			{
                if(pieces[x, y].isStone && (pieces[x, y] as Stone).type == type){ piecesToReturn.Add(pieces[x, y]); }
			}
		}
		return piecesToReturn;
	}

    /// <summary>Returns all the board pieces.</summary>
	private List<BoardPiece> GetAllPieces()
	{
        IEnumerable<BoardPiece> piecesToReturn = new List<BoardPiece>();
		for(int row=0; row < ROWS; row++)
		{
			piecesToReturn = piecesToReturn.Union(GetPiecesInRow(row));
		}
        return piecesToReturn.ToList();
	}

	#endregion
}
