﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public SpriteRenderer sr;
    public float margin;
    public Transform tr;
    [SerializeField]
    private Vector2 pivot;

    private void Start()
    {
        /*
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        */
        pivot = new Vector2(0.5f, 0.5f);
    }

    public Sprite Saw(Vector2 s, Vector2 e)
    {
        // Check that in bounds
        
        Vector3 bmax = sr.sprite.bounds.max + tr.position;
        Vector3 bmin = sr.sprite.bounds.min + tr.position;

        if (s.x > bmax.x || s.x < bmin.x || s.y > bmax.y || s.y < bmin.y)
        {
            return null;
        }

        e.x = Mathf.Clamp(e.x, bmin.x + margin, bmax.x - margin);
        e.y = Mathf.Clamp(e.y, bmin.y + margin, bmax.y - margin);
        

        // Locations and stuff
        Vector2 start = TextureSpaceCoord(tr, s, sr);
        Vector2 end = TextureSpaceCoord(tr, e, sr);

        int actualStartX = 0;
        int actualStartY = 0;

        Debug.Log(string.Format("Sawing from {0} to {1}", start, end));

        if (start.x < end.x) { actualStartX = (int)start.x; }
        else { actualStartX = (int)end.x; }
        if (start.y < end.y) { actualStartY = (int)start.y; }
        else { actualStartY = (int)end.y; }

        var size = new Vector2Int(
                (int)(Mathf.Abs(start.x - end.x)),
                (int)(Mathf.Abs(start.y - end.y)));

        // Texture manipulation
        var tex = CreateCopyTexture(sr.sprite.texture);
        var pixels = tex.GetPixels(actualStartX, actualStartY, size.x, size.y);

        var emptyPixels= CreateEmptyPixels(size.x * size.y);

        Debug.Log(string.Format("Start: {0} {1}, Size: {2} {3}", actualStartX, actualStartY, size.x, size.y));

        tex.SetPixels(actualStartX, actualStartY, size.x, size.y, emptyPixels);
        tex.Apply(false, false);

        Sprite newBoat = Sprite.Create(tex, sr.sprite.rect, pivot);
        sr.sprite = newBoat;

        // Return the stolen piece
        Texture2D planktex = new Texture2D(size.x, size.y);
        planktex.SetPixels(pixels);
        planktex.Apply(false, false);
        Rect rect = new Rect(Vector2.zero, size);
        Sprite plank = Sprite.Create(planktex, rect, pivot);
        return plank;
    }

    static public Vector2 TextureSpaceCoord(Transform tr, Vector3 worldPos, SpriteRenderer sr)
    {
        float ppu = sr.sprite.pixelsPerUnit;
        Vector2 localPos = tr.InverseTransformPoint(worldPos) * ppu;
        var texSpacePivot = new Vector2(sr.sprite.rect.x, sr.sprite.rect.y) + sr.sprite.pivot;
        Vector2 texSpaceCoord = texSpacePivot + localPos;

        return texSpaceCoord;
    }

    static public Texture2D CreateCopyTexture(Texture2D texture)
    {
        var newTex = new Texture2D(texture.width, texture.height, texture.format, false);
        Graphics.CopyTexture(texture, newTex);
        return newTex;
    }

    static public Color[] CreateEmptyPixels(int count)
    {
        var pixels = new Color[count];
        for(int i=0; i<count; i++)
        {
            pixels[i] = new Color(0, 0, 0, 0);
        }
        return pixels;
    }
}