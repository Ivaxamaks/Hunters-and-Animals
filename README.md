#### Тестовое задание на позицию UnityDeveloper.
### **Задание:** 
+ **1. Сцена**
  + **1.1.** - на сцене присутствует игровое поле (плоскость XZ) размерами 100х100 единиц со случайно расставленными препятствиями (прямоугольные параллелепипеды, размеры случайны)
+ **2. Выведено игровое меню со следующими полями:**
  + **2.1.** - создать объекты
  + **2.2.** - уничтожить все объекты
  + **2.3.** - инвертировать объекты
  + **2.4.** - количество создаваемых объектов
  + **2.5.** - радиус бегства синих объектов
  + **2.6.** - отдельно актуальное количество синих и красных объектов на сцене
  + **2.7.** - выход
+ **3. Логика меню:**
  + **3.1.** - при нажатии **2.1.** на поле случайным образом создаются случайные объекты (сферы D=1) 2х цветов (красный "охотник" и синий "дичь", цвета выбираются случайно) общим количеством указанном в **2.4.** Объекты не могут пересекаться друг с другом или препятствиями.
  + **3.2.** - при нажатии **2.2.** все созданные объекты уничтожаются
  + **3.3.** - при нажатии **2.3.** все охотники становится дичью, вся дичь становится охотниками
  + **3.4.** - в меню в пункте **2.4.** возможно изменить количество создаваемых объектов
  + **3.5.** - в меню в пункте **2.5.**  возможно изменить радиус бегства дичи
  + **3.6.** - выход из приложения
+ **4. Логика объектов**
  + **4.1.** - красные объекты "охотники" пытаются догнать синие (строят путь и и перемещаются к ним), перемещение осуществляется в плоскости XZ 
  + **4.2.** - синие объекты "дичь", если "охотник" в радиусе меньше пункт **2.5.** пытается "убежать" от "охотника", перемещение осуществляется в плоскости XZ
  + **4.3.** - при соприкосновении охотника и дичи происходит уничтожение дичи и увеличение размера охотника на 1
  + **4.4.** - никто из объектов не может выходить за границы игрового поля или пересекаться с препятствием или друг с другом
+ **5. Логика взаимодействия с окружением**
  + **5.1.** - при нажатии указателем на объект, он инвертируется из дичи в охотника и  наоборот
+ **6. Пояснения**
  + Верстка не обязательна, в приоритете – логика приложения
  + Использовать можно любые сторонние ассеты и библиотеки

### **Реализация:** 
[Короткое видео](https://www.youtube.com/watch?v=2zyakaIJEAE)
+ **1. Генерация карты:**

![UML](https://i.imgur.com/cLn0uiT.png)

   Для генерации карты используются настройки в **ScriptableObject**, далее в **Map** генерируются по точкам **Tail**-ы, в них уже я генерирую поверхность и рандомом устанавливаю препятствия на часть **Tail**-ов определяя их точки с помощью **Bounds**.
После генерации  карты  и препятствий, запекаю **NavMesh**.

+ **2. Генерация юнитов:**

![UML](https://i.imgur.com/k3fPJYU.png)

   В конструкторе **UnitManager** создается объекты **UnitGenerator**,  **UnitCounte**r и **UnitSettingsProvider**. Класс инициализирует сгенерированных юнитов и в классе регистрируются ивенты связанные с юнитами (через евенты **EventBus**, чтобы уменьшить связность).
  + **UnitGenerator** - в зависимости от переданного количества генерирует на случайных **Tail**-ах (без препятствий) юнитов. Юниты генерирую через **ObjectPool**
так как эти объекты могут “уничтожаться”  и “создаваться” заново.
  + **UnitCounter** - ведет подсчет активных юнитов и передает актуальное количество в **UIController** через **EventBus**.
  + **UnitSettingsProvider** - передается в **Unit**, содержит базовый настройки из **UnitSettings**. Также регистрирует эвент на изменения радиуса у Дичи.

+ **3. Логика Unit.**

![UML](https://i.imgur.com/4jFGfaK.png)

На инициализации **Unit**  создаю объекты  **UnitStatesController**  и **TargetDetector**, также инициализирую **UnitCollisionHandler**  и **UnitClickHandler** пробрасывая методы по событию. 
  + **TargetDetector** содержит в себе ближайшую цель, определяет с помощью
**Physics.OverlapSphereNonAlloc** цели и после вычислят ближайшею. 
  + **UnitStatesController** - взял готовую иерархическую машину состояний [Inspiaaa/UnityHFSM](https://github.com/Inspiaaa/UnityHFSM) . C помощью нее я могу вложить конечный автомат в другой качестве состояния, что представляют у меня AnimalState и HunterState.
  **Примеры инициализации, достаточно прост в использовании**

![UML](https://i.imgur.com/vij6qx1.png)
![UML](https://i.imgur.com/pWeIDDB.png)

Добавляет состояние:
```с#
_stateMachine.AddState("HunterState", hunterStateMachine);
```
Добавляет переход
```с#
_stateMachine.AddTransition(
                "AnimalState",
                "HunterState",
                transition => _currentType == UnitType.Hunter);
```
Передвижение в состояниях сделал с помощью **NavMeshAgent**, из состояний больше всего заинтересовало **RunState** у дичи, изначально хотел реализовать с помощью потенциальных полей для поиска места как на скрине ниже. Есть понимание как реализовать устанавливая опасность в **Tile**, но перешел более простому способу в соображениях производительности на большом количестве юнитов и скорости исполнения.

![UML](https://i.imgur.com/X0lLubx.jpg)
