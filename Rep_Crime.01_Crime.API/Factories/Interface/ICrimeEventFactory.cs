using Rep_Crime._01_Crime.API.Models;

namespace Rep_Crime._01_Crime.API.Factories.Interface
{
    public interface ICrimeEventFactory
    {
        CrimeEvent Create(CrimeEventRequest crimeEventRequest);
    }
}