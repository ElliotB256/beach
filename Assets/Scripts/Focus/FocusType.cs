using System;

namespace Beach.Focus
{
    public enum FocusType
    {
        None = 1 << 0,
        Buried = 1 << 1,
        Carryable = 1 << 2,
        Test = 1 << 3
    }
}
