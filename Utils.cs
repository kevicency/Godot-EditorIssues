using System;
using Godot;

namespace EditorIssues;

public static class Utils
{
    public static bool SetIfChanged<T>(ref T field, T value)
    {
        if (Equals(field, value) || value == null)
        {
            return false;
        }

        field = value;
        return true;
    }

    public static bool SetIfChanged<T>(ref T field, T value, Action<T, T> onChanged)
    {
        var oldValue = field;
        if (!SetIfChanged(ref field, value))
        {
            return false;
        }

        onChanged(value, oldValue);
        return true;
    }

    public static bool SetAndConnect<T>(ref T field, T value, Action onChanged) where T : Resource =>
        SetIfChanged(ref field, value, ((newValue, oldValue) =>
        {
            // throws error without the check
            // Attempt to disconnect a nonexistent connection from '<Resource#-9223365799220023991>'. Signal: 'changed', callable: 'Delegate::Invoke'.
            if (oldValue.IsConnected(Resource.SignalName.Changed, Callable.From(onChanged)))
            {
                oldValue.Changed -= onChanged;
            }

            newValue.Changed += onChanged;
        }));
}