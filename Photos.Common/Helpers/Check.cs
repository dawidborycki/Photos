#region Using

using System;

#endregion

namespace Photos.Common.Helpers
{
    public static class Check
    {
        #region Methods (Public)

        public static void IsNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }

        #endregion
    }
}
