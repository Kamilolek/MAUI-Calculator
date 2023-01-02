﻿using Calculator.Controllers;

namespace Calculator;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
    }
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 460;
        const int newHeight = 680;

        window.Width = newWidth;
        window.Height = newHeight;

        return window;
    }
}
