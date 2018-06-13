/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 1000)]
public class Level : ScriptableObject
{
	/// <summary>Maximum number of moves allowed.</summary>
    [Range(InspectorConstants.LEVEL_MOVES_MINIMUM_NUMBER_OF, InspectorConstants.LEVEL_MOVES_MAXIMUM_NUMBER_OF)] public int maximumNumberMoves = 5;
	/// <summary>Score to achieve 1 Star rating.</summary>
    [Range(InspectorConstants.LEVEL_SCORE_MINIMUM_TO_OBTAIN_STARS, InspectorConstants.LEVEL_SCORE_MAXIMUM_TO_OBTAIN_STARS)] public int scoreToAchieve1Star = 100;
	/// <summary>Score to achieve 2 Star rating.</summary>
    [Range(InspectorConstants.LEVEL_SCORE_MINIMUM_TO_OBTAIN_STARS, InspectorConstants.LEVEL_SCORE_MAXIMUM_TO_OBTAIN_STARS)] public int scoreToAchieve2Star = 200;
	/// <summary>Score to achieve 3 Star rating.</summary>
    [Range(InspectorConstants.LEVEL_SCORE_MINIMUM_TO_OBTAIN_STARS, InspectorConstants.LEVEL_SCORE_MAXIMUM_TO_OBTAIN_STARS)] public int scoreToAchieve3Star = 300;
    /// <summary>The level's probability distribution of stones.</summary>
    [HideInInspector] public float[] stonesDistribution = new float[Constants.NUMBER_STONE_TYPES].Repeat(1f / (Constants.NUMBER_STONE_TYPES * 1f)); //each element is intially chosen so that the array sums to 1
    /// <summary>The points awarded for each stone.</summary>
    [HideInInspector] public int[] stonesPoints = new int[Constants.NUMBER_STONE_TYPES].Repeat(10); //each element is intially 10

    /// <summary>Returns the string for a cell on the initial game board.</summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
	private string StringForCell(int x, int y)
	{
		return rows[GameBoard.ROWS-1-y].row[x];
	}

    /// <summary>Determines if a given position is a valid cell (i.e. isn't marked as x).</summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
	public bool IsValidCell(int x, int y)
	{
		return StringForCell(x, y) != "x" && StringForCell(x, y) != "X";
	}

    /// <summary>Determines if a given cell is a stone.</summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
	public DATuple<bool, int> IsStone(int x, int y)
	{
		if(IsValidCell(x, y))
		{
			int stoneIndex;
			if(System.Int32.TryParse(StringForCell(x, y), out stoneIndex))
			{
				if(stoneIndex >= 0 && stoneIndex < Constants.NUMBER_STONE_TYPES)
				{
					return new DATuple<bool, int> (true, stoneIndex);
				}
			}
		}
		return new DATuple<bool, int>(false, -1);
	}

    /// <summary>A serializeable 2d string array.</summary>
	[System.Serializable]
	public class TwoDimensionalStringArray
	{
        /// <summary>An array of rows.</summary>
		public string[] row = new string[GameBoard.COLUMNS];
	}

    /// <summary>A 2d string array used to represent the inital game board.</summary>
    [HideInInspector] public TwoDimensionalStringArray[] rows = new TwoDimensionalStringArray[GameBoard.ROWS];

	#if UNITY_EDITOR

    /// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
	private void OnValidate()
	{
        Assert.IsTrue(scoreToAchieve2Star > scoreToAchieve1Star && scoreToAchieve3Star > scoreToAchieve2Star);
        Assert.IsTrue(stonesDistribution.Sum() == 1f, string.Format("Level: {0}. stonesDistribution should sum to one.", name));
		Assert.IsTrue(stonesDistribution.Length == Constants.NUMBER_STONE_TYPES);
		Assert.IsTrue(stonesPoints.Length == Constants.NUMBER_STONE_TYPES);
        for (int i = 0; i < Constants.NUMBER_STONE_TYPES; i++)
        {
            Assert.IsTrue(stonesDistribution[i] >= 0 && stonesDistribution[i] <= 1, string.Format("Level: {0}. stonesDistribution element {1}: expected 0 <= value <= 1.", name, i));
            Assert.IsTrue(stonesPoints[i] >= 0, string.Format("Level: {0}. stonesPoints element {1}: expected value >= 0.", name, i));
        }

	}

    /// <summary>EDITOR ONLY: A method used to manually call OnValidate.</summary>
    public void EDITOR_ManualOnValidate()
    {
        OnValidate();
    }

	#endif
}
