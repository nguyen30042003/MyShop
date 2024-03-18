using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    internal interface Repository<T>
    {
        void create(T o);
        void update(T o);
        void delete(T o);
        List<T> findAll();
        List<T> findByName(string name);
        T findById(int id);
    }
}
