using MyShopProject.Service;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.ViewModel {
    internal class ProductInfoVM : BaseViewModel {
        ProductService productService = new ProductServiceImpl();

        public ProductInfoVM(int id) {
            
        }
    }
}
