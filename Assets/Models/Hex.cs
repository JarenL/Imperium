using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Hex class defines the grid position, world space position, size,
/// neighbors etc.... of a Hex Tile. Does NOT interact with Unity.
/// </summary>

public class Hex
{
	// Q + R + S = 0

	public readonly int Q; // Column
	public readonly int R; // Row
	public readonly int S;
	float radius = 1f;
	bool allowWrapEastWest = true;
	bool allowWrapNorthSouth = false;

	static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

	public Hex(int q, int r)
	{
		this.Q = q;
		this.R = r;
		this.S = -(q + r);
	}

	/// <summary>
	/// Returns the world-space position of this Hex.
	/// </summary>
	/// <returns></returns>
	public Vector3 Position()
	{
		float width = HexWidth();
		float horizontal_spacing = width * (this.Q + this.R / 2f);
		float vertical_spacing = HexHeight() * 0.75f * this.R;


		return new Vector3(
			horizontal_spacing,
			0,
			vertical_spacing
		);
	}

	public float HexHeight()
	{
		return radius * 2;
	}

	public float HexWidth()
	{
		return WIDTH_MULTIPLIER * HexHeight();
	}

	public float HexVerticalSpacing()
	{
		return HexHeight() * 0.75f;
	}

	public float HexHorizontalSpacing()
	{
		return HexWidth();
	}

	public Vector3 PositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns)
	{
		float mapHeight = numRows * HexVerticalSpacing();
		float mapWidth = numColumns * HexHorizontalSpacing();
		Vector3 position = Position();

        if (allowWrapEastWest)
        {
            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;

            if (howManyWidthsFromCamera > 0)
            {
                howManyWidthsFromCamera += 0.5f;
            }
            else
            {
                howManyWidthsFromCamera -= 0.5f;
            }

            int howManyWidthsToFix = (int)howManyWidthsFromCamera;

            position.x -= howManyWidthsToFix * mapWidth;
        }

        if (allowWrapNorthSouth)
		{ 
            float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapHeight ;

            if (howManyHeightsFromCamera > 0)
            {
                howManyHeightsFromCamera += 0.5f;
            }
            else
            {
                howManyHeightsFromCamera -= 0.5f;
            }

            int howManyHeightsToFix = (int) howManyHeightsFromCamera;

            position.z -= howManyHeightsToFix * mapHeight;
        }
		return position;
	}

}
