# Задача

1) Как бы вы реализовали абстрактный механизм способностей персонажей?

2) Какие ключевые классы и интерфейсы выделили бы?

3) Каким способом добились бы гибкой настройки со стороный гейм дизайн отдела и минимизировали вмешательство отдела разработки в создание новых вариаций способоностей?
В качестве примера можно описать реализацию способности "Исцеление"\"Массовое исцеление" и способности "Нанесение урона"\"Массовое нанесение урона"?


# Описание

Информация о скиллах может храниться как локально (см. Scripts/Base/ScriptableObject), так и на сервере(см. Scripts/Network). 

В этом проекте рассмотрим оба случая. 

-Для локального хранения/редактирования используем ScriptableObject и Sirenix Odin Inspector

-Для получения информации о скиллах с сервера используем UnityWebRequest, UniTask, Newtonsoft.Json 

---

Затронем следующие пункты:

-Класс Unit и класс UnitSkill

-Локальное хранилище способностей

-Локальное хранилище персонажей

-Получение информации из БД сервера с помощью REST API

-Локатор сервисов

-Интерфейсы

-Скрипты персонажа

-Тест




# Класс Unit

В начале создадим класс Unit, который содержит имя (Warrior, Wizzard) и коллекцию скиллов

![Снимок экрана 2023-10-05 в 08 58 58](https://github.com/playstel/skills_system_overview/assets/91478838/561a73af-916d-464c-9e63-e9dd7344b43a)


# Класс UnitSkill

Далее создадим класс UnitSkill, который включает в себя:

-название скилла (Heal, MassHeal, Damage, MassDamage)

-тип (InstantUse, RadiusAroundUnit, Throw, MelleHit)

-описание (SkillNameText, SkillDescriptionText, SkillImageByteCode)

-использование (SkillUseCost, SkillCooldownMsec)

-зона поражения (SkillMaxThrowDistance, SkillThrowPower, SkillZoneRadius)

-воздействие на персонажей (ImpactHealthPoints, ImpactSpeedPoints, ImpactDurationMsec, ImpactFX)


![Снимок экрана 2023-10-05 в 08 59 26](https://github.com/playstel/skills_system_overview/assets/91478838/7ee550e5-1e84-42e0-ad1a-041d0e6836db)

Подробнее см. в проекте Scripts/Base/Class/ClassUnit.cs

![Снимок экрана 2023-10-05 в 09 21 48](https://github.com/playstel/skills_system_overview/assets/91478838/86325963-6c57-4ffb-897c-71ddba031562)


# Локальное хранилище способностей

Чтобы визуализировать класс скиллов, создадим класс LocalUnitSkills и наследуем от ScriptableObject:

![Снимок экрана 2023-10-05 в 09 11 54](https://github.com/playstel/skills_system_overview/assets/91478838/b3de60c7-723f-4747-bfa1-f9c7d0599a12)

Далее в инспекторе можно создать и гибко настроить несколько его экземпляров:

Heal

![Снимок экрана 2023-10-05 в 09 27 20](https://github.com/playstel/skills_system_overview/assets/91478838/bd758ec1-8f14-477a-9596-181d868f962a)



Mass Heal

![Снимок экрана 2023-10-05 в 09 27 38](https://github.com/playstel/skills_system_overview/assets/91478838/c4a6ff68-ae15-4a73-9541-b19a9c1ab057)



Damage

![Снимок экрана 2023-10-05 в 09 27 56](https://github.com/playstel/skills_system_overview/assets/91478838/8612911f-c179-4ce2-88fb-257e10f3f4f7)



Mass Damage (в данном случае - заморозка персонажей)

![Снимок экрана 2023-10-05 в 09 28 21](https://github.com/playstel/skills_system_overview/assets/91478838/c3d31b53-e399-4c87-bc9c-9e611718d2c9)

Путь до локального хранилища скиллов: ScriptableObjects/Unit 

![Снимок экрана 2023-10-06 в 14 06 54](https://github.com/playstel/skills_system_overview/assets/91478838/f475c530-c4cf-413d-87b0-3a0ecb4fc1ea)


# Локальное хранилище персонажей

По аналогии создадим ScriptableObject для персонажей 

![Снимок экрана 2023-10-05 в 09 40 50](https://github.com/playstel/skills_system_overview/assets/91478838/c1fa3e81-4e87-4279-8d99-d18cb45b64f2)

И закрепим за ними подходящие скиллы:

![Снимок экрана 2023-10-05 в 09 40 25](https://github.com/playstel/skills_system_overview/assets/91478838/3dd623ff-79ff-4b24-85bd-a24f344034b6)

Путь до локального хранилища персонажей аналогичный: ScriptableObjects/Unit 

![Снимок экрана 2023-10-06 в 14 07 08](https://github.com/playstel/skills_system_overview/assets/91478838/b7e6342c-c4b6-4dd8-bdee-174b52c9bc7c)

Таким образом персонажей и способности из локального хранилища можно легко создавать и редактировать любому человеку, не разбирающемуся в коде. 

# Получение информации из БД сервера с помощью REST API

Теперь мы можем легко редактировать параметры скиллов персонажей и создавать новые вариации способоностей. 
Однако информация о скиллах все еще находится в Unity и зависит от сборки. 

При наличии сервера лучше создать в нем базу данных скиллов и получать ее по REST API перед каждым запуском игры. 
Редактируя записи в конфигурации на сервере, можно изменять параметры игры в любое время и не беспокоить пользователей необходимостью обновлять приложение, чтобы получить актуальную версию скиллов.  

---

Для этого создадим скрипт WebRequest, который будет отправлять GET запрос на потенциальный сервер через UnityWebRequest (см. Scripts/Network/WebRequest.cs)

![Снимок экрана 2023-10-05 в 09 48 00](https://github.com/playstel/skills_system_overview/assets/91478838/8c404710-af72-4b44-aaab-f73572132307)

Далее создадим надстройку WebRequestUnit.cs и метод GET_UnitSkills, который будет формировать конкретный запрос для скиллов, получать респонс с сервера, а затем десериализировать его с помощью Newtonsoft.Json.JsonConvert:

![Снимок экрана 2023-10-05 в 09 49 34](https://github.com/playstel/skills_system_overview/assets/91478838/7c24ce63-1f6c-442f-8fcc-52c853ad54ff)

Создадим ScriptableObject, который будет хранить путь к серверным запросам (для навигации в серверных запросах можно воспользоваться Swagger UI)

![Снимок экрана 2023-10-05 в 09 50 50](https://github.com/playstel/skills_system_overview/assets/91478838/b028a1c7-584e-491d-bbd6-433571eac562)

Путь до хранилища серверных запросов:

![Снимок экрана 2023-10-06 в 14 07 59](https://github.com/playstel/skills_system_overview/assets/91478838/975c709c-fe85-46cf-ac15-700aa0049d42)

Если на сервере есть админ-панель, можно подключить к ней интерфейс создания/редактирования скиллов для облегчения работы.

# Локатор сервисов

Теперь мы имеем возможность получать информацию о скиллах как локально, так и с помощью сервера. 

Чтобы обращаться к ней из любых скриптов проекта, создадим ServiceLocatior.cs, сделаем из него static singleton

![Снимок экрана 2023-10-05 в 10 11 21](https://github.com/playstel/skills_system_overview/assets/91478838/b61ab3de-872e-4fa4-9391-b592ee1d93f2)

Cоздаем скрипты, которые будут хранить ссылки на инструменты работы с серверными запросами, а так же с локальной БД персонажей и их способностей:

![Снимок экрана 2023-10-05 в 10 17 58](https://github.com/playstel/skills_system_overview/assets/91478838/92fffc50-945c-484b-9cf1-08152b09bc0d)

Чтобы была возможность обратиться к этим скриптам, они должны быть дочерними для gameObject с нашим компонентом ServiceLocator.cs. 
Далее в нем напишем метод получения дочерних объектов по их типу:

![Снимок экрана 2023-10-05 в 10 22 10](https://github.com/playstel/skills_system_overview/assets/91478838/2d339409-2245-4336-89ff-da963e8e8f3f)

Теперь мы можем обратиться к информации о скиллах из любого интересующего нас места через ServiceLocator. 

Для масштабных проектов вместо ServiceLocatior лучше использовать фреймворк Zenject - он более гибкий в использовнии.

# Интерфейсы

Так же для удобства создадим интерфейсы, которые будут отвечать за здоровье/скорость/эффекты/скиллы персонажа:

![Снимок экрана 2023-10-05 в 10 36 02](https://github.com/playstel/skills_system_overview/assets/91478838/a960578d-27f2-4649-9205-a8b2982396bf)

Например, здоровье персонажа:

![Снимок экрана 2023-10-05 в 10 36 39](https://github.com/playstel/skills_system_overview/assets/91478838/0ac8b322-5363-4839-bf3f-0caa402b5a60)

Пример использования:

![Снимок экрана 2023-10-05 в 10 37 08](https://github.com/playstel/skills_system_overview/assets/91478838/2bf27a53-fa2e-40ec-a12f-bbae56b91b6f)

![Снимок экрана 2023-10-05 в 10 36 57](https://github.com/playstel/skills_system_overview/assets/91478838/f2857343-9e9a-45d9-8c8c-e3e4211bcd11)

# Скрипты персонажа

Приступим к генерации героев и скиллов. Для этого создадим скрипт UnitList.cs, в котором можно выбрать тип получения скиллов - с помощью локальной базы данных ScriptableObject или через RestApi. 
Для этого используем переключатель UseRestApi

![Снимок экрана 2023-10-05 в 09 55 19](https://github.com/playstel/skills_system_overview/assets/91478838/c2007fb2-40a3-4f27-bd0c-933c73aa9246)

Когда экземляр персонажа создан, добавим и инциализируем его игровые компоненты (Health, Skills, Speed, VisualEffect, Impact):

![Снимок экрана 2023-10-05 в 10 24 08](https://github.com/playstel/skills_system_overview/assets/91478838/53bcec43-e6e6-4775-bde8-012adee7b475)

Рассмотрим UnitSkills.cs подробнее. 
На старте мы получаем скиллы с помощью ServiceLocator, либо из базы данных сервера через запрос из WebRequestUnit, либо через локальную базу данных через запрос из UnitLocalSource


![Снимок экрана 2023-10-05 в 10 27 58](https://github.com/playstel/skills_system_overview/assets/91478838/c36d6d4d-30bb-4f64-84ba-cdd29f4808e6)

Получив информацию о скиллах, мы можем провести небольшой тест:

![Снимок экрана 2023-10-05 в 10 31 49](https://github.com/playstel/skills_system_overview/assets/91478838/bb76dd6c-e855-4b74-a3cc-0089c86602cf)

В зависимости от типа скилла можно выбрать нужный компонент персонажа. Если это граната - создаем компонент, отвечающий за метание гранат. Если это зона урона/лечения - создаем компонент, отвечающий за создание вокруг персонажа окружности с соответствующим импактом, и т.д. 

![Снимок экрана 2023-10-05 в 10 32 05](https://github.com/playstel/skills_system_overview/assets/91478838/97c10c32-e2a3-4d84-aaf6-213459b9d433)

# Тест

В папке проекта ScriptableObjects/Unit хранятся данные о способностях, которые можно добавлять/убирать/редактировать

Запустим проект и проверим консоль:

![Снимок экрана 2023-10-05 в 09 42 18](https://github.com/playstel/skills_system_overview/assets/91478838/a29782ab-7f34-4c28-8b2e-32bc4df5f565)

# Тест с REST API

Eсли имеется сервер с готовыми скиллами и вы хотите получить их через Rest API, тогда поставьте галочку UseRestApi в скрипте UnitList.cs

![Снимок экрана 2023-10-06 в 14 05 46](https://github.com/playstel/skills_system_overview/assets/91478838/8e10a671-6d6e-4e80-93f5-fcc7e57a10fc)

Заполните URL и токен в запросе WebRequestUnit.cs

![Снимок экрана 2023-10-06 в 12 28 59](https://github.com/playstel/skills_system_overview/assets/91478838/c8412464-ab56-4628-a167-496cb502b9d8)

И укажите URL адреса запросов в UrlUnit из папки ScriptableObjects/Swagger Url:

![Снимок экрана 2023-10-06 в 12 31 20](https://github.com/playstel/skills_system_overview/assets/91478838/6e9cab98-35d3-4176-ba86-ef7630d44fb4)

# Ссылки

Больше проектов можно найти на моей сайте https://playstel.com/





