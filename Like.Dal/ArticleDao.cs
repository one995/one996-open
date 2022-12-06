using Like.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Dao
{
    public class ArticleDao :DbDao , Idao.IArticle
    {
        public int Add(Article article)
        {
            return RWDb.Saveable<Article>(article).ExecuteCommand();
        }

        public int Delete(int ID)
        {
            return RWDb.Updateable<Article>().SetColumns(s => s.Isdelete == 0 ) .Where(s => s.Id == ID).ExecuteCommand();
        }

        public Article GetArticle(int ID)
        {
            return RWDb.Queryable<Article>().Where(s => s.Id == ID).First();
        }

        public List<Article> GetArticles()
        {
            return RWDb.Queryable<Article>().Take(1000).ToList();
        }

        public int Update(Article article)
        {
            return RWDb.Updateable<Article>(article).ExecuteCommand();
        }
    }
}
