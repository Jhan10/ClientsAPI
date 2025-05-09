namespace ClientsAPI.Model
{
    public interface ISuperuserRepository
    {
        void Add(Superusuario superusuario);
        List<Superusuario> Get();
        void ImportBase(List<Superusuario> superusuarios);

    }
}
