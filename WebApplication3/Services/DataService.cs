using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class DataService
    {
        public List<Hajo> Hajok { get; private set; }
        public List<Tura> Turak { get; private set; }

        public DataService() 
        {
            Hajok = BeolvasHajok("Data/hajo.txt");
            Turak = BeolvasTurak("Data/tura.txt");
        }

        private List<Hajo> BeolvasHajok(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Select(sor =>
                {
                    var d = sor.Split('\t');
                    return new Hajo
                    {
                        Azon = int.Parse(d[0]),
                        Nev = d[1],
                        Tipus = d[2],
                        Ferohely = int.Parse(d[3])
                    };
                }).ToList();
        }

        private List<Tura> BeolvasTurak(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Select(sor =>
                {
                    var d = sor.Split('\t');
                    return new Tura
                    {
                        HajoAzon = int.Parse(d[0]),
                        Nap = int.Parse(d[1]),
                        Szemely = int.Parse(d[2])
                    };
                }).ToList();
        }
    }
}
