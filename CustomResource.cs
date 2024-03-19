using Godot;

namespace EditorIssues;

[GlobalClass, Tool]
public partial class CustomResource : Resource
{
    private SubResource _subResource = new();

    [Export]
    public SubResource SubResource
    {
        get => _subResource;
        set => Utils.SetAndConnect(ref _subResource, value, SubResourceChanged);
    }
    
    private void SubResourceChanged()
    {
        GD.Print("[CustomResource] SubResource changed");
        EmitChanged();
    }
}