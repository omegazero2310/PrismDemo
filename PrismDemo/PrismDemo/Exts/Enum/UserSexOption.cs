using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismDemo.Exts.Enum
{
    [StoreAsText]
    public enum UserGenderOption
    {
        Male = 0,
        Female = 1,
        Other = 2
    }
}
