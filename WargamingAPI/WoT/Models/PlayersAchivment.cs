using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models
{
    public class PlayersAchivment
    {
        Achievements achievements { get; set; }

        FragsAchivments fragsAchivments { get; set; }

        MaxSeries maxSeries { get; set; }
    }
}
