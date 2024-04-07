using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyShopProject.ViewModel
{
    public class Page
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPage { get; set; }
        public int PerPage { get; set; } = 10;

        public Tuple<ObservableCollection<object>, ObservableCollection<int>> LoadPage<T>(T obj, int page)
        {
            CurrentPage = page;
            int skipCount = (CurrentPage - 1) * PerPage;
            int takeCount = PerPage;

            ObservableCollection<object> resultData;
            ObservableCollection<int> pageNumbers;
            var pageResult = Tuple.Create<IEnumerable<object>, int>(null, 0);
            if (obj is Customer)
            {
                var customerPageResult = CustomerServiceImpl.Instance.findAllPage(skipCount, takeCount);
                pageResult = Tuple.Create(customerPageResult.Item1.Cast<object>(), customerPageResult.Item2);
                if (TotalItems != pageResult.Item2)
                {
                    totalPage(pageResult.Item2);
                }
            }
            else if (obj is Order)
            {
                var orderPageResult = OrderServiceImpl.Instance.findAllPage(skipCount, takeCount);
                pageResult = Tuple.Create(orderPageResult.Item1.Cast<object>(), orderPageResult.Item2);
                if (TotalItems != pageResult.Item2)
                {
                    totalPage(pageResult.Item2);
                }
            }
            else
            {
                var productPageResult = ProductServiceImpl.Instance.findAllPage(skipCount, takeCount);
                pageResult = Tuple.Create(productPageResult.Item1.Cast<object>(), productPageResult.Item2);
                if (TotalItems != pageResult.Item2)
                {
                    totalPage(pageResult.Item2);
                }
            }
            resultData = new ObservableCollection<object>(pageResult.Item1);
            pageNumbers = new ObservableCollection<int>(Enumerable.Range(1, TotalPage));
            return Tuple.Create(resultData, pageNumbers);
        }

        private void totalPage(int i)
        {
            TotalItems = i;
            TotalPage = TotalItems / PerPage;
            if (TotalItems % PerPage != 0)
            {
                TotalPage += 1;
            }
        }
    }
}
