using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(false)]
[assembly: Guid("95B2AE1E-E0DC-4306-8431-D81ED10A2D5D")]

#if !SIGNED
[assembly: InternalsVisibleTo(@"Modbus.UnitTests")]
[assembly: InternalsVisibleTo(@"DynamicProxyGenAssembly2")]
#endif