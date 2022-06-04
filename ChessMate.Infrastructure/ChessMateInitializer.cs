using ChessMate.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Infrastructure
{
    public class ChessMateInitializer : DropCreateDatabaseIfModelChanges<ChessMateDbContext>
    {
        protected override void Seed(ChessMateDbContext context)
        {
            context.Colors.Add(new Color { Description = "Белый" });
            context.Colors.Add(new Color { Description = "Черный" });

            context.Figures.Add(new Figure() { Code = "п", Description = "Пешка" });
            context.Figures.Add(new Figure() { Code = "С", Description = "Слон" });
            context.Figures.Add(new Figure() { Code = "К", Description = "Конь" });
            context.Figures.Add(new Figure() { Code = "Л", Description = "Ладья" });
            context.Figures.Add(new Figure() { Code = "Ф", Description = "Ферзь" });
            context.Figures.Add(new Figure() { Code = "Кр", Description = "Король" });

            base.Seed(context);
        }
    }
}
