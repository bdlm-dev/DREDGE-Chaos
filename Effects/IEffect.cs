using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaos.Effects;

public interface IEffect
{
    public void Trigger();
    public void Cleanup();
}
