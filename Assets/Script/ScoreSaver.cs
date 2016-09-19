using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class LevelScoreSaver {
    //存档文件数据格式
    //struct{
    //    int score[10];
    //    struct{
    //        int Length;
    //        char name[length];
    //    }name[10];
    //}savegame;
    //

    public int[] score;
    public string[] name;
    public LevelScoreSaver()
    {
        score = new int[10];
        name = new string[10];
        for(int i = 0; i < 10; ++i)
        {
            score[i] = 0;
            name[i] = "null";
        }
    }
    public void AddScore(string _name,int _score)
    {
        int i;
        for (i = 9; i >= 0 && _score > score[i]; --i)
        {
            if (i != 9)
            {
                score[i + 1] = score[i];
                name[i + 1] = name[i];
            }
        }
        ++i;
        if(i<10 && _score > score[i])
        {
            score[i] = _score;
            name[i] = _name;
        }
    }
    public void SaveToFile(FileStream fs)
    {
        byte[] tbyte;
        
        byte[] Lbyte;//需要将tbyte的长度写入文件
       for(int i = 0; i < 10; ++i)
        {
            tbyte = BitConverter.GetBytes(score[i]);
            fs.Write(tbyte, 0, tbyte.Length);
        }
       for(int i = 0; i < 10; ++i)
        {
            tbyte = Encoding.Unicode.GetBytes(name[i]);
            Lbyte = BitConverter.GetBytes(tbyte.Length);
            fs.Write(Lbyte, 0, Lbyte.Length);
            fs.Write(tbyte, 0, tbyte.Length);
        }
    }
    public void ReadFromFile(FileStream fs)
    {
        byte[] tbyte;
        byte[] Lbyte = new byte[sizeof(int)];
        int Length;
        for(int i = 0; i < 10; ++i)
        {
            //这里直接借用Lbyte来接收score了
            fs.Read(Lbyte, 0, sizeof(int));
            score[i] = BitConverter.ToInt32(Lbyte, 0);
        }
        for(int i = 0; i < 10; ++i)
        {
            fs.Read(Lbyte, 0, sizeof(int));
            Length = BitConverter.ToInt32(Lbyte, 0);
            tbyte = new byte[Length];
            fs.Read(tbyte, 0, Length);
            name[i] = Encoding.Unicode.GetString(tbyte);
        }
    }
}
public class ScoreSaver
{
    //数据结构
    //struct data{
    //    KeyCode Up, Down, Left, Right, Turn, OverView;
    //    int NowLevel;
    //    LevelScoreSaver levelScoreSavers[13];
    //}
    public int FirstLevel;
    public LevelScoreSaver [] levelScoreSaver;
    string FilePath;
    public KeyCode Key_Up;
    public KeyCode Key_Down;
    public KeyCode Key_Left;
    public KeyCode Key_Right;
    public KeyCode Key_Turn;
    public KeyCode Key_OverView;
    public ScoreSaver(string _FilePath)
    {
        levelScoreSaver = new LevelScoreSaver[13];
        for (int i = 0; i < 13; ++i)
            levelScoreSaver[i] = new LevelScoreSaver();
        FilePath = _FilePath;
        FileInfo fi = new FileInfo(FilePath);
        if (!fi.Exists)
        {
            //文件不存在，默认构造
            FirstLevel = 1;
            Key_Up = KeyCode.UpArrow;
            Key_Down = KeyCode.DownArrow;
            Key_Left = KeyCode.LeftArrow;
            Key_Right = KeyCode.RightArrow;
            Key_Turn = KeyCode.LeftShift;
            Key_OverView = KeyCode.Space;
            for(int i = 0; i < 13; ++i)
            {
                //这里设置默认分数排行榜，第i关,i=0 to 12.
                for (int j = 12; j>1; --j)
                {
                    levelScoreSaver[i].AddScore("Mr.Default", j * 300);
                }
            }
        }
        else
        {
            byte[] tbyte;
            //文件读取
            FileStream fs = fi.OpenRead();
            fs.Seek(0, SeekOrigin.Begin);

            tbyte = new byte[sizeof(int)];

            fs.Read(tbyte, 0, sizeof(int));
            Key_Up = (KeyCode)BitConverter.ToInt32(tbyte,0);
            fs.Read(tbyte, 0, sizeof(int));
            Key_Down = (KeyCode)BitConverter.ToInt32(tbyte, 0);
            fs.Read(tbyte, 0, sizeof(int));
            Key_Left = (KeyCode)BitConverter.ToInt32(tbyte, 0);
            fs.Read(tbyte, 0, sizeof(int));
            Key_Right = (KeyCode)BitConverter.ToInt32(tbyte, 0);
            fs.Read(tbyte, 0, sizeof(int));
            Key_Turn = (KeyCode)BitConverter.ToInt32(tbyte, 0);
            fs.Read(tbyte, 0, sizeof(int));
            Key_OverView = (KeyCode)BitConverter.ToInt32(tbyte, 0);

            fs.Read(tbyte, 0, sizeof(int));
            FirstLevel = BitConverter.ToInt32(tbyte, 0);

            for(int i = 0; i < 13; ++i)
            {
                levelScoreSaver[i].ReadFromFile(fs);
            }
            fs.Close();
        }
    }
    public void SaveToFile()
    {
        FileInfo fi = new FileInfo(FilePath);
        if (fi.Exists)
            fi.Delete();
        FileStream fs = fi.Open(FileMode.CreateNew);
        byte[] tbyte;

        tbyte = BitConverter.GetBytes((int)Key_Up);
        fs.Write(tbyte, 0, sizeof(int));
        tbyte = BitConverter.GetBytes((int)Key_Down);
        fs.Write(tbyte, 0, sizeof(int));
        tbyte = BitConverter.GetBytes((int)Key_Left);
        fs.Write(tbyte, 0, sizeof(int));
        tbyte = BitConverter.GetBytes((int)Key_Right);
        fs.Write(tbyte, 0, sizeof(int));
        tbyte = BitConverter.GetBytes((int)Key_Turn);
        fs.Write(tbyte, 0, sizeof(int));
        tbyte = BitConverter.GetBytes((int)Key_OverView);
        fs.Write(tbyte, 0, sizeof(int));

        tbyte = BitConverter.GetBytes(FirstLevel);
        fs.Write(tbyte, 0, sizeof(int));
        for(int i = 0; i < 13; ++i)
        {
            levelScoreSaver[i].SaveToFile(fs);
        }
        fs.Close();
    }
}

