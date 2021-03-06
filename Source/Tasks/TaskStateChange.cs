﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Tasks
{
    /// <summary>
    /// Represents the method that gets called from a <see cref="Task"/> when it has progress or state change
    /// </summary>
    /// <param name="task"><see cref="Task"/> that changed</param>
    public delegate void TaskStateChange(Task task);
}
