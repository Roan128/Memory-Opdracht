﻿using Memory.DAL.Services;
using Memory.Model.BusinessObjects;
using System;
using System.Windows;

namespace Memory.GUI;

/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
public partial class GameCreationWindow : Window
{
    private ImageSetService imageSetService { get; set; }

    public GameCreationWindow()
    {
        imageSetService = new ImageSetService();
        InitializeComponent();
        DisplayedSets.ItemsSource = imageSetService.GetImageSets();
    }

    private void StartBtn_Click(object sender, RoutedEventArgs e)
    {
        if (!IsValidNumber(TextBoxCards.Text) || TextBoxName.Text.Equals("") || TextBoxName.Text.Equals(null))
        {
            // Display an error message or take appropriate action
            MessageBox.Show("Invalid number or name entered. Please enter a valid number or name.");
            // Optionally, clear the TextBox or set a default value
            TextBoxCards.Text = "";
        }
        else
        {
            string playername = TextBoxName.Text;
            string cardamount = TextBoxCards.Text;
            Player player = new Player(playername, cardamount);
            Game game = new Game();
            GameWindow gameWindow = new GameWindow(game, player);
            gameWindow.Show();
            Close();
        }
    }

    //Teststatus: 
    private bool IsValidNumber(string input)
    {
        if (int.TryParse(input, out _))
        {
            if (int.Parse(input) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }

    }

    private void CreateSetBtn_Click(object sender, RoutedEventArgs e)
    {
        SetCreationPopup popup = new SetCreationPopup();
        popup.onClose += HandleCustomEvent;
        popup.Show();
    }

    private void HandleCustomEvent(object? sender, EventArgs e)
    {
        DisplayedSets.ItemsSource = imageSetService.GetImageSets();
    }

    private void SetSelect_Click(object sender, RoutedEventArgs e)
    {
        TextBoxCards.IsEnabled = false;
    }
}
