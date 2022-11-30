using CollectionsOfMine.Data.Context;
using CollectionsOfMine.Data.Models;

namespace CollectionsOfMine.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CollectionnsOfMineDbContext context)
        {
            SeedAreas(context);
            SeedTemplates(context);
            SeedContentTypes(context);
        }

        private static void SeedTemplates(CollectionnsOfMineDbContext context)
        {
            if (!context.Templates.Any(c => c.Name == "Table"))
            {
                var template = new Template()
                {
                    Name = "Table",
                    CreatedOn = DateTime.Now
                };

                context.Add(template);
            }
            if (!context.Templates.Any(c => c.Name == "Tiles"))
            {
                var template = new Template()
                {
                    Name = "Tiles",
                    CreatedOn = DateTime.Now
                };

                context.Add(template);
            }
            if (!context.Templates.Any(c => c.Name == "Timeline"))
            {
                var template = new Template()
                {
                    Name = "Timeline",
                    CreatedOn = DateTime.Now
                };

                context.Add(template);
            }

            context.SaveChanges();
        }

        private static void SeedContentTypes(CollectionnsOfMineDbContext context)
        {
            if (!context.ContentTypes.Any(c => c.Name == "Document"))
            {
                var contentType = new ContentType()
                {
                    Name = "Documents",
                    CreatedOn = DateTime.Now
                };

                context.Add(contentType);
            }
            if (!context.ContentTypes.Any(c => c.Name == "Picture"))
            {
                var contentType = new ContentType()
                {
                    Name = "Pictures",
                    CreatedOn = DateTime.Now
                };

                context.Add(contentType);
            }
            if (!context.ContentTypes.Any(c => c.Name == "Video"))
            {
                var contentType = new ContentType()
                {
                    Name = "Videos",
                    CreatedOn = DateTime.Now
                };

                context.Add(contentType);
            }
            if (!context.ContentTypes.Any(c => c.Name == "Custom"))
            {
                var contentType = new ContentType()
                {
                    Name = "Custom",
                    CreatedOn = DateTime.Now
                };

                context.Add(contentType);
            }

            context.SaveChanges();
        }

        private static void SeedAreas(CollectionnsOfMineDbContext context)
        {
            if (!context.Areas.Any(c => c.Name == "Art"))
            {
                var area = new Area()
                {
                    Name = "Art",
                    CreatedOn = DateTime.Now
                };

                context.Add(area);
            }

            if (!context.Areas.Any(c => c.Name == "Blogs"))
            {
                var area = new Area()
                {
                    Name = "Blogs",
                    CreatedOn = DateTime.Now
                };

                context.Add(area);
            }

            if (!context.Areas.Any(c => c.Name == "Books"))
            {
                var area = new Area()
                {
                    Name = "Books",
                    CreatedOn = DateTime.Now
                };

                context.Add(area);
            }

            if (!context.Areas.Any(c => c.Name == "Journal"))
            {
                var area = new Area()
                {
                    Name = "Journal",
                    CreatedOn = DateTime.Now
                };

                context.Add(area);
            }

            if (!context.Areas.Any(c => c.Name == "Movies"))
            {
                var area = new Area()
                {
                    Name = "Movies",
                    CreatedOn = DateTime.Now
                };

                context.Add(area);
            }

            if (!context.Areas.Any(c => c.Name == "Pictures"))
            {
                var area = new Area()
                {
                    Name = "Pictures",
                    CreatedOn = DateTime.Now
                };

                context.Add(area);
            }

            if (!context.Areas.Any(c => c.Name == "Posts"))
            {
                var area = new Area()
                {
                    Name = "Posts",
                    CreatedOn = DateTime.Now
                };

                context.Add(area);
            }

            context.SaveChanges();
        }
    }
}
