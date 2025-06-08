using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum DoseType
    {
        [Description("Miligrams")]
        MG,
        [Description("Mililiters")]
        ML,
        [Description("Tablet")]
        TAB,
        [Description("Grams")]
        G,
        [Description("Micrograms")]
        MCG

    }
}
