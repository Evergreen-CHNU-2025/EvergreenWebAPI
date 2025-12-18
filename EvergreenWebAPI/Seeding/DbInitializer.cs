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
        if (!dbContext.Flowers.Any() && !dbContext.HexColors.Any() && !dbContext.FlowersHexColors.Any())
        {
            var flowers =  await SeedFlowersAsync(dbContext);

            var colors = await SeedHexColorsAsync(dbContext);

            await AssignColorsToFlowersAsync(dbContext, flowers, colors);

            await dbContext.SaveChangesAsync();
        }
    }

    #endregion

    #region Private logic

    private static async Task<List<Flower>> SeedFlowersAsync(ApplicationDbContext dbContext)
    {
        var flowers = new List<Flower>
        {
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Анемона",
                NameLat = "Anemone",
                Description = "Ніжна весняна квітка з тонкими пелюстками, що часто зустрічається в садових та природних угіддях. Має короткий період цвітіння і може мати різні відтінки від білого до насиченого синьо-фіолетового.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Anemone.png",
                Symbolics = "Надія, очікування, вразливість",
                Meaning = "Уособлює тендітність життя і миттєве щастя, нагадує цінувати короткі моменти краси.",
                InspectRecomendations = "Любить помірний полив, добре дренований ґрунт і сонячні або напівтіньові місця. Вимагає захисту від сильних вітрів; у посушливі періоди поливати рідше, мульчувати для збереження вологи."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Айстра",
                NameLat = "Aster",
                Description = "Осіння квітка з численними дрібними пелюстками, що створюють м'який шар кольору. Часто використовується в бордюрах та як медоносна культура для пізніх бджіл.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Aster.png",
                Symbolics = "Мудрість, елегантність, терпіння",
                Meaning = "Символ стійкості й терпіння під час змін сезонів, нагадує про красу зрілості.",
                InspectRecomendations = "Надає перевагу сонячним місцям, регулярному поливу під час вегетації та легкому підживленню навесні. Після цвітіння обрізати для підтримки форми та стимуляції повторного цвітіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Бегонія",
                NameLat = "Begonia",
                Description = "Декоративна рослина з різноманітними формами листя і яскравими квітами, популярна як кімнатна і балконна культура.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Begonia.png",
                Symbolics = "Обережність, дбайливість",
                Meaning = "Уособлює дбайливе ставлення, домашній затишок та увагу до деталей.",
                InspectRecomendations = "Потребує розсіяного світла, помірного поливу з хорошим дренажем і підживлення кожні 4–6 тижнів у період росту. Уникайте переливу та холоду, регулярно видаляйте відцвілі квіти."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Гайлардія",
                NameLat = "Blanket Flower",
                Description = "Яскрава польова квітка червоно-жовтих тонів з декоративними контрастними пелюстками, стійка до посухи.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Blanket-Flower.png",
                Symbolics = "Життєва енергія, відкритість",
                Meaning = "Символ радості та оптимізму, приваблює погляд і піднімає настрій.",
                InspectRecomendations = "Невибаглива до ґрунту, віддає перевагу сонячним місцям і помірному поливу. Періодично зрізайте відцвілі суцвіття для тривалого цвітіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Ромашка",
                NameLat = "Chamomile",
                Description = "Дрібна біла квітка з жовтою серцевиною, відома своїми лікувальними властивостями і ніжним ароматом.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Chamomile.png",
                Symbolics = "Спокій, очищення",
                Meaning = "Уособлює зцілення і внутрішній спокій; часто використовується як символ відновлення.",
                InspectRecomendations = "Посаджувати на сонячних ділянках з помірно родючим ґрунтом. Помірний полив, уникати надлишкового азоту щоб уникнути розростання листя за рахунок квітів."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Хризантема",
                NameLat = "Chrysanthemum",
                Description = "Пізньоосіння квітка з густими суцвіттями різних форм і кольорів, часто використовується у флористиці.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Chrysanthemum.png",
                Symbolics = "Довголіття, шана",
                Meaning = "Символ поваги та стійкості, часто пов'язується з пам'яттю і традиціями.",
                InspectRecomendations = "Потребує яскравого світла та регулярного поливу; вимагає підв'язки високих сортів. Підживлюйте збалансованими добривами під час росту."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Мак польовий",
                NameLat = "Corn Poppy",
                Description = "Яскрава червона квітка з тонкими пелюстками, часто з'являється на луках і полях, символ пам'яті та свободи.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Corn-Poppy.png",
                Symbolics = "Пам'ять, сила",
                Meaning = "Нагадує про пам'ять та вшанування, пов'язана з історичними подіями та емоційними спогадами.",
                InspectRecomendations = "Сонячне місце, легкий дренований ґрунт; посуха переноситься добре. Не потребує інтенсивного догляду, краще уникати надлишкового підживлення."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Волошка",
                NameLat = "Cornflower",
                Description = "Класична синя польова квітка з витонченими пелюстками, часто використовується в сухих букетах та як декоративний елемент.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Cornflower.png",
                Symbolics = "Вірність, чистота думок",
                Meaning = "Уособлює щирість і простоту почуттів, прагнення до свободи й відкритості.",
                InspectRecomendations = "Сонячні ділянки, помірний полив; росте добре в садових сумішах і на галявинах. Легко розмножується самосівом."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Космея",
                NameLat = "Cosmos",
                Description = "Делікатна квітка з тонкими пелюстками і довгими стеблами, часто використовують у лугових композиціях і для приваблення комах.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Cosmos.png",
                Symbolics = "Гармонія, простота",
                Meaning = "Символ природної краси і легкості, асоціюється з невимушеністю та миром.",
                InspectRecomendations = "Потребує сонця і легкого, добре дренованого ґрунту. Полив помірний; надто родючий ґрунт може спричинити рясне листя за рахунок квітів."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Крокус",
                NameLat = "Crocus",
                Description = "Рання весняна цибулинна рослина з яскравими, щільними чашечками квітів, часто першою проростає після снігу.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Crocus.png",
                Symbolics = "Весна, оновлення, надія",
                Meaning = "Уособлює пробудження природи і надію на новий початок після зими.",
                InspectRecomendations = "Садити цибулини в добре дренований ґрунт на сонячних або напівсонячних місцях; мінімальний полив після посадки, зимостійкі."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Жоржина",
                NameLat = "Dahlia",
                Description = "Великі декоративні квіти з численними пелюстками різної форми й кольору, популярна у декоративному садівництві та на виставках.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Dahlia.png",
                Symbolics = "Вишуканість, гідність",
                Meaning = "Символ самовираження і драматичної краси, підкреслює індивідуальність.",
                InspectRecomendations = "Потребує сонця, багатого й добре дренованого ґрунту. Регулярний полив, підв'язування високих сортів, регулярне підживлення в період росту."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Маргаритка",
                NameLat = "Daisy",
                Description = "Проста біла квітка з жовтим центром, символ невинності й простоти, часто зустрічається на луках і в садках.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Daisy.png",
                Symbolics = "Невинність, чистота",
                Meaning = "Означає щирість і прості радощі життя, часто використовується в народній символіці.",
                InspectRecomendations = "Легко зростає на сонці або в напівтіні, потребує помірного поливу і періодичного підрізання для довшого цвітіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Кульбаба",
                NameLat = "Dandelion",
                Description = "Жовта весняна квітка, яка згодом утворює пухнасті насіння для розповсюдження вітром; має застосування в народній медицині.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Dandelion.png",
                Symbolics = "Свобода, мрії",
                Meaning = "Символ бажань і легкості життя; часто асоціюється з дитячими забавами та мріями.",
                InspectRecomendations = "Невибаглива рослина, росте майже в будь-яких умовах. Якщо використовується декоративно — видаляйте зайві розетки і контролюйте розповсюдження насіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Дельфіній",
                NameLat = "Delphinium",
                Description = "Висока стеблова квітка з вертикальними суцвіттями синіх або фіолетових відтінків, використовуються для створення вертикальних акцентів у саду.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Delphinium.png",
                Symbolics = "Гідність, захист",
                Meaning = "Уособлює внутрішню силу й благородство, часто асоціюється з гордістю та стабільністю.",
                InspectRecomendations = "Потребує сонячного місця, родючого ґрунту й регулярного поливу. Високі сорти потребують підв'язки і захисту від вітру."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Шипшина",
                NameLat = "Dog Rose",
                Description = "Дика троянда з рожевими квітами і їстівними плодами (шипшиною), що використовується в косметиці та народній медицині.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Dog-Rose.png",
                Symbolics = "Сила та простота, природність",
                Meaning = "Символ природної краси, витривалості та простоти життя.",
                InspectRecomendations = "Вимоглива до сонця, терпима до різних ґрунтів. Обрізка після цвітіння стимулює ріст і формування плодів, стійка до посухи."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Ехінацея",
                NameLat = "Echinacea",
                Description = "Лікувальна рослина з помітними рожевими або пурпуровими квітами і твердими насіннєвими голівками, цінується за імуномодулюючі властивості.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Echinacea.png",
                Symbolics = "Здоров'я, стійкість",
                Meaning = "Уособлює відновлення та турботу про здоров'я, часто висаджується в лікувальних садах.",
                InspectRecomendations = "Сонячне місце, помірний полив і добре дренований ґрунт. Не вимоглива, обрізка відцвілих суцвіть продовжує декоративність."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Наперстянка",
                NameLat = "Foxglove",
                Description = "Колокольчасті квіти на високих стеблах, декоративні, але деякі види отруйні — застосовуються обережно в оформленні садів.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Foxglove.png",
                Symbolics = "Таємничість, подвійність",
                Meaning = "Нагадує про те, що краса може мати приховану небезпеку; символ загадковості.",
                InspectRecomendations = "Краща в напівтіні або розсіяному сонці, вологий родючий ґрунт. Слід уникати поїдання тваринами та дітьми через токсичність."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Фуксія",
                NameLat = "Fuchsia",
                Description = "Рослина з підвісними витонченими квітами яскравих рожево-фіолетових тонів, чудова для підвісних композицій та балконів.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Fuchsia.png",
                Symbolics = "Елегантність, жіночність",
                Meaning = "Символ грації та витонченості, підкреслює романтичну естетику простору.",
                InspectRecomendations = "Надає перевагу розсіяному світлу, постійному рівномірному поливу та регулярному підживленню. Взимку потребує захисту при кімнатному вирощуванні."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Герань",
                NameLat = "Geranium",
                Description = "Універсальна садова і кімнатна рослина з приємним ароматом листя та квітами різних відтінків, зручна для віконних ящиків.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Geranium.png",
                Symbolics = "Затишок, гостинність",
                Meaning = "Уособлює домашній комфорт і теплоту, часто використовується для створення привітної атмосфери.",
                InspectRecomendations = "Потребує сонячних місць, помірного поливу і періодичного підрізання для формування куща. Взимку на підвіконні з меншим поливом."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Ромашка німецька",
                NameLat = "German Chamomile",
                Description = "Ароматна лікарська рослина, з дрібними суцвіттями, використовувана для трав'яних чаїв і косметичних засобів.",
                ImagePath = @$"/{DefaultDropboxFolderName}/German-Chamomile.png",
                Symbolics = "Спокій, зцілення",
                Meaning = "Символ лікування й заспокоєння, часто використовується у ритуалах відновлення.",
                InspectRecomendations = "Сонячне місце, легкий ґрунт та помірний полив. Використовуйте обрізку для збору суцвіть у період цвітіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Гібіскус",
                NameLat = "Hibiscus",
                Description = "Екзотична квітка великих розмірів з яскравими пелюстками, часто використовується у декоративному оформленні та напоях.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Hibiscus.png",
                Symbolics = "Пристрасть, краса",
                Meaning = "Символ інтенсивних почуттів і привабливості, часто асоціюється з теплом і екзотикою.",
                InspectRecomendations = "Потребує багато світла, регулярний рясний полив і підживлення під час росту. У холодному кліматі вирощувати в контейнерах і заносити на зиму."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Мальва",
                NameLat = "Hollyhock",
                Description = "Висока квітка з великими бутонами, додає вертикалі й текстури бордюрам та квітникам.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Hollyhock.png",
                Symbolics = "Гордість, велич",
                Meaning = "Уособлює стійкість і високе положення, створює враження величності у саду.",
                InspectRecomendations = "Потрібне сонячне місце, родючий ґрунт і підтримка для високих стебел. Періодичний полив і добриво для рясного цвітіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Гіацинт",
                NameLat = "Hyacinth",
                Description = "Весняна квітка з суцвіттями, насиченим ароматом і яскравими кольорами, популярна у контейнерах і клумбах.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Hyacinth.png",
                Symbolics = "Відродження, пристрасть",
                Meaning = "Символ пробудження почуттів і енергії, асоціюється з весняним оновленням.",
                InspectRecomendations = "Цибулини садити в добре дренований ґрунт на сонячному місці; після цвітіння зменшити полив і дозволити листю пожовтіти природно."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Бальзамін",
                NameLat = "Impatiens",
                Description = "Тендітна рослина із яскравими квітами, ідеальна для тіні та напівтіні в контейнерах і бордюрах.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Impatiens.png",
                Symbolics = "Ніжність, терпіння",
                Meaning = "Уособлює тиху красу і вміння пристосовуватися до умов тіні.",
                InspectRecomendations = "Потребує розсіяного світла або тіні, постійного помірного поливу і регулярного підживлення. Уникайте суші для збереження декоративності."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Ірис",
                NameLat = "Iris",
                Description = "Квітка з витонченими пелюстками та різноманітною гамою кольорів, часто використовується у декоративних композиціях.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Iris.png",
                Symbolics = "Мудрість, віра",
                Meaning = "Символ глибини думок, інтелектуальної сили та елегантності.",
                InspectRecomendations = "Сонячні місця або легка напівтінь, добре дренований ґрунт. Розподіл кореневищ кожні кілька років для підтримки життєздатності."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Лаванда",
                NameLat = "Lavender",
                Description = "Ароматна трав'яниста рослина з вузьким листям і гроноподібними суцвіттями, популярна в ароматерапії та декоративних посадках.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Lavender.png",
                Symbolics = "Спокій, чистота думок",
                Meaning = "Уособлює чистоту, релаксацію і стійкий внутрішній спокій.",
                InspectRecomendations = "Потребує багато сонця і піщаного, добре дренованого ґрунту. Помірний полив, обрізка після цвітіння для підтримки компактної форми."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Ландиш",
                NameLat = "Lily of the Valley",
                Description = "Дрібні білосніжні дзвіночки з ніжним ароматом, часто використовуються у весільній флористиці.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Lily-of-the-Valley.png",
                Symbolics = "Невинність, скромність",
                Meaning = "Символ скромного щастя і чистоти намірів, асоціюється з безтурботністю.",
                InspectRecomendations = "Кращий у напівтіні та вологому родючому ґрунті. Розмножується кореневищами, потребує стабільної вологості."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Ніжність",
                NameLat = "Love-in-a-Mist",
                Description = "Ажурна квітка з тонкими ниткоподібними листками і повітряними бутонами, додає ефект легкості в композиціях.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Love-in-a-Mist.png",
                Symbolics = "Таємниця, романтика",
                Meaning = "Символ прихованих почуттів і чарівної невизначеності у відносинах.",
                InspectRecomendations = "Сонячні місця або часткова тінь, помірний полив. Легко вирощувати з насіння, підходить для природних садів."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Люпин",
                NameLat = "Lupine",
                Description = "Вертикальна рослина з яскравими колосовими суцвіттями, підходить для створення акцентів у бордюрах.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Lupine.png",
                Symbolics = "Упевненість, стійкість",
                Meaning = "Символ рішучості і прагнення до розвитку, асоціюється з амбіціями.",
                InspectRecomendations = "Потребує сонця, прохолодного родючого ґрунту з хорошим дренажем. Регулярне видалення відцвілих квіток стимулює повторне цвітіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Мальва польова",
                NameLat = "Mallow",
                Description = "Дика рослина з ніжними рожевими квітами, використовується в народній медицині і як декоративний елемент луків.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Mallow.png",
                Symbolics = "Жіночність, турбота",
                Meaning = "Уособлює м'якість і прихильність, часто синонім теплоті стосунків.",
                InspectRecomendations = "Сонячне або напівсонячне місце, помірний полив. Невибаглива, підходить для природних куточків саду."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Чорнобривці",
                NameLat = "Marigold",
                Description = "Яскраві помаранчеві або жовті квіти з насиченим ароматом, часто застосовуються для відлякування шкідників і у посадках-бар'єрах.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Marigold.png",
                Symbolics = "Сонце, радість",
                Meaning = "Символ життєвої енергії та радості, додає відчуття тепла і світла в саду.",
                InspectRecomendations = "Сонячні ділянки, добре дренований ґрунт; стійкі до посухи. Використовуйте як супровідну культуру для захисту від шкідників."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Нарцис",
                NameLat = "Narcissus",
                Description = "Весняна цибулинна рослина з витонченими дзвоновиками, символ відродження й оновлення.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Narcissus.png",
                Symbolics = "Самолюбство, оновлення",
                Meaning = "Символ нового початку та особистісного оновлення після періодів спокою.",
                InspectRecomendations = "Садити в добре дренований ґрунт на сонячних або частково сонячних ділянках; мінімальний полив влітку після відцвітання."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Настурція",
                NameLat = "Nasturtium",
                Description = "Квітка з круглими листками та яскравими пелюстками, їстівна і часто використовувана в кулінарії і як декоративна огорожа.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Nasturtium.png",
                Symbolics = "Мужність, відкритість",
                Meaning = "Символ прямоти і сміливого вираження емоцій, додає живості композиціям.",
                InspectRecomendations = "Потребує сонця, бідний або середньо-родючий ґрунт стимулює цвітіння. Регулярний полив під час посухи; їстівні квіти та листя."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Анютина квітка",
                NameLat = "Pansy",
                Description = "Квітка з характерним малюнком на пелюстках, декоративна і стійка в прохолодніших умовах весни та осені.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Pansy.png",
                Symbolics = "Думки, пам'ять",
                Meaning = "Уособлює задумливість і пам'ять, часто використовується для вираження ніжних почуттів.",
                InspectRecomendations = "Краща у прохолодну пору року, потребує помірного поливу і захисту від сильного сонця в спеку. Добре реагує на періодичну підгодівлю."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "М'ята перцева",
                NameLat = "Peppermint",
                Description = "Ароматна лікарська рослина з освіжаючим запахом листя, широко використовується в кухні та для приготування чаїв.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Peppermint.png",
                Symbolics = "Очищення, свіжість",
                Meaning = "Символ свіжості і очищення думок, корисна для відновлення енергії.",
                InspectRecomendations = "Розростається швидко; краще вирощувати в контейнерах або обмеженому просторі. Потребує вологого ґрунту та напівтіні."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Петуния",
                NameLat = "Petunia",
                Description = "Популярна садова квітка для підвісних кашпо і клумб, відзначається багатством форм та кольорів.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Petunia.png",
                Symbolics = "Спокій, привабливість",
                Meaning = "Уособлює доброзичливість і приємну компанію, робить простір більш привітним.",
                InspectRecomendations = "Потребує сонця, регулярного рясного поливу та підживлення для тривалого цвітіння. Видаляйте відцвілі квітки."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Флокс",
                NameLat = "Phlox",
                Description = "Кущова або ґрунтопокривна рослина з щільними суцвіттями яскравих кольорів, приваблює метеликів і бджіл.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Phlox.png",
                Symbolics = "Єдність, дружба",
                Meaning = "Символ згуртованості та гармонії у стосунках, часто використовується в масивних посадках.",
                InspectRecomendations = "Сонячні або напівсонячні ділянки, регулярний полив; вимагає обрізки для підтримки форми і здоров'я."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Примула",
                NameLat = "Primrose",
                Description = "Рання весняна рослина з ніжними квітами, яка приносить перші барви після зими.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Primrose.png",
                Symbolics = "Молодість, надія",
                Meaning = "Символ пробудження і надії на нові початки.",
                InspectRecomendations = "Краща у частковій тіні та у вологому родючому ґрунті; уникати пересушування, періодично підживлювати навесні."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Шавлія",
                NameLat = "Sage",
                Description = "Ароматна лікарська і кулінарна рослина з густими листками, часто використовується в кухні та лікувальних настоях.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Sage.png",
                Symbolics = "Мудрість, захист",
                Meaning = "Символ знань і обережності, цінується за практичні властивості.",
                InspectRecomendations = "Сонячні місця, сухий до середньо-родючого ґрунт; помірний полив і обрізка для стимуляції новоростів."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Скабіоза",
                NameLat = "Scabiosa",
                Description = "Делікатна квітка пастельних тонів, часто використовується в букетах як заповнення та для текстурності.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Scabiosa.png",
                Symbolics = "Любов, чуттєвість",
                Meaning = "Символ ніжності і чуттєвого сприйняття, додає тонких емоційних відтінків композиціям.",
                InspectRecomendations = "Сонячні ділянки, помірний полив, добрий дренаж; регулярне видалення відцвілих суцвіть сприяє тривалому цвітінню."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Левиний зів",
                NameLat = "Snapdragon",
                Description = "Квітка з характерною формою «пащі», зі стійкими вертикальними квітконосами, додає характеру бордюрам.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Snapdragon.png",
                Symbolics = "Мужність, захист",
                Meaning = "Уособлює силу характеру та готовність захищати те, що важливе.",
                InspectRecomendations = "Надає перевагу сонячним або частково сонячним місцям, регулярний полив та підживлення; зрізати відцвілі суцвіття."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Очаток",
                NameLat = "Stonecrop",
                Description = "Сукулентна рослина з товстими листками, витривала та невибаглива, часто використовувана в рокаріях.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Stonecrop.png",
                Symbolics = "Стійкість, витривалість",
                Meaning = "Символ виживання та економного використання ресурсів, підходить для суворих ділянок.",
                InspectRecomendations = "Потребує сонця, мінімальний полив і дуже добрий дренаж; чудово підходить для кам'янистих садів."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Соняшник",
                NameLat = "Sunflower",
                Description = "Велика яскрава квітка, що повертає головку до сонця; символ енергії та тепла.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Sunflower.png",
                Symbolics = "Вірність, радість",
                Meaning = "Уособлює оптимізм і життєву силу, нагадує тягнутися до світла.",
                InspectRecomendations = "Сонячні ділянки з глибоким родючим ґрунтом; рясний полив у період росту і підпірки для високих сортів."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Корепсис",
                NameLat = "Tickseed",
                Description = "Яскрава польова квітка з простими, але виразними пелюстками; додає природності ландшафту.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Tickseed.png",
                Symbolics = "Радість, легкість",
                Meaning = "Символ безтурботності і простих радощів, приносить яскраві плями в сад.",
                InspectRecomendations = "Сонячні місця, сухі до середньо-вологих ґрунтів; невибаглива, підходить для масових посадок."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Тюльпан",
                NameLat = "Tulip",
                Description = "Весняна цибулинна рослина з гладкими пелюстками і різноманітними кольорами, культова в декоративному садівництві.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Tulip.png",
                Symbolics = "Кохання, щирість",
                Meaning = "Символ прямих і щирих почуттів, часто використовується для вираження поваги й уваги.",
                InspectRecomendations = "Садити цибулини в добре дренований ґрунт восени; сонячне місце, обмежити зволоження після цвітіння."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Вервена",
                NameLat = "Vervain",
                Description = "Давня лікарська рослина з дрібними квітами, використовувалася в ритуалах і народній медицині.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Vervain.png",
                Symbolics = "Магія, віра",
                Meaning = "Символ духовної сили і захисту, пов'язана зі стародавніми віруваннями.",
                InspectRecomendations = "Сонячні місця і помірний полив; підходить для змішаної бордюрної посадки і як медоносна рослина."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Біла лілія",
                NameLat = "White Lily",
                Description = "Елегантна біла квітка з великими пелюстками, часто використовується у весільній і ритуальній флористиці.",
                ImagePath = @$"/{DefaultDropboxFolderName}/White-Lily.png",
                Symbolics = "Чистота, досконалість",
                Meaning = "Уособлює чистоту намірів і високу мораль, часто присутня у важливих подіях.",
                InspectRecomendations = "Потребує сонячного або легкого напівтіньового місця, родючий, вологий ґрунт; регулярний полив і підживлення."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Біла водяна лілія",
                NameLat = "White Water Lily",
                Description = "Водяна рослина з плаваючими листками і білими квітами, символ умиротворення і духовності.",
                ImagePath = @$"/{DefaultDropboxFolderName}/White-Water-Lily.png",
                Symbolics = "Духовність, мир",
                Meaning = "Символ спокою і внутрішнього балансу, часто асоціюється з водними пейзажами.",
                InspectRecomendations = "Тримати в стоячій або повільнотекучій воді, достатня глибина для коренів; контролювати водорості і забезпечувати достатньо сонця."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Деревій",
                NameLat = "Yarrow",
                Description = "Лікарська рослина з білими суцвіттями, використовується у фітотерапії і як декоративний елемент у природних садах.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Yarrow.png",
                Symbolics = "Захист, зцілення",
                Meaning = "Символ зцілення і захисту, часто використовується для підтримки здоров'я і відновлення.",
                InspectRecomendations = "Сонячні ділянки, сухий до середньо-вологого ґрунту; невимогливий, використовувати для стабілізації схилів і луків."
            },
            new()
            {
                Id = Guid.NewGuid(),
                NameUa = "Цинія",
                NameLat = "Zinnia",
                Description = "Яскрава садова квітка різних форм і відтінків, підходить для масових посадок і букетних композицій.",
                ImagePath = @$"/{DefaultDropboxFolderName}/Zinnia.png",
                Symbolics = "Дружба, постійність",
                Meaning = "Уособлює надійність та дружні почуття, приносить радість і барвистість у сад.",
                InspectRecomendations = "Потребує сонця, родючий ґрунт і регулярний полив; обрізка відцвіліх квітів продовжує цвітіння."
            }
        };

        dbContext.AddRange(flowers);

        return flowers;
    }

    private static async Task<List<HexColor>> SeedHexColorsAsync(ApplicationDbContext dbContext)
    {
        var colors = new List<HexColor>
        {
            new() { Id = Guid.NewGuid(), Color = "#FFFFFF" },
            new() { Id = Guid.NewGuid(), Color = "#FFD700" },
            new() { Id = Guid.NewGuid(), Color = "#FF0000" },
            new() { Id = Guid.NewGuid(), Color = "#FF69B4" },
            new() { Id = Guid.NewGuid(), Color = "#800080" },
            new() { Id = Guid.NewGuid(), Color = "#0000FF" },
            new() { Id = Guid.NewGuid(), Color = "#FFA500" },
            new() { Id = Guid.NewGuid(), Color = "#008000" },
        };

        dbContext.HexColors.AddRange(colors);

        return colors;
    }

    private static async Task AssignColorsToFlowersAsync(ApplicationDbContext dbContext, List<Flower> flowers, List<HexColor> colors)
    {
        var colorRules = new Dictionary<string, string[]>
        {
            ["Anemone"] = new[] { "#FFFFFF", "#FF0000", "#800080" },
            ["Aster"] = new[] { "#800080", "#FF69B4", "#FFFFFF" },
            ["Begonia"] = new[] { "#FF0000", "#FF69B4", "#FFFFFF" },
            ["Blanket Flower"] = new[] { "#FF0000", "#FFD700" },
            ["Chamomile"] = new[] { "#FFFFFF", "#FFD700" },
            ["Chrysanthemum"] = new[] { "#FFFFFF", "#FFD700", "#800080" },
            ["Corn Poppy"] = new[] { "#FF0000" },
            ["Cornflower"] = new[] { "#0000FF" },
            ["Cosmos"] = new[] { "#FF69B4", "#FFFFFF" },
            ["Crocus"] = new[] { "#800080", "#FFFFFF" },
            ["Dahlia"] = new[] { "#FF0000", "#FF69B4", "#FFD700" },
            ["Daisy"] = new[] { "#FFFFFF", "#FFD700" },
            ["Dandelion"] = new[] { "#FFD700" },
            ["Delphinium"] = new[] { "#0000FF" },
            ["Dog Rose"] = new[] { "#FF69B4", "#FFFFFF" },
            ["Echinacea"] = new[] { "#FF69B4", "#800080" },
            ["Foxglove"] = new[] { "#800080", "#FF69B4" },
            ["Fuchsia"] = new[] { "#FF69B4", "#800080" },
            ["Geranium"] = new[] { "#FF0000", "#FF69B4" },
            ["German Chamomile"] = new[] { "#FFFFFF", "#FFD700" },
            ["Hibiscus"] = new[] { "#FF0000", "#FF69B4" },
            ["Hollyhock"] = new[] { "#FF69B4", "#800080" },
            ["Hyacinth"] = new[] { "#800080", "#FFFFFF" },
            ["Impatiens"] = new[] { "#FF69B4", "#FF0000" },
            ["Iris"] = new[] { "#800080", "#0000FF" },
            ["Lavender"] = new[] { "#800080" },
            ["Lily of the Valley"] = new[] { "#FFFFFF" },
            ["Love-in-a-Mist"] = new[] { "#0000FF", "#FFFFFF" },
            ["Lupine"] = new[] { "#800080", "#0000FF" },
            ["Mallow"] = new[] { "#FF69B4" },
            ["Marigold"] = new[] { "#FFA500", "#FFD700" },
            ["Narcissus"] = new[] { "#FFFFFF", "#FFD700" },
            ["Nasturtium"] = new[] { "#FFA500", "#FF0000" },
            ["Pansy"] = new[] { "#800080", "#FFD700", "#FFFFFF" },
            ["Peppermint"] = new[] { "#008000" },
            ["Petunia"] = new[] { "#800080", "#FF69B4" },
            ["Phlox"] = new[] { "#FF69B4", "#FFFFFF" },
            ["Primrose"] = new[] { "#FFD700", "#FFFFFF" },
            ["Sage"] = new[] { "#008000" },
            ["Scabiosa"] = new[] { "#800080", "#FF69B4" },
            ["Snapdragon"] = new[] { "#FF0000", "#FFD700" },
            ["Stonecrop"] = new[] { "#008000", "#FFD700" },
            ["Sunflower"] = new[] { "#FFD700", "#008000" },
            ["Tickseed"] = new[] { "#FFD700" },
            ["Tulip"] = new[] { "#FF0000", "#FFD700", "#FF69B4" },
            ["Vervain"] = new[] { "#800080" },
            ["White Lily"] = new[] { "#FFFFFF" },
            ["White Water Lily"] = new[] { "#FFFFFF" },
            ["Yarrow"] = new[] { "#FFFFFF", "#FFD700" },
            ["Zinnia"] = new[] { "#FF0000", "#FFD700", "#FF69B4" }
        };

        var links = new List<FlowersHexColor>();

        foreach (var colorRule in colorRules)
        {
            string flowerName = colorRule.Key;
            string[] colorValues = colorRule.Value;

            var flowerId = flowers.FirstOrDefault(flower => flower.NameLat == flowerName)?.Id;

            if (!flowerId.HasValue)
                continue;

            foreach (var colorValue in colorValues)
            {
                var colorId = colors.FirstOrDefault(color => color.Color == colorValue)?.Id;

                if (colorId.HasValue)
                {
                    links.Add(new FlowersHexColor
                    {
                        Id = Guid.NewGuid(),
                        FlowerId = flowerId.Value,
                        ColorId = colorId.Value
                    });
                }
            }
        }

        dbContext.FlowersHexColors.AddRange(links);
    }

    #endregion
}