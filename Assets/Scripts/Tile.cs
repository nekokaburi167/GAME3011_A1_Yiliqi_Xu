using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private int row;
    private int col;
    private int tileValue;
    private Color tileColor;
    private bool isHidden = true;
    public TileType type = TileType.MIN;
    
    public int Row
    {
        get => row;
        set => row = value;
    }

    public int Col
    {
        get => col;
        set => col = value;
    }
    
    public int Value
    {
        get => tileValue;
        set => this.tileValue = value;
    }
    
    public Color Color
    {
        get => tileColor;
        set => tileColor = value;
    }
    
    public bool IsHidden
    {
        get => isHidden;
        set => isHidden = value;
    }
    
    public TileType Type
    {
        get => type;
        set => type = value;
    }

    private void Start()
    {
        SetTileInfo();
        if (!isHidden) 
            GetComponent<Image>().color = tileColor;
    }

    public void SetTileInfo()
    {
        switch (type)
        {
            case TileType.MIN:
                tileValue = 0;
                tileColor = Color.black;
                break;
            case TileType.QUARTER:
                tileValue = 100;
                tileColor = Color.blue;
                break;
            case TileType.HALF:
                tileValue = 200;
                tileColor = Color.magenta;
                break;
            case TileType.MAX:
                tileValue = 400;
                tileColor = Color.red;
                break;
            default:
                break;
        }
    }

    public void RevealTile()
    {
        isHidden = false;
        GetComponent<Image>().color = tileColor;
    }

    public void OnPointerClicked()
    {
        if (GameManager.Instance.IsScanMode)
        {
            if (GameManager.Instance.ScanCount > 0)
            {
                TileManager.Instance.RevealTiles(row - 1, col - 1, 3);
                GameManager.Instance.ScanCount--;
                GameManager.Instance.UpdateUI();
            }
        }
        else
        {
            if (GameManager.Instance.ExtractCount > 0)
            {
                //RevealTile();
                GameManager.Instance.ExtractCount--;
                GameManager.Instance.Score += tileValue;
                GameManager.Instance.UpdateUI();
                
                if (type != TileType.MIN)
                {
                    type = TileType.MIN;
                    SetTileInfo();
                    RevealTile();
                    
                    TileManager.Instance.DegradeTileValue(row - 2, col - 2, row, col, 5);
                    TileManager.Instance.DegradeTileValue(row - 1, col - 1, row, col, 3);
                }
            }
        }
    }
}
