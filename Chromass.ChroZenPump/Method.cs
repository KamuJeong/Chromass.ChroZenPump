using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromass.ChroZenPump.APIs;

namespace Chromass.ChroZenPump;
public class Method
{
    public Information Information { get; }
    public Configuration Configuration { get; }
    public Setup Setup  { get; }

    public Method(Information information, Configuration configuration, Setup setup)
    {
        Information = information;
        Configuration = configuration;
        Setup = setup;
    }
}
