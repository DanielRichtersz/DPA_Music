﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands.EditorChain
{
    public interface IEditorHotkeyLink
    {
        bool ProcessKey(EditorAction key);
    }
}
