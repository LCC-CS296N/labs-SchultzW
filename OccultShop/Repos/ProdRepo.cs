﻿using Midterm.Infrastructure;
using Midterm.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Midterm.Models
{
    public class ProdRepo : IProdRepos
    {
        private AppDbContext context;
       
        public IQueryable<Product> Products
        {
            get
            {
                return context.Products.Include("Reviews");
            }
        }
        public ProdRepo(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public void AddProd(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }
        public Product GetProdByID(int id)
        {
            Product prod = context.Products.Include("Reviews").First(p => p.ProductId == id);
            return prod;
        }
        //public  Product GetProdByTag(string tag)
        //{
        //    Product prod = context.Products.Find(p => p.Tag == tag);
        //    return prod;
        //}
        public int Count()
        {
            return context.Products.Count();
        }
        public void AddReview(Product p, Review r)
        {
            p.Reviews.Add(r);
            context.Products.Update(p);
            context.SaveChanges();
        }

        public Product GetProdByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProd(string id, Product newP)
        {
            var prod = context.Products.Find(id);

            try
            {

                prod.Description = newP.Description;
                prod.ImgPath = newP.ImgPath;
                prod.IsNew = newP.IsNew;
                prod.Price = newP.Price;
                prod.Tag = newP.Tag;
                prod.Title = newP.Title;
                context.SaveChanges();

                return true;
            }
            catch
                { return false; }
        }


        //public void Clear()
        //{
        //    context.Products.clear();
        //}
    }
}