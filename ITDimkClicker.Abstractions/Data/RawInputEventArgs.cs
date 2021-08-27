﻿using System;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.Abstractions.Data
{
    public class RawInputEventArgs : EventArgs
    {
        public RawInputEventArgs(RawInputData data)
        {
            Data = data;
        }

        public RawInputData Data { get; }
    }
}
