using System;
using StructureMap;
using StructureMap.Graph;

namespace StructureMapSample
{
  public class Program
  {
    static void Main(string[] args) {
      var container = new Container();
      container.Configure(config => {
        config.For<IDumpable>().Use<DumpToScreen>();
        config.For<IDumpPeople>().Use<DumpPeople>();
      });

      var steven = new Person { Name = "Steven" };
      var jace = new Person { Name = "Jace" };

      var dumper = container.GetInstance<IDumpPeople>();

      dumper.DumpPerson(steven);
    }
  }

  class Person
  {
    public string Name;
  }

  static class PersonExtensions
  {
    public static IDumpable DefaultDumper = ObjectFactory.GetInstance<IDumpable>();

    public static void Dump(this Person p) {
      DefaultDumper.Dump(p);
    }
  }

  //adapter
  interface IDumpPeople
  {
    void DumpPerson(Person p);
  }

  class DumpPeople : IDumpPeople
  {
    private readonly IDumpable _dumper;

    public DumpPeople(IDumpable dumper) {
      _dumper = dumper;
    }

    public void DumpPerson(Person p) {
      _dumper.Dump(p.Name);
    }
  }

  interface IDumpable
  {
    void Dump(Object p);
  }

  class DumpToScreen : IDumpable
  {
    public void Dump(object o) {
      Console.WriteLine(o);
      Console.ReadKey();
    }
  }

  class DumpToPrinter : IDumpable
  {
    public void Dump(object o) {
      Console.WriteLine(o);
    }

  }

}
