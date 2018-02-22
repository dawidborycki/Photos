#region Using

using System;

#endregion

namespace Photos.iOS.WatchKitExtension
{
    public class RowSelection
    {
        #region Properties

        public int BeginIndex { get; private set; }
        public int EndIndex { get; private set; }

public int RowCount { get; private set; }
public string Title { get; private set; }

        #endregion

        #region Fields

        private static string titlePrefix = "Elements to show:";

        #endregion

        #region Constructor

        public RowSelection(int groupIndex, int rowsPerGroup, int elementCount)
        {
            BeginIndex = groupIndex * rowsPerGroup;
            EndIndex = Math.Min((groupIndex + 1) * rowsPerGroup, elementCount) - 1;

            RowCount = EndIndex - BeginIndex + 1;
            Title = $"{titlePrefix} {BeginIndex}-{EndIndex}";
        }

        #endregion
    }
}
