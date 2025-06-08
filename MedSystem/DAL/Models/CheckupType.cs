using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum CheckupType
    {
        [Description("Opći tjelesni pregled")]
        GP = 1,
        [Description("Test krvi")]
        KRV = 2,
        [Description("Rendgensko skeniranje")]
        X_RAY = 3,
        [Description("CT sken")]
        CT = 4,
        [Description("MRI sken")]
        MR = 5,
        [Description("Ultrazvuk")]
        ULTRA = 6,
        [Description("Elektrokardiogram")]
        EKG = 7,
        [Description("Ehokardiogram")]
        ECHO = 8,
        [Description("Pregled očiju")]
        EYE = 9,
        [Description("Dermatološki pregled")]
        DERM = 10,
        [Description("Pregled zuba")]
        DENTA = 11,
        [Description("Mamografija")]
        MAMMO = 12,
        [Description("Neurološki pregled")]
        NEURO = 13
    }

}
