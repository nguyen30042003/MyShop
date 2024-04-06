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
        public int PerPage { get; set; } = 4;

        public Tuple<ObservableCollection<object>, ObservableCollection<int>> LoadPage<T>(T obj, int page)
        {
            CurrentPage = page;
            int skipCount = (CurrentPage - 1) * PerPage;
            int takeCount = PerPage;

            ObservableCollection<object> resultData;
            ObservableCollection<int> pageNumbers;

            if (obj is Customer)
            {
                var pageResult = CustomerServiceImpl.Instance.findAllPage(skipCount, takeCount);
                if (TotalItems != pageResult.Item2)
                {
                    totalPage(pageResult.Item2);
                }
                resultData = new ObservableCollection<object>(pageResult.Item1.Cast<object>());
                pageNumbers = new ObservableCollection<int>(Enumerable.Range(1, TotalPage));
            }
            else if (obj is Order)
            {
                var pageResult = OrderServiceImpl.Instance.findAllPage(skipCount, takeCount);
                if (TotalItems != pageResult.Item2)
                {
                    totalPage(pageResult.Item2);
                }
                resultData = new ObservableCollection<object>(pageResult.Item1.Cast<object>());
                pageNumbers = new ObservableCollection<int>(Enumerable.Range(1, TotalPage));
            }
            else
            {
                var pageResult = ProductServiceImpl.Instance.findAllPage(skipCount, takeCount);
                if (TotalItems != pageResult.Item2)
                {
                    totalPage(pageResult.Item2);
                }
                resultData = new ObservableCollection<object>(pageResult.Item1.Cast<object>());
                pageNumbers = new ObservableCollection<int>(Enumerable.Range(1, TotalPage));
            }
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
