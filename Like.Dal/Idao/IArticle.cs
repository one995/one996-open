using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Dao.Idao
{
    interface IArticle
    {
        int Add(Like.Model.Models.Article article);

        int Delete(int ID);

        int Update(Model.Models.Article article);

        Model.Models.Article GetArticle(int ID);

        List<Model.Models.Article> GetArticles();


    }
}
