using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winch.Core;

namespace Chaos.Effects;

public class EffectHandler : IEffect
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

    public virtual void Trigger() {}
    public virtual void Cleanup() {}
}
