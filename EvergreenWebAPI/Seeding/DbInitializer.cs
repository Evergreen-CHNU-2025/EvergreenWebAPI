using EvergreenWebAPI.Models;

namespace EvergreenWebAPI.Seeding;

/// <summary>
/// Provides methods for initializing and seeding the application's database.
/// </summary>
public static class DbInitializer
{
    #region Constants

    private const string DefaultDropboxFolderName = "Flowers";

    #endregion

    #region Public logic

    /// <summary>
    /// Seeds the database with initial data if it is empty.
    /// </summary>
    /// <param name="dbContext">The application's database context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task Seed(ApplicationDbContext dbContext)
    {
        if (!dbContext.Flowers.Any())
        {
            dbContext.Flowers.AddRange(new List<Flower>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Анемона",
                    NameLat = "Anemone",
                    Description = "Ніжна весняна квітка, часто блакитних або білих відтінків.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Anemone.png",
                    Symbolics = "Надія, очікування", Meaning = "Крихкість та швидкоплинність"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Айстра",
                    NameLat = "Aster",
                    Description = "Осіння квітка з яскравими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Aster.png",
                    Symbolics = "Мудрість, елегантність",
                    Meaning = "Терпіння і віра"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Бегонія",
                    NameLat = "Begonia",
                    Description = "Популярна декоративна рослина з різноманітними формами листя.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Begonia.png",
                    Symbolics = "Обережність",
                    Meaning = "Гармонія та добробут"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Гайлардія",
                    NameLat = "Blanket Flower",
                    Description = "Яскрава квітка червоно-жовтих тонів.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Blanket-Flower.png",
                    Symbolics = "Життєва енергія",
                    Meaning = "Оптимізм"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Ромашка",
                    NameLat = "Chamomile",
                    Description = "Лікувальна квітка з білими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Chamomile.png",
                    Symbolics = "Спокій",
                    Meaning = "Зцілення та мир"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Хризантема",
                    NameLat = "Chrysanthemum",
                    Description = "Пізня осіння квітка з великими суцвіттями.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Chrysanthemum.png",
                    Symbolics = "Довголіття",
                    Meaning = "Вірність"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Мак польовий",
                    NameLat = "Corn Poppy",
                    Description = "Яскрава червона польова квітка.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Corn-Poppy.png",
                    Symbolics = "Памʼять",
                    Meaning = "Сон та спогади"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Волошка",
                    NameLat = "Cornflower",
                    Description = "Синя степова квітка.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Cornflower.png",
                    Symbolics = "Вірність",
                    Meaning = "Незалежність"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Космея",
                    NameLat = "Cosmos",
                    Description = "Делікатна квітка з довгими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Cosmos.png",
                    Symbolics = "Гармонія",
                    Meaning = "Чистота почуттів"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Крокус",
                    NameLat = "Crocus",
                    Description = "Одна з перших весняних квітів.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Crocus.png",
                    Symbolics = "Весна, оновлення",
                    Meaning = "Надія"
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Жоржина",
                    NameLat = "Dahlia",
                    Description = "Великі декоративні квіти різних кольорів.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Dahlia.png",
                    Symbolics = "Вишуканість",
                    Meaning = "Сила характеру"
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Маргаритка", 
                    NameLat = "Daisy", 
                    Description = "Маленька біла квітка з жовтою серединкою.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Daisy.png",
                    Symbolics = "Невинність", 
                    Meaning = "Щирість" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Кульбаба", 
                    NameLat = "Dandelion", 
                    Description = "Жовта весняна квітка з пухнастим насінням.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Dandelion.png",
                    Symbolics = "Свобода", 
                    Meaning = "Безтурботність"
                },
                new() 
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Дельфіній",
                    NameLat = "Delphinium",
                    Description = "Висока стеблова квітка синіх відтінків.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Delphinium.png",
                    Symbolics = "Гідність",
                    Meaning = "Захист"
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Шипшина", 
                    NameLat = "Dog Rose", 
                    Description = "Дика троянда з рожевими квітами.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Dog-Rose.png",
                    Symbolics = "Сила та простота", 
                    Meaning = "Природна краса" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Ехінацея", 
                    NameLat = "Echinacea", 
                    Description = "Лікувальна рослина з рожевими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Echinacea.png",
                    Symbolics = "Здоровʼя", 
                    Meaning = "Стійкість" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Наперстянка", 
                    NameLat = "Foxglove", 
                    Description = "Колокольчасті квіти на високому стеблі.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Foxglove.png",
                    Symbolics = "Таємничість", 
                    Meaning = "Двоякість" 
                },      
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Фуксія", 
                    NameLat = "Fuchsia", 
                    Description = "Яскрава квітка рожево-фіолетових тонів.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Fuchsia.png",
                    Symbolics = "Елегантність", 
                    Meaning = "Життєрадісність" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Герань", 
                    NameLat = "Geranium", 
                    Description = "Популярна кімнатна та садова квітка.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Geranium.png",
                    Symbolics = "Затишок", 
                    Meaning = "Домашнє тепло" 
                },
                new()
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Ромашка німецька", 
                    NameLat = "German Chamomile", 
                    Description = "Ароматна лікувальна рослина.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/German-Chamomile.png",
                    Symbolics = "Спокій", 
                    Meaning = "Зцілення"
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Гібіскус", 
                    NameLat = "Hibiscus",
                    Description = "Екзотична квітка з великими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Hibiscus.png",
                    Symbolics = "Пристрасть", 
                    Meaning = "Краса" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Мальва", 
                    NameLat = "Hollyhock", 
                    Description = "Висока квітка з великими бутонами.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Hollyhock.png",
                    Symbolics = "Гордість", 
                    Meaning = "Велич" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Гіацинт", 
                    NameLat = "Hyacinth", 
                    Description = "Весняна квітка з ароматним цвітом.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Hyacinth.png",
                    Symbolics = "Відродження", 
                    Meaning = "Щирі почуття" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Бальзамін",
                    NameLat = "Impatiens",
                    Description = "Тендітна рослина з яскравими квітами.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Impatiens.png",
                    Symbolics = "Ніжність",
                    Meaning = "Вразливість" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Ірис",
                    NameLat = "Iris",
                    Description = "Багаторічна квітка з фігурними пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Iris.png",
                    Symbolics = "Мудрість",
                    Meaning = "Вірність" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Лаванда",
                    NameLat = "Lavender",
                    Description = "Ароматна лікарська рослина.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Lavender.png",
                    Symbolics = "Спокій",
                    Meaning = "Чистота думок"
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Ландиш", 
                    NameLat = "Lily of the Valley", 
                    Description = "Білі дзвіночки з ніжним ароматом.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Lily-of-the-Valley.png",
                    Symbolics = "Невинність",
                    Meaning = "Скромність"
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Ніжність", 
                    NameLat = "Love-in-a-Mist", 
                    Description = "Ажурна квітка з тонкими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Love-in-a-Mist.png",
                    Symbolics = "Таємниця", 
                    Meaning = "Сховані почуття" 
                },
                new()
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Люпин",
                    NameLat = "Lupine",
                    Description = "Висока декоративна рослина у формі колосу.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Lupine.png",
                    Symbolics = "Упевненість",
                    Meaning = "Сміливість"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Мальва польова",
                    NameLat = "Mallow",
                    Description = "Дика квітка рожевих відтінків.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Mallow.png",
                    Symbolics = "Жіночність",
                    Meaning = "Турбота"
                },
                new()
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Чорнобривці", 
                    NameLat = "Marigold", 
                    Description = "Помаранчеві ароматні квіти.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Marigold.png",
                    Symbolics = "Сонце", 
                    Meaning = "Радість" 
                },
                new()
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Нарцис",
                    NameLat = "Narcissus",
                    Description = "Весняна цибулинна рослина.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Narcissus.png",
                    Symbolics = "Самолюбство",
                    Meaning = "Нова надія"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Настурція",
                    NameLat = "Nasturtium",
                    Description = "Квітка з круглим листям та яскравими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Nasturtium.png",
                    Symbolics = "Мужність",
                    Meaning = "Упевненість"
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Анютина квітка", 
                    NameLat = "Pansy", 
                    Description = "Квітка з характерним “обличчям”.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Pansy.png",
                    Symbolics = "Думки", 
                    Meaning = "Роздуми" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "М'ята перцева", 
                    NameLat = "Peppermint", 
                    Description = "Запашна лікарська рослина.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Peppermint.png",
                    Symbolics = "Очищення", 
                    Meaning = "Свіжість" 
                },
                new()
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Петуния",
                    NameLat = "Petunia",
                    Description = "Популярна садова квітка в підвісних композиціях.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Petunia.png",
                    Symbolics = "Спокій",
                    Meaning = "Довіра"
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Флокс", 
                    NameLat = "Phlox", 
                    Description = "Яскраві кущові квіти з приємним ароматом.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Phlox.png",
                    Symbolics = "Єдність", 
                    Meaning = "Вірність" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Примула", 
                    NameLat = "Primrose", 
                    Description = "Одна з перших весняних квітів.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Primrose.png",
                    Symbolics = "Молодість",
                    Meaning = "Надія"
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Шавлія", 
                    NameLat = "Sage", 
                    Description = "Ароматна лікарська рослина.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Sage.png",
                    Symbolics = "Мудрість", 
                    Meaning = "Захист" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Скабіоза", 
                    NameLat = "Scabiosa", 
                    Description = "Ніжна квітка пастельних тонів.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Scabiosa.png",
                    Symbolics = "Любов", 
                    Meaning = "Чуттєвість" 
                },
                new()
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Левиний зів",
                    NameLat = "Snapdragon",
                    Description = "Квітка з характерною формою “пащі”.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Snapdragon.png",
                    Symbolics = "Мужність", 
                    Meaning = "Захист"
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Очиток", 
                    NameLat = "Stonecrop", 
                    Description = "Сукулентна рослина з густими листями.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Stonecrop.png",
                    Symbolics = "Стійкість", 
                    Meaning = "Витривалість" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Соняшник", 
                    NameLat = "Sunflower", 
                    Description = "Велика яскрава квітка, що повертається за сонцем.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Sunflower.png",
                    Symbolics = "Вірність", 
                    Meaning = "Життя" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Корепсис", 
                    NameLat = "Tickseed", 
                    Description = "Американська польова квітка.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Tickseed.png",
                    Symbolics = "Радість", 
                    Meaning = "Світлі думки" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Тюльпан",
                    NameLat = "Tulip",
                    Description = "Весняна квітка з гладкими пелюстками.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Tulip.png",
                    Symbolics = "Кохання",
                    Meaning = "Щирість" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Вервена", 
                    NameLat = "Vervain", 
                    Description = "Давня лікарська рослина.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Vervain.png",
                    Symbolics = "Магія", 
                    Meaning = "Віра" 
                },
                new() 
                {
                    Id = Guid.NewGuid(),
                    NameUa = "Біла лілія", 
                    NameLat = "White Lily", 
                    Description = "Класична біла квітка довершених форм.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/White-Lily.png",
                    Symbolics = "Чистота", 
                    Meaning = "Досконалість" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Біла водяна лілія",
                    NameLat = "White Water Lily",
                    Description = "Водяна квітка, що плаває на поверхні.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/White-Water-Lily.png",
                    Symbolics = "Духовність",
                    Meaning = "Спокій" 
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    NameUa = "Деревій",
                    NameLat = "Yarrow",
                    Description = "Лікарська рослина з білими суцвіттями.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Yarrow.png",
                    Symbolics = "Захист",
                    Meaning = "Сила" 
                },
                new()
                { 
                    Id = Guid.NewGuid(), 
                    NameUa = "Цинія", 
                    NameLat = "Zinnia", 
                    Description = "Яскрава садова квітка багатьох відтінків.",
                    ImagePath = @$"/{DefaultDropboxFolderName}/Zinnia.png",
                    Symbolics = "Дружба",
                    Meaning = "Постійність" 
                }
            });

            await dbContext.SaveChangesAsync();
        }
    }

    #endregion
}