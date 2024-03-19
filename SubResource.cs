using Godot;

namespace EditorIssues;

[GlobalClass, Tool]
public partial class SubResource : Resource
{
    private int _counter;

    [Export]
    public int Counter
    {
        get => _counter;
        set
        {
            if (value != _counter)
            {
                _counter = value;
                EmitChanged();
            }
        }
    }
}