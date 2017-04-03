using System;
using UnityEngine;

using ProtoTurtle.BitmapDrawing;

public class DrawScript : MonoBehaviour
{
    private int width;
    private int height;

    private Texture2D texture;

    private int oldX = -1;
    private int oldY = -1;

    // 停留时间
    private float sleepTime;

    private MouseHook mouseHook;

    void Start()
    {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;

        texture = new Texture2D(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                texture.SetPixel(i, j, Color.clear);
            }
        }
        texture.Apply();

        this.mouseHook = new MouseHook();
        this.mouseHook.hook();

        oldX = this.mouseHook.getPointX();
        oldY = this.mouseHook.getPointY();

        sleepTime = 0;
    }

    void Update()
    {
        if (!mouseHook.MouseHooked)
        {
            return;
        }

        int x = this.mouseHook.getPointX();
        int y = this.mouseHook.getPointY();

        if (Math.Abs(x - oldX + y - oldY) <= 10)
        {
            sleepTime += Time.deltaTime;
        }
        else
        {
            if (sleepTime >= 5)
            {
                texture.DrawFilledCircle(oldX, oldY, (int)(Math.Log(sleepTime) * width / 85 / 6), Color.black);
            }

            texture.DrawLine(oldX, oldY, x, y, Color.black);

            sleepTime = 0;

            oldX = x;
            oldY = y;
        }

        texture.Apply();
    }

    private void OnDestroy()
    {
        this.mouseHook.unhook();
    }

    void OnGUI()
    {
        if (GlobalValue.Instance.DebugInfo)
        {
            GUILayout.Label("窗口大小： " + Screen.width + "x" + Screen.height);

            GUILayout.Label("屏幕分辨率： " + Screen.currentResolution.width + "x" + Screen.currentResolution.height);

            GUILayout.Label("鼠标位置： " + oldX + "," + oldY);
        }
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }

}
