﻿using Lab3.ProcessedObjects;
using Lab3.SystemElements;

namespace Lab3.NextElementPickers
{
    internal class NextElementSinglePicker : INextElementPicker
    {
        private readonly Process? nextElement;

        public NextElementSinglePicker(Process? _nextElement) { nextElement = _nextElement; }

        public Process? GetNextElement(IProcessedObject obj)
        {
            return nextElement;
        }
    }
}
