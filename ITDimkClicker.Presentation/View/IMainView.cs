using System;

namespace ITDimkClicker.Presentation.View
{
    public interface IMainView : IView
    {
        event EventHandler OpenMacro;
        event EventHandler SaveMacroAs;
    }
}