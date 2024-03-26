﻿using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    internal interface OrderRepository
    {
        bool create(Order order);
        bool update(Order order);
        bool delete(Order order);
        List<Order> findAll();
        List<Order> findByName(string name);
        Order findById(int id);


        List<Order> sortByCreateDate(DateTime previousDate, DateTime lastDate);
        List<Order> sortByPrice(float minPrice, float maxPrice);
        List<Order> sortByPriceASC();
        List<Order> sortByPriceDesc();
        List<Order> sortByQuantityASC();
        List<Order> sortByQuantityDesc();
    }
}
