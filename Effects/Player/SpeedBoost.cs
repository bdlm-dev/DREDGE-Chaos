using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winch.Core;

namespace Chaos.Effects.Player;

internal class SpeedBoost : EffectHandler
{
    new public void Trigger()
    {
        WinchCore.Log.Debug("Called speedboost");
    }

    new public void Cleanup()
    {
        WinchCore.Log.Debug("Cleaned up speedboost");
    }
}
