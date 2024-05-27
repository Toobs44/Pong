using Godot;
using System;
using System.Net.Http;

public partial class Main : Sprite2D
{
    //export paddle speed?
    public int[] score = {0, 0};// 0:Player, 1:CPU
    public const int PADDLE_SPEED = 500;


      public override void _Ready()
    {
        GD.Print("This is a message at ready!");
        GetNode<CanvasLayer>("PauseMenu").Hide();
    }


    public void OnPauseMenuRestart()
    {
        score[0] = 0;
        score[1] = 0;
        GetNode<Label>("HUD/CPUScore").Text = score[1].ToString();
        GetNode<Label>("HUD/PlayerScore").Text = score[0].ToString();
        GetNode<Ball>("Ball").NewBall();
        // GetNode<CanvasLayer> ("PauseMenu").Hide();
        // GetTree().Paused = false;
    }

    public void OnPauseMenuContinue()
    {
        GetNode<CanvasLayer> ("PauseMenu").Hide();
        GetTree().Paused = false;
    }

    public void OnBallTimerTimeout()
    {
        GetNode<Ball>("Ball").NewBall();
    }

    public void OnScoreLeftBodyEntered(Node2D body)
    {
        score[1]++;
        GetNode<Label>("HUD/CPUScore").Text = score[1].ToString();
        GetNode<Timer>("BallTimer").Start();
    }

    // player scores a point
     public void OnScoreRightBodyEntered(Node2D body)
    {
        score[0]++;
        GetNode<Label>("HUD/PlayerScore").Text = score[0].ToString();
        GetNode<Timer>("BallTimer").Start();
    }

}
