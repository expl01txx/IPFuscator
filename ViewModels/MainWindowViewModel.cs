using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace IPFuscatorUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "IP Obfuscation Tool";
    public string IpItemContent { get; set; } = "";

    [ObservableProperty]
    public string _obfuscatedIpItemContent = "";

    //Obfuscation types
    [ObservableProperty]
    private static string[] _obfuscationTypesItems =
    [
        "Default",
        "Hex",
        "Octal",
        "Advanced",
    ];


    //Value selected in combo box
    private string _selectedObfuscationType = _obfuscationTypesItems[0];
    public string? SelectedObfuscationType
    {
        get => _selectedObfuscationType;
        set
        {
            if (_selectedObfuscationType != value && value != null)
            {
                _selectedObfuscationType = value;
                OnPropertyChanged();
                HandleSelectionChange(_selectedObfuscationType);
            }
        }
    }

    //On select in combobox
    private void HandleSelectionChange(string? selectedItem)
    {
        Obfuscate();
    }

    //Obfuscate command
    [RelayCommand]
    private void Obfuscate()
    {

        switch (SelectedObfuscationType)
        {
            case "Default":
                var obfuscated_data = Obfuscator.ToNumber(IpItemContent);
                if (obfuscated_data != null) ObfuscatedIpItemContent = obfuscated_data;
                break;
            case "Hex":
                obfuscated_data = Obfuscator.ToHex(IpItemContent);
                if (obfuscated_data != null) ObfuscatedIpItemContent = obfuscated_data;
                break;
            case "Octal":
                obfuscated_data = Obfuscator.ToOct(IpItemContent);
                if (obfuscated_data != null) ObfuscatedIpItemContent = obfuscated_data;
                break;
            case "Advanced":
                var oct_data = Obfuscator.ToOct(IpItemContent);
                var hex_data = Obfuscator.ToHex(IpItemContent);

                if (oct_data is null || hex_data is null) return;

                var oct_array = oct_data.Split(".");
                var hex_array = hex_data.Split(".");

                List<string> combined_data = [oct_array[0], hex_array[1], oct_array[2], hex_array[3]];

                ObfuscatedIpItemContent = String.Join(".", combined_data.ToArray());
                break;
        }
    }
    
}
