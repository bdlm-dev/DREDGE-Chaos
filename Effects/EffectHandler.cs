using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaos.Effects;

internal class EffectHandler : IEffect
{
    public void Handle(bool flag)
    {
        if (flag)
        {
            Cleanup();
            return;
        }
        Trigger();
        return;
    }

    public void Trigger() {}
    public void Cleanup() {}
}
