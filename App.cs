using Godot;
using System;
using EditorIssues;

[Tool]
public partial class App : Control
{
    private CustomResource _customResource = new();

    [Export]
    private CustomResource CustomResource
    {
        get => _customResource;
        set
        {
            // Utils.SetAndConnect(ref _customResource, value, CustomResourceChanged);
            
            // this gives multiple UpdatUI calls when changing the counter
            if (Utils.SetIfChanged(ref _customResource, value))
            {
                _customResource.Changed += UpdateUI;
            }
            UpdateUI();
        }
    }

    public override void _Ready()
    {
        base._Ready();
        UpdateUI();
    }

    private void UpdateUI()
    {
        GD.Print("[App] UpdateUI");
        if (IsInsideTree())
        {
            GetNode<Label>("%Label").Text = $"Counter: {_customResource.SubResource.Counter}";
        }
    }

    public void OnAdd()
    {
        _customResource.SubResource.Counter += 1;
    }
    
    public void OnRemove()
    {
        _customResource.SubResource.Counter -= 1;
    }
}
