using ClientsAPI.Model;
using ClientsAPI.ViewModel;
using static ClientsAPI.Infraesctruture.ConextionContext;

namespace ClientsAPI.Infraesctruture
{
    public class SuperuserRepository : ISuperuserRepository
    {
        private readonly ConectionContext _conectionContext = new ConectionContext();
        public void Add(Superusuario superusuarioVM)
        {
            _conectionContext.superusuarios.Add(superusuarioVM);
            _conectionContext.SaveChanges();
        }

        public List<Superusuario> Get()
        {
            return _conectionContext.superusuarios.ToList();
        }

        public void ImportBase(List<Superusuario> superusuarios)
        {
            foreach (Superusuario superusuario in superusuarios)
            {
                _conectionContext.superusuarios.Add(superusuario);
                _conectionContext.SaveChanges();
            }
        }
    }
}
