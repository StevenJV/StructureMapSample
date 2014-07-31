using System;
using StructureMap;
using StructureMap.Graph;

namespace StructureMapSample
{
  public class Program
  {
    static void Main(string[] args) {
      ObjectFactory.Configure(config => {
        config.Scan(scan => {
          scan.TheCallingAssembly();
          scan.WithDefaultConventions();
        });
        config.For<IDumpable>().Use<DumpToScreen>();
      });

      var steven = new Person { Name = "Steven" };
      var jace = new Person { Name = "Jace" };

      var dumper = ObjectFactory.GetInstance<IDumpable>();

      dumper.Dump(steven);
    }
  }

  class Person
  {
    public string Name;
  }



  interface IDumpable
  {
    void Dump(Person p);
  }

  class DumpToScreen : IDumpable
  {
    public void Dump(Person p) {
      Console.WriteLine(p.Name);
      Console.ReadKey();
    }
  }

  class DumpToPrinter : IDumpable
  {
    public void Dump(Person p) {
      Console.WriteLine(p.Name);
    }

  }

}
