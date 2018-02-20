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

        public int RowCount { get => EndIndex - BeginIndex + 1; }
        public string Title { get => $"{titlePrefix} {BeginIndex}-{EndIndex}"; }

        #endregion

        #region Fields

        private static string titlePrefix = "Elements to show:";

        #endregion

        #region Constructors

        public RowSelection(int beginIndex = 0, int endIndex = 10)
        {
            BeginIndex = beginIndex;
            EndIndex = endIndex;
        }

        public RowSelection(int groupIndex, int rowsPerGroup, int elementCount)
        {
            BeginIndex = groupIndex * rowsPerGroup;
            EndIndex = Math.Min((groupIndex + 1) * rowsPerGroup, elementCount) - 1;
        }

        #endregion

        #region Methods (Public)

        public static RowSelection Parse(string title)
        {
            try
            {
                var titleWithoutPrefix = title.Substring(titlePrefix.Length);
                var strIndices = titleWithoutPrefix.Split('-');

                var beginIndex = int.Parse(strIndices[0]);
                var endIndex = int.Parse(strIndices[1]);

                return new RowSelection(beginIndex, endIndex);
            }
            catch (Exception)
            {
                return new RowSelection();
            }
        }

        #endregion
    }
}
