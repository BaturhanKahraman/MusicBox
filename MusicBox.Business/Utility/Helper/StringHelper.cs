using System;
using System.Collections.Generic;
using System.Xml;

namespace MusicBox.Business.Utility.Helper
{
    public static class StringHelper
    {
        public static string ClearQueryParameter(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentNullException(nameof(parameter),"Lütfen parametreyi boş göndermeyin.");
            }
            string newQuery = string.Empty;
            newQuery = parameter.Trim();
            return newQuery;
        }

    }
}